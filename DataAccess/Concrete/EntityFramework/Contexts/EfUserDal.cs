using System.Linq.Expressions;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class EfUserDal : EfEntityRepositoryBase<User,BaseAppDbContext>,IUserDal
{
   

    public List<OperationClaim> GetClaims(User User)
    {
        using var context = new BaseAppDbContext();

        // maybe return null
        return context.UserOperationClaims
            .Where(uoc => uoc.UserId == User.Id)
            .Include(uoc => uoc.OperationClaim)
            .Select(uoc => uoc.OperationClaim).ToList();
    }
}