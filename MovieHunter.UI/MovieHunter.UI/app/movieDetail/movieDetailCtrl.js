(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("MovieDetailCtrl",
                    ["$scope",
                     "$routeParams",
                     "movieResource",
                     MovieDetailCtrl]);

    function MovieDetailCtrl ($scope, $routeParams, movieResource) {
        $scope.movieId = $routeParams.movieId;

		movieResource.getMovies().get({ movieId: $scope.movieId },
            function (data) {
                $scope.movie = data;
            },
            function (response) {
                $scope.errorText = response.message + "\r\n";
                if (response.data && response.data.exceptionMessage)
                    $scope.errorText += response.data.exceptionMessage;
            }
        );
    }
}());