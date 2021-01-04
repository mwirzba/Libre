using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using X.PagedList;
using System.ComponentModel.DataAnnotations;

namespace Libre.Helpers
{
    public enum SortType
    {
        [Display(Name = "Ascending")]
        descending,
        [Display(Name = "Descending")]
        ascending
    }

    public class DataHelper<T> where T : class
    {
        public DataHelper(System.Linq.IQueryable<T> data, int pageNumber, int pageSize)      
        {
            query = data;
            this.pageNumber = pageNumber;
            this.pageSize = pageSize;
        }

        private readonly DbSet<T> data;
        private System.Linq.IQueryable<T> query;
        private int pageSize;
        private int pageNumber;

       

        /// <summary>
        /// Sorts data in descending or ascending order
        /// </summary>
        public DataHelper<T> SortBy(SortType type, string columnName)
        {
            if (query == null)
            {
                query = data.AsQueryable();
            }

            switch (type)
            {
                case SortType.descending:
                    query = query.OrderBy(columnName + " desc");
                    break;
                case SortType.ascending:
                    query = data.OrderBy(columnName);
                    break;
                default:
                    throw new System.Exception("This sorting type is not supported");
            }

            return this;
        }

        /// <summary>
        /// Filter data by given predicate
        /// </summary>
        public DataHelper<T> Where(string predicate)
        {
            if (predicate != string.Empty)
            {
                query = query.Where(predicate);
            }

            query = data.AsQueryable();
            return this;
        }


        /// <summary>
        /// Returns data as pagedList
        /// </summary>
        public PagedList<T> ToPagedList()
        {
            if (query == null)
            {
                query = data.AsQueryable();
            }

            return (PagedList<T>)query.ToPagedList(pageNumber, pageSize);
        }
    }
}