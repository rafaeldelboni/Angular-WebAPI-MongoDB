using Nancy;
using MongoDB.Driver;
using MovieHunter.API.Models;
using Newtonsoft.Json;

public class HomeModule : Nancy.NancyModule
{
	public HomeModule()
	{
		Get["/"] = _ =>
		{
			ViewBag.title = "Home Page!";
			ViewBag.url = Request.Url.ToString();
			return View["Index"];
		};
	}
}