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
        }
        else {
            APISearchService.checkProduct(item.key);
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

    hub.client.deleteProduct = function (name, redirect) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        };
        toastr.options.onclick = function () {
            window.location.href = redirect;
        };
        toastr["warning"]("Product " + name + " was deleted from your cabinet succesfully. Click to see.", "Delete Product");
    };

    hub.client.addProduct = function (name, redirect) {
        toastr.options = {
            "positionClass": "toast-bottom-right"
        };
        toastr.options.onclick = function () {
            window.location.href = redirect;
        };
        toastr["success"]("Product " + name + " was added to your cabinet succesfully. Click to see.", "Add Product");
    };

    $.connection.hub.start();
});