using ITLab29.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ITLab29.Models.ViewModels {
    public class FeedBackViewModel {
        public int id { get; set; }
        [Required(ErrorMessage = "Kies een score!")]
        public int Score { get; set; }
        [Display(Name = "Feedback")]
        [Required(ErrorMessage = "Geef je feedback door!")]
        [StringLength(350, ErrorMessage = "De feedback kan maximum 350 karakters bevatten")]
        public string Description { get; set; }
        public Session Session { get; set; }
        public string UserId { get; set; }
    }
}
