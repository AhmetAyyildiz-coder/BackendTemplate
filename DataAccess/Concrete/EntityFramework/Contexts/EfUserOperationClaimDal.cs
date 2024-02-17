using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim,BaseAppDbContext>,IUserOperationClaimDal
{
    
}