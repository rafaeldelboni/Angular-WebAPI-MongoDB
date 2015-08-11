
(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("actorResource",
                ["$resource",
                 "appSettings",
                    actorResource]);

    function actorResource($resource, appSettings) {
    	var actorResource = {};

    	actorResource.getActors = function() {
        	return $resource(appSettings.serverPath + "/api/actors/:actorId", {},
        	{ 'get':    {method:'GET'},
			  'query':  {method:'GET', isArray:true}
		  	});
        }

    	actorResource.getActorsByName = function() {
        	return $resource(appSettings.serverPath + "/api/actors/name/:name");
        }

    	actorResource.saveActor = function() {
        	return $resource(appSettings.serverPath + "/api/actors/:actorId", null,
        	{ 'post':	{method:'POST'},
			  'put':	{method:'PUT'}
		  	});
        }

        return actorResource;
    }
}());