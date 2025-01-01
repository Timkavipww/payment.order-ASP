var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder
    .AddSwagger()
    .AddData()
    .AddApplicationServices()
    .AddIntegrationServices();
           
//builder.Services.AddAuthorization();
            
var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
            
//app.UseHttpsRedirection();

//app.UseAuthorization();

            
            

app.Run();
 