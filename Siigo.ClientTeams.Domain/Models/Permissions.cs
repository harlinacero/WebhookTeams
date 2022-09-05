using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace Siigo.ClientTeams.Domain.Models
{
    public class Permissions
    {
        [BsonId]
        [BsonElement("_id")]
        public ObjectId _Id { get; set; }

        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("sujectCode")]
        public string SujectCode { get; set; }

        [BsonElement("sujectType")]
        public long SujectType { get; set; }

        [BsonElement("resourceCode")]
        public long ResourceCode { get; set; }

        [BsonElement("resourceType")]
        public long ResourceType { get; set; }

        [BsonElement("actions")]
        public string[] Actions { get; set; }

        [BsonElement("exists")]
        public bool Exists { get; set; }

        [BsonElement("replace")]
        public bool Replace { get; set; }

        [BsonElement("canImport")]
        public bool CanImport { get; set; }

        [BsonElement("invalidSujectCode")]
        public object InvalidSujectCode { get; set; }

        [BsonElement("invalidResourceCode")]
        public object InvalidResourceCode { get; set; }

        [BsonElement("changeTracker")]
        public long ChangeTracker { get; set; }

    }

    public partial class MessageBody
    {
        [JsonProperty("themeColor")]
        public string ThemeColor { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("sections")]
        public Section[] Sections { get; set; }

        [JsonProperty("urlTeams")]
        public Uri UrlTeams { get; set; }

        [JsonProperty("database")]
        public string Database { get; set; }

        [JsonProperty("collection")]
        public string Collection { get; set; }

        [JsonProperty("filters")]
        public Filter[] Filters { get; set; }

        [JsonProperty("potentialAction")]
        public PotentialAction[] PotentialAction { get; set; }
        [JsonProperty("provider")]
        public string Provider { get; set; }
    }

    public partial class Filter
    {
        [JsonProperty("field")]
        public string Field { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("compare")]
        public string Compare { get; set; }
    }

    public partial class Section
    {
        [JsonProperty("activityTitle")]
        public string ActivityTitle { get; set; }

        [JsonProperty("activitySubtitle")]
        public string ActivitySubtitle { get; set; }

        [JsonProperty("activityImage")]
        public Uri ActivityImage { get; set; }

        [JsonProperty("facts")]
        public Fact[] Facts { get; set; }

        [JsonProperty("markdown")]
        public bool Markdown { get; set; }
    }

    public partial class Fact
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("value")]
        public object Value { get; set; }
    }


    public partial class PotentialAction
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("inputs", NullValueHandling = NullValueHandling.Ignore)]
        public Input[] Inputs { get; set; }

        [JsonProperty("actions", NullValueHandling = NullValueHandling.Ignore)]
        public Action[] Actions { get; set; }

        [JsonProperty("targets", NullValueHandling = NullValueHandling.Ignore)]
        public Target[] Targets { get; set; }
    }

    public partial class Input
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isMultiline", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMultiline { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("isMultiSelect", NullValueHandling = NullValueHandling.Ignore)]
        public bool? IsMultiSelect { get; set; }

        [JsonProperty("choices", NullValueHandling = NullValueHandling.Ignore)]
        public Choice[] Choices { get; set; }
    }


    public partial class Choice
    {
        [JsonProperty("display")]
        public string Display { get; set; }

        [JsonProperty("value")]
        public long Value { get; set; }
    }

    public partial class Target
    {
        [JsonProperty("os")]
        public string Os { get; set; }

        [JsonProperty("uri")]
        public Uri Uri { get; set; }
    }

    public partial class Action
    {
        [JsonProperty("@type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("target")]
        public Uri Target { get; set; }
    }
}
