using Catalog.Core.filters;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Core.Specifications
{
    public static class CatalogSpecificationBuilder
    {
        public static CatalogSpecification<T> Build<T>(PaginationFilter filter)
        {
            var spec = new CatalogSpecification<T>();

            // Tìm kiếm nâng cao
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                var keywordFilters = new List<FilterDefinition<T>>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (prop.PropertyType == typeof(string))
                    {
                        var builder = Builders<T>.Filter;
                        keywordFilters.Add(builder.Regex(prop.Name, new BsonRegularExpression(filter.Keyword, "i")));
                    }
                }
                if (keywordFilters.Any())
                    spec.ApplyFilter(Builders<T>.Filter.Or(keywordFilters));
            }

            // Lọc nâng cao
            if (filter.AdvancedFilter != null)
            {
                var advanced = BuildAdvancedFilter<T>(filter.AdvancedFilter);
                spec.ApplyFilter(advanced);
            }

            // Sắp xếp
            if (filter.OrderBy != null && filter.OrderBy.Any())
            {
                var sortBuilder = Builders<T>.Sort;
                SortDefinition<T>? sort = null;
                foreach (var field in filter.OrderBy)
                {
                    var parts = field.Split(' ');
                    var fieldName = parts[0];
                    bool isDesc = parts.Length > 1 && parts[1].ToLower().StartsWith("desc");

                    sort = sort == null
                        ? (isDesc ? sortBuilder.Descending(fieldName) : sortBuilder.Ascending(fieldName))
                        : (isDesc ? sort.Descending(fieldName) : sort.Ascending(fieldName));
                }

                if (sort != null)
                    spec.ApplySort(sort);
            }

            // Phân trang
            int skip = (filter.PageNumber - 1) * filter.PageSize;
            spec.ApplyPagination(skip, filter.PageSize);

            return spec;
        }

        private static FilterDefinition<T> BuildAdvancedFilter<T>(Filter filter)
        {
            var builder = Builders<T>.Filter;

            if (!string.IsNullOrEmpty(filter.Logic))
            {
                var subFilters = filter.Filters?
                    .Select(f => BuildAdvancedFilter<T>(f))
                    .ToArray();

                return filter.Logic.ToLower() switch
                {
                    FilterLogic.AND => builder.And(subFilters!),
                    FilterLogic.OR => builder.Or(subFilters!),
                    FilterLogic.XOR => BuildXorFilter(builder, subFilters!),
                    _ => throw new Exception($"Unsupported logic operator: {filter.Logic}")
                };

            }

            var field = filter.Field!;
            var op = filter.Operator!;
            var val = BsonValue.Create(((JsonElement)filter.Value!).ToString());

            return op switch
            {
                FilterOperator.EQ => builder.Eq(field, val),
                FilterOperator.NEQ => builder.Ne(field, val),
                FilterOperator.GT => builder.Gt(field, val),
                FilterOperator.GTE => builder.Gte(field, val),
                FilterOperator.LT => builder.Lt(field, val),
                FilterOperator.LTE => builder.Lte(field, val),
                FilterOperator.OR => builder.Or(ParseArray(val).Select(v => builder.Eq(field, v))),
                FilterOperator.CONTAINS => builder.Regex(field, new BsonRegularExpression(val.AsString, "i")),
                FilterOperator.STARTSWITH => builder.Regex(field, new BsonRegularExpression("^" + val.AsString, "i")),
                FilterOperator.ENDSWITH => builder.Regex(field, new BsonRegularExpression(val.AsString + "$", "i")),
                _ => throw new Exception($"Unsupported operator: {op}")
            };
        }
        private static FilterDefinition<T> BuildXorFilter<T>(FilterDefinitionBuilder<T> builder, FilterDefinition<T>[] filters)
        {
            if (filters.Length != 2)
                throw new Exception("XOR logic only supports exactly 2 filters.");

            var a = filters[0];
            var b = filters[1];

            return builder.Or(
                builder.And(a, builder.Not(b)),
                builder.And(builder.Not(a), b)
            );
        }

        private static IEnumerable<BsonValue> ParseArray(BsonValue val)
        {
            if (val.IsBsonArray)
                return val.AsBsonArray;

            if (val.IsString)
            {
                // Nếu người dùng truyền dưới dạng chuỗi CSV
                return val.AsString.Split(',')
                    .Select(s => BsonValue.Create(s.Trim()));
            }

            throw new Exception("Invalid value for IN operator. Must be array or CSV string.");
        }
    }
}
