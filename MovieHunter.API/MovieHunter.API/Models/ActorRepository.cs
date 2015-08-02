using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace MovieHunter.API.Models
{
	// For demo only, used for initial load of actors the database
	public class ActorRepository
	{
		public List<Actor> ActorList { get; set; }

		public List<Actor> Retrieve(string rootPath)
		{
			var filePath = Path.Combine(rootPath, @"App_Data/actors.json");
			var json = System.IO.File.ReadAllText(filePath);
			var actors = JsonConvert.DeserializeObject<ActorRepository>(json);

			return actors.ActorList;
		}
	}
}