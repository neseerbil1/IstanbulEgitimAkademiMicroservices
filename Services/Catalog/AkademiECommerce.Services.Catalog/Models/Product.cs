﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace AkademiECommerce.Services.Catalog.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string Image { get; set; }
        public int Stock { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryID { get; set; }
        [BsonIgnore]
        public Category Category { get; set; }
    }
}

