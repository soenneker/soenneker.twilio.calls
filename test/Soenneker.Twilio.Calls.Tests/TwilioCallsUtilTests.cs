using Soenneker.Twilio.Calls.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Twilio.Calls.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class TwilioCallsUtilTests : HostedUnitTest
{
    private readonly ITwilioCallsUtil _util;

    public TwilioCallsUtilTests(Host host) : base(host)
    {
        _util = Resolve<ITwilioCallsUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
