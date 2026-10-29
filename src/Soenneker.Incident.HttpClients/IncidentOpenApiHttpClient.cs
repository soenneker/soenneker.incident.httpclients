using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Incident.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Incident.HttpClients;

public sealed class IncidentOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration configuration) : IIncidentOpenApiHttpClient
{
    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        string? configuredUrl = configuration["Incident:ClientBaseUrl"] ?? "https://api.incident.io";
        if (!Uri.TryCreate(configuredUrl, UriKind.Absolute, out Uri? baseAddress) ||
            baseAddress.Scheme != Uri.UriSchemeHttps || !string.IsNullOrEmpty(baseAddress.UserInfo) ||
            !string.IsNullOrEmpty(baseAddress.Query) || !string.IsNullOrEmpty(baseAddress.Fragment))
            throw new InvalidOperationException("Incident:ClientBaseUrl must be an absolute HTTPS API base URL without credentials, query, or fragment.");

        return httpClientCache.Get(nameof(IncidentOpenApiHttpClient), baseAddress, static address => new HttpClientOptions
        {
            BaseAddress = new Uri(address.AbsoluteUri.TrimEnd('/') + "/"),
            DefaultRequestHeaders = new Dictionary<string, string> { ["Accept"] = "application/json" }
        }, cancellationToken);
    }

    public void Dispose() => httpClientCache.RemoveSync(nameof(IncidentOpenApiHttpClient));

    public ValueTask DisposeAsync() => httpClientCache.Remove(nameof(IncidentOpenApiHttpClient));
}
