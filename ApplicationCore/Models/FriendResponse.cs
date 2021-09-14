using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class FriendResponse
    {
        [Key]
        public int ResponseId { get; set; }

        [ForeignKey("AdjectiveId")]
        public virtual Adjective AdjectiveId { get; set; }

        [ForeignKey("ClientId")]
        public virtual Client ClientId { get; set; }

        [ForeignKey("FriendId")]
        public virtual Friend FriendId { get; set; }



    }
}
