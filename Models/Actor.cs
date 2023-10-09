using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Actor : UploadImageViewModel
	{
        [Key]
        public int Id { get; set; }

        
        [DisplayName("ProfilePicture")]
		public string ProfilePicture { get; set; }

		/*[Display(Name = "prfile picture url")]
         [Required(ErrorMessage="Profile Picture is required")]
         public string ProfilePictureURL { get; set; }*/
		[Display(Name = "Full Name")]
		[Required(ErrorMessage = "Full Name is required")]
		[StringLength(50, MinimumLength = 3,ErrorMessage ="Full name must be between 3 and 50 chars")]
		public string FullName { get; set; }
		[Display(Name = "Biography")]
		[Required(ErrorMessage = "Biography is required")]
		public string Bio { get; set; }


        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
		
	}
}
