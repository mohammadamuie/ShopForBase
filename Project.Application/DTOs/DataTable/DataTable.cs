using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.DTOs.DataTable
{
    public class DatatableInput
    {
        public int? Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class StringDatatableInput
    {
        public string? Id { get; set; }
        public bool IsActive { get; set; } = true;
    }
    public class FiltersFromRequestDataTable
    {
        public int Length { get; set; }
        public int Start { get; set; }
        public string SortColumn { get; set; }
        public string SortColumnDirection { get; set; }
        public string SortColumnIndex { get; set; }
        public string Draw { get; set; }
        public string SearchValue { get; set; }
    }
    public class DatatableResponse<T>
    {
        public object Data { get; set; }
        public int RecordsFiltered { get; set; }
        public int RecordsTotal { get; set; }
        public string SEcho { get; set; }
    }
    
}
