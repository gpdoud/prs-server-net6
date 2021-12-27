using Microsoft.EntityFrameworkCore;

using prs_server_net6.Models;

namespace prs_server_net6.Middleware {

    public class ApiKeyMiddleware {
        private readonly RequestDelegate _next;
        private const string APIKEYNAME = "x-prs-api-key";

        public ApiKeyMiddleware(RequestDelegate next) {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, AppDbContext db) {
            if (!context.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey)) {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Api Key was not provided. (Using ApiKeyMiddleware) ");
                return;
            }
            var user = await db.Users.SingleOrDefaultAsync(x => x.Apikey!.Equals(extractedApiKey));

            //var appSettings = context.RequestServices.GetRequiredService<IConfiguration>();

            //var apiKey = appSettings.GetValue<string>(APIKEYNAME);

            if (user == null) {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized client. (Using ApiKeyMiddleware)");
                return;
            }

            await _next(context);
        }
    }
}
