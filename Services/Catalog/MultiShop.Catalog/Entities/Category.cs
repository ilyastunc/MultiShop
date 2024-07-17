using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MultiShop.Catalog.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] //Mongo'da CategoryId'nin benzersiz olduğunu belirtmek için
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
