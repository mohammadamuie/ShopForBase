using AutoMapper;
using Project.Application.DTOs.DataTable;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Project.Domain.Enums;

namespace Project.Application.Extensions
{
    public static class DatatableExtention
    {
        public static void GetDataFromRequest(this HttpRequest Request, out FiltersFromRequestDataTable filtersFromRequest)
        {
            //TODO: Make Strings Safe String
            filtersFromRequest = new FiltersFromRequestDataTable
            {
                Draw = Request.Form["draw"].FirstOrDefault(),
                Start = Convert.ToInt32(Request.Form["start"].FirstOrDefault()),
                Length = Convert.ToInt32(Request.Form["length"].FirstOrDefault()),
                SortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault(),
                SortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault(),
                SearchValue = Request.Form["search[value]"].FirstOrDefault()
            };
            filtersFromRequest.SortColumnIndex = Request.Form["order[0][column]"].FirstOrDefault();

            filtersFromRequest.SearchValue = filtersFromRequest.SearchValue?.ToLower();

        }

        public static async Task<DatatableResponse<TDest>> ToDataTableAsync<T, TDest>(this IQueryable<T> source, int totalRecords, FiltersFromRequestDataTable filtersFromRequest, IMapper _mapper)
        {
            var res = new DatatableResponse<TDest>
            {
                SEcho = filtersFromRequest.Draw,
                RecordsTotal = totalRecords,
                RecordsFiltered = await source.CountAsync(),
            };
            var data = await source.DatatableOrderBy(filtersFromRequest)
                            .DatatablePaginate(filtersFromRequest.Start, filtersFromRequest.Length)
                            .ToListAsync();

            //List<T> data;

            //if (string.IsNullOrWhiteSpace(filtersFromRequest.SortColumn) == false)
            //{
            //    data = await source
            //        .OrderByDynamic("UpdatedAt", "DESC")
            //        //.DatatableOrderBy(filtersFromRequest)
            //        .DatatablePaginate(filtersFromRequest.Start, filtersFromRequest.Length)
            //        .ToListAsync();
            //}
            //else
            //{
            //    data = await source
            //        .OrderByDynamic("UpdatedAt", "DESC")
            //        //.DatatableOrderBy(filtersFromRequest)
            //        .DatatablePaginate(filtersFromRequest.Start, filtersFromRequest.Length)
            //        .ToListAsync();
            //}


            res.Data = _mapper.Map<List<TDest>>(data);


            return res;
        }


        private static IQueryable<T> DatatableOrderBy<T>(this IQueryable<T> source, FiltersFromRequestDataTable filtersFromRequest)
        {
            var props = typeof(T).GetProperties();
            string propertyName = "";
            for (int i = 0; i < props.Length; i++)
            {
                if (i.ToString() == filtersFromRequest.SortColumnIndex)
                    propertyName = props[i].Name;
            }

            System.Reflection.PropertyInfo propByName = typeof(T).GetProperty(propertyName);
            if (propByName != null)
            {
                if (filtersFromRequest.SortColumnDirection == "desc")
                    source = source.OrderByDynamic(propertyName, KtOrderDir.Desc);
                else
                    source = source.OrderByDynamic(propertyName, KtOrderDir.Asc);
            }

            return source;
        }

        public static IQueryable<T> DatatablePaginate<T>(this IQueryable<T> enumerable, int start, int length)
        {
            return enumerable.Skip(start).Take(length);
        }

        
    }
}