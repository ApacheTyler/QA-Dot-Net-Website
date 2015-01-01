using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPData
{

    public class Announcement
    {
        [Key]
        [Required]
        public int ID { get; set; }

        public DateTime date { get; set; }

        [Required]
        [Display(Name = "Announcement")]
        public string Message { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
