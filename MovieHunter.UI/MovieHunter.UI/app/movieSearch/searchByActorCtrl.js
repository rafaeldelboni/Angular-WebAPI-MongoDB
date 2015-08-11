(function () {
    "use strict";

    angular
        .module("movieHunter")
        .controller("SearchByActorCtrl",
                    ["$scope",
                     "actorResource",
                     SearchByActorCtrl]);

    function SearchByActorCtrl ($scope, actorResource) {

        $scope.actorList = [];
        $scope.title = "Search by Actor";

		actorResource.getActors().query(
            function (data) {
                $scope.actorList = data;
            },
            function (response) {
                $scope.errorText = response.message + "\r\n";
                if (response.data && response.data.exceptionMessage)
                    $scope.errorText += response.data.exceptionMessage;
            }
       );

    }

}());