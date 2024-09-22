using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Catalog.Services;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Catalog.Areas.Catalog.Components
{
    public class CategoryFeaturedViewComponent : ViewComponent
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMediaService _mediaService;
        private readonly IRepository<Product> _tutorRepository;
        private readonly IContentLocalizationService _contentLocalizationService;

        public CategoryFeaturedViewComponent(IRepository<Category> categoryRepository,
            IMediaService mediaService,
            IRepository<Product> tutorRepository,
            IContentLocalizationService contentLocalizationService)
        {
            _categoryRepository = categoryRepository;
            _mediaService = mediaService;
            _tutorRepository = tutorRepository;
            _contentLocalizationService = contentLocalizationService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long? categoryId, int itemCount = 9)
        {
            IQueryable<Category> query = _categoryRepository.Query()
              .Where(x => x.IsPublished && x.IsFeatured)
                .Include(x => x.ThumbnailImage);
            if (categoryId.HasValue)
            {
                query = query.Where(x => x.Id != categoryId.Value);
            }

            var model = query.Take(itemCount)
                .Select(x => CategoryThumbnail.FromCategory(x)).ToList();

            foreach (var category in model)
            {
                category.Name = category.Name;
                category.ThumbnailUrl = _mediaService.GetThumbnailUrl(category.ThumbnailImage);
            }

            foreach (var category in model)
            {
                int totalProduct = _tutorRepository.Query().Count(x => x.Categories.Any(c => c.CategoryId == category.Id));
                category.TotalProduct = totalProduct;
            }

            return View(this.GetViewPath(), model);
        }
    }
}
