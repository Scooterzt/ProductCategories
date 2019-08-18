using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace ProductCategories.Models{
    public class DisplayProductView{
        public Product product {get;set;}
        public List<Categoria> categorias {get;set;}
        public int CategoriaId {get;set;}
    }
}