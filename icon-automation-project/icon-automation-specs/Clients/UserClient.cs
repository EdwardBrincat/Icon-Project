using FluentlyHttpClient;
using Icon_Automation_Libs.Http;
using Icon_Automation_Libs.Models;
using Sc_Casino_Client;

namespace Icon_Automation_Libs.Clients;

public interface IUserClient
{
	Task<FluentHttpResponse<UsersResponse>> GetUsers(
         UsersRequest usersRequest,
		 HttpRequestClientContext context = null
     );    
}

public class UserClient : IUserClient
{
    private readonly IFluentHttpClient _httpClient;

    public UserClient(IFluentHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.GetUserClient();
    }
	

    public async Task<FluentHttpResponse<UsersResponse>> GetUsers(
      UsersRequest usersRequest,
      HttpRequestClientContext context = null
    ) 
		=> await _httpClient.CreateRequest($"users?page={usersRequest.Page}")
			.AsGet()
			.FromClientContext(context)
			.ReturnAsResponse<UsersResponse>();    
}
