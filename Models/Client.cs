using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Client
    {
        public int ID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(10, ErrorMessage = "Invalid EGN")]
        public string EGN { get; set; }
        public string NAME { get; set; }
        public string address { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(13, ErrorMessage = "Invalid phone number")]
        public string PN { get; set; }
        [ForeignKey("ID_user")]
        public virtual IdentityUser user { get; set; }

        public Client() { }
    }
}
