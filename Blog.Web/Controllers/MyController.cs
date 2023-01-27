using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class MyController : Controller
	{
	}
}
