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
    /// <summary>
    /// Gets all calls for number.
    /// </summary>
    /// <param name="phoneNumber">The phone number.</param>
    /// <param name="startTimeAfter">The start time after.</param>
    /// <param name="startTimeBefore">The start time before.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<List<CallResource>> GetAllCallsForNumber(string phoneNumber, DateTimeOffset? startTimeAfter = null, DateTimeOffset? startTimeBefore = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets all calls for numbers split by number.
    /// </summary>
    /// <param name="phoneNumbers">The phone numbers.</param>
    /// <param name="startTimeAfter">The start time after.</param>
    /// <param name="startTimeBefore">The start time before.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<Dictionary<string, List<CallResource>>> GetAllCallsForNumbersSplitByNumber(IEnumerable<string> phoneNumbers, DateTimeOffset? startTimeAfter = null, DateTimeOffset? startTimeBefore = null, CancellationToken cancellationToken = default);
}
