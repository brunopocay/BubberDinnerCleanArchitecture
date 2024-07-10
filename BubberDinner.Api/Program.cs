using BubberDinner.Api;
using BubberDinner.Application;
using BubberDinner.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services
		.AddPresentation()
		.AddAplication()
		.AddInfrastructure(builder.Configuration);
	// builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
}

var app = builder.Build();
{
	//app.UseMiddleware<ErrorHandlingMiddleware>();
	app.UseExceptionHandler("/error");
	
	// use with MinimalApi
	// app.Map("/error", (HttpContext httpContext) =>
	// {
	// 	Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
	// 	return Results.Problem();
	// });
	app.UseHttpsRedirection();
	app.MapControllers();
	app.Run();
}
