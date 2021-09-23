using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class Adjective
    {
        [Key]
        [Required]
        public int AdjectiveId { get; set; }

        public string AdjName { get; set; }

        public string AdjDefinition { get; set; }

        public bool AdjType { get; set; }
    }
}
