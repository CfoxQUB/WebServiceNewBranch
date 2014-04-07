using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConorFoxProject 
{
    public class TimetableObject
    {
        public int Timeslot { get; set; }
        public int Enumerations { get; set; }
        public int Percentage { get; set; }
    }

    public class TimetableDisplayListObject
    {
        public List<TimetableObject> MondayList { get; set; }
        public List<TimetableObject> TuesdayList { get; set; }
        public List<TimetableObject> WednesdayList { get; set; }
        public List<TimetableObject> ThursdayList { get; set; }
        public List<TimetableObject> FridayList { get; set; }
        public List<TimetableObject> SaturdayList { get; set; }
        public List<TimetableObject> SundayList { get; set; }
    }

    public class TimetableEventObject
    {
        public Event Event { get; set; }
        public Time Time { get; set; }
    }

    public class TimetableEventsListObject
    {
        public List<TimetableEventObject> MondayList { get; set; }
        public List<TimetableEventObject> TuesdayList { get; set; }
        public List<TimetableEventObject> WednesdayList { get; set; }
        public List<TimetableEventObject> ThursdayList { get; set; }
        public List<TimetableEventObject> FridayList { get; set; }
        public List<TimetableEventObject> SaturdayList { get; set; }
        public List<TimetableEventObject> SundayList { get; set; }
    }
}
