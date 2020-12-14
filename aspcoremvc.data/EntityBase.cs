using System;
using System.ComponentModel.DataAnnotations;
using Common.Dto;
using Domain.User;

namespace Data
{
    public class EntityBase: IKeyedObject
    {
        //[Key]
        public int Id { get; set; }

        public DateTime? AddedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public int? AddedById { get; set; }

        public User AddedBy { get; set; }

        public int? UpdatedById { get; set; }

        public User UpdatedBy { get; set; }
    }
}