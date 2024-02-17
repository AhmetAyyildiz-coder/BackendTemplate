using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class OperationClaimDal : EfEntityRepositoryBase<OperationClaim, BaseAppDbContext>, IOperationClaimDal
{
    
}