
(function () {
    "use strict";

    angular
        .module("common.services")
        .factory("movieResource",
                ["$resource",
                 "appSettings",
                    movieResource]);

    function movieResource($resource, appSettings) {
    	var movieResource = {};

    	movieResource.getMovies = function() {
        	return $resource(appSettings.serverPath + "/api/movies/:movieId", {},
        	{ 'get':    {method:'GET'},
			  'query':  {method:'GET', isArray:true}
		  	});
        }

    	movieResource.getMoviesByTitle = function() {
        	return $resource(appSettings.serverPath + "/api/movies/title/:title");
        }

    	movieResource.saveMovie = function() {
        	return $resource(appSettings.serverPath + "/api/movies/:movieId", null,
        	{ 'post':	{method:'POST'},
			  'put':	{method:'PUT'},
			  'delete':	{method:'DELETE'}
		  	});
        }

        return movieResource;
    }
}());