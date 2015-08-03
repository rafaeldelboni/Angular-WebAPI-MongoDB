using System.Linq;
using MongoDB.Driver;
using MovieHunter.API.Models;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MovieHunter.API.Modules
{
	public class ActorsModule : NancyModule
	{
		public ActorsModule(IMongoDatabase database) : base("/api")
		{
			// GET api/actors
			Get["/actors"] = _ =>
			{
				var actorsDb = new ActorDAO(database);
				var actorList = actorsDb.GetActorsList();

				return JsonConvert.SerializeObject(actorList.Result);
			};

			// GET api/actors/name/actorname
			Get["/actors/name/{name:alpha}"] = parameters =>
			{
				var actorsDb = new ActorDAO(database);
				var actorList = actorsDb.GetActorsList();
				var filteredList = actorList.Result.Where(t => t.name.Contains(parameters.name));

				return JsonConvert.SerializeObject(filteredList);
			};

			// GET api/actors/5
			Get["/actors/{id}"] = parameters =>
			{
				var actorsDb = new ActorDAO(database);
				var actorList = actorsDb.GetActorsList();
				var actor = actorList.Result.FirstOrDefault(t => t.actorId == parameters.id);

				var moviesDb = new MovieDAO(database);
				var movieList = moviesDb.GetMoviesList();
				var keyMovies = movieList.Result.SelectMany(
					m => m.keyActors.Where(
						a => a.actorId == actor.actorId
					), (m, a) => m
				);

				actor.keyMovies = keyMovies.ToList();

				return JsonConvert.SerializeObject(actor);
			};

			// POST api/actors
			Post["/actors"] = _ =>
			{
				try
				{
					Actor actor = this.Bind<Actor>(); // Nancy.ModelBinding

					var actorsDb = new ActorDAO(database);
					var actorInserted = actorsDb.PostActor(actor);

					return Negotiate.WithModel(actorInserted.Result)
						.WithStatusCode(HttpStatusCode.Created);
				}
				catch
				{
					return HttpStatusCode.InternalServerError;
				}
			};

			// PUT api/actors/5
			Put["/actors/{id}"] = parameters =>
			{
				try
				{
					Actor actor = this.Bind<Actor>();
					actor.actorId = parameters.id;

					var actorsDb = new ActorDAO(database);
					Task<string> updateResult = actorsDb.PutActor(actor);

					return Negotiate.WithModel(updateResult.Result)
						.WithStatusCode(HttpStatusCode.OK);
				}
				catch
				{
					return HttpStatusCode.InternalServerError;
				}
			};

			// DELETE api/actors/5
			Delete["/actors/{id}"] = parameters =>
			{
				try
				{
					var actorsDb = new ActorDAO(database);
					Task<string> deleteResult = actorsDb.DeleteActor(parameters.id);

					return Negotiate.WithModel(deleteResult.Result)
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
