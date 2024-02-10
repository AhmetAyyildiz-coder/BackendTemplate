using Core.DataAccess.EntityFramework;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework.Contexts;

public class EfCategoryDal : EfEntityRepositoryBase<Category,BaseAppDbContext>,ICategoryDal 
{
    
}