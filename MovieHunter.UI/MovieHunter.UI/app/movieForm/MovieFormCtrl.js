(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("MovieFormCtrl",
                    ["$scope",
                     "$http",
                     "$routeParams",
                     MovieFormCtrl]);

    function MovieFormCtrl ($scope, $http, $routeParams) {
    	$scope.movieId = $routeParams.movieId;

		if ($scope.movieId == null) {
        	$scope.title = "Create Movie";
        } else {
        	$scope.title = "Update Movie";
        }

        $scope.message = "";
		$scope.master = {};

		$scope.actors = [
	        {actorId: 1, name: "Actor 1"}, 
	        {actorId: 2, name: "Actor 2"},  
	        {actorId: 3, name: "Actor 3"},  
	        {actorId: 4, name: "Actor 4"},  
	        {actorId: 5, name: "Actor 5"},  
	        {actorId: 6, name: "Actor 6"}
    	];
    	$scope.actorsModel = {actorId: null, name: null};

		$scope.update = function(movie) {
			if($scope.form.$valid){
		    	$scope.master = angular.copy(movie);
		    	$scope.message = "Saved!";
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

		$scope.reset();
    }
}());