var addSubscriber = createSubscriber("ws://localhost:81", "addProduct");

var removeSubscriber = createSubscriber("ws://localhost:81", "removeProduct");

var infoSubscriber = createSubscriber("ws://localhost:81", "infoProduct");

var addSubscriberSearch = createSubscriber("ws://localhost:81", "addProductSearch");

var removeSubscriberSearch = createSubscriber("ws://localhost:81", "removeProductSearch");

addSubscriber.onMessage = function (message) {
    message.popString();
    toastr.options = {
        "positionClass": "toast-bottom-right"
    };
    toastr["success"]("Product " + message.popString() + " was returned to your cabinet succesfully.", "Add Product");
};

removeSubscriber.onMessage = function (message) {
    message.popString();
    toastr.options = {
        "positionClass": "toast-bottom-right"
    };
    toastr["warning"]("Product " + message.popString() + " was deleted from your cabinet succesfully.", "Delete Product");
};

infoSubscriber.onMessage = function (message) {
    message.popString();
    toastr.options = {
        "positionClass": "toast-bottom-right"
    };
    toastr["info"]("Now you will have messages at " + message.popString(), "Settings Changed");
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
    toastr["warning"]("Product " + text + " was deleted from your cabinet succesfully.", "Delete Product");
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
    toastr["success"]("Product " + text + " was added to your cabinet succesfully. Click to see.", "Add Product");
};

function createSubscriber(route, name) {
    var subscriber = new JSMQ.Subscriber();
    subscriber.connect(route);
    subscriber.subscribe(name);
    return subscriber;
}