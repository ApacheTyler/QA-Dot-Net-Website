using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPData
{
    public class Answer
    {

        [Required]
        [Key]
        public int AnswerID { get; set; }

        public String CreatedByID { get; set; }

        [Required, ForeignKey("Question")]
        public int QuestionAnsID { get; set; }
        public virtual Question Question { get; set; }

        [Required]
        public String AnswerText { get; set; }

        public DateTime DateModified { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public bool Like { get; set; }

    }
}
