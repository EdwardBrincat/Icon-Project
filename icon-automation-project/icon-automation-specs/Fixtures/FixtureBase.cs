using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using Icon_Automation_Libs.Exceptions;
using Icon_Automation_Libs.Models;
using Icon_Automation_Libs.Scenario;
using NUnit.Framework;
using Polly;
using Polly.Retry;

namespace Icon_Automation_Libs.Fixtures;

public class FixtureBase
{
    protected readonly AsyncRetryPolicy AssertNullRetryPolicyAsync;
    protected readonly AsyncRetryPolicy AssertOrUnexpectedRetryPolicyAsync;
    protected readonly AsyncRetryPolicy AssertRetryPolicyAsync;
    protected readonly AsyncRetryPolicy AssertOrStepTestFailureRetryPolicyAsync;
    protected readonly AsyncRetryPolicy ExponentialBackOffRetryPolicyAsync;
    protected readonly AsyncRetryPolicy RetryPolicyAsync;
    protected readonly AsyncRetryPolicy RetryPolicyRealTimeAsync;
    protected readonly AsyncRetryPolicy RetryPolicyDataAsync;

    protected readonly RetryPolicy ExponentialBackOffRetryPolicy;
    protected readonly RetryPolicy RetryPolicy;
    protected readonly RetryPolicy AssertRetryPolicy;
    protected readonly RetryPolicy RetryPolicyRealTime;
    protected readonly RetryPolicy RetryPolicyData;

    public FixtureBase(
        ConfigModel config,
        IScenarioContext scenarioContext
    )
    {
        ScenarioContext = scenarioContext;

        RetryPolicyAsync = Policy
            .Handle<TestStepFailureException>()
            .WaitAndRetryAsync(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        AssertRetryPolicyAsync = Policy
            .Handle<AssertionException>()
            .WaitAndRetryAsync(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        RetryPolicyRealTimeAsync = Policy
            .Handle<TestStepFailureException>()
            .WaitAndRetryAsync(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        RetryPolicyRealTime = Policy
            .Handle<TestStepFailureException>()
            .WaitAndRetry(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        RetryPolicyDataAsync = Policy
            .Handle<TestStepFailureException>()
            .WaitAndRetryAsync(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        RetryPolicyData = Policy
            .Handle<TestStepFailureException>()
            .WaitAndRetry(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        AssertOrUnexpectedRetryPolicyAsync = Policy
            .Handle<AssertionException>()
            .Or<TestStepFailureException>()
            .WaitAndRetryAsync(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        AssertNullRetryPolicyAsync = Policy
            .Handle<NullReferenceException>()
            .WaitAndRetryAsync(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        ExponentialBackOffRetryPolicyAsync = Policy
            .Handle<TestStepFailureException>()
            .WaitAndRetryAsync(6, attempt => TimeSpan.FromSeconds(Math.Pow(20, attempt)));

        AssertRetryPolicy = Policy
            .Handle<AssertionException>()
            .WaitAndRetry(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        RetryPolicy = Policy
            .Handle<TestStepFailureException>()
            .WaitAndRetry(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));

        ExponentialBackOffRetryPolicy = Policy
            .Handle<TestStepFailureException>()
            .WaitAndRetry(6, attempt => TimeSpan.FromSeconds(Math.Pow(20, attempt)));

        AssertOrStepTestFailureRetryPolicyAsync = Policy
            .Handle<AssertionException>()
            .Or<TestStepFailureException>()
            .WaitAndRetryAsync(config.Retries, x => TimeSpan.FromSeconds(config.Timeout));
    }

    public IScenarioContext ScenarioContext { get; set; }

    public async Task The_test_is_throttled_by_AMOUNT_milliseconds(int amount)
        => await Task.Delay(amount);
}
