app.controller('APIController', function ($scope, $filter, APIService) {
    var hub = $.connection.personalHub;
    getAll();
    $scope.currencies = [
        { name: 'BYN', factor: 1 },
        { name: 'USD', factor: 0.5225 }
    ];
    $scope.selectedCurrency = $scope.currencies[0];


    $scope.checkboxChange = function (item) {
        if (!item.is_checked) {
            APIService.uncheckProduct(item.id);
            hub.server.deleteProduct(item.full_name);
        }
        else {
            APIService.checkProduct(item.key);
            hub.server.addProduct(item.full_name);
        }
        return true;
    };

    $scope.submitTime = function () {
        var time = $('#timepicker').val();
        $scope.emailtime = time;
        APIService.changeTime(time);
        $('#submitbutton').hide();
        $('#cancelbutton').hide();
        hub.server.changeSettings(time);
    };

    function getAll() {
        var servCall = APIService.getProducts();

        servCall.then(function (d) {
            $scope.products = d.data.products;
            $scope.emailtime = d.data.emailtime;
        }, function (error) {
            $log.error('Something went wrong while fetching the data.');
        });
    }

    hub.client.changeTime = function (time) {
        toastr.options= {
            "positionClass": "toast-bottom-right"
        }
        toastr["info"]("Now you will have messages at " + time, "Settings Changed");
    };

    hub.client.deleteProduct = function (name) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        }
        toastr["warning"]("Product " + name + " was deleted from your cabinet succesfully.", "Delete Product");
    };

    hub.client.addProduct = function (name) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        }
        toastr["success"]("Product " + name + " was returned to your cabinet succesfully.", "Add Product");
    };

    $.connection.hub.start();
});