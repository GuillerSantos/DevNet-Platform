namespace DevNet.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        #region Properties

        public string RefreshTokens { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsRevoked { get; set; } = false;
        public Guid ApplicationUserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual ApplicationUser? ApplicationUser { get; set; }

        #endregion Properties
    }
}