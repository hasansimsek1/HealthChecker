using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace HealthChecker.Presentation.Web.Mvc.Controllers
{
    /// <summary>
    /// This application shows the app logs to the user. With this controller, user navigates to list of log files and sees their details.
    /// </summary>
    [Authorize]
    public class LogController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<LogController> _logger;

        public LogController(IWebHostEnvironment env, ILogger<LogController> logger)
        {
            _env = env;
            _logger = logger;
        }

        /// <summary>
        /// Sends a list page containing list of log files to the client.
        /// </summary>
        public IActionResult Index()
        {
            try
            {
                string projectPath = _env.ContentRootPath;
                var files = Directory.GetFiles(projectPath + "/logs/", "*.txt");
                List<string> fileList = new List<string>();
                foreach (var filePath in files)
                {
                    fileList.Add(Path.GetFileNameWithoutExtension(filePath));
                }

                return View(fileList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Log-Index");
                return RedirectToAction("Exception", "Error");
            }
        }

        /// <summary>
        /// Sends the content of the specified log file to the client.
        /// </summary>
        public IActionResult Detail(string fileName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileName) || !fileName.StartsWith("log") || fileName.Length > 15)
                {
                    return RedirectToAction("Index");
                }

                var filePath = GetFilePath(fileName);
                var log = ReadTheFile(filePath);
                var logAsHtml = ArrangeFileContent(log);
                return View(logAsHtml);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception occurred in Log-Detail. File Name: {fileName}", fileName);
                return RedirectToAction("Exception", "Error");
            }
        }

        /// <summary>
        /// Helper method to generate the path of the file with a little bit security in mind.
        /// </summary>
        private string GetFilePath(string fileName)
        {
            fileName = Regex.Replace(fileName, "[^a-zA-Z0-9]", string.Empty);
            return _env.ContentRootPath + "/logs/" + fileName + ".txt";
        }

        /// <summary>
        /// Helper method to read the content of the log file.
        /// </summary>
        private string ReadTheFile(string filePath)
        {
            using FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using StreamReader streamReader = new StreamReader(fileStream);
            return streamReader.ReadToEnd();
        }

        /// <summary>
        /// Helper method to replace NewLine characters with <br />
        /// </summary>
        private List<string> ArrangeFileContent(string log)
        {
            log = log[log.IndexOf("Application is starting")..];
            log = log.Replace(Environment.NewLine, "<br />");
            return new List<string> { log };
        }
    }
}
