(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("ActorFormCtrl",
                    ["$scope",
                     "$routeParams",
                     "actorResource",
                     ActorFormCtrl]);

    function ActorFormCtrl ($scope, $routeParams, actorResource) {
    	$scope.actorId = $routeParams.actorId;
        $scope.message = "";
		$scope.master = {};

		if ($scope.actorId == null) {
        	$scope.title = "Create Actor";
        } else {
        	$scope.title = "Update Actor";

			actorResource.getActors().get({ actorId: $scope.actorId },
	            function (data) {
	                $scope.actor = data;
	                $scope.actor.birthDate = new Date($scope.actor.birthDate);
	                $scope.master = angular.copy($scope.actor);
	            },
	            function (response) {
	                $scope.errorText = response.message + "\r\n";
	                if (response.data && response.data.exceptionMessage)
	                    $scope.errorText += response.data.exceptionMessage;
	            }
        	);

        }

		$scope.update = function(actor) {
			if($scope.form.$valid){

				$scope.master = angular.copy(actor);

		    	if ($scope.actorId == null) {
			    	actorResource.saveActor().post($scope.master,
	    	            function (data) {
			                $scope.actorId = data;
			                $scope.message = "Saved!";
			            },
			            function (response) {
			                $scope.message = response.message + "\r\n";
			                if (response.data && response.data.exceptionMessage)
			                    $scope.message += response.data.exceptionMessage;
			            }
		            );
	            } else {
			    	actorResource.saveActor().put(
			    		{ actorId: $scope.actorId },
			    		$scope.master,
	    	            function (data) {
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
	  		$scope.actor = angular.copy($scope.master);
		};

		$scope.back = function() {			
			history.back();
        	if(!$scope.$$phase) {
				$scope.$apply();
			}
		};

		$scope.reset();
    }

}());