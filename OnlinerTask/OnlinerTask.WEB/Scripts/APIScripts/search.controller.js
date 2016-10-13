app.controller('APIController', function ($scope, APISearchService) {
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
});