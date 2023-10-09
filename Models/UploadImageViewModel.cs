using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class UploadImageViewModel
    {
		[NotMapped]
		[Display(Name = "Picture")]
		public IFormFile ActorPicture { get; set; }
	}
}
