var model = {
    message: ko.observable(""),
    search_string: ko.observable(""),
    products: ko.observableArray()
};

function handleKeyDown() {
    sendAjaxRequest("POST", function (data) {
        model.products.removeAll();
        for (var i = 0; i < data.length; i++) {
            model.products.push(data[i]);
        }
    }, null, {
        SearchString: model.search_string
    });
}

function handleCheckBox(item) {
    if (!item.is_checked) {
        sendAjaxRequest("DELETE", null, null, {
            ItemId: item.id
        });
    }
    else {
        sendAjaxRequest("PUT", null, null, {
            SearchString: item.key
        });
    }
    return true;
}

function sendAjaxRequest(httpMethod, callback, url, reqData) {
    $.ajax("/api/Product" + (url ? "/" + url : ""), {
        type: httpMethod,
        success: callback,
        data: reqData,
        headers: {
            'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken")
        }
    });
}

function getAllItems() {
    sendAjaxRequest("GET", function (data) {
        model.message("хоп хей лалалей");
    });
}

$(document).ready(function () {
    getAllItems();
    ko.applyBindings(model);
});