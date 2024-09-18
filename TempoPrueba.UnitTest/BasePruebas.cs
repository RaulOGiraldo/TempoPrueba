using MongoDB.Driver;

namespace TempoPrueba.UnitTest
{
    public class BasePruebas
    {
        private readonly IMongoDatabase _database;

        public BasePruebas()
        {
            // Configura la conexión a MongoDB. Ajusta la cadena de conexión según tus necesidades.
            var connectionString = "mongodb+srv://mern_user:yJ5EtZFXkIg2x663@cluster0.lvvbno9.mongodb.net/"; 
            var client = new MongoClient(connectionString);

            // Define el nombre de la base de datos
            var databaseName = "TempoPrueba"; 
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<T> ObtenerColeccion<T>(string nombreColeccion)
        {
            return _database.GetCollection<T>(nombreColeccion);
        }
    }

}
