using Microsoft.Extensions.Configuration;

namespace Services.Email;

public class ElasticEmailCredentials(IConfiguration configuration)
{
    public ElasticEmailApiCredentials Api => new(configuration);
    public ElasticEmailSmtpCredentials Smtp => new(configuration);
}

public class ElasticEmailApiCredentials(IConfiguration configuration)
{
    public string Key => configuration["ElasticEmail:Api:Key"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:ApiKey'");
    public string Url => configuration["ElasticEmail:Api:Url"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:ApiUrl'");
    public string FromEmail => configuration["ElasticEmail:Api:FromEmail"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:FromEmail'");
    public string FromName => configuration["ElasticEmail:Api:FromName"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:FromName'");
}

public class ElasticEmailSmtpCredentials(IConfiguration configuration)
{
    public string DisplayName => configuration["ElasticEmail:Smtp:DisplayName"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:Smtp:DisplayName'");
    public string From => configuration["ElasticEmail:Smtp:From"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:Smtp:From'");
    public string Password => configuration["ElasticEmail:Smtp:Password"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:Smtp:Password'");
    public string Port => configuration["ElasticEmail:Smtp:Port"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:Smtp:Port'");
    public string Server => configuration["ElasticEmail:Smtp:Server"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:Smtp:Server'");
    public string Username => configuration["ElasticEmail:Smtp:Username"]
        ?? throw new ArgumentException($"{nameof(configuration)} does not contain a value for 'ElasticEmail:Smtp:Username'");
}