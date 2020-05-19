using Igloo.SmartHome.Server.Data;
using Igloo.SmartHome.Server.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Igloo.SmartHome.Server.Controller
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly ApplicationDbContext dbContext;

		public UserController(ApplicationDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// Registers user.
		/// </summary>
		/// <param name="name">User name.</param>
		[HttpPost("register/{name}")]
		public async Task RegisterUser(string name)
		{
			await dbContext.Users.AddAsync(new User
			{
				Name = name,
				Provider = "google",
				Sid = Guid.NewGuid().ToString()
			});
			await dbContext.SaveChangesAsync();
		}
	}
}
