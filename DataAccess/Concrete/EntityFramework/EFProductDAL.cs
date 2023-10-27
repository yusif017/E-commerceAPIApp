﻿using Core.DataAccess.EntitFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EFProductDAL : EFRepositoryBase<Product, AppDbContext>, IProductDAL
    {
        public List<Product> GetFeaturedProducts()
        {
            using var context = new AppDbContext();
            var products = context.Products
                .Where(x => x.IsFeatured == true && x.Status == true)
                .OrderByDescending(x => x.CreatedDate).Take(8).ToList();
            return products;
        }

        public Product GetProduct(int id)
        {
            using var context = new AppDbContext();
            var product = context.Products
                .Include(x => x.Specifications)
                .Include(x => x.Category).SingleOrDefault(p => p.Id == id);

            return product;
        }
        public List<Product> GetRecentProducts()
        {
            using var context = new AppDbContext();
            var products = context.Products
                .Where(x => x.Status == true)
                .OrderByDescending(x => x.CreatedDate).Take(8).ToList();
            return products;
        }

        public void RemoveProductCount(List<ProductDecrementQuantityDTO> productDecrementQuantityDTOs)
        {
            using var context = new AppDbContext();

            for (int i = 0; i < productDecrementQuantityDTOs.Count; i++)
            {
                var products = context.Products.FirstOrDefault(x => x.Id == productDecrementQuantityDTOs[i].ProductId);
                products.Quantity -= productDecrementQuantityDTOs[i].Quantity;
                context.Products.Update(products);
                context.SaveChanges();
            }
        }
    }
}
