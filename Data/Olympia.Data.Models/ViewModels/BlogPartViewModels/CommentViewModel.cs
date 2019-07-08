namespace Olympia.Data.Models.ViewModels.BlogPartViewModels
{
    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain;
    using System.ComponentModel.DataAnnotations;

    public class CommentViewModel : BaseModel<int>
    {

        [Required]
        [StringLength(maximumLength: 255, MinimumLength = 5, ErrorMessage = "The content's lenght must be between 5 and 255 characters long.")]
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public ArticleViewModel Article { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public OlympiaUser Author { get; set; }
    }
}
