using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eTickets.Models
{
    public class Producer
    {

        [Key]
        public int Id { get; set; }
		[Display(Name = "profile picture ")]
		public string ProfilePictureURL { get; set; }
		[Display(Name = "Full Name")]
		public string FullName { get; set; }
		[Display(Name = "Biography")]
		public string Bio { get; set; }
        [NotMapped]
        //RELATIOSHIPS
        public List<Movie> Movies { get; set; }

    }
}
