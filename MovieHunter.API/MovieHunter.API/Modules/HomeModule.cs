using Nancy;
using MongoDB.Driver;
using MovieHunter.API.Models;
using Newtonsoft.Json;

public class HomeModule : Nancy.NancyModule
{
	public HomeModule(IMongoDatabase database, IRootPathProvider rootPathProvider)
	{
		Get["/"] = _ =>
		{
			ViewBag.title = "Home Page!";
			ViewBag.url = Request.Url.ToString();
			return View["Index"];
		};

		Get["/test"] = _ =>
		{
			var moviesDb = new MovieDAO(database);
			var movies = moviesDb.Retrieve();

			return JsonConvert.SerializeObject(movies);
		};
	}
}