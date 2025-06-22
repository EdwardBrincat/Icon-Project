using AutoMapper;
using Icon_Automation_Libs.ActionContext.Models;
using Icon_Automation_Libs.Clients;
using Icon_Automation_Libs.Contracts;
using Icon_Automation_Libs.Models;

namespace Icon_Automation_Libs.Services.User;

public interface IUserService
{
    Task<UsersResult> GetUsers(
         UsersInput usersInput,
         UserActionContext context = null
     );    
}
	
public class UserService : IUserService
{
    private readonly IUserClient _userClient;
    private readonly IMapper _mapper;

    public UserService(
    IUserClient userClient
)
    {
        _userClient = userClient;
        IMapper mapper;
    }
	public async Task<UsersResult> GetUsers(UsersInput usersInput, UserActionContext context = null)
	{
        var request = _mapper.Map<UsersRequest>(usersInput);
        var response = await _userClient.GetUsers(request, context);
        var result = _mapper.Map<UsersResult>(response.Data);
        result.ResponseCode = response.StatusCode;
        return result;
	}
}
