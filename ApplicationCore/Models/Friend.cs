using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Models
{
    public class Friend
    {
        [Key]
        public int FriendId { get; set; }

        [Required]
        [Display(Name = "Relationship")]
        public string Relationship { get; set; }

        [Display(Name = "How Long have you known your friend?")]
        public int howLong { get; set; }
    }
}
