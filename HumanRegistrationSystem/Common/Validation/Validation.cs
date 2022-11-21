namespace Common.Validation;

public static class Validation
{
    public static bool CheckIfNull(object responce)
    {
        return string.IsNullOrWhiteSpace(responce.ToString());
    }
}