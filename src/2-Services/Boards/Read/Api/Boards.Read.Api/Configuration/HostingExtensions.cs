using Serilog;
using TaskoMask.BuildingBlocks.Infrastructure.MongoDB;
using TaskoMask.BuildingBlocks.Web.MVC.Configuration.Serilog;
using TaskoMask.BuildingBlocks.Web.MVC.Configuration;
using TaskoMask.Services.Boards.Read.Api.Infrastructure.DbContext;
using TaskoMask.Services.Boards.Read.Api.Infrastructure.Mapper;
using TaskoMask.BuildingBlocks.Infrastructure.Extensions;
using TaskoMask.BuildingBlocks.Application.Services;

namespace TaskoMask.Services.Boards.Read.Api.Configuration
{
    internal static class HostingExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.AddCustomSerilog();

            builder.Services.AddBuildingBlocksInfrastructure(builder.Configuration,typeof(Program), typeof(Program));

            builder.Services.AddBuildingBlocksApplication(typeof(Program));

            builder.Services.AddMapper();

            builder.Services.AddMongoDbContext(builder.Configuration);

            builder.Services.AddWebApiPreConfigured(builder.Configuration);

            return builder.Build();
        }



        /// <summary>
        /// 
        /// </summary>
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {

            app.UseSerilogRequestLogging();

            app.UseWebApiPreConfigured(app.Environment);

            app.Services.InitialDatabase();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }



        /// <summary>
        /// 
        /// </summary>
        private static void AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetSection("MongoDB");
            services.AddScoped<BoardReadDbContext>().AddOptions<MongoDbOptions>().Bind(options);
        }

    }
}