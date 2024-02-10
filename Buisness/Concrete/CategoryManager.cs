using Buisness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Buisness.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;

    public CategoryManager(ICategoryDal categoryDal)
    {
        _categoryDal = categoryDal;
    }

    public IDataResult<Category> GetById(int Id)
    {
        try
        {
            return new DataResult<Category>( _categoryDal.Get(c => c.CategoryId == Id),true);
        }
        catch (Exception e)
        {
            return new DataResult<Category>(null, false, e.Message);
        }
    }

    public IDataResult<List<Category>> GetList()
    {
        try
        {
            return new DataResult<List<Category>>( _categoryDal.GetList().ToList(),true);
        }
        catch (Exception e)
        {
            return new DataResult<List<Category>>(null, false, e.Message);
        }
    }

    

    public IResult Add(Category product)
    {
        try
        {
            _categoryDal.Add(product);
            return new Result(true);
        }
        catch (Exception e)
        {
            return new Result(false,e.Message);
        }
    }

    public IResult Delete(Category product)
    {
        try
        {
            _categoryDal.Delete(product);
            return new Result(true);
        }
        catch (Exception e)
        {
            return new Result(false,e.Message);
        }
    }

    public IResult Update(Category product)
    {
        try
        {
            _categoryDal.Update(product);
            return new Result(true);
        }
        catch (Exception e)
        {
            return new Result(false,e.Message);
        }
    }
}