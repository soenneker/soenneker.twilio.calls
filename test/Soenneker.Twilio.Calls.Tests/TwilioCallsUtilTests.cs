using Soenneker.Twilio.Calls.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;
using Xunit.Abstractions;

namespace Soenneker.Twilio.Calls.Tests;

[Collection("Collection")]
public class TwilioCallsUtilTests : FixturedUnitTest
{
    private readonly ITwilioCallsUtil _util;

    public TwilioCallsUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<ITwilioCallsUtil>(true);
    }
}
