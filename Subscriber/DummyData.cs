using System;
using System.Collections.Generic;
using System.Text;

namespace Subscriber
{
    public class DummyData
    {

        public Warehouse getDummyWarehouses(string country) {
            foreach (var warehouse in Warehouses)
            {
                if (warehouse.Country.ToLower().Equals(country.ToLower()))
                {
                    return warehouse;
                }
            }

            return null;
        }

        List<Warehouse> Warehouses = new List<Warehouse>
        {
            new Warehouse(){ Products = new List<Product>
                {
                    new Product(){ ProdID = 1, count =  3},
                    new Product() { ProdID = 2, count = 1},
                    new Product() { ProdID = 3, count = 4},
                    new Product() { ProdID = 4, count = 2},
                    new Product() { ProdID = 5, count = 3}
                },
                Country = "DK"
            },
            new Warehouse(){ Products = new List<Product>
                {
                    new Product(){ ProdID = 2, count =  1},
                    new Product() { ProdID = 3, count = 1},
                    new Product() { ProdID = 4, count = 4}
                },
                Country = "EN"
            },
            new Warehouse(){ Products = new List<Product>
                {
                    new Product(){ ProdID = 1, count =  5},
                    new Product() { ProdID = 5, count = 7}
                },
                Country = "IR"
            }
        };


    }
}
