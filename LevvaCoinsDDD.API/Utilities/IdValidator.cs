namespace LevvaCoinsDDD.API.Utilities;

public static class IdValidator
{
    public static bool IsValidIdFormat(string id) => Guid.TryParse(id, out _);

}
