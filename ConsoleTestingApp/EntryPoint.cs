using Modules.Modules;
using Modules.FileDB;
using System.Linq.Expressions;
using System.Formats.Asn1;
using System.Reflection;

Console.WriteLine("hello");
Tag.table.InitialiseTable();
Tag tag = new Tag("Long", TagCategory.Book);
tag.Record();
List<Tag> tags = Tag.getAll(Tag.table);

foreach (Tag t in tags)
{
    Console.WriteLine(t);
}

