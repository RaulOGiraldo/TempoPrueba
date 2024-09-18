namespace TempoPrueba.Intrastructure.Data
{
    public class MongoDBProvider
    {
        public string? ConnectionString { get; set; } = null;
        public string? DataBaseName { get; set; } = null;
        public string? CollectionName { get; set; } = null;
    }
}
