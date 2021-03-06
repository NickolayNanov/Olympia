﻿namespace Olympia.Data.Domain
{
    using Olympia.Data.Common.Models;

    using System;
    using System.ComponentModel.DataAnnotations;

    public class Article : BaseModel<int>
    {
        public Article()
        {
            this.CreatedOn = DateTime.UtcNow;
        }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Title { get; set; }

        public int TimesRead { get; set; }

        public string ImgUrl { get; set; }
        public OlympiaUser Author { get; set; }

        public string AuthorId { get; set; }
    }
}
