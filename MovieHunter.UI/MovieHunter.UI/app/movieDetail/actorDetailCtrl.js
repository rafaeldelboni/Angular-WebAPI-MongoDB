(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("ActorDetailCtrl",
                    ["$scope",
                     "$routeParams",
                     "actorResource",
                     ActorDetailCtrl]);

    function ActorDetailCtrl ($scope, $routeParams, actorResource) {
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

    }
}());