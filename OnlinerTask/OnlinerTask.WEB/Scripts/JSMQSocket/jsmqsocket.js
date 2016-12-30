var addSubscriber = createSubscriber("wss://onlinertask.azurewebsites.net", "addProduct");

var removeSubscriber = createSubscriber("wss://onlinertask.azurewebsites.net", "removeProduct");

var infoSubscriber = createSubscriber("wss://onlinertask.azurewebsites.net", "infoProduct");

var addSubscriberSearch = createSubscriber("wss://onlinertask.azurewebsites.net", "SearchAddProduct");

var removeSubscriberSearch = createSubscriber("wss://onlinertask.azurewebsites.net", "SearchRemoveProduct");

addSubscriber.onMessage = function (message) {
    message.popString();
    toastr.options = {
        "positionClass": "toast-bottom-right"
    };
    toastr["success"]("Product " + message.popString() + " was returned to your cabinet succesfully. NetMQ.", "Add Product");
};

removeSubscriber.onMessage = function (message) {
    message.popString();
    toastr.options = {
        "positionClass": "toast-bottom-right"
    };
    toastr["warning"]("Product " + message.popString() + " was deleted from your cabinet succesfully. NetMQ.", "Delete Product");
};

infoSubscriber.onMessage = function (message) {
    message.popString();
    toastr.options = {
        "positionClass": "toast-bottom-right"
    };
    toastr["info"]("Now you will have messages at " + message.popString() + " NetMQ.", "Settings Changed");
};

removeSubscriberSearch.onMessage = function (message) {
    message.popString();
    var text = message.popString();
    var path = message.popString();
    toastr.options = {
        "positionClass": "toast-bottom-right"
    };
    toastr.options.onclick = function () {
        window.location.href = path;
    };
    toastr["warning"]("Product " + text + " was deleted from your cabinet succesfully. Click to see. NetMQ.", "Delete Product");
};

addSubscriberSearch.onMessage = function (message) {
    message.popString();
    var text = message.popString();
    var path = message.popString();
    toastr.options = {
        "positionClass": "toast-bottom-right"
    };
    toastr.options.onclick = function () {
        window.location.href = path;
    };
    toastr["success"]("Product " + text + " was added to your cabinet succesfully. Click to see. NetMQ.", "Add Product");
};

function createSubscriber(route, name) {
    var subscriber = new JSMQ.Subscriber();
    subscriber.connect(route);
    subscriber.subscribe(name);
    return subscriber;
}