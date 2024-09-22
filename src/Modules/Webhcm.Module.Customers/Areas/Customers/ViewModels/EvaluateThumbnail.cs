using System;
using NhaHang.Module.Customers.Models;
using NhaHang.Module.Core.Models;
using NhaHang.Module.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace NhaHang.Module.Customers.Areas.Customers.ViewModels
{
    public class EvaluateThumbnail
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Position { get; set; }

        public string Company { get; set; }

        public string Content { get; set; }

        public string IdYoutube { get; set; }

        public string SourceYou { get; set; }

        public string LinkImage { get; set; }

        public Media ThumbnailImage { get; set; }

        public string ThumbnailUrl { get; set; }

        public static EvaluateThumbnail FromEvaluate(Evaluate evaluate)
        {
            var evaluateThumbnail = new EvaluateThumbnail
            {
                Id = evaluate.Id,
                Name = evaluate.Name,
                Slug = evaluate.Slug,
                Position = evaluate.Position,
                Company = evaluate.Company,
                Content = evaluate.Content,
                ThumbnailImage = evaluate.ThumbnailImage,
                IdYoutube = evaluate.IdYoutube,
                SourceYou = evaluate.SourceYou,
                LinkImage = evaluate.LinkImage
            };

            return evaluateThumbnail;
        }
    }
}
