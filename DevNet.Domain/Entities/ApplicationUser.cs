using DevNet.Domain.Enums;

namespace DevNet.Domain.Entities
{
    public class ApplicationUser : BaseEntity
    {
        #region Properties

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Roles Role { get; set; }
        public virtual ICollection<RefreshTokens>? RefreshTokens { get; set; } = new List<RefreshTokens>();

        #endregion Properties
    }
}