using Igloo.SmartHome.Server.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Igloo.SmartHome.Server.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{

		}

		public DbSet<User> Accounts { get; set; }
		public DbSet<Device> Devices { get; set; }
		public DbSet<DeviceSubscription> DeviceSubscriptions { get; set; }
	}
}
