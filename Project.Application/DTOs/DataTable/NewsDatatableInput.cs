
using Project.Application.DTOs.DataTable;
using Project.Domain.Enums;

namespace Project.Application.DTOs.Datatable
{
    public class NewsDatatableInput : DatatableInput
    {
        public string Title { get; set; }
        public bool IsToday { get; set; }
        public PurchaseRequestStatus? Status { get; set; }
    }
}