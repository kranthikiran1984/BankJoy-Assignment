using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankingApi.Core.Common
{
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, bool countOnly = false)
        {
            var total = source.Count();
            TotalCount = total;

            pageSize = pageSize == 0 ? total : pageSize;
            TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            PageSize = pageSize;
            PageIndex = pageIndex;

            if (countOnly)
                return;

            AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalCount { get; }

        public int TotalPages { get; }

        public bool HasPreviousPage => PageIndex > 0;

        public bool HasNextPage => PageIndex + 1 < TotalPages;
    }
}
