
using NPoco;
using ROCPOP.Client.Models.DomainModels.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ROCPOP.Client.Models.Repositories.Concrete
{
    public class ProductsRepository
    {
        private string connectionString;
        public ProductsRepository()
        {
            connectionString = @"Server=MIMSY;Database=RocCityPopCorn;Trusted_Connection=True;";
        }

        public IDatabase Connection
        {
            get
            {
                return new Database(connectionString, DatabaseType.SqlServer2012, SqlClientFactory.Instance);
            }
        }

        public List<ProductDTO> GetProducts()
        {
            List<ProductDTO> dto = new List<ProductDTO>();
            string sql = String.Format(@";with productsalescount(productid, qty)
                              as
                              (select 
                            		productid, sum(qty)
                               from 
                            		Sales.OrderDetail d
                            		join Sales.Orders o on d.orderid = o.orderid
                            	where 
                            		o.orderdate between DATEADD(month, -1, getdate()) and getdate() 
                            		group by productid
                            	)
                            select 
                            	p.productid
                            	, productname = p.name
                            	, p.producttypeid
                            	, producttypename = t.name
                            	, productdetail_small = min(case when d.size = 'SM' then  d.description end)
                            	, price_small = min(case when d.size = 'SM' then  d.price end)
                            	, cost_small = min(case when d.size = 'SM' then  d.cost end)
                            	, productdetail_medium = min(case when d.size = 'MD' then  d.description end)
                            	, price_medium = min(case when d.size = 'MD' then  d.price end)
                            	, cost_medium = min(case when d.size = 'MD' then  d.cost end)
                            	,productdetail_large = min(case when d.size = 'LG' then  d.description end)
                            	, price_large = min(case when  d.size = 'LG' then  d.price end)
                            	, cost_large = min(case when d.size = 'LG' then  d.cost end)
                            	, create_date = min(created)
                            	, monthsalescount = min(c.qty)
                            from products.product as p
                            	join products.ProductType as t on p.producttypeid = t.producttypeid
                            	join products.Detail as d on p.productid = d.productid
                            	left join productsalescount c on p.productid = c.productid
                            where 
                            	p.isactive = 1
                            group by p.productid, p.producttypeid, p.name, t.name");
            using (IDatabase db = Connection)
            {
                var d = db.Fetch<dynamic>(sql);
                foreach (var item in d)
                {
                    dto.Add(new ProductDTO() {
                        Id = item.productid
                        ,ProductTypeId = item.producttypeid
                        ,Name = item.productname
                        ,Created = item.create_date
                        ,ProductTypeName = item.producttypename
                        , ProductDetailSmall = item.productdetail_small
                        , PriceSmall = item.price_small
                        , CostSmall = item.cost_small
                        , ProductDetailMedium = item.productdetail_medium
                        , PriceMedium = item.price_medium
                        , CostMedium = item.cost_medium
                        , ProductDetailLarge = item.productdetail_large
                        , PriceLarge = item.price_large
                        , CostLarge = item.cost_large
                        , MonthlySalesCount = item.monthsalescount
                    });
                }
            }
            return dto;
        }

        public ProductDTO GetProduct(int prodID)
        {
            ProductDTO dto;
            string sql = String.Format(@"
select 
    p.productid
    , productname = p.name
    , p.producttypeid
    , producttypename = t.name
    , productdetail_small = min(case when d.size = 'SM' then  d.description end)
    , price_small = min(case when d.size = 'SM' then  d.price end)
    , cost_small = min(case when d.size = 'SM' then  d.cost end)
    , productdetail_medium = min(case when d.size = 'MD' then  d.description end)
    , price_medium = min(case when d.size = 'MD' then  d.price end)
    , cost_medium = min(case when d.size = 'MD' then  d.cost end)
    ,productdetail_large = min(case when d.size = 'LG' then  d.description end)
    , price_large = min(case when  d.size = 'LG' then  d.price end)
    , cost_large = min(case when d.size = 'LG' then  d.cost end)
    , create_date = min(created)
from products.product as p
    join products.ProductType as t on p.producttypeid = t.producttypeid
    join products.Detail as d on p.productid = d.productid
where 
    p.isactive = 1
    and p.productid = {0}
group by p.productid, p.producttypeid, p.name, t.name", prodID);
            using (IDatabase db = Connection)
            {
                var d = db.Single<dynamic>(sql);
                    dto = new ProductDTO()
                    {
                        Id = d.productid
                        ,ProductTypeId = d.producttypeid
                        ,Name = d.productname
                        ,Created = d.create_date
                        ,ProductTypeName = d.producttypename
                        , ProductDetailSmall = d.productdetail_small
                        , PriceSmall = d.price_small
                        , CostSmall = d.cost_small
                        , ProductDetailMedium = d.productdetail_medium
                        , PriceMedium = d.price_medium
                        , CostMedium = d.cost_medium
                        , ProductDetailLarge = d.productdetail_large
                        , PriceLarge = d.price_large
                        , CostLarge = d.cost_large
                    };
            }
            return dto;
        }
    }
}
