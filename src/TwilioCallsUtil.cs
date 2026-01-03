using Soenneker.Twilio.Calls.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.Base;
using Soenneker.Twilio.Client.Abstract;
using Soenneker.Extensions.ValueTask;
using Soenneker.Extensions.Task;
using System.Linq;
using System.Threading;

namespace Soenneker.Twilio.Calls;

/// <inheritdoc cref="ITwilioCallsUtil"/>
public sealed class TwilioCallsUtil: ITwilioCallsUtil
{
    private readonly ITwilioClientUtil _twilioClientUtil;

    public TwilioCallsUtil(ITwilioClientUtil twilioClientUtil)
    {
        _twilioClientUtil = twilioClientUtil;
    }

    public async ValueTask<List<CallResource>> GetAllCallsForNumber(string phoneNumber, DateTimeOffset? startTimeAfter = null, DateTimeOffset? startTimeBefore = null, 
        CancellationToken cancellationToken = default)
    {
        await _twilioClientUtil.Init(cancellationToken).NoSync();

        ResourceSet<CallResource>? result = await CallResource.ReadAsync(
            to: new PhoneNumber(phoneNumber),
            startTimeAfter: startTimeAfter?.UtcDateTime,
            startTimeBefore: startTimeBefore?.UtcDateTime).NoSync();

        return result.ToList();
    }

    public async ValueTask<Dictionary<string, List<CallResource>>> GetAllCallsForNumbersSplitByNumber(IEnumerable<string> phoneNumbers, DateTimeOffset? startTimeAfter = null, 
        DateTimeOffset? startTimeBefore = null, CancellationToken cancellationToken = default)
    {
        await _twilioClientUtil.Init(cancellationToken).NoSync();

        var callsByNumber = new Dictionary<string, List<CallResource>>();

        foreach (string phoneNumber in phoneNumbers)
        {
            ResourceSet<CallResource>? calls = await CallResource.ReadAsync(
                to: new PhoneNumber(phoneNumber),
                startTimeAfter: startTimeAfter?.UtcDateTime,
                startTimeBefore: startTimeBefore?.UtcDateTime).NoSync();

            callsByNumber[phoneNumber] = calls.ToList();
        }

        return callsByNumber;
    }
}