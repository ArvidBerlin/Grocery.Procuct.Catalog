using Shared.Enums;
using Shared.Models;

namespace Shared.Interfaces;

public interface IProductService
{
    Product? CurrentProduct { get; set; }

    StatusCodes CreateProduct(Product product);
    IEnumerable<Product> GetAllProductsFromList();
    StatusCodes Delete(string id);
    StatusCodes Update(Product product);
}