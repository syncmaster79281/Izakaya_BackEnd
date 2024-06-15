using System;
using System.Collections.Generic;

namespace ISPAN.Izakaya.Dtos
{
    public class Pagination
    {
        public Pagination(int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }

        public int PageNumber { get; }// 頁碼, 1表示第1一頁
        public int PageSize { get; } // 每頁筆數
        public int TotalCount { get; } // 符合條件的總筆數

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / (double)PageSize); //總頁數
        public int CurrentPage => PageNumber > 0 ? PageNumber : 1;  //當前頁
    }
    public class PagedList<T> where T : class // 存放單頁記錄以及分頁資訊
    {
        public PagedList(IEnumerable<T> data, int pageNumber, int pageSize, int totalCount)
        {
            Data = data;
            Pagination = new Pagination(pageNumber, pageSize, totalCount);
        }
        public IEnumerable<T> Data { get; private set; }
        public Pagination Pagination { get; private set; }
    }
}
