
using Core.DataAccess.EntityFramework;
using Core.Entities;
using DataAccess.Abstract;


namespace DataAccess.Concrete.EntityFramework.Contexts;

public class EfUserDal : EfEntityRepositoryBase<User,BaseAppDbContext>,IUserDal
{
   

    public List<OperationClaim> GetClaims(User User)
    {
        using var context = new BaseAppDbContext();

      

        return (from uop in context.UserOperationClaims
            join u in context.Users
                on uop.UserId equals u.Id
            join op in context.OperationClaims
                on uop.OperationClaimId equals op.Id
            select new OperationClaim()
            {
                Id = op.Id,
                Name = op.Name
            }).ToList();
    }
}