using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class ClientResponse
    {
        [Key]
        public int ResponseId { get; set; }

        //[NotMapped] //temporary, resolving issue with add migration
        [ForeignKey("AdjectiveId")]
        public virtual Adjective Adjective { get; set; }

        //[NotMapped] //temporary, resolving issue with add migration
        [ForeignKey("ClientId")]
        public virtual Client Client { get; set; }
    }
}
