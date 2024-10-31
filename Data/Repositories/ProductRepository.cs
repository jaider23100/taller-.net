﻿using Actividadenclase.Data.Models;
using Actividadenclase.Data.Repositories.Interfaces;
using MongoDB.Driver;

namespace Actividadenclase.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoClient client)
        {
            var database = client.GetDatabase("Empanadasdb");
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _products.Find(product => true).ToListAsync();
        }      
        

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            return await _products.Find(product => product.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateProductAsync(Product product)
        {
            await _products.InsertOneAsync(product);
        }

        public async Task<bool> UpdateProductAsync(string id, Product product)
        {
            var result = await _products.ReplaceOneAsync(p => p.Id == id, product);
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProductAsync(string id)
        {
            var result = await _products.DeleteOneAsync(p => p.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

       
    }


}
