app.service("APISearchService", function ($http) {

    this.getSearchProducts = function (search_string, page_number) {
        return $http({
            method: 'POST',
            url: '/api/Product',
            data: {
                SearchString: search_string,
                PageNumber: page_number
            },
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken")
            }
        });
    };

    this.checkProduct = function (key) {
        return $http({
            method: 'PUT',
            url: '/api/Product',
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
            url: '/api/Product',
            data: {
                ItemId: productid
            },
            headers: {
                'Authorization': 'Bearer ' + sessionStorage.getItem("accessToken"),
                'Content-Type': 'application/json'
            }
        });
    };

});