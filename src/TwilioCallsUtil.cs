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
public class TwilioCallsUtil: ITwilioCallsUtil
{
    private readonly ITwilioClientUtil _twilioClientUtil;

    public TwilioCallsUtil(ITwilioClientUtil twilioClientUtil)
    {
        _twilioClientUtil = twilioClientUtil;
    }

    public async ValueTask<List<CallResource>> GetAllCallsForNumber(string phoneNumber, DateTime? startTimeAfter = null, DateTime? startTimeBefore = null, CancellationToken cancellationToken = default)
    {
        await _twilioClientUtil.Init(cancellationToken).NoSync();

        ResourceSet<CallResource>? result = await CallResource.ReadAsync(
            to: new PhoneNumber(phoneNumber),
            startTimeAfter: startTimeAfter,
            startTimeBefore: startTimeBefore).NoSync();

        return result.ToList();
    }

    public async ValueTask<Dictionary<string, List<CallResource>>> GetAllCallsForNumbersSplitByNumber(IEnumerable<string> phoneNumbers, DateTime? startTimeAfter = null, DateTime? startTimeBefore = null, CancellationToken cancellationToken = default)
    {
        await _twilioClientUtil.Init(cancellationToken).NoSync();

        var callsByNumber = new Dictionary<string, List<CallResource>>();

        foreach (string phoneNumber in phoneNumbers)
        {
            ResourceSet<CallResource>? calls = await CallResource.ReadAsync(
                to: new PhoneNumber(phoneNumber),
                startTimeAfter: startTimeAfter,
                startTimeBefore: startTimeBefore).NoSync();

            callsByNumber[phoneNumber] = calls.ToList();
        }

        return callsByNumber;
    }
}