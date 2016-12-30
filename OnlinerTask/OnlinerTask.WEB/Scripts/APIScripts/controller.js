app.controller('APIController', ["$scope", "$filter", "APIService", function ($scope, $filter, APIService) {
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
        }
        else {
            APIService.checkProduct(item.key);
        }
        return true;
    };

    $scope.submitTime = function () {
        var time = $('#timepicker').val();
        $scope.emailtime = time;
        APIService.changeTime(time);
        $('#submitbutton').hide();
        $('#cancelbutton').hide();
    };

    function getAll() {
        document.getElementById("productLoader").style.display = "block";
        document.getElementById("productDiv").style.display = "none";
        var servCall = APIService.getProducts();

        servCall.then(function (d) {
            $scope.products = d.data.products;
            $scope.emailtime = d.data.emailtime;
            document.getElementById("productLoader").style.display = "none";
            document.getElementById("productDiv").style.display = "block";
        }, function (error) {
            document.getElementById("productLoader").style.display = "none";
            document.getElementById("productDiv").style.display = "block";
        });
    }

    hub.client.changeTime = function (time) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        };
        toastr["info"]("Now you will have messages at " + time, "Settings Changed");
    };

    hub.client.deleteProduct = function (name, redirect) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        };
        toastr["warning"]("Product " + name + " was deleted from your cabinet succesfully.", "Delete Product");
    };

    hub.client.addProduct = function (name, redirect) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        };
        toastr["success"]("Product " + name + " was returned to your cabinet succesfully.", "Add Product");
    };

    $.connection.hub.start();
}]);