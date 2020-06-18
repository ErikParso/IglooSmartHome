using Igloo.SmartHome.Server.Data;
using Igloo.SmartHome.Server.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Igloo.SmartHome.Server.Controller
{
	[Authorize]
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
		/// Registers user in database and gets user info.
		/// </summary>
		[HttpGet]
		public async Task<User> Get()
		{
			var user = new User
			{
				Name = "tets",
				Provider = "google",
				Sid = Guid.NewGuid().ToString()
			};
			await dbContext.Users.AddAsync(user);
			await dbContext.SaveChangesAsync();
			return await Task.FromResult(user);
		}
	}
}
