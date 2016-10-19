app.controller('APIController', function ($scope, APISearchService) {
    var hub = $.connection.personalHub;
    $scope.currencies = [
        { name: 'BYN', factor: 1 },
        { name: 'USD', factor: 0.5225 }
    ];
    $scope.selectedCurrency = $scope.currencies[0];

    $scope.checkboxChange = function (item) {
        if (!item.is_checked) {
            APISearchService.uncheckProduct(item.id);
            hub.server.deleteProduct(item.full_name);
        }
        else {
            APISearchService.checkProduct(item.key);
            hub.server.addProduct(item.full_name);
        }
        return true;
    };

    $scope.getSearchProducts = function (search_string) {
        var servCall = APISearchService.getSearchProducts(search_string);

        servCall.then(function (d) {
            $scope.products = d.data;
        }, function (error) {
            $log.error('Something went wrong while fetching the data.');
        });
    };

    hub.client.deleteProduct = function (name) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        };
        toastr.options.onclick = function () {
            window.location.href = "http://localhost:33399/Manage";
        };
        toastr["warning"]("Product " + name + " was deleted from your cabinet succesfully. Click to see.", "Delete Product");
    };

    hub.client.addProduct = function (name) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        };
        toastr.options.onclick = function () {
            window.location.href = "http://localhost:33399/Manage";
        };
        toastr["success"]("Product " + name + " was returned to your cabinet succesfully. Click to see.", "Add Product");
    };

    $.connection.hub.start();
});