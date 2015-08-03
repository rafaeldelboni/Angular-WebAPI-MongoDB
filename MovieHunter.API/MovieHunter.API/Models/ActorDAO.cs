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

		public async Task<List<Actor>> GetActorsList ()
		{
			var collection = database.GetCollection<Actor>("actors");
			List<Actor> actors = await collection
				.Find (new BsonDocument ())
				.ToListAsync();

			return actors;
		}

		public async Task<string> PostActor (Actor actor)
		{
			actor.actorId = ObjectId.GenerateNewId().ToString();

			var collection = database.GetCollection<Actor>("actors");
			await collection.InsertOneAsync(actor);

			return actor.actorId;
		}

		public async Task<string> PutActor (Actor actor)
		{
			var collection = database.GetCollection<Actor>("actors");
			var filter = Builders<Actor>.Filter.Eq(s => s.actorId, actor.actorId);
			var update = Builders<Actor>.Update
				.Set ("actorId", actor.actorId)
				.Set ("name", actor.name)
				.Set ("country", actor.country)
				.Set ("imdbLink", actor.imdbLink)
				.Set ("birthDate", actor.birthDate);

			var result = await collection.UpdateOneAsync(filter, update);

			return result.ToString();
		}

		public async Task<string> DeleteActor (string actorId)
		{
			var collection = database.GetCollection<Actor>("actors");
			var filter = Builders<Actor>.Filter.Eq(s => s.actorId, actorId);
			var result = await collection.DeleteManyAsync(filter);

			return result.ToString();
		}

	}
}