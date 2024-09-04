using CP4.Interfaces;
using CP4.Services;

namespace CP4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient<IConversionRate, ConversionRate>(client =>
            {
                client.BaseAddress = new Uri("https://api.example.com"); // Replace with your actual API base URL
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.AddHttpClient<IExchangeRateService, ExchangeRateService>(client =>
            {
                client.BaseAddress = new Uri("https://v6.exchangerate-api.com"); // Base URL for ExchangeRateService
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            builder.Services.Configure<ExchangeRateApiSettings>(builder.Configuration.GetSection("ExchangeRateApiSettings"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Currency Converter API",
                    Version = "v1"
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAllOrigins");

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Currency Converter API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            a
