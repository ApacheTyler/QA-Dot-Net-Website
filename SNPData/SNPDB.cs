using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNPData
{
    public class SNPDB : DbContext
    {

        public SNPDB():base() {}

        public DbSet<Announcement> Announcements { get; set; } // Collection of announcements

        public DbSet<Question> Questions { get; set; } //Collection of questions

        public DbSet<Answer> Answers { get; set; } //Collection of answers


    }
}
