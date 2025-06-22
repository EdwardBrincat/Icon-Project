using Icon_Automation_Libs.ActionContext.Models;
using Icon_Automation_Libs.Contracts;
using Icon_Automation_Libs.Services.User;

namespace Icon_Automation_Libs.Account;

public class UserCommandFactory
{
    private readonly IUserService _service;

    public UserCommandFactory(
        IUserService service
    )
    {
        _service = service;
    }
	
	public async Task<UsersResult> ExecuteGetUsersCommand(UsersInput usersInput, UserActionContext context = null)
		=> await _service.GetUsers(usersInput, context);
}
