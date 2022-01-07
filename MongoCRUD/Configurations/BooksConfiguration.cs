namespace MongoCRUD.Configurations
{
    public class BooksConfiguration
    {
        public string BooksCollectionName { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
    }
}
