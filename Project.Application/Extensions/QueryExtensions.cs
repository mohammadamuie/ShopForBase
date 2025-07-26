using System;
using System.Linq;
using System.Linq.Expressions;
using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.Extensions
{
    public static class QueryExtensions
    {
        public static IQueryable<T> OrderByDynamic<T>(
             this IQueryable<T> query,
             string orderByMember,
             KtOrderDir ascendingDirection)
        {
            var param = Expression.Parameter(typeof(T), "c");

            var body = orderByMember.Split('.').Aggregate<string, Expression>(param, Expression.PropertyOrField);

            var queryable = ascendingDirection == KtOrderDir.Asc ?
                (IOrderedQueryable<T>)Queryable.OrderBy(query.AsQueryable(), (dynamic)Expression.Lambda(body, param)) :
                (IOrderedQueryable<T>)Queryable.OrderByDescending(query.AsQueryable(), (dynamic)Expression.Lambda(body, param));

            return queryable;
        }
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginateDTO filter)
        {
            if (filter.Start.HasValue && filter.Start.Value > 0)
            {
                queryable = queryable.Skip(filter.Start.Value);
            }
            if (filter.Length.HasValue && filter.Length.Value > 0)
            {
                queryable = queryable.Take(filter.Length.Value);
            }
            return queryable;
        }
        public static IQueryable<T> PaginateDefault<T>(this IQueryable<T> queryable, int? page)
        {
            if (page.HasValue == false || page.Value < 1)
            {
                page = 1;
            }

            return queryable.Paginate(new PaginateDTO
            {
                Length = 10,
                Start = 10 * (page - 1)
            });
        }

        public static IQueryable<T> WhereDynamic<T>(
            this IQueryable<T> sourceList, string query)
        {

            if (string.IsNullOrEmpty(query))
            {
                return sourceList;
            }

            try
            {

                var properties = typeof(T).GetProperties()
                    .Where(x => x.CanRead && x.CanWrite && !x.GetGetMethod().IsVirtual);

                //Expression
                sourceList = sourceList.Where(c =>
                    properties.Any(p => p.GetValue(c) != null && p.GetValue(c).ToString()
                        .Contains(query, StringComparison.InvariantCultureIgnoreCase)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return sourceList;
        }
    }
}