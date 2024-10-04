namespace ApiContracts.Dto_User;

public class UpdateUserDto
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}