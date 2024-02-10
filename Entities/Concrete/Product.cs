using Core.Entities;

namespace Entities.Concrete;

public class Product : IEntity
{
    public Product(int productId, string productName, int categoryId, string quantityPerUnit, decimal unitPrice, short unitsInStock)
    {
        ProductId = productId;
        ProductName = productName;
        CategoryId = categoryId;
        QuantityPerUnit = quantityPerUnit;
        UnitPrice = unitPrice;
        UnitsInStock = unitsInStock;
    }

    public Product(string productName, string quantityPerUnit)
    {
        ProductName = productName;
        QuantityPerUnit = quantityPerUnit;
    }

    public Product()
    {
        
    }

    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public int CategoryId { get; set; }

    /// <summary>
    /// 12 adet, 1 litre, 500gr gibi değerler alabilir.
    /// </summary>
    public string QuantityPerUnit { get; set; }

    /// <summary>
    /// Birim fiyat
    /// </summary>
    public decimal UnitPrice { get; set; }

    public short UnitsInStock { get; set; }
    public virtual Category Category { get; set; }
}