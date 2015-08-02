using System.Linq;
using MongoDB.Driver;
using MovieHunter.API.Models;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;

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
				var actorList = actorsDb.Retrieve();

				return JsonConvert.SerializeObject(actorList);
			};

			// GET api/actors/name
			Get["/actors/{name:alpha}"] = parameters =>
			{
				var actorsDb = new ActorDAO(database);
				var actorList = actorsDb.Retrieve();
				var filteredList = actorList.Where(t => t.name.Contains(parameters.name));

				return JsonConvert.SerializeObject(filteredList);
			};

			// GET api/actors/5
			Get["/actors/{id:int}"] = parameters =>
			{
				var actorsDb = new ActorDAO(database);
				var actorList = actorsDb.Retrieve();
				var actor = actorList.FirstOrDefault(t => t.actorId == parameters.id);

				var moviesDb = new MovieDAO(database);
				var movieList = moviesDb.Retrieve();
				var keyMovies = movieList.SelectMany(
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
				Actor actor = this.Bind<Actor>(); //Bind is an extension method defined in Nancy.ModelBinding
				return "Post actor form: " + actor.name;
			};

			// PUT api/actors/5
			Put["/actors/{id}"] = parameters =>
			{
				Actor actor = this.Bind<Actor>();
				return "Put param: " + parameters.id + ", actor form: " + actor.name;
			};

			// DELETE api/actors/5
			Delete["/actors/{id}"] = parameters =>
			{
				return "Delete param: " + parameters.id;
			};

		}
	}
}
