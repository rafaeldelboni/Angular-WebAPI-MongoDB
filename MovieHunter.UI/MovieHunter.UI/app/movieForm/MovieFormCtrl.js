(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("MovieFormCtrl",
                    ["$scope",
                     "$routeParams",
                     "actorResource",
                     "movieResource",
                     MovieFormCtrl]);

    function MovieFormCtrl ($scope, $routeParams, actorResource, movieResource) {
    	$scope.movieId = $routeParams.movieId;
        $scope.message = "";
		$scope.master = {};

		if ($scope.movieId == null) {
        	$scope.title = "Create Movie";
        } else {
        	$scope.title = "Update Movie";

			movieResource.getMovies().get({ movieId: $scope.movieId },
            function (data) {
                $scope.movie = data;
                $scope.movie.releaseDate = new Date($scope.movie.releaseDate);
                $scope.master = angular.copy($scope.movie);
            },
            function (response) {
                $scope.errorText = response.message + "\r\n";
                if (response.data && response.data.exceptionMessage)
                    $scope.errorText += response.data.exceptionMessage;
            }
        );

        }

        $scope.actors = [];
		actorResource.getActors().query(
            function (data) {
                $scope.actors = data;
            },
            function (response) {
                $scope.errorText = response.message + "\r\n";
                if (response.data && response.data.exceptionMessage)
                    $scope.errorText += response.data.exceptionMessage;
            }
        );

    	$scope.actorsModel = {actorId: null, name: null};

		$scope.update = function(movie) {
			if($scope.form.$valid){

				$scope.master = angular.copy(movie);

		    	if ($scope.movieId == null) {
			    	movieResource.saveMovie().post($scope.master,
	    	            function (data) {
			                $scope.movieId = data;
			                $scope.message = "Saved!";
			            },
			            function (response) {
			                $scope.message = response.message + "\r\n";
			                if (response.data && response.data.exceptionMessage)
			                    $scope.message += response.data.exceptionMessage;
			            }
		            );
	            } else {
			    	movieResource.saveMovie().put(
			    		{ movieId: $scope.movieId },
			    		$scope.master,
	    	            function (data) {
			                $scope.movieId = data;
			                $scope.message = "Updated!";
			            },
			            function (response) {
			                $scope.message = response.message + "\r\n";
			                if (response.data && response.data.exceptionMessage)
			                    $scope.message += response.data.exceptionMessage;
			            }
		            );
	            }
		  	}
		};

		$scope.reset = function(form) {			
	  		if (form) {
		    	form.$setPristine();
		    	form.$setUntouched();
		    	$scope.message = "";
		  	}
	  		$scope.movie = angular.copy($scope.master);
		};

		$scope.back = function() {			
			history.back();
        	scope.$apply();
		};

		$scope.reset();
    }

}());