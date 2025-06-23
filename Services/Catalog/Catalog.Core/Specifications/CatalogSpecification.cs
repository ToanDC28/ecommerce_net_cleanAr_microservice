using Catalog.Core.filters;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Catalog.Core.Specifications
{
    public class CatalogSpecification<T>
    {
        public FilterDefinition<T> Filter { get; private set; } = Builders<T>.Filter.Empty;
        public SortDefinition<T>? Sort { get; private set; }
        public int Skip { get; private set; }
        public int Take { get; private set; }

        public void ApplyFilter(FilterDefinition<T> filter)
        {
            Filter = filter == Builders<T>.Filter.Empty ? Filter : Builders<T>.Filter.And(Filter, filter);
        }

        public void ApplySort(SortDefinition<T> sort) => Sort = sort;
        public void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }
    }
}
