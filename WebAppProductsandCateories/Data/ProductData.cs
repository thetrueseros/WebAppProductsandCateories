using WebAppProductsandCateories.Models;
using Microsoft.Data.SqlClient;

namespace WebAppProductsandCateories.Data
{
    public class ProductData: DataAccess
    {
        public ProductData(IConfiguration configuration) : base(configuration)
        {
        }

        // Leer todos los productos
        public List<ProductModel> GetProducts()
        {
            var products = new List<ProductModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        products.Add(new ProductModel
                        {
                            ProductId = (int)reader["ProductId"],
                            Name = reader["Name"].ToString(),
                            Price = (decimal)reader["Price"],
                            CategoryId = (int)reader["CategoryId"]
                        });
                    }
                }
            }
            return products;
        }

        // Crear un producto nuevo
        public void CreateProduct(ProductModel product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Products (Name, Price, CategoryId) VALUES (@Name, @Price, @CategoryId)", connection);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Actualizar un producto existente
        public void UpdateProduct(ProductModel product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Products SET Name = @Name, " +
                                                                 "Price = @Price, " +
                                                                 "CategoryId = @CategoryId " +
                                             "WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", product.ProductId);
                command.Parameters.AddWithValue("@Name", product.Name);
                command.Parameters.AddWithValue("@Price", product.Price);
                command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Borrar un producto por su ID
        public void DeleteProduct(int productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Products WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", productId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Obtener un único producto por su ID
        public ProductModel? GetProductById(int productId)
        {
            ProductModel? product = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Products WHERE ProductId = @ProductId", connection);
                command.Parameters.AddWithValue("@ProductId", productId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new ProductModel
                        {
                            ProductId = (int)reader["ProductId"],
                            Name = reader["Name"].ToString(),
                            Price = (decimal)reader["Price"],
                            CategoryId = (int)reader["CategoryId"]
                        };
                    }
                }
            }
            return product;
        })
    }
}
