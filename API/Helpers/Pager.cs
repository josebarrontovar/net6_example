﻿namespace API.Helpers
{
    public class Pager<T> where T : class
    {
        public string Search { get; private set; }
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public int Totals { get; private set; }
        public IEnumerable<T> Registers { get; private set; }

        public Pager(IEnumerable<T> registers, int total,int pageIndex, int pageSize, string search)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Total = total;
            Registers = registers;
            Search = search;    
        }

        public int TotalPage
        {
            get
            {
                return (int)Math.Ceiling(Total / (double)PageSize);
            }
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPage);
            }
        }
    }
}
