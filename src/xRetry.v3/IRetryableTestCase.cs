using Xunit.Sdk;
using Xunit.v3;

namespace xRetry
{
    public interface IRetryableTestCase : IXunitTestCase
    {
        int MaxRetries { get; }
        int DelayBetweenRetriesMs { get; }
        string[] SkipOnExceptionFullNames { get; }
    }
}
