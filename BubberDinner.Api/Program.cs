using BubberDinner.Api.Errors;
using BubberDinner.Application;
using BubberDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services.AddAplication().AddInfrastructure(builder.Configuration);
	//builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
	builder.Services.AddControllers();
	builder.Services.AddSingleton<ProblemDetailsFactory, BubberDinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
	//app.UseMiddleware<ErrorHandlingMiddleware>();
	app.UseExceptionHandler("/error");
	
	//use with MinimalApi
	// app.Map("/error", (HttpContext httpContext) =>
	// {
	// 	Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
	// 	return Results.Problem();
	// });
	app.UseHttpsRedirection();
	app.MapControllers();
	app.Run();
}
