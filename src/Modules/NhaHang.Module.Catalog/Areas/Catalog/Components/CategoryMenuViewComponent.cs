using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Catalog.Areas.Catalog.Components
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMediaService _mediaService;
        private readonly IContentLocalizationService _contentLocalizationService;

        public CategoryMenuViewComponent(IRepository<Category> categoryRepository, IMediaService mediaService,
            IContentLocalizationService contentLocalizationService)
        {
            _categoryRepository = categoryRepository;
            _mediaService = mediaService;
            _contentLocalizationService = contentLocalizationService;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _categoryRepository.Query()
                .Where(x => !x.IsDeleted && x.IncludeInMenu)
                .Include(x => x.ThumbnailImage).ToList();

            var categoryMenuItems = new List<CategoryMenuItem>();
            var topCategories = categories.Where(x => !x.ParentId.HasValue).OrderBy(x => x.DisplayOrder);
            foreach (var category in topCategories)
            {
                var categoryMenuItem = Map(category);
                categoryMenuItems.Add(categoryMenuItem);
            }

            return View(this.GetViewPath(), categoryMenuItems);
        }

        private CategoryMenuItem Map(Category category)
        {
            var categoryMenuItem = new CategoryMenuItem
            {
                Id = category.Id,
                Name = _contentLocalizationService.GetLocalizedProperty(category, nameof(category.Name), category.Name),
                Slug = category.Slug,
                ThumbnailUrl = _mediaService.GetThumbnailUrl(category.ThumbnailImage)

        };

            var childCategories = category.Children;
            foreach (var childCategory in childCategories.OrderBy(x => x.DisplayOrder))
            {
                var childCategoryMenuItem = Map(childCategory);
                categoryMenuItem.AddChildItem(childCategoryMenuItem);
            }

            return categoryMenuItem;
        }
    }
}
