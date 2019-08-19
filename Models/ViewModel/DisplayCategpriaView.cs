using System.Collections.Generic;
namespace ProductCategories.Models{
    public class DisplayCategoriaView{
        public Categoria categoria {get;set;}
        public List<Product> products {get;set;}
        public int ProductId {get;set;}
    }
}