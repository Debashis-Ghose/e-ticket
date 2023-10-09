using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace eTickets.Controllers
{
	public class ActorsController : Controller
	{
		private readonly IActorsService _service;

		/* public ActorsController(IActorsService service)
		 {
			 _service = service;
		 }*/
		public async Task<IActionResult> Index()
		{
			var data = await _service.GetAll();
			return View(data);
		}
		public IActionResult Create()
		{
			return View();
		}
		/*[HttpPost]
        public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
		{
			if (!ModelState.IsValid) 
            { 
              return View(actor);
            }
            _service.Add(actor);
            return RedirectToAction(nameof(Index));
		}*/
		private readonly IWebHostEnvironment _environment;


		public ActorsController(IWebHostEnvironment _environment, IActorsService service)
		{
			_service = service;
			this._environment = _environment;
		}



		/*[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("FullName,ProfilePictureURL,Bio")] Actor actor)
		{
			if (ModelState.IsValid)
			{
				//Save image to wwwroot/image
				string wwwRootPath = _hostEnvironment.WebRootPath;
				string fileName = Path.GetFileNameWithoutExtension(actor.ProfilePictureURL.FileName);
				string extension = Path.GetExtension(actor.ProfilePictureURL.FileName);
				actor.FullName = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
				string path = Path.Combine(wwwRootPath + "/Image/", fileName);
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					await actor.ProfilePictureURL.CopyToAsync(fileStream);
				}
				//Insert record
				_service.Add(actor);
				await _service.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(actor);
		}*/
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Actor model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					string uniqueFileName = ProcessUploadedFile(model);
					Actor actor = new()
					{
						FullName = model.FullName,
						Bio = model.Bio,
						ProfilePicture = uniqueFileName
					};

					_service.Add(actor);
					await _service.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
			}
			catch (Exception)
			{

				throw;
			}
			return View(model);
		}






		private string ProcessUploadedFile(Actor model)
		{
			string uniqueFileName = null;
			string path = Path.Combine(_environment.WebRootPath, "Uploads");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}

			if (model.ActorPicture != null)
			{
				string uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads");
				uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ActorPicture.FileName;
				string filePath = Path.Combine(uploadsFolder, uniqueFileName);
				using (var fileStream = new FileStream(filePath, FileMode.Create))
				{
					model.ActorPicture.CopyTo(fileStream);
				}
			}

			return uniqueFileName;
		}


	}
}

/*[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Actor model, IFormFile ProfilePictureURL)
{
	if (ProfilePictureURL == null || ProfilePictureURL.Length == 0)
	{
		return Content("File not selected");
	}
	var path = Path.Combine(_hostEnvironment.WebRootPath, "Image", ProfilePictureURL.FileName);
	using (FileStream stream = new FileStream(path, FileMode.Create))
	{
		await ProfilePictureURL.CopyToAsync(stream);
		stream.Close();
	}

	model.FullName = ProfilePictureURL.FileName;

	if (model != null)
	{
		var actor = new Actor
		{
			FullName = model.FullName,
			Bio = model.Bio,



		};

		_service.Add(actor);
		await _service.SaveChangesAsync();
	}
	return RedirectToAction(nameof(Index));

}


}
}
*/