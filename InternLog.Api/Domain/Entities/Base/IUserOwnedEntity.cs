﻿namespace InternLog.Api.Domain.Entities.Base
{
    public interface IUserOwnedEntity
    {
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
