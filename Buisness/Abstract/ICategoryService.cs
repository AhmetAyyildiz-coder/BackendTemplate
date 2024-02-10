using Core.Utilities.Results;
using Entities.Concrete;

namespace Buisness.Abstract;

public interface ICategoryService
{
    IDataResult<Category> GetById(int Id);
    IDataResult<List<Category>> GetList();
    
    
    IResult Add(Category product);
    IResult Delete(Category product);
    IResult Update(Category product);
}