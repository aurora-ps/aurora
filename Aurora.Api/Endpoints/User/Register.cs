namespace Aurora.Api.Endpoints.User
{
    public static class Register
    {
        public static RouteGroupBuilder ConfigureUserEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet(GetUserEndpoint.Route, GetUserEndpoint.GetUser);
            group.MapGet(GetUsersEndpoint.Route, GetUsersEndpoint.GetUsers);
            group.MapPost(AddUserEndpoint.Route, AddUserEndpoint.AddUser);
            group.MapDelete(DeleteUserEndpoint.Route, DeleteUserEndpoint.DeleteUser);
            return group.WithOpenApi();
        }
    }
}
