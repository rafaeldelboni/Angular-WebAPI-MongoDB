using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MovieHunter.API.Models;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MovieHunter.API.Modules
{
	public class MoviesModule : NancyModule
	{
		public MoviesModule(IMongoDatabase database) : base("/api")
		{
			// GET api/movies
			Get["/movies", true] = async (parameters, ct) =>
			{
				var moviesDb = new MovieDAO(database);
				var movieList = await moviesDb.GetMoviesList();

				return JsonConvert.SerializeObject(movieList);
			};

			// GET api/movies/title/moviename
			Get["/movies/title/{title*}", true] = async (parameters, ct) =>
			{
				var moviesDb = new MovieDAO(database);
				var movieList = await moviesDb.GetMoviesList();
				var filteredList = movieList.Where(t => t.title.Contains(parameters.title));

				return JsonConvert.SerializeObject(filteredList);
			};

			// GET api/movies/5
			Get["/movies/{id}", true] = async (parameters, ct) =>
			{
				var moviesDb = new MovieDAO(database);
				var movieList = await moviesDb.GetMoviesList();
				var movie = movieList.FirstOrDefault(t => t.movieId == parameters.id);

				var actorsDb = new ActorDAO(database);
				var actorList = await actorsDb.GetActorsList();

				if(movie.keyActors != null) 
				{
					var populatedActorList = new List<Models.Actor>();
					foreach (var item in movie.keyActors)
					{
						var actor = actorList.FirstOrDefault(a => a.actorId == item.actorId);
						populatedActorList.Add(actor);
					}
					movie.keyActors = populatedActorList;
				}

				return JsonConvert.SerializeObject(movie);
			};

			// POST api/movies
			Post["/movies", true] = async (parameters, ct) =>
			{
				try
				{
					Movie movie = this.Bind<Movie>(); // Nancy.ModelBinding

					var moviesDb = new MovieDAO(database);
					var movieInserted = await moviesDb.PostMovie(movie);

					return Negotiate.WithModel(movieInserted)
						.WithStatusCode(HttpStatusCode.Created);
				}
				catch
				{
					return HttpStatusCode.InternalServerError;
				}
			};

			// PUT api/movies/5
			Put["/movies/{id}", true] = async (parameters, ct) =>
			{
				try
				{
					Movie movie = this.Bind<Movie>();
					movie.movieId = parameters.id;

					var moviesDb = new MovieDAO(database);
					string updateResult = await moviesDb.PutMovie(movie);

					return Negotiate.WithModel(updateResult)
						.WithStatusCode(HttpStatusCode.OK);
				}
				catch
				{
					return HttpStatusCode.InternalServerError;
				}
			};

			// DELETE api/movies/5
			Delete["/movies/{id}", true] = async (parameters, ct) =>
			{
				try
				{
					var moviesDb = new MovieDAO(database);
					string deleteResult = await moviesDb.DeleteMovie(parameters.id);

					return Negotiate.WithModel(deleteResult)
						.WithStatusCode(HttpStatusCode.OK);
				}
				catch
				{
					return HttpStatusCode.InternalServerError;
				}
			};
		}
	}
}
