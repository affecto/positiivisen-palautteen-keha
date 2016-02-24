using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Affecto.PositiveFeedback.Api;
using Affecto.PositiveFeedback.Store.MongoDb;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
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
            builder.RegisterModule<ApiModule>();
            builder.RegisterModule<StoreModule>();
            IContainer container = builder.Build();

            HttpConfiguration config = new HttpConfiguration
            {
                DependencyResolver = new AutofacWebApiDependencyResolver(container)
            };

            ConfigureWebApi(config);
            app.UseAutofacWebApi(config);
            app.UseCors(CorsOptions.AllowAll);
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