using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using System.Collections.Generic;
using System;
using System.IO;

using NhaHang.Module.ElFinder.Drivers.FileSystem;
using Microsoft.AspNetCore.Http;
using NhaHang.Module.ElFinder.Services;

namespace NhaHang.Module.ElFinder.Helpers
{
    public static class ElFinderHelper
    {
        public static Connector GetConnector(string scheme, HostString hostString)
        {
            var driver = new FileSystemDriver();

            string absoluteUrl = UriHelper.BuildAbsolute(scheme, hostString); //// Request.Scheme, Request.Host
            var uri = new Uri(absoluteUrl);

            var root = new RootVolume(
                PathHelper.MapPath("~/Files"),
                $"{uri.Scheme}://{uri.Authority}/Files/",
                $"{uri.Scheme}://{uri.Authority}/el-finder/file-system/thumb/")
            {
                //IsReadOnly = !User.IsInRole("Administrators")
                IsReadOnly = false, // Can be readonly according to user's membership permission
                IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                Alias = "Files", // Beautiful name given to the root/home folder
                //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                //AccessControlAttributes = new HashSet<NamedAccessControlAttributeSet>()
                //{
                //    new NamedAccessControlAttributeSet(PathHelper.MapPath("~/Files/readonly.txt"))
                //    {
                //        Write = false,
                //        Locked = true
                //    },
                //    new NamedAccessControlAttributeSet(PathHelper.MapPath("~/Files/Prohibited"))
                //    {
                //        Read = false,
                //        Write = false,
                //        Locked = true
                //    },
                //    new NamedAccessControlAttributeSet(PathHelper.MapPath("~/Files/Parent/Children"))
                //    {
                //        Read = true,
                //        Write = false,
                //        Locked = true
                //    },
                //    new NamedAccessControlAttributeSet(PathHelper.MapPath("~/Files"))
                //    {
                //        Read = true,
                //        Write = true,
                //        Locked = false
                //    }
                //},
                // Upload file type constraints
                //UploadAllow = new[] { "image", "text" },
                //UploadDeny = new[] { "text/csv" },
                //UploadOrder = new[] { "allow", "deny" }
            };

            driver.AddRoot(root);

            return new Connector(driver)
            {
                // This allows support for the "onlyMimes" option on the client.
                MimeDetect = MimeDetectOption.Internal
            };
        }

        public static Connector GetConnector(string scheme, HostString hostString, string files = "Files", string thumb = "thumb"
            ////, IElFinderServices elFinderServices = null
            )
        {
            var driver = new FileSystemDriver();

            string absoluteUrl = UriHelper.BuildAbsolute(scheme, hostString); //// Request.Scheme, Request.Host
            var uri = new Uri(absoluteUrl);

            var root = new RootVolume(
                PathHelper.MapPath($"~/{files}"),
                $"{uri.Scheme}://{uri.Authority}/{files}/",
                $"{uri.Scheme}://{uri.Authority}/el-finder/file-system/{thumb}/")
            {
                //IsReadOnly = !User.IsInRole("Administrators")
                IsReadOnly = false, // Can be readonly according to user's membership permission
                IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                Alias = files, // Beautiful name given to the root/home folder
                //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                AccessControlAttributes = new HashSet<NamedAccessControlAttributeSet>()
                {
                    new NamedAccessControlAttributeSet(PathHelper.MapPath($"~/{files}/readonly.txt"))
                    {
                        Write = false,
                        Locked = true
                    },
                    new NamedAccessControlAttributeSet(PathHelper.MapPath($"~/{files}/Prohibited"))
                    {
                        Read = false,
                        Write = false,
                        Locked = true
                    },
                    new NamedAccessControlAttributeSet(PathHelper.MapPath($"~/{files}/Parent/Children"))
                    {
                        Read = true,
                        Write = false,
                        Locked = true
                    },
                    new NamedAccessControlAttributeSet(PathHelper.MapPath($"~/{files}"))
                    {
                        Read = true,
                        Write = true,
                        Locked = false,
                    }
                },
                // Upload file type constraints
                //UploadAllow = new[] { "image", "text" },
                //UploadDeny = new[] { "text/csv" },
                //UploadOrder = new[] { "allow", "deny" }
            };

            driver.AddRoot(root);

            return new Connector(driver)
            {
                // This allows support for the "onlyMimes" option on the client.
                ////MimeDetect = MimeDetectOption.Internal
            };
        }

        public static Connector GetConnectorV1(string scheme, HostString hostString, IElFinderServices elFinderServices = null, string files = "Files", string thumb = "thumb"
       )
        {
            var driver = new FileSystemDriver();

            string absoluteUrl = UriHelper.BuildAbsolute(scheme, hostString); //// Request.Scheme, Request.Host
            var uri = new Uri(absoluteUrl);

            var root = new RootVolume(
                PathHelper.MapPath($"~/{files}"),
                $"{uri.Scheme}://{uri.Authority}/{files}/",
                $"{uri.Scheme}://{uri.Authority}/el-finder/file-system/{thumb}/")
            {
                //IsReadOnly = !User.IsInRole("Administrators")
                IsReadOnly = false, // Can be readonly according to user's membership permission
                IsLocked = false, // If locked, files and directories cannot be deleted, renamed or moved
                Alias = files, // Beautiful name given to the root/home folder
                //MaxUploadSizeInKb = 2048, // Limit imposed to user uploaded file <= 2048 KB
                //AccessControlAttributes = new HashSet<NamedAccessControlAttributeSet>()
                //{
                //    new NamedAccessControlAttributeSet(PathHelper.MapPath($"~/{files}/readonly.txt"))
                //    {
                //        Write = false,
                //        Locked = true
                //    },
                //    new NamedAccessControlAttributeSet(PathHelper.MapPath($"~/{files}/Prohibited"))
                //    {
                //        Read = false,
                //        Write = false,
                //        Locked = true
                //    },
                //    new NamedAccessControlAttributeSet(PathHelper.MapPath($"~/{files}/Parent/Children"))
                //    {
                //        Read = true,
                //        Write = false,
                //        Locked = true
                //    },
                //    new NamedAccessControlAttributeSet(PathHelper.MapPath($"~/{files}"))
                //    {
                //        Read = true,
                //        Write = true,
                //        Locked = false,
                //    }
                //},
                // Upload file type constraints
                //UploadAllow = new[] { "image", "text" },
                //UploadDeny = new[] { "text/csv" },
                //UploadOrder = new[] { "allow", "deny" }
            };

            driver.AddRoot(root);

            return new Connector(driver, elFinderServices)
            {
                // This allows support for the "onlyMimes" option on the client.
                MimeDetect = MimeDetectOption.Internal
            };
        }

        /////// Try catch khi folder error
        public static string GetHash(string path)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(path));
        }

        public static string GetMimeType(string path)
        {
            // Logic để lấy kiểu MIME của file
            // Ví dụ: dùng System.Web.MimeMapping.GetMimeMapping
            return "application/octet-stream"; // MIME type mặc định nếu không xác định được
        }
    }
}