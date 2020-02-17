using System;
using Bcc.DocumentManager.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Bcc.DocumentManager
{
    public static class Extensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, 
                                         int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();


            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);
        
            var skip = (page - 1) * pageSize;     
            result.Results = await query.Skip(skip).Take(pageSize).ToListAsync();
        
            return result;
        }
    }
}