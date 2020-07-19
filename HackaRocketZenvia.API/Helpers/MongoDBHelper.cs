using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackaRocketZenvia.API.Helpers
{
    public class MongoDBHelper : IDisposable
    {
        protected static IMongoClient _client;
        protected static IMongoDatabase _database;

        public MongoDBHelper(string connectionString, string dbName)
        {
            _client = new MongoClient(connectionString);
            _database = _client.GetDatabase(dbName);
        }        

        public long? SelectCount(string collectionName)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                return collection.Find(new BsonDocument()).CountDocuments();
            }
            catch (Exception ex)
            {
                this.WriteError("SelectCount", "SelectCount(string collectionName)", ex.Message);
                return null;
            }
        }

        public long SelectCount(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(filter).CountDocuments();
        }

        public long SelectCount(string collectionName, string field, string value)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builders.FilterEq(field, value)).CountDocuments();
        }

        public long SelectCount(string collectionName, string field, ObjectId id)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builders.FilterEq(field, id)).CountDocuments();
        }

        public long SelectCount<T>(string collectionName, string field, T value)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            return collection.Find(Builders.FilterEq<T>(field, value)).CountDocuments();
        }

        public List<T> Select<T>(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            if (filter == null)
                filter = new BsonDocument();
            var result = collection.Find(filter).ToList();
            var returnList = new List<T>();
            foreach (var item in result)
            {
                returnList.Add(BsonSerializer.Deserialize<T>(item));
            }
            return returnList;
        }

        public T SelectOne<T>(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            var collection = _database.GetCollection<BsonDocument>(collectionName);
            var result = collection.Find(filter).ToList();
            if (result.Count > 1)
                throw new Exception("To many results");
            return BsonSerializer.Deserialize<T>(result.ElementAt(0));
        }

        public bool Insert(string collectionName, BsonDocument doc)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.InsertOne(doc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Insert<T>(string collectionName, T doc)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                var bDoc = doc.ToBsonDocument(); //se precisar retornar o ID, dessa forma ja volta preenchido;
                collection.InsertOne(bDoc);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool InsertMany<T>(string collectionName, IEnumerable<T> documents)
        {
            try
            {
                var docs = new List<BsonDocument>();
                for (int i = 0; i < documents.Count(); i++)
                {
                    docs[i] = documents.ElementAt(i).ToBsonDocument();
                }
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.InsertMany(docs);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateOne(string collectionName, FilterDefinition<BsonDocument> filter, UpdateDefinition<BsonDocument> update)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateOne(string collectionName, string field, string value, UpdateDefinition<BsonDocument> update)
        {
            try
            {
                var filter = Builders.FilterEq(field, value);
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.UpdateOne(filter, update);
                return true;
            }
            catch (Exception ex)
            {
                string xx = ex.Message;
                return false;
            }
        }

        public bool UpdateArray<T>(string collectionName, string arrayField, List<T> list, FilterDefinition<BsonDocument> filter)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                var update = Builders<BsonDocument>.Update.PushEach(arrayField, list);
                collection.FindOneAndUpdate<BsonDocument>(filter, update);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOne(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOne(string collectionName, string field, string value)
        {
            try
            {
                var filter = Builders.FilterEq(field, value);
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOne(string collectionName, string field, ObjectId value)
        {
            try
            {
                var filter = Builders.FilterEq<ObjectId>(field, value);
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteOne<T>(string collectionName, string field, T value)
        {
            try
            {
                var filter = Builders.FilterEq<T>(field, value);
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.DeleteOne(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteMany(string collectionName, FilterDefinition<BsonDocument> filter)
        {
            try
            {
                var collection = _database.GetCollection<BsonDocument>(collectionName);
                collection.DeleteMany(filter);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void WriteError(string title, string function, string message)
        {
            //if (_logger != null)
            //    _logger.WriteError(title, function, message);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

    public class Builders
    {
        public static FilterDefinition<BsonDocument> FilterEq(string field, string value)
        {
            return Builders<BsonDocument>.Filter.Eq(field, value);
        }

        public static FilterDefinition<BsonDocument> FilterEq<T>(string field, T value)
        {
            return Builders<BsonDocument>.Filter.Eq(field, value);
        }

        public static FilterDefinition<BsonDocument> FilterEq(string field, ObjectId id)
        {
            return Builders<BsonDocument>.Filter.Eq(field, id);
        }

        public static UpdateDefinition<BsonDocument> Update<T>(string field, T value)
        {
            return Builders<BsonDocument>.Update.Set(field, value);
        }
    }
}
