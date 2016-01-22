using System.Data.Entity;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace PagedList.EntityFramework {
	public class PagedList<T> : BasePagedList<T> {
		private PagedList() {}

		public static async Task<IPagedList<T>> Create(IQueryable<T> superset, int pageNumber, int pageSize) {
			var list = new PagedList<T>();
			await list.Init(superset, pageNumber, pageSize);
			return list;
		}

		private async Task Init(IQueryable<T> superset, int pageNumber, int pageSize) {
			if (pageNumber < 1)
				throw new ArgumentOutOfRangeException(Name.Of(pageNumber), pageNumber, "PageNumber cannot be below 1.");
			if (pageSize < 1)
				throw new ArgumentOutOfRangeException(Name.Of(pageSize), pageSize, "PageSize cannot be less than 1.");
			TotalItemCount = superset == null ? 0 : await superset.CountAsync();
			PageSize = pageSize;
			PageNumber = pageNumber;
			PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
			HasPreviousPage = PageNumber > 1;
			HasNextPage = PageNumber < PageCount;
			IsFirstPage = PageNumber == 1;
			IsLastPage = PageNumber >= PageCount;
			FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
			var num = FirstItemOnPage + PageSize - 1;
			LastItemOnPage = num > TotalItemCount ? TotalItemCount : num;
			if (superset == null || TotalItemCount <= 0)
				return;
			var skipCount = pageNumber == 1 ? 0 : (pageNumber - 1) * pageSize;
			Subset.AddRange(await superset.Skip(skipCount).Take(pageSize).ToListAsync());
		}
	}
}