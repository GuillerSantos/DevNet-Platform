using DevNet.Application.Contracts.Persistence;
using DevNet.Domain.Entities;
using DevNet.Persistence.DBContext;
using Microsoft.EntityFrameworkCore;

namespace DevNet.Persistence.Repositories
{
    public class ApplicationUserRepository : AsyncBaseRepository<ApplicationUser>, IApplicationUserRepository
    {
        #region Public Constructors

        public ApplicationUserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task<bool> EmailExistsAsync(string email)
        {
            return await dbContext.ApplicationUsers
                .AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await dbContext.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public Task<ApplicationUser?> GetByFirstNameAsync(string firstName)
        {
            return dbContext.ApplicationUsers
                .FirstOrDefaultAsync(u => u.FirstName.ToLower() == firstName.ToLower());
        }

        #endregion Public Methods
    }
}