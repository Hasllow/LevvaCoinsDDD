﻿using System.Diagnostics.CodeAnalysis;

namespace LevvaCoinsDDD.Domain.Models;
[ExcludeFromCodeCoverage]
public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string AvatarUrl { get; set; }
    public virtual ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
