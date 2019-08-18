using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
namespace ProductCategories.Models{
    public class Product{
        public int ProductId{get;set;}
        [Required]
        public string Name{get;set;}
        [MinLength(10)]
        public string Description{get;set;}
        [Required]
        [Range(0.0,1000000.00)]
        public double Price {get;set;}
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        public List<Association> Associations {get;set;}
    }

}