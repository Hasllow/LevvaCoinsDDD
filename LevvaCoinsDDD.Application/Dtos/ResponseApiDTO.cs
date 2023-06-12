namespace LevvaCoinsDDD.Application.Dtos;
public class ResponseApiDTO<T>
{
    public bool hasError { get; set; }
    public string? message { get; set; }
    public T? data { get; set; }
    public IEnumerable<T>? collectionData { get; set; } = null!;

}