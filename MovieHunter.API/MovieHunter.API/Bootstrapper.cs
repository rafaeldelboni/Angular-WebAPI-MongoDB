using Nancy;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;

namespace MovieHunter.API
{
	public class Bootstrapper : DefaultNancyBootstrapper
	{
		protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
		{
			base.ApplicationStartup(container, pipelines);

			StaticConfiguration.DisableErrorTraces = false;

		}
		protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
		{

			//CORS Enable
			pipelines.AfterRequest.AddItemToEndOfPipeline ((ctx) => {
				ctx.Response.WithHeader ("Access-Control-Allow-Origin", "*")
						.WithHeader ("Access-Control-Allow-Methods", "POST,GET")
						.WithHeader ("Access-Control-Allow-Headers", "Accept, Origin, Content-type");

			});
		}
	}
}