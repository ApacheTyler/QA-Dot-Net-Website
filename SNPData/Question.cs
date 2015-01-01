using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPData
{
    public class Question
    {

        [Key]
        [Required]
        public int QuestionID { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        [Display(Name = "QuestionText")]
        public string QuestionText { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
}
