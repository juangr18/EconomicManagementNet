using Microsoft.AspNetCore.Mvc.Rendering;

namespace EconomicManagementAPP.Models
{
    public class CategorieTypeOperationViewModel : Categories
    {
        public IEnumerable<SelectListItem> OperationType { get; set; }
    }
}
