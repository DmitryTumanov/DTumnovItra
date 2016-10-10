app.service("APIService", function ($http) {

    this.getProducts = function () {
        return $http({
            method: 'GET',
            url:'/api/Personal',
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken")
            }
        });
    };

    this.checkProduct = function (key) {
        return $http({
            method: 'PUT',
            url: '/api/Personal',
            data: {
                SearchString: key
            },
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken")
            }
        });
    };

    this.uncheckProduct = function (productid) {
        return $http({
            method: 'DELETE',
            url: '/api/Personal',
            data:{
                ItemId: productid
            },
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken"),
                'Content-Type': 'application/json'
            }
        });
    };

});