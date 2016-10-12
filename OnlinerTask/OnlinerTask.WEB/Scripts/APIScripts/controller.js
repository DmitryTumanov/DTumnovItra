app.controller('APIController', function ($scope, $filter, APIService) {
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
        var servCall = APIService.getProducts();

        servCall.then(function (d) {
            $scope.products = d.data.products;
            $scope.emailtime = d.data.emailtime;
        }, function (error) {
            $log.error('Something went wrong while fetching the data.');
        });
    }
});