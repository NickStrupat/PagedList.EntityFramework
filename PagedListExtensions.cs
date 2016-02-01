using System;
using System.Linq;
using System.Threading.Tasks;

namespace PagedList.EntityFramework {
	public static class PagedListExtensions {

		/// <summary>
		/// Creates a subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.
		/// </summary>
		/// <typeparam name="T">The type of object the collection should contain.</typeparam>
		/// <param name="superset">The collection of objects to be divided into subsets. If the collection implements <see cref="IQueryable{T}"/>, it will be treated as such.</param>
		/// <param name="pageNumber">The one-based index of the subset of objects to be contained by this instance.</param>
		/// <param name="pageSize">The maximum size of any individual subset.</param>
		/// <returns>A subset of this collection of objects that can be individually accessed by index and containing metadata about the collection of objects the subset was created from.</returns>
		/// <seealso cref="PagedList{T}"/>
		public static Task<IPagedList<T>> ToPagedListForEntityFrameworkAsync<T>(this IQueryable<T> superset, int pageNumber, int pageSize) {
            // changed method name, because X.PagedList has its own ToPagedListAsync extensions that has nothing to do with EF.
            return PagedList<T>.Create(superset, pageNumber, pageSize);
		}
	}
}