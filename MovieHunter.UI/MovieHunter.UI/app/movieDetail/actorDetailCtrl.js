(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("ActorDetailCtrl",
                    ["$scope",
                     "$routeParams",
                     "$location",
                     "actorResource",
                     ActorDetailCtrl]);

    function ActorDetailCtrl ($scope, $routeParams, $location, actorResource) {
        $scope.actorId = $routeParams.actorId;

		actorResource.getActors().get({ actorId: $scope.actorId },
            function (data) {
                $scope.actor = data;
            },
            function (response) {
                $scope.errorText = response.message + "\r\n";
                if (response.data && response.data.exceptionMessage)
                    $scope.errorText += response.data.exceptionMessage;
            }
        );

		$scope.delete = function() {			
			actorResource.getActors().delete({ actorId: $scope.actorId },
				function (data) {
	                $location.path('/searchByActor');
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