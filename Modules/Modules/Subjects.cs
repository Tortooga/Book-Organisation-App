using Modules.FileDB;
using Modules.FileDB.Utilities;
using System.Globalization;

namespace Modules.Modules
{
    public class Subject : ORMModel<Subject>
    {
        public static readonly Table table = new Table("Subjects", [
            ["ID", "Int32"],
            ["Name", "String"],
            ["OpenFreq", "Int32"],
            ["TimeTracked", "String"]
        ]);

        protected override Table TableI() => table;

        protected override Dictionary<string, object?> GetFields() => new Dictionary<string, object?>
        {
            ["Id"] = Id,
            ["Name"] = Name,
            ["OpenFreq"] = OpenFreq,
            ["TimeTracked"] = TimeTracked.ToString(Constants.TimeSpanFormat)
        };

        public override int? Id { get; set; }
        public override string Name { get; set; }
        public int OpenFreq { get; set; }
        public TimeSpan TimeTracked { get; set; }

        public Subject(string name, int openFreq, TimeSpan timeTracked)
        {
            Name = name;
            OpenFreq = openFreq;
            TimeTracked = timeTracked;
        }
        
        public Subject(string name, int openFreq, string timeTracked)
        {
            Name = name;
            OpenFreq = openFreq;
            try
            {
                TimeTracked = TimeSpan.ParseExact(timeTracked, Constants.TimeSpanFormat, CultureInfo.InvariantCulture);
            }
            catch (System.Exception)
            {
                TimeTracked = new TimeSpan(0,0,0,0);
            }
        }   

        public override string ToString()
        {
            return $@"Subject
            Id: {Id}
            Name: {Name}
            Open Frequence: {OpenFreq}
            Time Tracked: {TimeTracked}";
        }
    }
}