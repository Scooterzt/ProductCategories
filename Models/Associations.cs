using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace ProductCategories.Models{
    public class Association{
        [Key]
        public int AssociationId{get;set;}
        public int ProductId {get;set;}
        public int CategoriaId {get;set;}
        public Product Product {get;set;}
        public Categoria Categoria {get;set;}
    }
}