using System.Diagnostics.CodeAnalysis;

namespace LevvaCoinsDDD.Domain.Models;
[ExcludeFromCodeCoverage]
public class Category : BaseEntity
{
    public string Description { get; set; }
}
