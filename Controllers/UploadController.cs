using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("[controller]")]
[EnableCors("AllowAnyOrigin")]
[AllowAnonymous]
public class UploadController : Controller
{
    private readonly IHostingEnvironment _environment;

    public UploadController(IHostingEnvironment environment)
    {
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(string id)
    {

        var fileName = id;
        var uploads = Path.Combine(_environment.WebRootPath, "Uploads");
        var filePath = Path.Combine(uploads, fileName);
        if (!System.IO.File.Exists(filePath))
        {
            return await Task.Run(() => NotFound());
        }

        return await Task.Run(() => PhysicalFile(filePath, "image/*"));
    }

    [HttpGet("temp/{id}")]
    public async Task<ActionResult> GetTemp(string id)
    {

        var fileName = id;
        var uploads = Path.Combine(_environment.WebRootPath, "Uploads/Temp");
        var filePath = Path.Combine(uploads, fileName);
        if (!System.IO.File.Exists(filePath))
        {
            return await Task.Run(() => NotFound());
        }

        return await Task.Run(() => PhysicalFile(filePath, "image/*"));
    }

    [HttpPost("{id}")]
    public async Task<ActionResult> Post(string id, IFormFile file)
    {
        var uploads = Path.Combine(_environment.WebRootPath, "Uploads");
        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }

        if (file.Length > 0)
        {
            var fileName = id;
            var filePath = Path.Combine(uploads, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return await Task.Run(() => new CreatedResult(new Uri("upload" + id, UriKind.Relative), file));
        }

        return await Task.Run(() => BadRequest());
    }

    [HttpPost("temp/{id}")]
    public async Task<ActionResult> PostTemp(string id, IFormFile file)
    {
        var uploads = Path.Combine(_environment.WebRootPath, "Uploads/Temp");

        if (!Directory.Exists(uploads))
        {
            Directory.CreateDirectory(uploads);
        }

        if (file.Length > 0)
        {
            var fileName = id;
            var filePath = Path.Combine(uploads, fileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return await Task.Run(() => new CreatedResult(new Uri("upload/temp/" + id, UriKind.Relative), file));
        }

        return await Task.Run(() => BadRequest());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var fileName = id;
        var uploads = Path.Combine(_environment.WebRootPath, "Uploads");

        if (fileName == "temp")
        {
            var tempDir = Path.Combine(uploads, "Temp");
            var tmpFiles = Directory.GetFiles(tempDir);
            foreach (var file in tmpFiles)
            {
                System.IO.File.Delete(file);
            }
        }
        else
        {
            var filePath = Path.Combine(uploads, fileName);
            if (!System.IO.File.Exists(filePath))
            {
                return await Task.Run(() => NotFound());
            }
            System.IO.File.Delete(filePath);
        }

        return await Task.Run(() => new NoContentResult());
    }
}