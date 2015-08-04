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
			Get["/actors", true] = async (parameters, ct) =>
			{
				var actorsDb = new ActorDAO(database);
				var actorList = await actorsDb.GetActorsList();

				return JsonConvert.SerializeObject(actorList);
			};

			// GET api/actors/name/actorname
			Get["/actors/name/{name:alpha}", true] = async (parameters, ct) =>
			{
				var actorsDb = new ActorDAO(database);
				var actorList = await actorsDb.GetActorsList();
				var filteredList = actorList.Where(t => t.name.Contains(parameters.name));

				return JsonConvert.SerializeObject(filteredList);
			};

			// GET api/actors/5
			Get["/actors/{id}", true] = async (parameters, ct) =>
			{
				var actorsDb = new ActorDAO(database);
				var actorList = await actorsDb.GetActorsList();
				var actor = actorList.FirstOrDefault(t => t.actorId == parameters.id);

				var moviesDb = new MovieDAO(database);
				var movieList = await moviesDb.GetMoviesList();
				var keyMovies = movieList
					.Where(m => m.keyActors != null)
					.SelectMany(
						m => m.keyActors.Where(
							a => a.actorId == actor.actorId), 
						(m, a) => m);

				if (keyMovies != null)
					actor.keyMovies = keyMovies.ToList();

				return JsonConvert.SerializeObject(actor);
			};

			// POST api/actors
			Post["/actors", true] = async (parameters, ct) =>
			{
				try
				{
					Actor actor = this.Bind<Actor>(); // Nancy.ModelBinding

					var actorsDb = new ActorDAO(database);
					var actorInserted = await actorsDb.PostActor(actor);

					return Negotiate.WithModel(actorInserted)
						.WithStatusCode(HttpStatusCode.Created);
				}
				catch
				{
					return HttpStatusCode.InternalServerError;
				}
			};

			// PUT api/actors/5
			Put["/actors/{id}", true] = async (parameters, ct) =>
			{
				try
				{
					Actor actor = this.Bind<Actor>();
					actor.actorId = parameters.id;

					var actorsDb = new ActorDAO(database);
					string updateResult = await actorsDb.PutActor(actor);

					return Negotiate.WithModel(updateResult)
						.WithStatusCode(HttpStatusCode.OK);
				}
				catch
				{
					return HttpStatusCode.InternalServerError;
				}
			};

			// DELETE api/actors/5
			Delete["/actors/{id}", true] = async (parameters, ct) =>
			{
				try
				{
					var actorsDb = new ActorDAO(database);
					string deleteResult = await actorsDb.DeleteActor(parameters.id);

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
