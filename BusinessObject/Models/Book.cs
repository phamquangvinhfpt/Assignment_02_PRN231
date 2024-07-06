﻿using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public string? Type { get; set; }

    public int? PubId { get; set; }

    public decimal? Price { get; set; }

    public decimal? Advance { get; set; }

    public decimal? Royalty { get; set; }

    public int? YtdSales { get; set; }

    public string? Notes { get; set; }

    public DateOnly? PublishedDate { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    public virtual Publisher? Pub { get; set; }
}
