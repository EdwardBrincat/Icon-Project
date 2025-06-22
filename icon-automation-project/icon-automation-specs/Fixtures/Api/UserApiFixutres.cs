using AutoFixture;
using FluentAssertions;
using Icon_Automation_Libs.Account;
using Icon_Automation_Libs.ActionContext.Factory;
using Icon_Automation_Libs.Contracts;
using Icon_Automation_Libs.Models;
using Icon_Automation_Libs.Scenario;
using System.Net;

namespace Icon_Automation_Libs.Fixtures.Api;

public class UserApiFixutres : FixtureBase
{
    private readonly IActionContextFactory _actionContextFactory;
    private readonly UserCommandFactory _userCommandFactory;
    private readonly ConfigModel _config;

    public UserApiFixutres(
        ConfigModel config,
        IScenarioContext scenarioContext,
        IActionContextFactory actionContextFactory,
        UserCommandFactory userCommandFactory
    ) : base(config, scenarioContext)
    {
        _config = config;
        _actionContextFactory = actionContextFactory;
        _userCommandFactory = userCommandFactory;
    }

    public async Task A_get_user_action_is_requested(int page)
    {
        var action = _actionContextFactory.CreateUserActionContext()
            .WithXApiKey(_config.apiKey)
            .Build();

        var result = await _userCommandFactory.ExecuteGetUsersCommand(new Fixture().BuildData<UsersInput>()
            .With(p => p.Page, page)
            .Create(),
            action);

        ScenarioContext.AddOrUpdateValue($"{UserClientKeys.UsersResult}", result);
    }

    public void The_get_user_response_is_verified(string scenarioId, int page)
    {
        var userResult = ScenarioContext.GetValue<UsersResult>($"{UserClientKeys.UsersResult}");
        var expectedResult = _config.TestData.Data[scenarioId];

        userResult.ResponseCode.Should().Be(HttpStatusCode.OK);

        userResult.Page.Should().Be(page);
        userResult.PageSize.Should().Be(expectedResult.PageSize);

        var user = userResult.Data.FirstOrDefault(user => user.Id == expectedResult.User.Id);

        user.Should().NotBeNull();
        user.Email.Should().Be(expectedResult.User.Email);
        user.FirstName.Should().Be(expectedResult.User.FirstName);
        user.LastName.Should().Be(expectedResult.User.LastName);
        user.Avatar.Should().Be(expectedResult.User.Avatar);
        userResult.Support.Should().BeNull();
     }

    public void The_get_user_response_is_verified_for_a_non_existent_page(string scenarioId, int page)
    {
        var userResult = ScenarioContext.GetValue<UsersResult>($"{UserClientKeys.UsersResult}");
        var expectedResult = _config.TestData.Data[scenarioId];

        userResult.ResponseCode.Should().Be(HttpStatusCode.OK);

        userResult.Page.Should().Be(page);
        userResult.PageSize.Should().Be(expectedResult.PageSize);

        userResult.Data.Should().BeEmpty();
        userResult.Support.Should().NotBeNull();
        userResult.Support.Text.Should().Be(expectedResult.Support.Text);
        userResult.Support.Url.Should().Be(expectedResult.Support.Url);
    }
}
