app.controller('APIController', function ($scope, APISearchService) {
    var hub = $.connection.personalHub;
    $scope.currencies = [
        { name: 'BYN', factor: 1 },
        { name: 'USD', factor: 0.5225 }
    ];
    $scope.selectedCurrency = $scope.currencies[0];
    $scope.busy = false;
    $scope.page = 1;
    $scope.search_string = "";

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
        $scope.search_string = search_string;
        var servCall = APISearchService.getSearchProducts(search_string, 1);

        servCall.then(function (d) {
            $scope.products = d.data;
        }, function (error) {
            $log.error('Something went wrong while fetching the data.');
        });
    };

    $scope.nextProductPage = function () {
        if (this.busy || typeof $scope.products === "undefined" || $scope.page++ === 1 || $scope.page++ === 0) return;
        $scope.busy = true;
        var servCall = APISearchService.getSearchProducts($scope.search_string, $scope.page++);
        servCall.then(function (d) {
            console.log(d.data);
            for (var i = 0; i < 10; i++) {
                if (typeof d.data[i] !== "undefined") {
                    $scope.products.push(d.data[i]);
                }
            }
        }, function (error) {
            $log.error('Something went wrong while fetching the data.');
        });
        $scope.busy = false;
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