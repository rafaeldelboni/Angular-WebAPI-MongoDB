using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace MovieHunter.API.Models
{
	public class MovieDAO
	{
		private IMongoDatabase database;

		public MovieDAO (IMongoDatabase _database)
		{
			database = _database;
		}

		public async Task<List<Movie>> GetMoviesList ()
		{
			var collection = database.GetCollection<Movie>("movies");
			List<Movie> actors = await collection
				.Find (new BsonDocument ())
				.ToListAsync();

			return actors;
		}

		public async Task<List<Movie>> PostMovie (Movie movie)
		{
			var collection = database.GetCollection<Movie>("movies");
			List<Movie> actors = await collection
				.Find (new BsonDocument ())
				.ToListAsync();

			return actors;
		}

	}
}