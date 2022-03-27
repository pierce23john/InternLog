using InternLog.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternLog.Api.Services.Contracts
{
    public interface IUserOwnedEntityService<TUser, TOwnedEntity> where TUser : IEntity
                                                                  where TOwnedEntity : IUserOwnedEntity
    {
        Task<bool> UserOwnsEntityAsync(Guid id, Guid userId);
    }
}
