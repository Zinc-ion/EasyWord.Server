using EasyWord.Server.Services.ImplService;
using EasyWord.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IChatCompletionFactory, AzureChatCompletionFactory>();
builder.Services.AddScoped<ISentenceComposingService, SentenceComposingService>();
builder.Services.AddScoped<ITextComposingService,TextComposingService>();
builder.Services.AddScoped<IImage2WordService, Image2WordService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
