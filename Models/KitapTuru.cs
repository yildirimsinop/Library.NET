using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class KitapTuru
    {
        [Key] // primary key
        public int Id { get; set; }

        [Required (ErrorMessage ="Book Items is not empty!")]
        [MaxLength(25)]
        [DisplayName("Book Items Name")]
        public string Title { get; set; }

        

    }
}
