using ROCPOP.Client.Models.DomainModels.DTO;
using ROCPOP.Client.Models.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.DomainModels.Service
{
    public class ProductService
    {
        private ProductsRepository rep;

        public ProductService()
        {
            rep = new ProductsRepository();
        }
        public List<ProductDTO> GetProducts()
        {
            List<ProductDTO> dto = rep.GetProducts();
            return dto;
        }
        public ProductDTO GetProductById(int id)
        {
            ProductDTO dto = rep.GetProduct(id);
            return dto;
        }
    }
}
