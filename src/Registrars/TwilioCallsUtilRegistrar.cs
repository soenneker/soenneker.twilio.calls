using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Twilio.Calls.Abstract;
using Soenneker.Twilio.Client.Registrars;

namespace Soenneker.Twilio.Calls.Registrars;

/// <summary>
/// A utility library for Twilio call related operations
/// </summary>
public static class TwilioCallsUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ITwilioCallsUtil"/> as a singleton service. <para/>
    /// </summary>
    public static void AddTwilioCallsUtilAsSingleton(this IServiceCollection services)
    {
        services.AddTwilioClientUtilAsSingleton();
        services.TryAddSingleton<ITwilioCallsUtil, TwilioCallsUtil>();
    }

    /// <summary>
    /// Adds <see cref="ITwilioCallsUtil"/> as a scoped service. <para/>
    /// </summary>
    public static void AddTwilioCallsUtilAsScoped(this IServiceCollection services)
    {
        services.AddTwilioClientUtilAsSingleton();
        services.TryAddScoped<ITwilioCallsUtil, TwilioCallsUtil>();
    }
}