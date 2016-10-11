app.controller('APIController', function ($scope, APIService) {
    getAll();

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
        APIService.changeTime(time);
        $('#submitbutton').hide();
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