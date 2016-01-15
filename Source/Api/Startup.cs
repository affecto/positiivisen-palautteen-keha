using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Affecto.PositiveFeedback.Api;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Newtonsoft.Json.Serialization;
using Owin;

[assembly: OwinStartup(typeof(Startup))]
namespace Affecto.PositiveFeedback.Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            //builder.RegisterType<Log4NetLoggerFactory>().As<ILoggerFactory>();

            IContainer container = builder.Build();

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };

            ConfigureWebApi(config);
            app.UseAutofacWebApi(config);
#if(DEBUG)
            app.UseCors(CorsOptions.AllowAll);
#endif

            app.UseWebApi(config);

            config.EnsureInitialized();
        }

        private static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            JsonMediaTypeFormatter jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}