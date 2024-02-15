using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Drivers.Api.Models;

public class Driver
{
    [BsonId] //Para obtener el ID auto-generado por Mongo
    [BsonRepresentation(BsonType.ObjectId)]

    public string Id {get; set;} = string.Empty;

    [BsonElement("Name")]
    public string Name {get; set;} = string.Empty;
    [BsonElement("Number")]
    public int Number { get; set; }
    [BsonElement("Team")]
    public string Team {get; set;} = string.Empty;
}