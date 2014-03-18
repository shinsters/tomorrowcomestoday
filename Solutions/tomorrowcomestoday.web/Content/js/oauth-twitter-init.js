$(function () {
    OAuth.initialize('Q_8wbyIV-9bGSdYmS-UREl-DYmk');

    // todo: we can easily make this work for any service 
    $('#loginTwitter').click(function(){
        OAuth.popup('twitter', function (err, result) {
            if (err !==  null) {
                alert("Uh oh! Twitter didn't quite respond how we expected it to. Try again.");
                return;
            }

            $('#provider').val('Twitter');
            $('#response').val($.param(result));
            
            $('#login').submit();
        });
    });
});