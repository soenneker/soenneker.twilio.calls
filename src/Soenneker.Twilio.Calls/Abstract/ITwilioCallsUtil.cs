using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using Twilio.Rest.Api.V2010.Account;

namespace Soenneker.Twilio.Calls.Abstract;

/// <summary>
/// A utility library for Twilio call related operations
/// </summary>
public interface ITwilioCallsUtil
{
    ValueTask<List<CallResource>> GetAllCallsForNumber(string phoneNumber, DateTimeOffset? startTimeAfter = null, DateTimeOffset? startTimeBefore = null, CancellationToken cancellationToken = default);

    ValueTask<Dictionary<string, List<CallResource>>> GetAllCallsForNumbersSplitByNumber(IEnumerable<string> phoneNumbers, DateTimeOffset? startTimeAfter = null, DateTimeOffset? startTimeBefore = null, CancellationToken cancellationToken = default);
}
