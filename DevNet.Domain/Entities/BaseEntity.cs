namespace DevNet.Domain.Entities
{
    public class BaseEntity
    {
        #region Properties

        public Guid Id { get; set; }
        public DateTime JoinedAt { get; set; }

        #endregion Properties
    }
}