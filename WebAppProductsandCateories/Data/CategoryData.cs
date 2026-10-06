using WebAppProductsandCateories.Models;
using Microsoft.Data.SqlClient;

namespace WebAppProductsandCateories.Data
{
    public class CategoryData : DataAccess
    {
        public CategoryData(IConfiguration configuration) : base(configuration)
        {
        }

        // Obtener todas las categorías
        public List<CategoriesModel> GetCategories()
        {
            var categories = new List<CategoriesModel>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Categories", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new CategoriesModel
                        {
                            CategoryId = (int)reader["CategoryId"],
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }
            return categories;
        }

        // Crear una nueva categoría
        public void CreateCategory(CategoriesModel category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Categories (Name) VALUES (@Name)", connection);
                command.Parameters.AddWithValue("@Name", category.Name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Actualizar una categoría existente por ID
        public void UpdateCategory(CategoriesModel category)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Categories SET Name = @Name WHERE CategoryId = @CategoryId", connection);
                command.Parameters.AddWithValue("@CategoryId", category.CategoryId);
                command.Parameters.AddWithValue("@Name", category.Name);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Borrar una categoría por ID
        public void DeleteCategory(int categoryId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM Categories WHERE CategoryId = @CategoryId", connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        // Obtener una única categoría por ID
        public CategoriesModel? GetCategoryById(int categoryId)
        {
            CategoriesModel? category = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Categories WHERE CategoryId = @CategoryId", connection);
                command.Parameters.AddWithValue("@CategoryId", categoryId);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        category = new CategoriesModel
                        { 
                            CategoryId = (int)reader["CategoryId"],
                            Name = reader["Name"].ToString()

                        };
                    }
                }
            }
            return category;
        }
    }
}
