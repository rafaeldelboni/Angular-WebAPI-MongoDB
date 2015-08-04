using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieHunter.API.Models
{
	public class Movie
	{
		[BsonId]
		public string movieId { get; set; }
		public string title { get; set; }
		public string description { get; set; }
		public string director { get; set; }
		public string imdbLink { get; set; }
		public string imageUrl { get; set; }
		public string mpaa { get; set; }
		public DateTime releaseDate { get; set; }
		public List<Actor> keyActors { get; set; }
	}
}