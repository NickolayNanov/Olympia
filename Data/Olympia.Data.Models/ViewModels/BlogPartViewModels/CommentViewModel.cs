namespace Olympia.Data.Models.ViewModels.BlogPartViewModels
{
    using Olympia.Common;
    using Olympia.Data.Common.Models;
    using Olympia.Data.Domain;

    using System.ComponentModel.DataAnnotations;

    public class CommentViewModel : BaseModel<int>
    {
        private const int CommentMaxLength = 255;
        private const int CommentMinLength = 5;


        [Required]
        [StringLength(CommentMaxLength, ErrorMessage = GlobalConstants.CommentLengthMessage, MinimumLength = CommentMinLength)]
        public string Content { get; set; }

        public int ArticleId { get; set; }

        public ArticleViewModel Article { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public OlympiaUser Author { get; set; }
    }
}
