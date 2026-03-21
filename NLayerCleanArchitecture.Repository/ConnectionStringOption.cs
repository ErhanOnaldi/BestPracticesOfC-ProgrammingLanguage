namespace NLayerCleanArchitecture.Repository;

public class ConnectionStringOption
{
    public const string Key = "ConnectionStrings";
    public string DefaultConnection { get; set; } = null!;
}