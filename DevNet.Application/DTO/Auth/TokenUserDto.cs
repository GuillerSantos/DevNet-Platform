namespace DevNet.Application.DTO.Auth
{
    public sealed record TokenUserDto(
        Guid Id,
        string FirstName,
        string LastName,
        string Email,
        string Role
    );
}