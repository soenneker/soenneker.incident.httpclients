using Soenneker.Incident.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Incident.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class IncidentOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IIncidentOpenApiHttpClient _httpclient;

    public IncidentOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IIncidentOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
