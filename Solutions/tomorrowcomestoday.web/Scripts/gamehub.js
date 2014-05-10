$(function () {
    // Reference the auto-generated proxy for the hub.  
    var gameHub = $.connection.gameHub;

    gameHub.client.GetServiceMessage(function(message) {
        alert(message);
    });


    $.connection.hub.start().done(function () {

        gameHub.server.JoinServer();

    });
});

// This optional function html-encodes messages for display in the page.
function htmlEncode(value) {
    var encodedValue = $('<div />').text(value).html();
    return encodedValue;
}
