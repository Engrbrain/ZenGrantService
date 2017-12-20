using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZenGrantService.Models
{
    public class Organization
    {
        public int ID { get; set; }
        public string OrgName { get; set; }
        public string OrgAddress { get; set; }
        public string OrgState { get; set; }
        public string OrgCountry { get; set; }
        public string OrgPhone { get; set; }
        public string OrgEmail { get; set; }
        public string OrgWebsite { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool isDeleted { get; set; }
        public DateTime TimeStamp { get; set; }
        public byte[] OrgLogo { get; set; }


        [Key]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public Organization()
        {
            TimeStamp = DateTime.Now;
        }

        public ICollection<Subscription> Subscription { get; set; }
        public ICollection<Renewal> Renewal { get; set; }
        public ICollection<FocusArea> FocusArea { get; set; }
    }
}