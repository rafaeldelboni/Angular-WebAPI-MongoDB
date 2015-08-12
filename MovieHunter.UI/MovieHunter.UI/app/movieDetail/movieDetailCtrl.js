(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("MovieDetailCtrl",
                    ["$scope",
                     "$routeParams",
                     "$location",
                     "movieResource",
                     MovieDetailCtrl]);

    function MovieDetailCtrl ($scope, $routeParams, $location, movieResource) {
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

		$scope.delete = function() {			
			movieResource.getMovies().delete({ movieId: $scope.movieId },
				function (data) {
	                $location.path('/searchByTitle');
	            },
	            function (response) {
	                $scope.errorText = response.message + "\r\n";
	                if (response.data && response.data.exceptionMessage)
	                    $scope.errorText += response.data.exceptionMessage;
	            }
        	);
		};
    }
}());