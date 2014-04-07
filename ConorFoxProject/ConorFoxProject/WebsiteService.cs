using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

namespace ConorFoxProject
{
    [ServiceContract]
    public interface IWebsiteService
    {
        #region Account Functions

        [OperationContract]
        Student StudentLogin(string studentEmail, string studentPassword);

        [OperationContract]
        Staff StaffLogin(string staffEmail, string staffPassword);

        #endregion

        #region Event Functions

        [OperationContract]
        Event EventSummary(int userId, int userType, int eventId);

        [OperationContract]
        List<Event> StudentsEvents(int userId, int userType);

        [OperationContract]
        List<Event> StaffEvents(int userId, int userType);

        #endregion

    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WebsiteService : IWebsiteService
    {
       private TimetableDatabase _db = new TimetableDatabase();
        #region Account Functions
        /// <summary>
        /// Written:
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public Student StudentLogin(string studentEmail, string studentPassword)
        {
            if (studentEmail != null && studentPassword != null)
            {
                var userInformation = _db.Students.SingleOrDefault(x => x.StudentEmail == studentEmail);

                if (userInformation.Password == studentPassword)
                {
                    return (userInformation);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        /// <summary>
        ///  Written:
        /// </summary>
        /// <param name="staffEmail"></param>
        /// <param name="staffPassword"></param>
        /// <returns></returns>
        public Staff StaffLogin(string staffEmail, string staffPassword)
        {
            if (staffEmail != null && staffPassword != null)
            {
                var userInformation = _db.Staffs.SingleOrDefault(x => x.StaffEmail == staffEmail);

                if (userInformation.Password == staffPassword)
                {
                    return (userInformation);
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        #endregion

        #region Event Functions
        /// <summary>
        ///  Written: 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event EventSummary(int userId, int userType, int eventId)
        {
            if (userId > 0 && eventId > 0)
            {
                Event returnEvent = _db.Events.SingleOrDefault(x => x.EventId == eventId);

                return returnEvent;
            }
            return null;
        }


        /// <summary>
        /// Written:
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public List<Event> StudentsEvents(int userId, int userType)
        {
            if (userId > 0 && userType > 0)
            {
                //var userEventsIds = _db.StudentEvents.Where(x => x.StudentId == userId).ToList();

                //List<Event> userEvents = new List<Event>();

                //foreach (var id in userEventsIds)
                //{
                //    var temp = _db.Events.SingleOrDefault(x => x.EventId == id.EventId);
                //    userEvents.Add(temp);
                //}
                //return userEvents;
            }
            return null;
        }

        /// <summary>
        /// Written:
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public List<Event> StaffEvents(int userId, int userType)
        {
            //if (userId > 0 && userType > 0)
            //{
            //    var userEventsIds = _db.StaffEvents.Where(x => x.StaffId == userId).ToList();

            //    List<Event> userEvents = new List<Event>();

            //    foreach (var id in userEventsIds)
            //    {
            //        var temp = _db.Events.SingleOrDefault(x => x.EventId == id.EventId);
            //        userEvents.Add(temp);
            //    }
            //    return userEvents;
            //}
            return null;
        }
        #endregion
    }
}
