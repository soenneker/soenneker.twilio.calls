[![](https://img.shields.io/nuget/v/soenneker.twilio.calls.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.twilio.calls/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.twilio.calls/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.twilio.calls/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.twilio.calls.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.twilio.calls/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.twilio.calls/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.twilio.calls/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Twilio.Calls
Retrieves Twilio calls by destination phone number and optional start-time range.

## Installation

```bash
dotnet add package Soenneker.Twilio.Calls
```

## Configuration

```json
{
  "Twilio": {
    "AccountSid": "AC...",
    "AuthToken": "your-auth-token"
  }
}
```

## Registration

```csharp
using Soenneker.Twilio.Calls.Registrars;

services.AddTwilioCallsUtilAsScoped();
```

The scoped calls utility reuses the singleton Twilio client initializer. Singleton registration is also available through `AddTwilioCallsUtilAsSingleton()`.

## Get calls for one number

```csharp
using Soenneker.Twilio.Calls.Abstract;
using Twilio.Rest.Api.V2010.Account;

List<CallResource> calls = await callsUtil.GetAllCallsForNumber(
    phoneNumber: "+15551234567",
    startTimeAfter: DateTimeOffset.UtcNow.AddDays(-7),
    startTimeBefore: DateTimeOffset.UtcNow,
    cancellationToken);
```

`phoneNumber` maps to Twilio's `To` filter. The method returns calls delivered to that destination; it does not combine calls where the number appears only in `From`.

## Group calls for several numbers

```csharp
Dictionary<string, List<CallResource>> callsByNumber =
    await callsUtil.GetAllCallsForNumbersSplitByNumber(
        new[] { "+15551234567", "+15557654321" },
        startTimeAfter: DateTimeOffset.UtcNow.AddDays(-30),
        cancellationToken: cancellationToken);
```

Each number causes a separate Twilio query. These methods materialize the complete result set and may traverse multiple API pages, so use a bounded time range for accounts with substantial call history.

Cancellation is checked before requests and while results are materialized. The Twilio SDK's call-history `ReadAsync` API does not accept a cancellation token, so an individual in-flight SDK request cannot be interrupted by this wrapper.
