﻿namespace ProCards.DAL.Models;

public class Card: ModelBase
{
    public string? FirstSide { get; set; }
    
    public string? SecondSide { get; set; }
    
    public Category Category { get; set; }
    
    public DateTime? PublishedAt { get; set; }
}