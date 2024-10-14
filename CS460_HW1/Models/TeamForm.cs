using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CS460_HW1.Models
{
    public class TeamForm
    {
        [Required]
        [Range(2, 10, ErrorMessage = "Team size must be between 2 and 10.")]
        public int TeamSize { get; set; }


        [Required]
        [RegularExpression(@"^[a-zA-Z\s,.\-_'\n]+$", ErrorMessage = "Names can only contain letters, spaces, ,.-_' and must be one per line.")]
        public string Names { get; set; }
    }
}