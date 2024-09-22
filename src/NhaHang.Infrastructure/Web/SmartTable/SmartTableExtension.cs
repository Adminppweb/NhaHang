using AutoMapper.Internal;
using System;
using System.Linq;
using System.Linq.Expressions;
using NhaHang.Infrastructure.Extensions;

namespace NhaHang.Infrastructure.Web.SmartTable
{
    public static class SmartTableExtension
    {
		public static SmartPaginationTableResult<TModel> ToSmartPaginationTableResult2<TModel>(this IQueryable<TModel> query, SmartTableParam2 param)
		{
			var totalRecord = query.Count();
			////var querySortAndPagingation = query.AppendSortAndPagingation(param);
			////var items = querySortAndPagingation.ToList();

			return new SmartPaginationTableResult<TModel>
			{
				Items = query,
				///Pagination = param.Pagination,
				///SmartTableParam = param
			};
		}

		public static SmartPaginationTableResult<TModel> ToSmartPaginationTableResult<TModel>(this IQueryable<TModel> query, SmartTableParam param)
		{
			var totalRecord = query.Count();
			var querySortAndPagingation = query.AppendSortAndPagingation(param);
			var items = querySortAndPagingation.ToList();

			return new SmartPaginationTableResult<TModel>
			{
				Items = items,
				Pagination = param.Pagination,
				///SmartTableParam = param
			};
		}
        
		public static SmartTableResult<TModel> ToSmartTableResult<TModel>(this IQueryable<TModel> query, SmartTableParam param)
        {
            var totalRecord = query.Count();
            var items = query.AppendSortAndPagingation(param).ToList();

            return new SmartTableResult<TModel>
            {
                Items = items,
                TotalRecord = totalRecord,
                NumberOfPages = (int)Math.Ceiling((double)totalRecord / param.Pagination.Number)
            };
        }

        public static SmartTableResult<TResult> ToSmartTableResult<TModel, TResult>(this IQueryable<TModel> query, SmartTableParam param, Expression<Func<TModel, TResult>> selector)
        {
            var totalRecord = query.Count();
            query = query.AppendSortAndPagingation(param);
            var items = query.Select(selector).ToList();

            return new SmartTableResult<TResult>
            {
                Items = items,
                TotalRecord = totalRecord,
                NumberOfPages = (int)Math.Ceiling((double)totalRecord / param.Pagination.Number)
            };
        }

        // ToLits() before Select() to loaded related many-to-many entity
        public static SmartTableResult<TResult> ToSmartTableResultNoProjection<TModel, TResult>(this IQueryable<TModel> query, SmartTableParam param, Expression<Func<TModel, TResult>> selector)
        {
            var totalRecord = query.Count();
            var items = query.AppendSortAndPagingation(param).ToList();

            return new SmartTableResult<TResult>
            {
                Items = items.AsQueryable().Select(selector),
                TotalRecord = totalRecord,
                NumberOfPages = (int)Math.Ceiling((double)totalRecord / param.Pagination.Number)
            };
        }

		private static IQueryable<TModel> AppendSortAndPagingation<TModel>(this IQueryable<TModel> query, SmartTableParam param)
		{
			try
			{

				if (query.Count() == 0 || param == null)
				{
					return query;
				}

				if (param.Filter != null)
				{
					////query.Where(x => x.GetType().GetMemberValue(param.Filters.Name) == param.Filters.Value);
					//var allObject = query.Select(x => x.GetType().GetProperties()).FirstOrDefault();
					//var Object = query.Select(x => x.GetType().GetFieldOrProperty(param.Filters.Name) ).FirstOrDefault();

					///query = query.Where(x => x.GetType().GetFieldOrProperty(param.Filters.Name) != null && x.GetType().GetMemberValue);
					///
					query = query.WhereByName(param.Filter.Name, "Contains", "");
				}

				param.Pagination.TotalItemCount = query.Count();
				if (!string.IsNullOrWhiteSpace(param.Sort.Predicate))
				{
					query = query.OrderByName(param.Sort.Predicate, param.Sort.Reverse);
				}
				else
				{
					query = query.OrderByName("Id", true);
				}




				query = query
					.Skip(param.Pagination.Start)
					.Take(param.Pagination.Number);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}

			////Set Pagination			
			param.Pagination = param.Pagination.SetPagination();
			return query;
		}

		private static IEnumerable<T> GetPage<T>(object items = null, Pagination pagination = null)
		{
			pagination.TotalItemCount = (items as IEnumerable<T>).Count();
			////SetPagination();
			return (items as IEnumerable<T>).Skip(pagination.PageSize * (pagination.CurentPage - 1)).Take(pagination.PageSize).ToList();
		}

		private static Pagination SetPagination(this Pagination pagination)
		{
			if (pagination == null)
			{
				return pagination;
			}

			//PageSize
			if (pagination.PageSize == 0)
			{
				pagination.PageSize = 8;
			}

			//NumberOfPages
			pagination.NumberOfPages = (pagination.TotalItemCount + pagination.PageSize - 1) / pagination.PageSize;
			////pagination.NumberOfPages = (int)Math.Ceiling((double)pagination.TotalItemCount / pagination.Number);

			//CurentPage
			if (pagination.CurentPage > pagination.NumberOfPages)
				pagination.CurentPage = pagination.NumberOfPages;
			else if (pagination.CurentPage < 1)
				pagination.CurentPage = 1;

			//Số lượng page trong pagenation
			var pagePagition = (int)Math.Ceiling((float)pagination.NumberOfPages / pagination.PageSizePagition);

			//Set Start/End
			if (pagination.PageSizePagition < pagination.NumberOfPages)
			{
				pagination.End = pagination.Start * pagination.PageSizePagition;
				for (var i = 1; i <= pagePagition; i++)
				{
					if (((i - 1) * pagination.PageSizePagition + 1) <= pagination.CurentPage && pagination.CurentPage <= pagination.NumberOfPages)
					{
						if (i == 1)
						{
							pagination.Start = 1;
							pagination.End = pagination.PageSizePagition;
						}
						else if (i == pagePagition)
						{
							pagination.Start = (i - 1) * pagination.PageSizePagition + 1;
							pagination.End = pagination.NumberOfPages;
						}
						else
						{
							pagination.Start = (i - 1) * pagination.PageSizePagition + 1;
							pagination.End = pagination.Start + pagination.PageSizePagition - 1;
						}
					}

				}
			}
			else
			{
				pagination.Start = 1;
				pagination.End = pagination.NumberOfPages;
			}

			return pagination;
		}
	}
}
