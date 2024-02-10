using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class EfProductDal : EfEntityRepositoryBase<Product,BaseAppDbContext>, IProductDal
{
    
}