using MovieHunter.API.Models;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace MovieHunter.API.Modules
{
	public class MoviesModule : NancyModule
	{
		public MoviesModule() : base("/api")
		{
			// GET api/movies
			Get["/movies"] = _ =>
			{
				var movies = new Models.MovieRepository();
				return JsonConvert.SerializeObject(movies.Retrieve());
			};

			// GET api/movies/title
			Get["/movies/{title:alpha}"] = parameters =>
			{
				var movies = new Models.MovieRepository();
				var movieList = movies.Retrieve();
				var filteredList = movieList.Where(t => t.title.Contains(parameters.title));
				return JsonConvert.SerializeObject(filteredList);
			};

			// GET api/movies/5
			Get["/movies/{id:int}"] = parameters =>
			{
				var movies = new Models.MovieRepository();
				var movieList = movies.Retrieve();
				var movie = movieList.FirstOrDefault(t => t.movieId == parameters.id);

				var actors = new Models.ActorRepository();
				var actorList = actors.Retrieve();

				var populatedActorList = new List<Models.Actor>();
				foreach (var item in movie.keyActors)
				{
					var actor = actorList.FirstOrDefault(a => a.actorId == item.actorId);
					populatedActorList.Add(actor);
				}
				movie.keyActors = populatedActorList;

				return JsonConvert.SerializeObject(movie);
			};

			// POST api/movies
			Post["/movies"] = _ =>
			{
				Movie movie = this.Bind<Movie>(); //Bind is an extension method defined in Nancy.ModelBinding
				return "Post movie form: " + movie.title;
			};

			// PUT api/movies/5
			Put["/movies/{id}"] = parameters =>
			{
				Movie movie = this.Bind<Movie>();
				return "Put param: " + parameters.id + ", actor form: " + movie.title;
			};

			// DELETE api/movies/5
			Delete["/movies/{id}"] = parameters =>
			{
				return "Delete param: " + parameters.id;
			};
		}
	}
}
