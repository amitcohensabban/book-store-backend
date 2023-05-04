using System.ComponentModel.DataAnnotations;

namespace book_store.Models
{
    public class UpdateDetailsModel
    {
        [Required]
        [EmailAddress]
        public string OldEmail { get; set; }
        [Required]
        [EmailAddress]
        public string NewEmail { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
