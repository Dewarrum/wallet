using Wallet.Api;
using Wallet.Api.Auth;
using Wallet.Api.Categories;
using Wallet.Api.Profiles;
using Wallet.Api.Transactions;
using Wallet.Application;
using Wallet.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Valkey");
    options.InstanceName = "Sessions";
});

if (builder.Environment.IsDevelopment())
{
    builder
        .Services.AddReverseProxy()
        .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
}

builder.ConfigureAuthentication();
builder.ConfigureAuthorization();
builder.ConfigureSessions();
PersistenceModule.Configure(builder.Services, builder.Configuration);
ApplicationModule.Configure(builder.Services);
ApiModule.Configure(builder.Services);

var app = builder.Build();
app.UsePathBase("/api");

AuthEndpoints.Map(app);
ProfileEndpoints.Map(app);
TransactionEndpoints.Map(app);
CategoryEndpoints.Map(app);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseSession();
app.MapReverseProxy();

app.Run();
