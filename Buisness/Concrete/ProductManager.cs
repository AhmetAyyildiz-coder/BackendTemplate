using Buisness.Abstract;
using Buisness.BuisnessAspect.Autofac;
using Buisness.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;

namespace Buisness.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;
    // private IHttpContextAccessor _contextAccessor;
    //public ProductManager(IProductDal productDal, IHttpContextAccessor contextAccessor)
    //{
    //    _productDal = productDal;
    //    // bu sekilde HttpContextAccessor'a baglamak bu katmanı web tabanlı bir yaklaşıma zorlar. 
    //    // winform vb uygulamalar icin bu calisamayacaktır.
    //    // Bu sebeple Bunu aspect kullanarak çözüyoruz. 
    //    _contextAccessor = contextAccessor;
    //}
    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;

    }

    public IDataResult<Product> GetById(int Id)
    {
        return new DataResult<Product>(_productDal.Get(p => p.ProductId == Id), true);
    }

    [SecuredOperation("Product.Read,Product.Viewer")]
    public IDataResult<List<Product>> GetList()
    {
        try
        {
            return new DataResult<List<Product>>(_productDal.GetList().ToList(), true);
        }
        catch (Exception e)
        {
            return new DataResult<List<Product>>(null, false, e.Message);
        }

    }

    public IDataResult<List<Product>> GetListByCategory(int categoryId)
    {
        return new DataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList(),true);
    }

    
    [TransactionScopeAspect]
    [ValidationAspect(typeof(ProductValidator),1)]
    public IResult Add(Product product)
    {
        // normalde buraya buisness code yazılır.
        try
        {
            ValidationTool.Validate(new ProductValidator(),product);
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