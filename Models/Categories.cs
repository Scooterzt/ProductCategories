using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace ProductCategories.Models{
    public class Categoria {
        public int CategoriaId{get;set;}
        [Required]
        public string Name {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Association> Associations {get;set;}
    }

}