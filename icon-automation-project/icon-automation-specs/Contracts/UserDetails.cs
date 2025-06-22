using System.Net;

namespace Icon_Automation_Libs.Contracts;

public class UsersInput
{
    public int Page { get; set; }
}

public class UsersResult
{
    public HttpStatusCode ResponseCode { get; set; }
    public int Page {  get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public int TotalPages { get; set; }
    public List<UserDetails> Data { get; set; }
    public SupportDetails? Support { get; set; }
}

public class UserDetails
{
    public int Id {  get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Avatar {  get; set; }
}

public class SupportDetails
{
    public string Url { get; set; }
    public string Text { get; set; }
}
