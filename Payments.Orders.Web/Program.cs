using Payments.Orders.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => {
    opt.Filters.Add<ApiExceptionFilter>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpLogging(opt =>
{
    opt.LoggingFields = HttpLoggingFields.RequestBody | HttpLoggingFields.RequestHeaders |
                        HttpLoggingFields.Duration | HttpLoggingFields.RequestPath | HttpLoggingFields.ResponseBody |
                        HttpLoggingFields.ResponseHeaders;
});

builder
    .AddBearerAuthentication()
    .AddOptions()
    .AddSwagger()
    .AddData()
    .AddApplicationServices()
    .AddIntegrationServices()
    .AddBackgroundService();
           
var app = builder.Build();

app.UseHttpLogging();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Orders API v1");
    options.SwaggerEndpoint("/swagger/v2/swagger.json", "Orders API v2");
});
app.MapControllers();
            

app.Run();
 