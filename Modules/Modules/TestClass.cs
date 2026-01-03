using Modules.FileDB;
namespace Modules.Modules
{
    public class Person
    {
        public static void print(string message)
        {
            Table.log(message);
        }
    }
}