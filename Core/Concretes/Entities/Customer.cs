using Core.Abstract.Bases;
using Core.Concretes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Concretes.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string? TaxNumber { get; set; }
        public string? Industury { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }

        [ForeignKey("AssignedUser")]
        public string? AssignedUserId { get; set; }
        public virtual ApplicationUser? AssignedUser { get; set; }
        public bool IsPerson { get; set; }
        public CustomerStatus Status { get; set; } = CustomerStatus.potential;

        public virtual ICollection<Contact>? Contacts { get; set; } = [];
        public virtual ICollection<Activity>? Activities { get; set; } = [];
        public virtual ICollection<Opportunity>? Opportunities { get; set; } = [];



    }


}
