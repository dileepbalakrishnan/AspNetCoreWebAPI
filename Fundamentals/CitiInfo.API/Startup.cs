using CitiInfo.API.Entities;
using CitiInfo.API.Models;
using CitiInfo.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CitiInfo.API
{
    public class Startup
    {
        public static IConfiguration Configuration;
        private const string ConnectionString = "connectionStrings:cityInfoDbConnectionString";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //var configurationBuilder = new ConfigurationBuilder(); // For ASP.NET Core 1.0
            //configurationBuilder.SetBasePath(environment.ContentRootPath);
            //configurationBuilder.AddJsonFile("appSettings.json");
            //Configuration = configurationBuilder.Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // This code adds the XML output formatter to the list of output formatters so that the responses can
            // be formatted as XML if clients ask so by setting the accept header in their http request to application/xml
            services.AddMvc().AddMvcOptions(o =>
            {
                o.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
#if DEBUG
            services.AddTransient<IEmailService, IntranetEmailService>();
#else
            services.AddTransient<IEmailService, InternetEmailService>();
#endif
            // This code can disable the JSON serializer from changing the casing of properties (id instead of Id, for example)
            //services.AddMvc().AddJsonOptions(o =>
            //{
            //    var resolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
            //    resolver.NamingStrategy = null;
            //});
            services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(Configuration[ConnectionString]));
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CityInfoContext cityInfoContext)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseMvc();
            cityInfoContext.EnsureSeedDataForContext();
            app.UseStatusCodePages();
            AutoMapper.Mapper.Initialize( cfg =>
            {
                cfg.CreateMap<City, CitiWithoutPointsOfInterestDto>();
                cfg.CreateMap<City, CitiDto>();
                cfg.CreateMap<PointsOfInterest, PointOfInterestDto>();
                cfg.CreateMap<PointOfInterestDto, PointsOfInterest>();
                cfg.CreateMap<PointOfInterestForCreationDto, PointsOfInterest>();
                cfg.CreateMap<PointOfInterestForUpdateDto, PointsOfInterest>();
                cfg.CreateMap<PointsOfInterest, PointOfInterestForUpdateDto>();
            });
        }
    }
}