using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MovieHunter.API.Models
{
	public class ActorDAO
	{
		private IMongoDatabase database;

		public ActorDAO (IMongoDatabase _database)
		{
			database = _database;
		}

		private async Task<List<Actor>> getActorsDb ()
		{
			var collection = database.GetCollection<Actor>("actors");
			List<Actor> actors = await collection
				.Find (new BsonDocument ())
				.ToListAsync();

			return actors;
		}

		public List<Actor> Retrieve()
		{
			var actors = getActorsDb ();
			return actors.Result;
		}
	}
}