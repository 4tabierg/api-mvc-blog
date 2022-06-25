using BilgeAdamBlog.Core.Entity.Enums;
using System;

namespace BilgeAdamBlog.Core.Entity
{
    public class CoreEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }

        public DateTime? CreatedDate { get; set; }
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public Guid? CreatedUserId { get; set; }

        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }
        public Guid? ModifiedUserId { get; set; }

    }
}
