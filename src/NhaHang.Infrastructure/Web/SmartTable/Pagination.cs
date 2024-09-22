using System;
using System.Collections.Generic;
using System.Linq;

namespace NhaHang.Infrastructure.Web.SmartTable
{
    public class Pagination
    {

        public int CurentPage { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int Index { get; set; } = 1;
        public int Start { get; set; } = 1;
        public int End { get; set; } = 1;
        public int TotalItemCount { get; set; } = 0;
        public int Number { get; set; } = 4;
        public int NumberOfPages { get; set; } = 0;
        public int PageSizePagition { get; set; } = 4;
        public string PaginationId { get; set; }

        //IEnumerable<T>
        public IEnumerable<T> GetPage<T>(object items = null)
        {
            //if (this == null)
            //{
            //    pagination = new Pagination();
            //}

            //TotalItemCount = items.Count();
            //pagination = pagination.SetCurrentPage();
            //return items.Skip(pagination.PageSize * (pagination.CurentPage - 1)).Take(pagination.PageSize).ToList();


            TotalItemCount = (items as IEnumerable<T>).Count();
            SetCurrentPage();
            return (items as IEnumerable<T>).Skip(PageSize * (CurentPage - 1)).Take(PageSize).ToList();
        }


        public void SetCurrentPage()
        {
            //PageSize
            if (PageSize == 0)
            {
                PageSize = 12;
            }

            //NumberOfPages
            NumberOfPages = (TotalItemCount + PageSize - 1) / PageSize;

            //CurentPage
            if (CurentPage > NumberOfPages)
                CurentPage = NumberOfPages;
            else if (CurentPage < 1)
                CurentPage = 1;

            //Số lượng page trong pagenation
            var pagePagition = (int)Math.Ceiling((float)NumberOfPages / PageSizePagition);

            //Set Start/End
            if (PageSizePagition < NumberOfPages)
            {
                End = Start * PageSizePagition;
                for (var i = 1; i <= pagePagition; i++)
                {
                    if (((i - 1) * PageSizePagition + 1) <= CurentPage && CurentPage <= NumberOfPages)
                    {
                        if (i == 1)
                        {
                            Start = 1;
                            End = PageSizePagition;
                        }
                        else if (i == pagePagition)
                        {
                            Start = (i - 1) * PageSizePagition + 1;
                            End = NumberOfPages;
                        }
                        else
                        {
                            Start = (i - 1) * PageSizePagition + 1;
                            End = Start + PageSizePagition - 1;
                        }
                    }

                }
            }
            else
            {
                Start = 1;
                End = NumberOfPages;
            }
        }
    }
}
