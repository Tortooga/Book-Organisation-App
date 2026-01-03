using Modules.FileDB;

namespace Modules.Modules
{
    public class Tag : ORMModel<Tag>
    {
        public static readonly Table table = new Table("Tags", [
            ["ID", "Int32"],
            ["Name", "String"],
            ["TagType", "String"]
        ]);
        
        protected override Table TableI() => table;
        protected override Dictionary<string, object?> GetFields() => new Dictionary<string, object?>
        {
            ["Id"] = Id,
            ["Name"] = Name,
            ["Type"] = Category

        };

        public override int? Id { get; set; }
        public override string Name { get; set; }
        public TagCategory Category { get; set; }

        public Tag(string name, TagCategory category)
        {
            Name = name;
            Category = category;
        }

        public Tag(string name, string category)
        {
            Name = name;
            Category = Enum.Parse<TagCategory>(category);
        }

        public override string ToString()
        {
            return $@"Tag
    Id: {Id}
    Name: {Name}
    Category: {Category}";
        }
    }

    public enum TagCategory
    {
        Subject,
        Book
    }
}