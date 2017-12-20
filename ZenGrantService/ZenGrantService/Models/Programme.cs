using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZenGrantService.Models
{
    public class Programme
    {
        public int ID { get; set; }
        public string ProgrammeName { get; set; }
        public string ProgrammeCode { get; set; }
        public string ProgrammeDesc { get; set; }
        public string ApplicationStartDate { get; set; }
        public string ApplicationDueDate { get; set; }
        public string ProposalTemplateID { get; set; }

        public int OrganizationID { get; set; }

        public virtual Organization Organization { get; set; }

        public int MaximumApplication { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isDeleted { get; set; }
        public DateTime TimeStamp { get; set; }
        public byte[] ProgrammeLogo { get; set; }


        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public Programme()
        {
            TimeStamp = DateTime.Now;
        }

       
    }
}