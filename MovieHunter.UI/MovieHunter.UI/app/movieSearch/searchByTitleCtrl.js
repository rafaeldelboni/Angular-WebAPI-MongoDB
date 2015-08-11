(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("SearchByTitleCtrl",
                    ["$scope",
                     "movieResource",
                     SearchByTitleCtrl]);

    function SearchByTitleCtrl($scope, movieResource) {

        $scope.movieList = [];
        $scope.title = "Search by Movie Title";
        $scope.showImage = false;

        $scope.toggleImage = function () {
            $scope.showImage = !$scope.showImage;
        };

		movieResource.getMovies().query(
            function (data) {
                $scope.movieList = data;
            },
            function (response) {
                $scope.errorText = response.message + "\r\n";
                if (response.data && response.data.exceptionMessage)
                    $scope.errorText += response.data.exceptionMessage;
            }
       );
    }
}());