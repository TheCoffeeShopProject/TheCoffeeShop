using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using TheCoffeeCatRepository.IRepository;
using TheCoffeeCatRepository.Repository;
using TheCoffeeCatService.IServices;
using TheCoffeeCatService.Services;
using TheCoffeeCatStore.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ApplicationMapper));

builder.Services.AddScoped<ICatRepo, CatRepo>();
builder.Services.AddScoped<ICatServices, CatServices>();
builder.Services.AddScoped<ICoffeeShopRepo, CoffeeShopRepo>();
builder.Services.AddScoped<ICoffeeShopServices, CoffeeShopServices>();
builder.Services.AddScoped<IStaffRepo, StaffRepo>();
builder.Services.AddScoped<IStaffServices, StaffServices>();
builder.Services.AddScoped<IDrinkRepo, DrinkRepo>();
builder.Services.AddScoped<IDrinkServices, DrinkServices>();

builder.Services.AddScoped<IAccountRepo, AccountRepo>();
builder.Services.AddScoped<IAccountServices, AccountServices>();

//Jwt
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
builder.Services.AddScoped<ICatProductRepo,CatProductRepo>();
builder.Services.AddScoped<ICatProductServices,CatProductServices>();
builder.Services.AddScoped(_=> new BlobServiceClient(builder.Configuration.GetConnectionString("AzureBlobStorage")));




builder.Services.AddMvc()
                .AddNewtonsoftJson(
                        options => { options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; }
        );
//builder.Services.AddAutoMapper


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();




app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

