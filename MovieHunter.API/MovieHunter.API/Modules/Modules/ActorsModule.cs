using MovieHunter.API.Models;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System.Linq;

namespace MovieHunter.API.Modules
{
	public class ActorsModule : NancyModule
	{
		public ActorsModule() : base("/api")
		{
			// GET api/actors
			Get["/actors"] = _ =>
			{
				var actors = new Models.ActorRepository();
				return JsonConvert.SerializeObject(actors.Retrieve());
			};

			// GET api/actors/name
			Get["/actors/{name:alpha}"] = parameters =>
			{
				var actors = new Models.ActorRepository();
				var actorList = actors.Retrieve();
				var filteredList = actorList.Where(t => t.name.Contains(parameters.name));
				return JsonConvert.SerializeObject(filteredList);
			};

			// GET api/actors/5
			Get["/actors/{id:int}"] = parameters =>
			{
				var actors = new Models.ActorRepository();
				var actorList = actors.Retrieve();
				var actor = actorList.FirstOrDefault(t => t.actorId == parameters.id);

				var movies = new Models.MovieRepository();
				var movieList = movies.Retrieve();

				var keyMovies = movieList.SelectMany(m => m.keyActors.Where(a => a.actorId == actor.actorId),
					(m, a) => m);
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
