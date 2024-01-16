using Cms.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Cms.Web.Controllers
{
    [Route("/editor")]
    [ApiController]
    public class CmsApiController : ControllerBase
    {

        [HttpPost]
        [Route("uploadimage")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file is null || file.Length == 0)
            {
                return BadRequest("Nieprawidłowy plik");
            }

            var serverUploadDirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var filePath = Path.Combine(serverUploadDirPath, file.FileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            var imageUrl = $"{Request.Scheme}://{Request.Host}/uploads/{file.FileName}";
            var responseJsonContent = JsonConvert.SerializeObject(new {
                location = imageUrl
            }); 

            return Content(responseJsonContent, "application/json");
        }

        [HttpGet]
        [Route("getuploaded")]
        public async Task<string> GetUploadedImages()
        {
            var serverUploadDirPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
            var fielsList = Directory.EnumerateFiles(serverUploadDirPath, "*" , SearchOption.AllDirectories);
            var responseListFiles = new List<object>();

            foreach(var file in fielsList)
            {
                responseListFiles.Add(new {title = Path.GetFileName(file), value = $"{Request.Scheme}://{Request.Host}/uploads/{Path.GetFileName(file)}"});
            }

            var json = JsonConvert.SerializeObject(responseListFiles);

            return json;
        }
    }
}