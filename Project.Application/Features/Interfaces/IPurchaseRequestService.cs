using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.PurchaseRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IPurchaseRequestService
    {
        Task ChangeStatus(ChangePurchaseRequestStatus input);
        Task<DatatableResponse<PurchaseRequestFactorDTO>> Datatable(NewsDatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<PurchaseRequestFactorDTO> Detail(int Id);
        Task<PurchaseRequestResultDTO> Submit(SubmitPurchaseRequestDTO input);
    }
}
