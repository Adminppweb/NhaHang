using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NhaHang.Infrastructure.Data;
using NhaHang.Infrastructure.Web;
using NhaHang.Infrastructure.Web.SmartTable;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Catalog.Services;
using NhaHang.Module.Core.Services;

namespace NhaHang.Module.Catalog.Areas.Catalog.Components
{
    public class CategoryProductViewComponent : ViewComponent
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IMediaService _mediaService;
        private readonly IProductPricingService _productPricingService;
        private readonly IContentLocalizationService _contentLocalizationService;
        private readonly ICategoryService _categoryService;

        public CategoryProductViewComponent(IRepository<Product> productRepository,
            IMediaService mediaService,
            IProductPricingService productPricingService,
            IContentLocalizationService contentLocalizationService,
            ICategoryService categoryService)
        {
            _productRepository = productRepository;
            _mediaService = mediaService;
            _productPricingService = productPricingService;
            _contentLocalizationService = contentLocalizationService;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync(SmartTableParam model)
        {
            var result = new SmartTableResultV2();
            try
            {

                if (model == null)
                {
                    model = new SmartTableParam();
                }

                List<Filter> shopCartFilterVm = new List<Filter>();
                if (model.FilterCategorys.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterCategorys);
                }
                if (model.FilterRatings.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterRatings);
                }
                if (model.FilterRices.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterRices);
                }
                if (model.FilterAttributes.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterAttributes);
                }

                if (model.FilterBrands.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterBrands);
                }

                if (model.FilterColors.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterColors);
                }

                if (model.FilterColors.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterColors);
                }

                if (model.FilterWarrantyPeriods.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterWarrantyPeriods);
                }

                if (model.FilterAttributeGroups.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterAttributeGroups);
                }

                if (model.FilterSizes.Count > 0)
                {
                    shopCartFilterVm.AddRange(model.FilterSizes);
                }

                model.Pagination.PaginationId = "CategoryProduct";
                var productFilter = (await _categoryService.GetProductFilter(shopCartFilterVm)).OrderBy(x => x.Id).ToList();
                if (productFilter != null)
                {
                    model.Pagination.PageSize = 16;
                    //////Sort
                    if (!string.IsNullOrEmpty(model.Sort.Predicate))
                    {
                        if (model.Sort.Predicate == "name" && model.Sort.Reverse)
                        {
                            productFilter = productFilter.OrderByDescending(x => x.Name).ToList();
                        }
                        else if (model.Sort.Predicate == "name")
                        {
                            productFilter = productFilter.OrderBy(x => x.Name).ToList();
                        }

                        if (model.Sort.Predicate == "price" && model.Sort.Reverse)
                        {
                            productFilter = productFilter.OrderByDescending(x => x.Price).ToList();
                        }
                        else if (model.Sort.Predicate == "price")
                        {
                            productFilter = productFilter.OrderBy(x => x.Price).ToList();
                        }

                        if (model.Sort.Predicate == "created" && model.Sort.Reverse)
                        {
                            productFilter = productFilter.OrderByDescending(x => x.CreatedOn).ToList();
                        }
                        else if (model.Sort.Predicate == "created")
                        {
                            productFilter = productFilter.OrderBy(x => x.CreatedOn).ToList();
                        }
                    }
                    var shopCartDisplay = model.Pagination.GetPage<ProductThumbnail>(productFilter);
                    result.Items = shopCartDisplay;
                    result.SmartTableParam = model;
                    result.TotalRecord = model.Pagination.TotalItemCount;
                    result.NumberOfPages = model.Pagination.NumberOfPages;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = new SmartTableResultV2();
            }
            return View(this.GetViewPath(), result);
        }
    }
}
