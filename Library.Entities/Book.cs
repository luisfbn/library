﻿using System.ComponentModel.DataAnnotations;

namespace Library.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; }

    }
}
