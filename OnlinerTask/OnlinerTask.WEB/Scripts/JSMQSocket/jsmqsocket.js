var addSubscriber = createSubscriber("ws://localhost:81", "addProduct");

var removeSubscriber = createSubscriber("ws://localhost:81", "removeProduct");

var infoSubscriber = createSubscriber("ws://localhost:81", "infoProduct");

addSubscriber.onMessage = function (message) {
    message.popString();
    console.log(message.popString());
};

removeSubscriber.onMessage = function (message) {
    message.popString();
    console.log(message.popString());
};

infoSubscriber.onMessage = function (message) {
    message.popString();
    console.log(message.popString());
};

function createSubscriber(route, name) {
    var subscriber = new JSMQ.Subscriber();
    subscriber.connect(route);
    subscriber.subscribe(name);
    return subscriber;
}