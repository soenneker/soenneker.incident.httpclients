using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Incident.HttpClients.Abstract;

/// <summary>
/// A cached HTTP transport for Incident. Authentication is attached by the OpenAPI client utility.
/// </summary>
public interface IIncidentOpenApiHttpClient: IDisposable, IAsyncDisposable
{
    /// <summary>Gets the shared HTTP transport using Incident:ClientBaseUrl. Configure the base URL before first use.</summary>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
