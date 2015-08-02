using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MovieHunter.API.Models
{
	// For demo only, used for initial load of movies the database
	public class MovieRepository
	{
		public List<Movie> MovieList { get; set; }

		public List<Movie> Retrieve(string rootPath)
		{
			var filePath = Path.Combine(rootPath, @"App_Data/movies.json");
			var json = System.IO.File.ReadAllText(filePath);
			var movies = JsonConvert.DeserializeObject<MovieRepository>(json);

			return movies.MovieList;
		}
	}
}