using Modules.Modules;
using Modules.FileDB;
using System.Linq.Expressions;
using System.Formats.Asn1;
using System.Reflection;

Console.WriteLine("hello");
// Tag.table.InitialiseTable();
Subject.table.InitialiseTable();

Subject subject = new Subject("Math", 0, new TimeSpan(0,0,2,0));

subject.Record();

List<Subject> subjects = Subject.getAll(Subject.table);

foreach (Subject item in subjects)
{
    Console.WriteLine(item);
}