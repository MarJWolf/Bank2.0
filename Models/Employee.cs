﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bank.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string NAME { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(13, ErrorMessage = "Invalid phone number")]
        public string PN { get; set; }
        [ForeignKey("ID_position")]
        public virtual Position position { get; set; }
        [ForeignKey("ID_user")]
        public virtual IdentityUser user { get; set; }

        public Employee() { }
    }
}
