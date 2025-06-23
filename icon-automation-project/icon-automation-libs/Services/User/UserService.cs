using AutoMapper;
using Icon_Automation_Libs.ActionContext.Models;
using Icon_Automation_Libs.Clients.User;
using Icon_Automation_Libs.Clients.User.Model;
using Icon_Automation_Libs.Contracts;

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

    public UserService(IUserClient userClient, IMapper mapper )
    {
        _userClient = userClient;
        _mapper = mapper;
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
