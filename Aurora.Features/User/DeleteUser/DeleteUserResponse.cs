namespace Aurora.Features.User.DeleteUser;

public class DeleteUserResponse
{
    private DeleteUserResponse(bool success, DeleteUserStatusEnum status)
    {
        Success = success;
        Status = status;
    }

    public bool Success { get; set; }

    public DeleteUserStatusEnum Status { get; set; }

    public static DeleteUserResponse CreateSuccess() => new(true, DeleteUserStatusEnum.Deleted);

    public static DeleteUserResponse CreateFailure() => new(false, DeleteUserStatusEnum.Error);

    public static DeleteUserResponse CreateNotFound() => new(false, DeleteUserStatusEnum.NotFound);

    public enum DeleteUserStatusEnum
    {
        NotFound,
        Deleted,
        Error,
        Unauthorized
    }
}