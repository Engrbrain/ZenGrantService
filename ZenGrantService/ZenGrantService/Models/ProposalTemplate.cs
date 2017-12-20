using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZenGrantService.Models
{
    public class ProposalTemplate
    {
        public int ID { get; set; }
        public string ProgrammeName { get; set; }
    }
}