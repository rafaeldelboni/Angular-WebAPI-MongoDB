using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieHunter.API.Models
{
	public class Actor
	{
		[BsonId]
		public string actorId { get; set; }
		public string name { get; set; }
		public string country { get; set; }
		public string imdbLink { get; set; }
		public DateTime birthDate { get; set; }
		public List<Movie> keyMovies { get; set; }
	}
}