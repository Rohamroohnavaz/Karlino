using System;
using System.Text;
using System.Text.Json;

namespace FinalProject_MVC.Helpers
{
    public static class JwtHelper
    {
        public static string? GetRoleFromJwt(string? token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            try
            {
                var parts = token.Split('.');

                if (parts.Length < 2)
                    return null;

                var payload = parts[1]
                    .Replace('-', '+')
                    .Replace('_', '/');

                switch (payload.Length % 4)
                {
                    case 2: payload += "=="; break;
                    case 3: payload += "="; break;
                }

                var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));

                using var doc = JsonDocument.Parse(json);

                if (doc.RootElement.TryGetProperty("role", out var roleElement))
                    return roleElement.GetString();

                if (doc.RootElement.TryGetProperty("Role", out var roleElement2))
                    return roleElement2.GetString();

                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}