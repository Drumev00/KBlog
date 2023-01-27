using Microsoft.AspNetCore.Http;

namespace Blog.Infrastructure.Data.Services
{
	public class ImagesService
	{
		public async Task<string> ImageToByteArrayAsync(IFormFile file)
		{
			string result = string.Empty;

			if (file.Length > 0)
			{
				using (var ms = new MemoryStream())
				{
					await file.CopyToAsync(ms);
					var fileBytes = ms.ToArray();
					result = Convert.ToBase64String(fileBytes);
				}
			}

			return result;
		}
	}
}
