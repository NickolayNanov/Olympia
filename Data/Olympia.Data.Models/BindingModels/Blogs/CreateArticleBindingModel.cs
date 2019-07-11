namespace Olympia.Data.Models.BindingModels.Blogs
{
    using Microsoft.AspNetCore.Http;
    using Olympia.Common;
    using System.ComponentModel.DataAnnotations;

    public class CreateArticleBindingModel
    {
        private const int TitleMaxLength = 255;
        private const int TitleMinLength = 3;


        [Required]
        [Display(Name = DisplayModelConstatnts.DisplayTitle)]
        [DataType(DataType.Text)]
        [StringLength(TitleMaxLength, ErrorMessage = ErrorConstants.ErrorInputMessage, MinimumLength = TitleMinLength)]
        public string Title { get; set; }

        [Required]
        [Display(Name = DisplayModelConstatnts.DisplayContent)]
        [DataType(DataType.Text)]
        public string Content { get; set; }

        [Display(Name = DisplayModelConstatnts.DisplayImg)]
        [DataType(DataType.Upload)]       
        public IFormFile ImgUrl { get; set; }
    }
}
