﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCVidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public MembershipType MembershipType { get; set; }

        [Display(Name ="Membership Type")]
        public byte MembershipTypeId { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        [Display(Name ="Birth Date")]
        public DateTime? BirthDate { get; set; }
    }
}