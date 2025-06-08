using System;
using System.Collections.Generic;
using Xunit.Sdk;
using Xunit.v3;

namespace xRetry
{
    public class RetryTheoryDiscoverer : TheoryDiscoverer
    {
        public RetryTheoryDiscoverer(IMessageSink diagnosticMessageSink)
        { }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForDataRow(
            ITestFrameworkDiscoveryOptions discoveryOptions,
            ITestMethod testMethod,
            IAttributeInfo theoryAttribute,
            object[] dataRow)
        {
            int maxRetries = theoryAttribute.GetNamedArgument<int>(nameof(RetryTheoryAttribute.MaxRetries));
            int delayBetweenRetriesMs =
                theoryAttribute.GetNamedArgument<int>(nameof(RetryTheoryAttribute.DelayBetweenRetriesMs));
            Type[] skipOnExceptions =
                theoryAttribute.GetNamedArgument<Type[]>(nameof(RetryTheoryAttribute.SkipOnExceptions));
            return new[]
            {
                new RetryTestCase(
                    DiagnosticMessageSink,
                    discoveryOptions.MethodDisplayOrDefault(),
                    discoveryOptions.MethodDisplayOptionsOrDefault(),
                    testMethod,
                    maxRetries,
                    delayBetweenRetriesMs,
                    skipOnExceptions,
                    dataRow)
            };
        }

        protected override IEnumerable<IXunitTestCase> CreateTestCasesForTheory(
            ITestFrameworkDiscoveryOptions discoveryOptions, ITestMethod testMethod, IAttributeInfo theoryAttribute)
        {
            int maxRetries = theoryAttribute.GetNamedArgument<int>(nameof(RetryTheoryAttribute.MaxRetries));
            int delayBetweenRetriesMs =
                theoryAttribute.GetNamedArgument<int>(nameof(RetryTheoryAttribute.DelayBetweenRetriesMs));
            Type[] skipOnExceptions =
                theoryAttribute.GetNamedArgument<Type[]>(nameof(RetryTheoryAttribute.SkipOnExceptions));

            return new[]
            {
                new RetryTheoryDiscoveryAtRuntimeCase(DiagnosticMessageSink, discoveryOptions.MethodDisplayOrDefault(),
                    discoveryOptions.MethodDisplayOptionsOrDefault(), testMethod, maxRetries, delayBetweenRetriesMs, skipOnExceptions)
            };
        }
    }
}
