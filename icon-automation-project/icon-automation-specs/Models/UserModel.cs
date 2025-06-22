namespace Icon_Automation_Libs.Models;

public class UsersRequest
{
    public int Page { get; set; }
}

public class UsersResponse
{
    public int Page {  get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public List<UserModel> Data { get; set; }
    public SupportModel? Support { get; set; }
}

public class UserModel
{
    public int Id {  get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Avatar {  get; set; }
}

public class SupportModel
{
    public string Url { get; set; }
    public string Text { get; set; }
}
