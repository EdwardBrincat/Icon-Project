using Icon_Automation_Libs.Fixtures.Api;
using Icon_Automation_Libs.Scenario;

namespace Icon_Automation_Specs.Context;

public class ApiContext : ContextBase
{
    public UserApiFixutres UserApiFixutres { get; }


	public ApiContext(
        IScenarioContext scenarioContext,
        UserApiFixutres userApiFixutres
    ) : base(scenarioContext)
    {
        UserApiFixutres = userApiFixutres;
    }        
}
