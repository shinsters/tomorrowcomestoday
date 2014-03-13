using System;
using System.IO;
using System.Drawing;
using System.Net;
using System.Reflection;

namespace MeetFurries.ImageGenerator
{
    public class SvgCreator
    {
        public string CreateSmallMap(string title, double latitude, double longitude)
        {
            // get the data URI for the map
            var mapUrl = this.GetMapImage(160, 160, latitude, longitude);
            var dataUri = this.GenerateDataUri(mapUrl);

            // load embedded SVG, insert map and return!
            var svg = this.LoadEmbeddedResource("MeetFurries.ImageGenerator.SVG.Small.svg");

            // set map
            svg = svg.Replace("%%map-data-uri%%", dataUri);
            svg = svg.Replace("%%title%%", title);
            return svg;
        }

        private string GenerateDataUri(string imageUrl)
        {
            // fetch imageUrl as stream
            var client = new WebClient();
            var imageStream = client.OpenRead(imageUrl);
            
            // convert the stream to a memory stream
            var memoryStream = new MemoryStream();
            var buffer = new byte[2048];
            int bytesRead;
            while ((bytesRead = imageStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                memoryStream.Write(buffer, 0, bytesRead);
            }

            var base64EncodedImage = Convert.ToBase64String(memoryStream.ToArray());
            return "data:image/png;base64," + base64EncodedImage;
        }

        private string GetMapImage(int height, int width, double lat, double lon)
        {
            // fetch from 
            const int zoom = 16;
            var url =
                string.Format(
                    "http://staticmap.openstreetmap.de/staticmap.php?center={0},{1}&zoom={2}&size={3}x{4}&maptype=mapnik&markers={0},{1},lightblue",
                    lat,
                    lon,
                    zoom,
                    height,
                    width);
            return url;
        }

        private string LoadEmbeddedResource(string resource)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream(resource))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

    }
}
