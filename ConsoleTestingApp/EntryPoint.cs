using Modules.Modules;
using Modules.FileDB;
using System.Linq.Expressions;
using System.Formats.Asn1;
using System.Reflection;

Console.WriteLine("hello");
// Tag.table.InitialiseTable();
Subject.table.InitialiseTable();
Subject subject = new Subject("Math", 10, new TimeSpan(0,0,0,0));
Book.table.InitialiseTable();

Book book = new Book("Steven Abott", subject, 10, new TimeSpan(0,0,0,0), false, null);

book.Record();