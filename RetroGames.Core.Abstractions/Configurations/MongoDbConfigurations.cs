namespace RetroGames.Core.Abstractions.Configurations
{
    public class MongoDbConfigurations
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public MongoDbCollection Collections { get; set; }
    }

    public class MongoDbCollection
    {
        public string User { get; set; }
        public string Provider { get; set; }
        public string Comment { get; set; }

    }
}
