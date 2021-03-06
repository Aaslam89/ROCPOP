﻿
using NPoco;
using ROCPOP.Client.Models.Repositories.DTO;
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
                        , ProductTypeId = item.producttypeid
                        , Name = item.productname
                        , Created = item.create_date
                        , ProductTypeName = item.producttypename
                        , ProductDetailSmall = item.productdetail_small
                        , PriceSmall = item.price_small
                        , CostSmall = item.cost_small
                        , ProductDetailMedium = item.productdetail_medium
                        , PriceMedium = item.price_medium
                        , CostMedium = item.cost_medium
                        , ProductDetailLarge = item.productdetail_large
                        , PriceLarge = item.price_large
                        , CostLarge = item.cost_large
                        , MonthlySalesCount = Object.ReferenceEquals(null, item.monthsalescount) ? 0 : item.monthsalescount
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
                    , ProductTypeId = d.producttypeid
                    , Name = d.productname
                    , Created = d.create_date
                    , ProductTypeName = d.producttypename
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

        public List<ProductDTO> GetBestSellers(bool topFour)
        {
            List<ProductDTO> dto = new List<ProductDTO>();
            string sql = "";
            if (topFour)
                sql += "select top 4 ";
            else
                sql += "select ";

            sql += @"
      p.productid
	, sum(qty) as qty
	, min(p.name) as productName
from
    Sales.Orders as o
join Sales.OrderDetail as d on o.orderid = d.orderid

    join Products.Product as p on d.productid = p.productid
where p.isactive = 1
group by p.productid
order by qty desc";

            using (IDatabase db = Connection)
            {
                var d = db.Fetch<dynamic>(sql);
                foreach (var item in d)
                {
                    dto.Add(new ProductDTO() {
                        Id = item.productid
                        , Name = item.productName
                    });
                }
            }
            return dto;
        }

        public List<ProductDTO> GetNewestItems(bool topFour)
        {
            List<ProductDTO> dto = new List<ProductDTO>();
            string sql = "";
            if (topFour)
                sql += "select top 4 ";
            else
                sql += "select ";

            sql += " * from Products.Product where isactive = 1 order by created desc";

            using (IDatabase db = Connection)
            {
                var d = db.Fetch<dynamic>(sql);
                foreach (var item in d)
                {
                    dto.Add(new ProductDTO() {
                        Id = item.productid
                        , ProductTypeId = item.producttypeid
                        , Name = item.name
                        , Created = item.created
                    });
                }
            }
            return dto;
        }

        public List<ProductDTO> GetProductByCategory(string category)
        {
            List<ProductDTO> dto = new List<ProductDTO>();
            string sql = String.Format(@"
select 
	p.productid
	, p.[name]
from
	Products.Product as p 
	join Products.ProductType as pp on p.producttypeid = pp.producttypeid
where
	p.isactive = 1
	and pp.[name] = '{0}'", category);

            using (IDatabase db = Connection)
            {
                var d = db.Fetch<dynamic>(sql);
                foreach (var item in d)
                {
                    dto.Add(new ProductDTO()
                    {
                        Id = item.productid,
                        Name = item.name,
                    });
                }
            }
            return dto;
        }

        public List<ProductCategoryDTO> GetProductCategories()
        {
            List<ProductCategoryDTO> dto = new List<ProductCategoryDTO>();

            string sql = String.Format(@"
select 
	categoryname = case when pp.[name] is not null then pp.[name] else 'All' end,
	categorycount = count(pp.name),
	[rank] = case when pp.[name] is null then 1 else 2 end
from
	Products.Product as p 
	join Products.ProductType as pp on p.producttypeid = pp.producttypeid
where
	p.isactive = 1
group by pp.[name]
with rollup
order by [rank], categoryname");

            using (IDatabase db = Connection)
            {
                var d = db.Fetch<dynamic>(sql);
                foreach (var item in d)
                {
                    dto.Add(new ProductCategoryDTO()
                    {
                        CategoryName = item.categoryname,
                        CategoryCount = item.categorycount,
                        Rank = item.rank
                    });
                }
            }

            return dto;
        }

        public List<string> GetCategories()
        {
            List<string> categories = new List<string>();
            string sql = String.Format(@"
select 
	pp.[name]
from
	Products.Product as p 
	join Products.ProductType as pp on p.producttypeid = pp.producttypeid
where
	p.isactive = 1
group by pp.[name]
");
            using (IDatabase db = Connection)
            {
                var d = db.Fetch<string>(sql);
                foreach (var item in d)
                {
                    categories.Add(item);
                }
            }
            return categories;
        }

    }
}
