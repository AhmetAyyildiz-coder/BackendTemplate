using Core.Utilities.Results;
using Entities.Concrete;

namespace Buisness.Abstract;

// APİ'dan expression vs göndermeyin. APİ sadece olmalı
// ayrıca service'ler generic yazılmamalıdır.
public interface IProductService
{
    IDataResult<Product> GetById(int Id);
    IDataResult<List<Product>> GetList();
    IDataResult<List<Product>> GetListByCategory(int categoryId);
    IResult Add(Product product);
    IResult Delete(Product product);
    IResult Update(Product product);

}