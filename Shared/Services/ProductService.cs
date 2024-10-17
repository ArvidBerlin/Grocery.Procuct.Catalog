using Newtonsoft.Json;
using Shared.Enums;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Services;

public class ProductService(IFileService fileService) : IProductService
{
    public Product? CurrentProduct { get; set; }

    private List<Product> _products = [];
    private readonly IFileService _fileService = fileService;

    public StatusCodes CreateProduct(Product product)
    {
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return StatusCodes.NoNameSet;
        }
        if (product.Price <= 0)
        {
            return StatusCodes.NoPriceSet;
        }
        if (product.Category == null!)
        {
            return StatusCodes.NoCategorySet;
        }
        if (_products.Any(x => x.Name == product.Name))
        {
            return StatusCodes.Exists;
        }

        try
        {
            _products.Add(product);
            var result = _fileService.SaveToFile(JsonConvert.SerializeObject(_products, Formatting.Indented));
            return result;
        }

        catch (Exception  ex)
        {
            return StatusCodes.Failed;
        }
    }

    public StatusCodes Update(Product product)
    {
        var existingProduct = _products.FirstOrDefault(x => x.Id == product.Id);

        if (existingProduct == null)
        {
            return StatusCodes.NotFound;
        }
        if (string.IsNullOrWhiteSpace(product.Name))
        {
            return StatusCodes.NoNameSet;
        }
        if (product.Price <= 0)
        {
            return StatusCodes.NoPriceSet;
        }
        if (product.Category == null!)
        {
            return StatusCodes.NoCategorySet;
        }

        try
        {
            existingProduct = product;
            var result = _fileService.SaveToFile(JsonConvert.SerializeObject(_products, Formatting.Indented));
            return result;
        }
        catch (Exception ex)
        {
            return StatusCodes.Failed;
        }
    }

    public IEnumerable<Product> GetAllProductsFromList()
    {
        try
        {
            var content = _fileService.LoadFromFile();
            if (!string.IsNullOrEmpty(content))
            {
                var tempList = JsonConvert.DeserializeObject<List<Product>>(content)!;
                if (tempList != null && tempList.Count > 0)
                {
                    _products = tempList;
                }
            }
        }
        catch { }

        return _products;
    }

    public StatusCodes Delete(string id)
    {
        try
        {
            _products = GetAllProductsFromList().ToList();

            var product = _products.FirstOrDefault(x => x.Id == id);
            if (product == null!)
            {
                return StatusCodes.NotFound;
            }

            _products.Remove(product);
            var result = _fileService.SaveToFile(JsonConvert.SerializeObject(_products, Formatting.Indented));
            _products = GetAllProductsFromList().ToList();

            return StatusCodes.Success;
        }
        catch
        {
            return StatusCodes.Failed;
        }
    }

    public StatusCodes DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }
}
