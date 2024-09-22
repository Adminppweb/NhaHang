using AutoMapper;
using NhaHang.Module.Catalog.Areas.Catalog.ViewModels;
using NhaHang.Module.Catalog.Models;
using NhaHang.Module.Core.Areas.Core.ViewModels;

namespace NhaHang.WebHost.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Product, ProductVm>();
            CreateMap<Product, ProductThumbnail>();
            //CreateMap<NewsItem, NewsItemVm>();
            //CreateMap<NewsCategory, NewsCategoryVm>();
            //CreateMap<Comment, CommentVm>();
            //CreateMap<Comment, CommentItem>();
            //CreateMap<CartItem, CartItemVm>();
            //CreateMap<Cart, CartVm>();
            CreateMap<Category, FilterCategory>();
            //CreateMap<Page, PageVm>();
            //CreateMap<Page, PageForm>();
            CreateMap<WidgetInstanceViewModel, WidgetInstanceViewModel>();
        }
    }
}