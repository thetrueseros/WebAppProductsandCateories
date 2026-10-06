using System.ComponentModel;

namespace WebAppProductsandCateories.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
            public string? Name { get; set; }
            public string? Description { get; set; }
            public decimal Price { get; set; }
        public int CategoryId { get; set; } // FK para category
        public CategoriesModel? Category { get; set; } // Navigation property

    }
}
