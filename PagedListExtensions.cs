using System;
using System.Linq;
using System.Threading.Tasks;

namespace PagedList.EntityFramework {
	public static class PagedListExtensions {
		public static async Task<IPagedList<T>> ToPagedListAsync<T>(this IQueryable<T> superset, Int32 pageNumber, Int32 pageSize) {
			return await PagedList<T>.Create(superset, pageNumber, pageSize);
		}
	}
}