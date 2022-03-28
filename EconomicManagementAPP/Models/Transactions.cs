using System.ComponentModel.DataAnnotations;

namespace EconomicManagementAPP.Models
{
    public class Transactions
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public DateTime TransactionDate { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        public decimal Total { get; set; }

        [StringLength(maximumLength: 1000, ErrorMessage = "Description cannot have more than 1000 characters")]
        [Required(ErrorMessage = "{0} is required")]
        public string Description { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "You must select an account")]
        [Required(ErrorMessage = "{0} is required")]
        public int AccountId { get; set; }

        [Range(1, maximum: int.MaxValue, ErrorMessage = "You must select a category")]
        [Required(ErrorMessage = "{0} is required")]
        public int CategoryId { get; set; }

    }
}
