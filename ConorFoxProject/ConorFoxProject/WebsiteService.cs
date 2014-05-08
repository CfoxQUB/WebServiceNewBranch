using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;


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
        Event EventSummary(int eventId);

        [OperationContract]
        Event ReturnEventDetail(int eventId);

        [OperationContract]
        List<Event> StudentsEvents(int userId);

        [OperationContract]
        List<Event> StaffEvents(int userId);
        
        [OperationContract]
        List<StudentInvite> StudentsInvites(int userId);

        [OperationContract]
        List<StaffInvite> StaffInvites(int userId);
        
        [OperationContract]
        bool ConfirmStudentInvite(int eventId, int userId);

        [OperationContract]
        bool ConfirmStaffInvite(int eventId, int userId);


        #endregion

    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class WebsiteService : IWebsiteService
    {
       private readonly TimetableDatabase _db = new TimetableDatabase();

        #region Account Functions

        /// <summary>
        /// Login function for students
        /// </summary>
        /// <param name="studentEmail"></param>
        /// <param name="studentPassword"></param>
        /// <returns></returns>
        public Student StudentLogin(string studentEmail, string studentPassword)
        {
            //check to ensure email and password are not null
            if (studentEmail != null && studentPassword != null)
            {
                //returns informtion associated with email address provided
                var userInformation = _db.Students.SingleOrDefault(x => x.StudentEmail == studentEmail);

                //if record exists pasword is compared
                if (userInformation != null)
                {
                    //Password comparison
                    if (userInformation.Password == studentPassword)
                    {
                        //user Inforation returned
                        return (userInformation);
                    }
                    //Password rejected
                    return null;
                }
                //user does not exist
                return null;
            }
            //Password or email invalid
            return null;
        }

        /// <summary>
        ///  Login function for staff
        /// </summary>
        /// <param name="staffEmail"></param>
        /// <param name="staffPassword"></param>
        /// <returns></returns>
        public Staff StaffLogin(string staffEmail, string staffPassword)
        {
            //check to ensure email and password valid
            if (staffEmail != null && staffPassword != null)
            {
                //return user information asociated with email address
                var userInformation = _db.Staffs.SingleOrDefault(x => x.StaffEmail == staffEmail);

                //user inforation returned
                if (userInformation != null)
                {
                    //Check to see if passwords match
                    if (userInformation.Password == staffPassword)
                    {
                        //on password match user information returned
                        return (userInformation);
                    }
                    //Passwords dont match
                    return null;
                }
                //user does not exist
                return null;
                }
            //email or password invalid
            return null;
        }

        #endregion

        #region Event Functions
        /// <summary>
        ///  returns information of  event that is selected
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event EventSummary(int eventId)
        {
            if (eventId > 0)
            {
                //returns event that is selected
                Event returnEvent = _db.Events.SingleOrDefault(x => x.EventId == eventId);

                return returnEvent;
            }
            return null;
        }


        /// <summary>
        /// Returns list of events taht is associated wit the student selected
        /// for the module association of user and the confirmed invitations that exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Event> StudentsEvents(int userId)
        {
            if (userId > 0)
            {
                //return of student events and student invitatiuons thathave been confirmed
                var userEventsIds = _db.StudentEvents.Where(x => x.StudentId == userId).ToList();
                var studentInvites = _db.StudentInvites.Where(x => x.StudentId == userId && x.Attending).ToList();
                //event list that is to be populated to return
                var userEvents = new List<Event>();

                //check to ensure events are associated with student
                if (userEventsIds.Any())
                {
                    foreach (var id in userEventsIds)
                    {   //events are selected and adde to return list
                        var temp = _db.Events.Where(x => x.EventId == id.EventId).ToList();
                        userEvents.AddRange(temp);
                    }
                }
                //If invites exist
                if (studentInvites.Any())
                {
                    //for each einvite that is confimed event associated is selected
                    foreach (var i in studentInvites)
                    {
                        var invitedEvent = _db.Events.SingleOrDefault(x => x.EventId == i.EventId && x.Status == "Confirmed");
                        //as long as event exists the event is adde to the rturn list
                        if (invitedEvent != null)
                        {
                            userEvents.Add(invitedEvent);
                        }
                    }
                }
                //events returned
                return userEvents;
            }
            return null;
        }

        /// <summary>
        /// Returns list of events taht is associated wit the staff selected
        /// for events adde to and the confirmed invitations that exist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<Event> StaffEvents(int userId)
        {
            if (userId > 0)
            {//return of staff events and staff invitatiuons that have been confirmed
                var userEventsIds = _db.StaffEvents.Where(x => x.StaffId == userId).ToList();
                var staffInvites = _db.StaffInvites.Where(x => x.StaffId == userId && x.Attending).ToList();
                //event list that is to be populated to return
                var userEvents = new List<Event>();

                //check to ensure events are associated with staff
                if(userEventsIds.Any())
                {//events are selected and added to return list
                    foreach (var id in userEventsIds)
                    {
                    var temp = _db.Events.Where(x => x.EventId == id.EventId).ToList();
                    userEvents.AddRange(temp);
                    }
                }
                //If invites exist
                if (staffInvites.Any())
                {//for each invite that is confimed event associated is selected
                    foreach (var i in staffInvites)
                    {
                        var invitedEvent = _db.Events.SingleOrDefault(x => x.EventId == i.EventId && x.Status =="Confirmed");
                        if (invitedEvent != null)
                        {
                            //as long as event exists the event is added to the return list
                            userEvents.Add(invitedEvent);
                        }
                    }
                }
                //events returned
                return userEvents;
            }
            //no events exist
            return null;
        }
        
        /// <summary>
        /// Invitations for event are returned for student that have yet to be responded to
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<StudentInvite> StudentsInvites(int userId)
        {
            //empty invitations list initialized
            var none = new List<StudentInvite>();
            //student invites yet to be confirmed returned
            var temp = _db.StudentInvites.Where(x => x.StudentId == userId && x.Attending != true).ToList();
            if (temp.Any())
            {//invites retured
                return temp;
            }
            //no invitations exist
            return none;
        }

        /// <summary>
        /// Invitations for staff member yet to be confirmed are returned
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<StaffInvite> StaffInvites(int userId)
        {
            //empty invitations list initialized
            var none = new List<StaffInvite>();
            //staff invites yet to be confirmed returned
            var temp = _db.StaffInvites.Where(x => x.StaffId == userId && x.Attending != true).ToList();
            if (temp.Any())
            {//invites retured
                return temp;
            }
            //no invites exist
            return none;
        }
        
        /// <summary>
        /// returns event details associated with id passed in from website
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public Event ReturnEventDetail(int eventId)
        {
            //default evet declared
            var none = new Event();
            //selection of event by event Id passed in
            var temp = _db.Events.SingleOrDefault(x => x.EventId == eventId);
            if (temp != null)
            {
                //event returned
                return temp;
            }
            //empty event returned
            return none;
        }
      

        /// <summary>
        /// Confirmation of invitation for student invite
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ConfirmStudentInvite(int eventId, int userId)
        {
            //Invite taht is to confirmed selected
            var invite = _db.StudentInvites.SingleOrDefault(x => x.EventId == eventId && x.StudentId == userId);
            if (invite != null)
            {//changing attendance to true
                invite.Attending = true;
                _db.SaveChanges();
                return true;
            }
            //comfirm failed
            return false;
        }
        
        /// <summary>
        /// Confirmation of invitation for staff invite
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ConfirmStaffInvite(int eventId, int userId)
        {
            //return invite that is to be confirmed
            var invite = _db.StaffInvites.SingleOrDefault(x => x.EventId == eventId && x.StaffId == userId);
            if (invite != null)
            {//invitation is updated
                invite.Attending = true;
                _db.SaveChanges();
                return true;
            }
            //confrim invite failed
            return false;
        }
        #endregion
    }
}
