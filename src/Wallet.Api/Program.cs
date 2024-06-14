using System.Text.Json.Serialization;
using Wallet.Api.Categories;
using Wallet.Api.Profiles;
using Wallet.Api.Transactions;
using Wallet.Api.Users;
using Wallet.Application;
using Wallet.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

PersistenceModule.Configure(builder.Services, builder.Configuration);
ApplicationModule.Configure(builder.Services);

var app = builder.Build();
app.UsePathBase("/api");

UserEndpoints.Map(app);
ProfileEndpoints.Map(app);
TransactionEndpoints.Map(app);
CategoryEndpoints.Map(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();
