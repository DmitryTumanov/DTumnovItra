var addProd = createDealer("ws://localhost:3339");

var remProd = createDealer("ws://localhost:3340");

var infoChng = createDealer("ws://localhost:3341");

var addSubscriber = createSubscriber("ws://localhost:81", "addProduct");

var removeSubscriber = createSubscriber("ws://localhost:82", "removeProduct");

var infoSubscriber = createSubscriber("ws://localhost:83", "infoProduct");

addSubscriber.onMessage = function (message) {
    console.log("asf");
};

removeSubscriber.onMessage = function (message) {
    console.log("asfausytf");
};

infoSubscriber.onMessage = function (message) {
    console.log("sdjghj");
};

function addProduct(name) {
    var message = createMessage(name);
    addProd.send(message);
}

function removeProduct(name) {
    var message = createMessage(name);
    remProd.send(message);
}

function changeInfo(name) {
    var message = createMessage(name);
    infoChng.send(message);
}




function createMessage(name) {
    var message = new JSMQ.Message();
    message.addString(name);
    return message;
}

function createSubscriber(route, name) {
    var subscriber = new JSMQ.Subscriber();
    subscriber.connect(route);
    subscriber.subscribe(name);
    return subscriber;
}

function createDealer(path) {
    var dealer = new JSMQ.Dealer();
    dealer.connect(path);
    return dealer;
}