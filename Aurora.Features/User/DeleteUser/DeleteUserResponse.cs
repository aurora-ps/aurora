namespace Aurora.Features.User.DeleteUser;

public class DeleteUserResponse
{
    public enum DeleteUserStatusEnum
    {
        NotFound,
        Deleted,
        Error,
        Unauthorized
    }

    private DeleteUserResponse(bool success, DeleteUserStatusEnum status)
    {
        Success = success;
        Status = status;
    }

    public bool Success { get; set; }

    public DeleteUserStatusEnum Status { get; set; }

    public static DeleteUserResponse CreateSuccess()
    {
        return new DeleteUserResponse(true, DeleteUserStatusEnum.Deleted);
    }

    public static DeleteUserResponse CreateFailure()
    {
        return new DeleteUserResponse(false, DeleteUserStatusEnum.Error);
    }

    public static DeleteUserResponse CreateNotFound()
    {
        return new DeleteUserResponse(false, DeleteUserStatusEnum.NotFound);
    }
}