namespace TheApp.Api.Entities
{
    /// <summary>
    /// Base User class
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
    }

    /// <summary>
    /// User class used for inbound API communication
    /// </summary>
    public class UserDTOIn : User
    {
        public string Password { get; set; }
    }

    /// <summary>
    /// User class used four outbound API comunication
    /// </summary>
    public class UserDTOOut : User
    {
        public string Token { get; set; }
    }

    /// <summary>
    /// Used for the Database, only used internally NEVER to be used in outbound API communication
    /// </summary>
    public class DBUser : User
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}