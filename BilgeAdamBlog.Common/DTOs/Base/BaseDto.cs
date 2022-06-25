using BilgeAdamBlog.Common.Clients.Enums;
using System;

namespace BilgeAdamBlog.Common.DTOs.Base
{
    public class BaseDto
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
    }
}
