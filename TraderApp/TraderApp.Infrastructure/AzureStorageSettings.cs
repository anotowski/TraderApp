namespace TraderApp.Infrastructure;

public class AzureStorageSettings
{
    public bool UseDevelopmentStorage { get; set; }
    public string ConnectionString { get; set; }
    public string ContainerName { get; set; }
    public string TableName { get; set; }
}
