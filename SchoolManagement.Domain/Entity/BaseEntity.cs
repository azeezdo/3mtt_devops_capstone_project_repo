using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Domain.Entity
{
	public class BaseEntity
	{
        public long Id { get; set; }

        public DateTime DateCreated { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? DateUpdated { get; set; }

        [StringLength(50)]
        public string UpdatedBy { get; set; }
    }
}

