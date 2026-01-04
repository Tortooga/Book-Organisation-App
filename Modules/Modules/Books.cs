using Modules.FileDB.Utilities;
using Modules.FileDB;
using System.Globalization;
using System.Runtime.InteropServices;

namespace Modules.Modules
{
    public class Book : ORMModel<Book>
    {
        public static readonly Table table = new Table("Books", [
            ["ID", "Int32"],
            ["Name", "String"],
            ["ParentSubject", "Int32"],
            ["OpenFreq", "Int32"],
            ["TimeTracked", "String"],
            ["IsOpen", "Boolean"], 
            ["MostRecentOpen", "String"]
        ]);

        protected override Table TableI() => table;

        protected override Dictionary<string, object?> GetFields() => new Dictionary<string, object?>
        {
            ["Id"] = Id,
            ["Name"] = Name,
            ["ParentSubject"] = GetSubjectId(),
            ["OpenFreq"] = OpenFreq,
            ["TimeTracked"] = TimeTracked.ToString(Constants.TimeSpanFormat),
            ["IsOpen"] = IsOpen,
            ["MostRecentOpen"] = GetMostRecentOpen().ToString(Constants.DateTimeFormat)
        };

        public override int? Id { get; set; }
        public override string Name { get; set; }

        public Subject? ParentSubject { get; set; }
        private int GetSubjectId() => (ParentSubject == null || ParentSubject.Id == null) ? 69420 : (int)ParentSubject.Id;

        public int OpenFreq { get; set; }
        public TimeSpan TimeTracked { get; set; }
        public bool IsOpen { get; set; }

        public DateTime? MostRecentOpen { get; set; }
        private DateTime GetMostRecentOpen() => (MostRecentOpen == null) ? DateTime.MinValue : (DateTime)MostRecentOpen;

        public Book(string name, Subject? parentSubject, int openFreq, TimeSpan timeTracked, bool isOpen = false, DateTime? mostRecentOpen = null)
        {
            Name = name;
            ParentSubject = parentSubject;
            OpenFreq = openFreq;
            TimeTracked = timeTracked;
            IsOpen = isOpen;
            MostRecentOpen = mostRecentOpen;
        }

        public Book(string name, int parentSubject, int openFreq, string timeTracked, bool isOpen, string mostRecentOpen)
        {
            Name = name;
            OpenFreq = openFreq;
            IsOpen = isOpen;

            try
            {
                TimeTracked = TimeSpan.ParseExact(timeTracked, Constants.TimeSpanFormat, CultureInfo.InvariantCulture);
            }
            catch (System.Exception)
            {
                TimeTracked = new TimeSpan(0,0,0,0);
            }
            
            ParentSubject = ReconcileSubjectRecord(parentSubject);

            try
            {
                MostRecentOpen = DateTime.ParseExact(mostRecentOpen, Constants.DateTimeFormat, CultureInfo.InvariantCulture);
            }
            catch (System.Exception)
            {
                Table.log("Error: Could not resolve field MostRecentOpen");
                MostRecentOpen = null;
            }
        }

        Subject? ReconcileSubjectRecord(int subjectId)
        {
            if (subjectId == 69420)//TODO: fix this
            {
                return null;
            }

            Parser parser = new Parser(Subject.table);
            string[]? subjectRecord = parser.find(subjectId);

            if (subjectRecord == null)
            {
                return null;
            }

            try
            {
                int? openFreq = (int?)TypeConversion.ConvertTo(subjectRecord[1], "Int32");
                if (openFreq == null)
                {
                    Table.log($"Error: Failed to build Subject object in Book class using record. Subject Id: {subjectId}");
                    return null;
                }
                return new Subject(subjectRecord[0], (int)openFreq, subjectRecord[2]);
            }
            catch (System.Exception)
            {
                Table.log($"Error: Failed to build Subject object in Book class using record. Subject Id: {subjectId}");
                return null;
            }
        }
    }
}