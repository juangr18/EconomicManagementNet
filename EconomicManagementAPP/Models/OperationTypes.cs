using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class OperationTypes
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [Remote(action: "VerificaryOperationTypes", controller: "OperationTypes")]//Activamos la validacion se dispara peticion http hacia el back
        public string Description { get; set; }
    }
}
