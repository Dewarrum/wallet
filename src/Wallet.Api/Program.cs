using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.Tokens;
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
builder
    .Services.AddAuthentication()
    .AddJwtBearer(options =>
    {
        var key = ECDsa.Create(
            new ECParameters
            {
                Curve = ECCurve.NamedCurves.nistP384,
                Q = new ECPoint
                {
                    X = Base64UrlEncoder.DecodeBytes(builder.Configuration["Logto:SigningKey:X"]),
                    Y = Base64UrlEncoder.DecodeBytes(builder.Configuration["Logto:SigningKey:Y"])
                }
            }
        );

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["Logto:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Logto:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new ECDsaSecurityKey(key)
        };
        options.IncludeErrorDetails = true;
    });
builder.Services.AddAuthorization();

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
app.UseAuthentication();
app.UseAuthorization();

app.Run();
