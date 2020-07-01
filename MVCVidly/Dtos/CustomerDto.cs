using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVCVidly.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }              
        public byte MembershipTypeId { get; set; }

        public MemberShipTypeDto MemberShipType { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }       
        public DateTime? BirthDate { get; set; }
    }
}