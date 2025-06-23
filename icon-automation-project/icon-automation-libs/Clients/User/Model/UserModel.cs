using Newtonsoft.Json;

namespace Icon_Automation_Libs.Clients.User.Model;

public class UsersRequest
{
    public int Page { get; set; }
}

public class UsersResponse
{
    [JsonProperty("page")]
    public int Page {  get; set; }

    [JsonProperty("per_page")]
    public int PageSize { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("total_pages")]
    public int TotalPages { get; set; }

    [JsonProperty("data")]
    public List<UserModel> Data { get; set; }

    [JsonProperty("support")]
    public SupportModel? Support { get; set; }
}

public class UserModel
{
    [JsonProperty("id")]
    public int Id {  get; set; }

    [JsonProperty("email")]
    public string Email { get; set; }

    [JsonProperty("first_name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    public string LastName { get; set; }

    [JsonProperty("avatar")]
    public string Avatar {  get; set; }
}

public class SupportModel
{
    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("text")]
    public string Text { get; set; }
}
