using System.Net.Mail;

namespace ExpensesTracker.Domain.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhitespace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static bool IsValidEmail(this string email)
    {
        try
        {
            var trimmedEmail = email.Trim();
            var addr = new MailAddress(email);
            return addr.Address == trimmedEmail;
        }
        catch
        {
            return false;
        }
    }
}