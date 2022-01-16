using InternLog.Api.Domain.Entities.Base;

namespace InternLog.Api.Domain.Entities
{
    public class RefreshToken : EntityBase, IUserOwnedEntity
    {
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool Used { get; set; }
        public bool Invalidated { get; set; }
        public ApplicationUser User { get; set; }
        public Guid UserId { get; set; }
    }
}
