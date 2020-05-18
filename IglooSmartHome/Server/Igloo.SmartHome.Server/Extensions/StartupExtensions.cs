using Igloo.SmartHome.Server.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Igloo.SmartHome.Server.Extensions
{
	public static class StartupExtension
	{
		public static void UpdateDatabase(this IApplicationBuilder app)
		{
			using (var serviceScope = app.ApplicationServices
				.GetRequiredService<IServiceScopeFactory>()
				.CreateScope())
			{
				using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
				{
					context.Database.Migrate();
				}
			}
		}
	}
}
