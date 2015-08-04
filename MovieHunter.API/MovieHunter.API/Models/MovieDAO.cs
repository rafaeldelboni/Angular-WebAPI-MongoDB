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
			List<Movie> movies = await collection
				.Find (new BsonDocument ())
				.ToListAsync();

			return movies;
		}

		public async Task<string> PostMovie (Movie movie)
		{
			movie.movieId = ObjectId.GenerateNewId().ToString();

			var collection = database.GetCollection<Movie>("movies");
			await collection.InsertOneAsync(movie);

			return movie.movieId;
		}

		public async Task<string> PutMovie (Movie movie)
		{
			var collection = database.GetCollection<Movie>("movies");
			var filter = Builders<Movie>.Filter.Eq(s => s.movieId, movie.movieId);
			var result = await collection.ReplaceOneAsync (filter, movie);

			return result.ToString();
		}

		public async Task<string> DeleteMovie (string movieId)
		{
			var collection = database.GetCollection<Movie>("movies");
			var filter = Builders<Movie>.Filter.Eq(s => s.movieId, movieId);
			var result = await collection.DeleteManyAsync(filter);

			return result.ToString();
		}

	}
}