using System;
using System.ComponentModel.DataAnnotations;
using NhaHang.Infrastructure.Models;

namespace NhaHang.Module.Core.Models
{
    public class About : EntityBase
    {
        public About()
        { }

        public About(long id)
        {
            Id = id;
        }

        [StringLength(450)]
        public string Email { get; set; }

        [StringLength(450)]
        public string Hotline { get; set; }

        [StringLength(450)]
        public string Phone { get; set; }

        [StringLength(450)]
        public string Fax { get; set; }

        [StringLength(450)]
        public string Address { get; set; }

        [StringLength(450)]
        public string TaxCode { get; set; }

        [StringLength(450)]
        public string Zalo { get; set; }

        [StringLength(450)]
        public string Facebook { get; set; }

        [StringLength(450)]
        public string Messenger { get; set; }

        [StringLength(450)]
        public string Twiter { get; set; }

        [StringLength(450)]
        public string Instagram { get; set; }

        [StringLength(450)]
        public string Youtube { get; set; }

        [StringLength(450)]
        public string Pinterest { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }

        public string Description { get; set; }

        public string Slogan { get; set; }

        public string Linkedin { get; set; }

        public string Tiktok { get; set; }

        public string Map { get; set; }

        public string GoogleAnalytics { get; set; }

        public string FacebookPixel { get; set; }

        public string GoogleTag { get; set; }

        public string GoogleAds { get; set; }

        public string FacebookFanpage { get; set; }

        public string FacebookLibrary { get; set; }
    }
}
