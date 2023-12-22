namespace CurrencyService.Config;

public class AccessTokenSettings
{
    public AccessTokenSettings() { }

    public AccessTokenSettings(string issuer, string audience, long lifeTimeInSeconds, string key)
    {
        Issuer = issuer;
        Audience = audience;
        LifeTimeInSeconds = lifeTimeInSeconds;
        Key = key;
    }

    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public long LifeTimeInSeconds { get; set; }
    public string Key { get; set; } = string.Empty;
}