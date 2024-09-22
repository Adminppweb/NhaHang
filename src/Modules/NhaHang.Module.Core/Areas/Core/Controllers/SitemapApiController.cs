using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using NhaHang.Infrastructure.Data;
using NhaHang.Module.Core.Models;

namespace NhaHang.Module.Core.Areas.Core.Controllers
{
    [Route("api/sitemaps")]
    public class SitemapApiController : Controller
    {
        private readonly IRepository<Entity> _entityRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public SitemapApiController(IRepository<Entity> entityRepository, IWebHostEnvironment hostingEnvironment)
        {
            _entityRepository = entityRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult GenerateSitemapXml()
        {
            var entities = _entityRepository.Query().Where(x => x.EntityType.IsMenuable);

            var xmlString = GenerateSitemapXmlString(entities);
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "sitemap.xml");

            SaveSitemapToFile(xmlString, filePath);

            return Ok("Sitemap.xml Được tạo và lưu thành công.");
        }

        private string GenerateSitemapXmlString(IEnumerable<Entity> entities)
        {
            var settings = new XmlWriterSettings
            {
                Encoding = Encoding.UTF8,
                Indent = true
            };

            using (var stream = new StringWriter())
            {
                using (var writer = XmlWriter.Create(stream, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
                    writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                    writer.WriteAttributeString("xsi", "schemaLocation", null, "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd");

                    foreach (var entity in entities)
                    {
                        writer.WriteStartElement("url");
                        writer.WriteElementString("loc", $"https://xenangdienthuanphat.com/{entity.Slug}");
                        writer.WriteElementString("lastmod", DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK"));
                        writer.WriteElementString("priority", "1.00");
                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }

                return stream.ToString();
            }
        }


        private void SaveSitemapToFile(string xmlString, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (var writer = new StreamWriter(fileStream, Encoding.UTF8))
                {
                    writer.Write(xmlString);
                }
            }
        }
    }
}
