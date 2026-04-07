namespace WebUI.Helpers;

public static class PhoneNumberHelper
{
    public static string Normalize(string? phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
        {
            return string.Empty;
        }

        return new string(phoneNumber.Where(char.IsDigit).ToArray());
    }

    public static string Format(string? phoneNumber)
    {
        var digits = Normalize(phoneNumber);

        if (digits.Length == 10)
        {
            return $"({digits[..2]}){digits.Substring(2, 4)}-{digits.Substring(6, 4)}";
        }

        if (digits.Length == 11)
        {
            return $"({digits[..2]}){digits.Substring(2, 5)}-{digits.Substring(7, 4)}";
        }

        return phoneNumber ?? string.Empty;
    }
}
