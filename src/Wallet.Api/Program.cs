using Wallet.Api;
using Wallet.Api.Accounts;
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

builder.ConfigureAuthentication();
builder.ConfigureAuthorization();
builder.ConfigureSessions();
PersistenceModule.Configure(builder.Services, builder.Configuration);
ApplicationModule.Configure(builder.Services);
ApiModule.Configure(builder.Services);

var app = builder.Build();

var userGroup = app.MapGroup("account");
userGroup.MapGet("signin", AccountEndpoints.SignIn);
userGroup.MapGet("signin-google", AccountEndpoints.GoogleSignIn);
userGroup.MapGet("signup", AccountEndpoints.SignUp);
userGroup.MapGet("signup-google", AccountEndpoints.GoogleSignUp);
userGroup.MapGet("my-info", AccountEndpoints.MyInfo);

var transactionGroup = app.MapGroup("transactions").RequireAuthorization();
transactionGroup.MapGet("{id:guid}", TransactionEndpoints.Get);

var profileGroup = app.MapGroup("profiles").RequireAuthorization();
profileGroup.MapPost("", ProfileEndpoints.Create);
profileGroup.MapGet("", ProfileEndpoints.GetForUser);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseSession();

app.Run();
