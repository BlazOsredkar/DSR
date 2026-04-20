using System.Text.Json;

namespace _1_test.Infrastructure;

public static class SessionExtensions
{
    // Naloga: PRG med koraki s shranjevanjem podatkov (navodilo: "Pri preusmerjanju upostevajte vzorec PRG").
    public static void SetObject<T>(this ISession session, string key, T value)
    {
        session.SetString(key, JsonSerializer.Serialize(value));
    }

    public static T? GetObject<T>(this ISession session, string key)
    {
        var json = session.GetString(key);
        if (string.IsNullOrWhiteSpace(json))
        {
            return default;
        }

        return JsonSerializer.Deserialize<T>(json);
    }
}
