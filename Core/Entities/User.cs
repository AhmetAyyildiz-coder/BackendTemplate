namespace Core.Entities;

public class User : IEntity
{
    
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    /// <summary>
    /// Kullanicinin sifresini guclendirmek icin
    /// </summary>
    public byte[] PasswordSalt { get; set; }
    /// <summary>
    /// Sifreli password
    /// </summary>
    public byte[] PasswordHash { get; set; }

    public bool Status { get; set; }
    
}
