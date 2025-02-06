using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using OnlineShop.Application;
using OnlineShop.Persistence;
using OnlineShop.WebApi.Configurations;
using OnlineShop.WebApi.DocumentFilters;
using OnlineShop.WebApi.Middleware;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace OnlineShop.WebApi;

public partial class Startup(IConfiguration configuration, IWebHostEnvironment env)
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddPersistence(configuration, env.EnvironmentName);
        services.AddControllers();
        services.AddSwaggerGen();

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(options =>
        {
            options.DocumentFilter<RemoveDuplicateEndpointsDocumentFilter>();
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-Version"),
                new MediaTypeApiVersionReader("X-Version"));
        })
       .AddMvc()
       .AddApiExplorer(options =>
       {
           options.GroupNameFormat = "'v'VVV";
           options.SubstituteApiVersionInUrl = true;
           options.AddApiVersionParametersWhenVersionNeutral = true;
       });

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(config =>
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                config.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });

        app.UseCustomExceptionHandler();
        app.UseRouting();
        app.UseHttpsRedirection();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}