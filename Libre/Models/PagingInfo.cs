using System;
using System.Collections.Generic;
using System.Linq;

namespace Libre.Models
{
      public class PagingInfo
      {
            public int TotalItems { get; set; }
            public int ItemsPerPage { get; set; }
            public int CurrentPage { get; set; }
            public int TotalPages { get; set; }

        public PagingInfo( int count, int pageNumber, int pageSize )
        {
            TotalItems = count;
            CurrentPage = pageNumber;
            ItemsPerPage = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        public static PagingInfo GetPaginationInfo<T>(IQueryable<T> source, int pageNumber, int pageSize, out List<T> items)
        {
            var count = source.Count();
            items = source.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToList();

            return new PagingInfo(count, pageNumber, pageSize);
        }
    }
}
