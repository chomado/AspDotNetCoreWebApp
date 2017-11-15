using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspDotNetCoreWebApp.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace AspDotNetCoreWebApp.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration configuration;
        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Upload()
        {
            var constr = this.configuration["UploaderSettings:StorageAccountConnectionString"];
            var containerName = this.configuration["UploaderSettings:TargetContainer"];
            var strAccount = CloudStorageAccount.Parse(constr);

            var container = strAccount.CreateCloudBlobClient().GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();

            ViewData["URL"] = container.Uri.ToString();

            var policy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Write,
                SharedAccessStartTime = DateTimeOffset.UtcNow.AddMinutes(-5),
                SharedAccessExpiryTime = DateTimeOffset.UtcNow.AddHours(1)
            };
            var sas = container.GetSharedAccessSignature(policy);
            ViewData["SAS"] = sas;
            return View();
        }
    }
}
