using BookStore.Helper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [MyCustomValidationAttribute]
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        public int LanguageId { get; set; }
        public string Language { get; set; }
        [Required]
        public int TotalPages { get; set; }
        [Required]
        [Display(Name = "Choose the cover photo of your book")]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }
        [Required]
        [Display(Name = "Choose the photo of your gallery")]
        public IFormFileCollection GalleryFiles { get; set; } 
        public List<GalleryModel> Gallery { get; set; }
        [Required]
        [Display(Name = "Choose the PDF file of your book")]
        public IFormFile BookPDF { get; set; }
        public string BookPDFUrl { get; set; }
    }
}
