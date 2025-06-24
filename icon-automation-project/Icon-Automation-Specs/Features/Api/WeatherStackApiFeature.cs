using Icon_Automation_Libs.ApiTestCaseData;
using Icon_Automation_Libs.Attributes;
using Icon_Automation_Libs.Const;
using Icon_Automation_Libs.Runner;
using Icon_Automation_Specs.Context;
using LightBDD.Framework;
using LightBDD.Framework.Scenarios;
using LightBDD.NUnit3;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Icon_Automation_Specs.Features.Api;

[Suite(SuiteType.API)]
[Category(FeatureConst.API.WeatherStack)]
[Label("WeatherStack Feature")]
[FeatureDescription(
    @"In order to verify current weather details as a tester I want to be able to call the weatherStack current weather endpoint")]
public class WeatherStackApiFeature : FeatureFixture
{
    private readonly RunnerContext _runnerContext = new();

    [IconScenario("Scenario-API-WeatherStack-1", FeatureConst.API.WeatherStack)]
    [TestCaseSource(typeof(WeathersSackTestCaseData))]
    public async Task Scenario_Api_WeatherStack_001_TESTID(string testId, string city)
        => await Runner.WithContext<ApiContext>()
            .AddAsyncSteps(
                given => given.WeatherStackApiFixture.A_get_weather_stack_current_weather_action_is_requested(city))
            .AddSteps(
                when => when.WeatherStackApiFixture.The_api_response_status_is_verified(),
                then => then.WeatherStackApiFixture.The_current_weather_response_is_verified(city),
                then => then.WeatherStackApiFixture.The_last_weather_observation_time_never_exceeds_24h_is_verified())
            .RunAsync();
}

