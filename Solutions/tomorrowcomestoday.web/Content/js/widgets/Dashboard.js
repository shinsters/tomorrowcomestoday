var hub;

function DashboardViewModel() {
    var self = this;

    // MODELS ************************************
    // chat messages
    function chatMessage(message) {
        var self = this;
        self.Message = message;
        self.token = token;
    }

    // BINDINGS **********************************
    // chat messages
    self.addMessageText = ko.observable();
    self.Messages = ko.observableArray();

    self.addMessage = function(message) { self.Messages.push(message); };
    
    self.sendMessage = function () {
        var message = new chatMessage();
        message.Message = self.addMessageText();
        message.Token = token;
        SendMessage(message);
        self.addMessageText("");
    };
    
    // attendance statuses
    self.Attendances = ko.observableArray();
    self.addAttending = function (attending) { self.Attendances.push(attending); }; 

}

// start hub connection
$(function() {
    var viewModel = new DashboardViewModel();
    hub = $.connection.MeetHub;

    ko.applyBindings(viewModel);

    // SERVER EVENTS *******************************
    // chat messages
    hub.client.addMessage = function (message) { viewModel.addMessage(message); };
    
    // attendance
    hub.client.addAttending = function (attendance) {
        viewModel.addAttending(attendance);
    };

    $.connection.hub.start(function () { hub.server.join(token); });
});

function SendMessage(message) {
    hub.server.send(message);
}
