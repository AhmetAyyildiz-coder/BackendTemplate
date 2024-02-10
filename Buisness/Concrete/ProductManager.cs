using Buisness.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Buisness.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }

    public IDataResult<Product> GetById(int Id)
    {
        return new DataResult<Product>(_productDal.Get(p => p.ProductId == Id), true);
    }

    public IDataResult<List<Product>> GetList()
    {
        return new DataResult<List<Product>>(_productDal.GetList().ToList(), true);

    }

    public IDataResult<List<Product>> GetListByCategory(int categoryId)
    {
        return new DataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList(),true);
    }

    public IResult Add(Product product)
    {
        // normalde buraya buisness code yazılır.
        try
        {
            _productDal.Add(product);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
        return new Result(true);
        // eğer mesaj döndürmek istenirse 
        // return new Result(true,Messages.ProductAdded);
    }

    public IResult Delete(Product product)
    {
        try
        {
            _productDal.Delete(product);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
        return new Result(true);
    }

    public IResult Update(Product product)
    {
        try
        {
            _productDal.Update(product);
        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
        return new Result(true);
    }
}