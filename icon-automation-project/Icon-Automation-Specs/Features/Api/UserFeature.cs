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
[Category(FeatureConst.API.User)]
[Label("User Feature")]
[FeatureDescription(
    @"In order to verify user details as a tester I want to be able to call the user endpoint")]
public class UserFeature : FeatureFixture
{
    private readonly RunnerContext _runnerContext = new();

    [IconScenario("Scenario-1", FeatureConst.API.User)]
    [TestCase("01", 1 )]
    [TestCase("02", 2)]
    [TestCase("03", 12)]
    public async Task Scenario_001_TESTID(string testId, int page) =>
        await Runner.WithContext<ApiContext>()            
            .AddAsyncSteps(
                given => given.UserApiFixutres.A_get_user_action_is_requested(page))
            .AddSteps(
                then => then.UserApiFixutres.The_get_user_response_is_verified($"scenario_{testId}_page_{page}", page))
            .RunAsync();
}