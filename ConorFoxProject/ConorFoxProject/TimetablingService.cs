using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Security.Cryptography;
using System.Text;


namespace ConorFoxProject
{
        [ServiceContract]
        public interface ITimetablingService
        {
            #region Client Users Functions

            [OperationContract]
            string Encrypt(string unencryptedString);
            
            [OperationContract]
            bool Register_User(User newUser);

            [OperationContract]
            bool Check_Email_Not_Exist(string email);
            
            [OperationContract]
            int Login(string userName, string userPassword);
            
            #endregion

            #region Event Actions
            
            [OperationContract]
            int CreateEvent(string eventTitle, int userId, string eventDescription, string eventType, string repeatType, int eventDuration, DateTime startDate, string eventTime, string roomName, string courseName, string moduleName);
            
            [OperationContract]
            bool EditEvent(int editedEventId, int userId, string eventTitle, string eventDescription, string eventType, int eventDuration, DateTime startDate, string eventTime, string roomName, string courseName, string moduleName);
            
            [OperationContract]
            bool DeleteEvent(int eventId);

            [OperationContract]
            bool ChangeEventStatus(String status, int eventId);

            #region Event Attendees
            
            [OperationContract]
            bool StaffEvent(int eventId, int staffId);

            [OperationContract]
            bool ModuleEvent(int eventId, int moduleId, int courseId);
            
            [OperationContract]
            bool DeleteStudentEvent(int inviteId);

            [OperationContract]
            bool DeleteStaffEvent(int inviteId);

            [OperationContract]
            bool DeleteModuleEvent(int inviteId);
            #endregion

            #region Event Invites

            [OperationContract]
            bool StudentInvite(int eventId, int studentId);

            [OperationContract]
            bool StaffInvite(int eventId, int staffId);

            [OperationContract]
            bool DeleteStudentInvite(int inviteId);

            [OperationContract]
            bool DeleteStaffInvite(int inviteId);
            #endregion

            #endregion

            #region Timetabling Actions

            [OperationContract]
            List<Event> ReturnWeeksEvents(DateTime weekBeginning, int roomId);

            [OperationContract]
            TimetableEventsListObject ReturnWeeksEventsWithFilters(DateTime dateRequested, int roomId, int moduleId);
            
            [OperationContract]
            TimetableDisplayListObject ReturnTimetableToolListObject();
            
            #endregion 

            #region Listed Types

            [OperationContract]
            List<Event> ReturnEvents();

            [OperationContract]
            Event ReturnEventDetails(int eventId);

            [OperationContract]
            List<Event> ReturnEventsWithModules();
            
            [OperationContract]
            List<Event> ReturnEventsWithRooms();
            
            [OperationContract]
            List<Event> ReturnEventsWithRoomsNoModules();
            
            [OperationContract]
            List<EventType> ReturnEventTypes();
            
            [OperationContract]
            Staff ReturnEventStaff(int eventId);
            
            [OperationContract]
            List<RoomType> ReturnRoomTypes();
            
            [OperationContract]
            List<Building> ReturnBuildings();
            
            [OperationContract]
            List<Course> ReturnCourses();
            
            [OperationContract]
            List<Module> ReturnModules();
            
            [OperationContract]
            List<Student> ReturnModuleStudents(int moduleId);
            
            [OperationContract]
            List<Staff> ReturnCourseStaff(int courseId);
            
            [OperationContract]
            List<Module> ReturnCourseModules(int courseId);
            
            [OperationContract]
            int ReturnBuildingIdFromBuildingName(string buildingName);
            
            [OperationContract]
            int ReturnRoomTypeIdFromTypeName(string typeName);
            
            [OperationContract]
            int ReturnCourseIdFromCourseName(string courseName);

            [OperationContract]
            int ReturnModuleIdFromModuleName(string moduleName);
            
            [OperationContract]
            int ReturnRoomId(int buildingId, string roomName);
            
            [OperationContract]
            List<Room> ReturnBuildingRooms(int buildingId);
            
            [OperationContract]
            int ReturnRoomBuilding(int roomId);
            
            [OperationContract]
            List<Event> ReturnRoomEvents(int roomName);
            
            [OperationContract]
            List<Event> ReturnBuildingEvents(int buildingId);
            
            [OperationContract]
            List<Time> ReturnTimes();
            
            [OperationContract]
            List<RepeatType> ReturnRepeatTypes();
            
            [OperationContract]
            List<Staff> ReturnStaff();
            
            [OperationContract]
            List<Student> ReturnStudents();
            
            [OperationContract]
            List<Student> ReturnCourseStudents(int courseId);

            #endregion

            #region Resource Creation

            [OperationContract]
            bool CheckBuildingExists(string buildingName);

            [OperationContract]
            int CreateNewBuilding(string buildingName, int buildingNumber, string addressLine1, string addressLine2, string postCode, string buildingCity, int creatorId);

            [OperationContract]
            Building ReturnBuildingDetail(int buildingId);

            [OperationContract]
            bool CheckRoomExists(string roomName);

            [OperationContract]
            int CreateNewRoom(int buildingId, string roomName, string roomDescription, int roomCapacity, int roomTypeId, int creatorId);

            [OperationContract]
            Room ReturnRoomDetail(int roomId);

            [OperationContract]
            bool CheckCourseExists(string courseName);

            [OperationContract]
            int CreateCourse(string courseName, string courseDescription, int creatorId, int duration);

            [OperationContract]
            Course ReturnCourseDetail(int courseId);
            
            [OperationContract]
            int ReturnModuleStudentsNumbers(int moduleId);

            [OperationContract]
            bool CheckModuleExists(string moduleName);

            [OperationContract]
            int CreateModule(string moduleName, string moduleDescription, int creatorId, int staffId);

            [OperationContract]
            Module ReturnModuleDetail(int moduleId);

            [OperationContract]
            bool CheckStaffExists(string staffName);

            [OperationContract]
            int CreateStaff(string staffTitle, string staffForename, string staffSurname, string staffEmail, string staffPassword, int courseId, int creatorId);

            [OperationContract]
            bool Check_Staff_Email_Exists(string email);

            [OperationContract]
            Staff ReturnStaffDetail(int staffId);

            [OperationContract]
            bool Check_Student_Email_Exists(string email);

            [OperationContract]
            bool CheckStudentExists(string studentName);

            [OperationContract]
            int CreateStudent(string studenttitle, string studentForeame, string studentSurname, string studentEmail,string studentPassword,int courseId, int yearStarted, int creatorId);

            [OperationContract]
            Student ReturnStudentDetail(int studentId);

            [OperationContract]
            int CreateNewRoomType(string roomTypeDescription, int creatorId);

            [OperationContract]
            int CreateNewRepeat(string repeatTypeName, string repeatTypeDescription);

            [OperationContract]
            int CreateSetting(string settingName, string settingDescription);

            #endregion

            #region Resource Management

            [OperationContract]
            bool EditRoom(int roomId, int buildingId, string roomName, string roomDescription, int roomCapacity, int roomTypeId, int creatorId);
            
            [OperationContract]
            bool EditBuilding(int buildingId, string buildingName, int buildingNumber, string buildingAddress1, string buildingAddress2, string city, string postcode, int userId);

            [OperationContract]
            bool EditCourse(int courseId, string courseName, int courseDuration, string courseDescription, int userId);

            [OperationContract]
            bool EditStaff(int staffId, string staffTitle, string staffForename, string staffSurname, string staffEmail, string staffPassword, int courseId);

            [OperationContract]
            bool EditStudent(int studentId, string studenttitle, string studentForeame, string studentSurname, string studentEmail, string studentPassword, int courseId, int yearStarted);
            
            #region Delete Resources

            [OperationContract]
            bool DeleteRoom(int roomId);
            
            [OperationContract]
            bool DeleteBuilding(int buildingId);
            
            [OperationContract]
            bool DeleteCourse(int courseId);

            [OperationContract]
            bool DeleteModule(int moduleId);
            
            [OperationContract]
            bool DeleteStaff(int studentId);

            [OperationContract]
            bool DeleteStudent(int staffId);
            
            [OperationContract]
            bool DeleteUser(int userId);

            #endregion

            #endregion

            #region Invites and Attendees

            #region Adding Invites and Attendants

            [OperationContract]
            bool AddModulesToCourse(List<Module> modules, int courseId);
            
            [OperationContract]
            bool AddModulesToEvent(List<CourseModule> modules, int eventId);
            
            [OperationContract]
            bool AddStaffAttendentsToEvent(List<Staff> staff, int eventId);
            
            [OperationContract]
            bool AddStudentsToModule(List<StudentModule> students, int moduleId);
            
            [OperationContract]
            bool AddStaffInvitesToEvent(List<Staff> staff, int eventId);

            [OperationContract]
            bool AddStudentInvitesToEvent(List<Student> students, int eventId);
            #endregion

            #region Remove Invites and Attendants

            [OperationContract]
            bool RemoveModulesFromCourse(List<CourseModule> coursemodules);
            
            [OperationContract]
            bool RemoveModulesFromEvent(List<ModuleEvent> moduleEvents);
            
            [OperationContract]
            bool RemoveStaffAttendentsFromEvent(List<StaffEvent> staff);
            
            [OperationContract]
            bool RemoveStaffInvitesFromEvent(List<StaffInvite> staff);
            
            [OperationContract]
            bool RemoveStudentInvitesFromEvent(List<StudentInvite> students);
            
            #endregion





            #region Return Lists
            [OperationContract]
            List<CourseModule> ReturnCoursesModules(int courseId);

            [OperationContract]
            List<StudentEvent> ReturnEventsStudentsAttendees(int eventId);

            [OperationContract]
            List<StaffEvent> ReturnEventsStaffAttendees(int eventId);

            [OperationContract]
            List<StudentInvite> ReturnEventsStudentInvites(int eventId);

            [OperationContract]
            List<StaffInvite> ReturnEventsStaffInvites(int eventId);
            #endregion


            #endregion

            #region Search Function
            [OperationContract]
            List<Event> SearchFunction(string appliedFilter, string searchItem);
            
            [OperationContract]
            List<Room> SearchRoomFunction(int buildingId, string searchItem);
            
            [OperationContract]
            List<Building> SearchBuildingFunction(string searchItem);

            [OperationContract]
            List<Course> SearchCourseFunction(string searchItem);
            
            [OperationContract]
            List<Staff> SearchStaffFunction(string searchItem);
            
            [OperationContract]
            List<Student> SearchStudentFunction(string searchItem); 
            
            [OperationContract]
            List<Student> SearchCourseStudentsFunction(string searchItem, int courseId); 
            
            [OperationContract]
            List<Module> SearchCourseModulesFunction(string searchItem, int courseId); 
            
            [OperationContract]
            List<Module> SearchModulesFunction(string searchItem);
            
            [OperationContract]
            List<Event> SearchEventsWithModulesFunction(string searchItem);
            
            [OperationContract]
            List<Event> SearchEventsWithRoomsOnlyFunction(string searchItem);

            #endregion

        }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TimetablingService : ITimetablingService
    {
        private readonly TimetableDatabase _dBase = new TimetableDatabase();
        private const int MaxId = 2000000;
        private readonly CultureInfo _currentculture = CultureInfo.CurrentCulture;

        #region Encryption and Decryption
        /// <summary>
        /// Encrypts login details before submission
        /// created using example in http://msdn.microsoft.com/en-us/library/system.security.cryptography.md5(v=vs.110).aspx
        /// </summary>
        /// <param name="unencryptedString"></param>
        /// <returns></returns>
        public string Encrypt(String unencryptedString)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(unencryptedString));

                var sBuilder = new StringBuilder();

                foreach (var b in data)
                {
                    sBuilder.Append(b.ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
        
        #endregion

        #region Registration

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Check_Email_Not_Exist(string email)
        {
            var emailList = _dBase.Users.Select(x => x.UserEmail).ToList();
            if (emailList.Contains(email))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Written: 05/11/2013
        /// Client Application passes across User object to be added to the database.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public bool Register_User(User newUser)
        {
            var genId = UserIdGeneration();
            if (newUser != null && genId != 0)
            {
                var generateUser = new User
                {
                    UserId = genId,
                    CreateDate = DateTime.Now,
                    LastLogin = DateTime.Now,
                    Password = newUser.Password,
                    UserEmail = newUser.UserEmail,
                    UserForename = newUser.UserForename,
                    UserSurname = newUser.UserSurname,
                    UserTitle = newUser.UserTitle,
                    UserType = 1
                };

                _dBase.Users.Add(generateUser);
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion

        #region Login

        /// <summary>
        /// Written: 05/11/2013
        /// Client passes across username and password fields, method below 
        /// checks the information and affirms or denies login request
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public int Login(string userEmail, string userPassword)
        {
            if (!String.IsNullOrEmpty(userEmail) && !String.IsNullOrEmpty(userPassword))
            {
                var temp = _dBase.Users.SingleOrDefault(x => x.UserEmail == userEmail);

                if (temp != null && temp.Password == userPassword)
                {
                    return temp.UserId;
                }
            }
            return 0;
        }

        #endregion

        #region Id Generation

        /// <summary>
        /// Written: 18/11/2013
        /// Refactored: 25/03/2014
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table for the building table or the current highest valued 
        /// Id in the Building table.
        /// </summary>
        /// <returns></returns>
        private int BuildingIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Building");
            const int maxIdValue = MaxId;
                
            var buildingCheck = _dBase.Buildings.Count();

            if (recycledIdCount == 0 && buildingCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && buildingCheck > 0)
            {
                var largestId = _dBase.Buildings.OrderByDescending(x => x.BuildingId).First().BuildingId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount > 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Building");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }

            return 0;

        }

        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table for the Course table or the current highest valued Id 
        /// in the Course table.
        /// </summary>
        /// <returns></returns>
        private int CourseIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Course");

            const int maxIdValue = MaxId;
            
            var courseCheck = _dBase.Courses.Count();

            if (recycledIdCount == 0 && courseCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && courseCheck != 0)
            {
                var largestId = _dBase.Courses.OrderByDescending(x => x.CourseId).First().CourseId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId =
                    _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Course");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;

        }

        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table in the Event table or the current highest valued Id in 
        /// the Event table.
        /// </summary>
        /// <returns></returns>
        private int EventIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Event");
            
            const int maxIdValue = MaxId;
            
            var eventCheck = _dBase.Events.Count();

            if (recycledIdCount == 0 && eventCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && eventCheck != 0)
            {
                var largestId = _dBase.Events.OrderByDescending(x => x.EventId).First().EventId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered)
                    .First(x => x.TableName == "Event");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;

            }
            return 0;
        }

        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table in the module table or the current highest valued Id 
        /// in the Module table. 
        /// </summary>
        /// <returns></returns>
        private int ModuleIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Module");
           
            const int maxIdValue = MaxId;

            var moduleCheck = _dBase.Modules.Count();

            if (recycledIdCount == 0 && moduleCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && moduleCheck != 0)
            {
                var largestId = _dBase.Modules.OrderByDescending(x => x.ModuleId).First().ModuleId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId =
                    _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Module");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int CourseModuleIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Course Module");
            
           const int maxIdValue = MaxId;

            var courseModuleCheck = _dBase.CourseModules.Count();

            if (recycledIdCount == 0 && courseModuleCheck == 0)
            {
                return 1;
            }

            if (recycledIdCount == 0 && courseModuleCheck != 0)
            {
                var largestId = _dBase.CourseModules.OrderByDescending(x => x.CourseModuleId).First().CourseModuleId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId =
                _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Course Module");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }
        
        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table for the Repeat types table or the current highest valued 
        /// Id in the Repeat type table.
        /// </summary>
        /// <returns></returns>
        private int RepeatTypesIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "RepeatType");
            
            const int maxIdValue = MaxId;

            var repeatsCheck = _dBase.RepeatTypes.Count();

            if (recycledIdCount == 0 && repeatsCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && repeatsCheck != 0)
            {
                var largestId = _dBase.RepeatTypes.OrderByDescending(x => x.RepeatTypeId).First().RepeatTypeId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId =
                    _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "RepeatType");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;

        }

        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids in the Room table table or the current highest valued Id in 
        /// the Room table.
        /// </summary>
        /// <returns></returns>
        private int RoomIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Room");

            const int maxIdValue = MaxId;

            var roomCheck = _dBase.Rooms.Count();

            if (recycledIdCount == 0 && roomCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && roomCheck != 0)
            {
                var largestId = _dBase.Rooms.OrderByDescending(x => x.RoomId).First().RoomId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Room");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }
        
        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids in the Room type table or the current highest valued Id in 
        /// the Room types table.
        /// </summary>
        /// <returns></returns>
        private int RoomTypeIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Room Type");
            
            const int maxIdValue = MaxId;
            
            var roomCheck = _dBase.RoomTypes.Count();

            if (recycledIdCount == 0 && roomCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && roomCheck != 0)
            {
                var largestId = _dBase.RoomTypes.OrderByDescending(x => x.RoomTypeId).First().RoomTypeId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Room Type");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }

        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids in the Staff table or the current highest valued Id in the 
        /// Staff table. 
        /// </summary>
        /// <returns></returns>
        private int StaffIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Staff");
            
            const int maxIdValue = MaxId;
            
            var staffCheck = _dBase.Staffs.Count();

            if (recycledIdCount == 0 && staffCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && staffCheck != 0)
            {
                var largestId = _dBase.Staffs.OrderByDescending(x => x.StaffId).First().StaffId;

                if (largestId != maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered)
                    .First(x => x.TableName == "Staff");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }

        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids in the Student table or the current highest valued Id in 
        /// the Student table. 
        /// </summary>
        /// <returns></returns>
        private int StudentInvitesIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Student Invite");
            const int maxIdValue = MaxId;
            
            var invitesCheck = _dBase.StudentInvites.Count();

            if (recycledIdCount == 0 && invitesCheck == 0)
            {
                return 1;
            }

            if (recycledIdCount == 0 && invitesCheck != 0)
            {
                var largestId = _dBase.StudentInvites.OrderByDescending(x => x.StudentInviteId).First().StudentInviteId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId =
                    _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Student Invite");
                    _dBase.RecycledIds.Remove(recoveredId);
                    _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }
        
        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids in the Student Module table or the current highest valued Id in 
        /// the Student table. 
        /// </summary>
        /// <returns></returns>
        private int StudentModulesIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Student Module");
            const int maxIdValue = MaxId;

            var existsCheck = _dBase.StudentModules.Count();

            if (recycledIdCount == 0 && existsCheck == 0)
            {
                return 1;
            }

            if (recycledIdCount == 0 && existsCheck != 0)
            {
                var largestId = _dBase.StudentModules.OrderByDescending(x => x.StudentModuleId).First().StudentModuleId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId =
                    _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Student Module");
                    _dBase.RecycledIds.Remove(recoveredId);
                    _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int ModuleEventsIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Module Invite");

            const int maxIdValue = MaxId;

            var inviteCheck = _dBase.ModuleEvents.Count();

            if (recycledIdCount == 0 && inviteCheck == 0)
            {
                return 1;
            }

            if (recycledIdCount == 0 && inviteCheck != 0)
            {
                var largestId = _dBase.ModuleEvents.OrderByDescending(x => x.EventModule).First().EventModule;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered)
                    .First(x => x.TableName == "Module Invite");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        } 
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int StaffInvitesIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Staff Invite");

            const int maxIdValue = MaxId;

            var inviteCheck = _dBase.StaffInvites.Count();

            if (recycledIdCount == 0 && inviteCheck == 0)
            {
                return 1;
            }

            if (recycledIdCount == 0 && inviteCheck != 0)
            {
                var largestId = _dBase.StaffInvites.OrderByDescending(x => x.StaffInviteId).First().StaffInviteId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered)
                    .First(x => x.TableName == "Staff Invite");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        } 
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private int StaffEventsIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Staff Event");

            const int maxIdValue = MaxId;

            var inviteCheck = _dBase.StaffEvents.Count();

            if (recycledIdCount == 0 && inviteCheck == 0)
            {
                return 1;
            }

            if (recycledIdCount == 0 && inviteCheck != 0)
            {
                var largestId = _dBase.StaffEvents.OrderByDescending(x => x.StaffEventId).First().StaffEventId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered)
                    .First(x => x.TableName == "Staff Event");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }

        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids in the Student table or the current highest valued Id in 
        /// the Student table. 
        /// </summary>
        /// <returns></returns>
        private int StudentIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Student");
            
            const int maxIdValue = MaxId;

            var studentCheck = _dBase.Students.Count();

            if (recycledIdCount == 0 && studentCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && studentCheck != 0)
            {
                var largestId = _dBase.Students.OrderByDescending(x => x.StudentId).First().StudentId;

                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId =
                    _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Student");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;
        }

        /// <summary>
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids in the User table or the current highest valued Id in the 
        /// User table. 
        /// </summary>
        /// <returns></returns>
        private int UserIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "User");
            
            const int maxIdValue = MaxId;

            var userCheck = _dBase.Users.Count();

            if (recycledIdCount == 0 && userCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && userCheck != 0)
            {
                var largestId = _dBase.Users.OrderByDescending(x => x.UserId).First().UserId;

                if (largestId != maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            if (recycledIdCount != 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "User");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            return 0;

        }

        #endregion

        #region Event Actions

        /// <summary>
        /// Written: 18/11/2012
        /// This method passes all the appropriate information into an event which can be 
        /// viewed and edited through the client software.
        /// </summary>
        /// <param name="eventTitle"></param>
        /// <param name="userId"></param>
        /// <param name="eventDescription"></param>
        /// <param name="eventType"></param>
        /// <param name="repeatType"></param>
        /// <param name="eventDuration"></param>
        /// <param name="startDate"></param>
        /// <param name="eventTime"></param>
        /// <param name="roomName"></param>
        /// <param name="courseName"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public int CreateEvent(string eventTitle, int userId, string eventDescription, string eventType,
            string repeatType, int eventDuration, DateTime startDate, string eventTime, string roomName,
            string courseName, string moduleName)
        {
            var generatedEventId = EventIdGeneration();

            if (generatedEventId == 0)
            {
                return 0;
            }
            
            var repeatId = 0;
            var repeat = _dBase.RepeatTypes.SingleOrDefault(x => x.RepeatTypeName == repeatType);
            
            if (repeatType != "0" && repeat != null)
            {
                repeatId = repeat.RepeatTypeId;
            } 
            
            var roomId = 0;
            var room = _dBase.Rooms.SingleOrDefault(x => x.RoomName == roomName);
            
            if (roomName != "0" && room != null)
            {
                roomId = room.RoomId;
            } 
            
            var timeId = 0;
            var time = _dBase.Times.SingleOrDefault(x => x.TimeLiteral == eventTime);
            
            if (eventTime != "0" && time != null)
            {
                timeId = time.TimeId;
            }
            
            var courseId = 0;
            var course = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName);

            if (courseName != "0" && course != null)
            {
                courseId = course.CourseId;
            }
            
            var moduleId = 0;
            var module = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName);
            
            if (moduleName != "0" && module != null)
            {
                moduleId = module.ModuleId;
            }

            var typeId = 0;
            var type = _dBase.EventTypes.SingleOrDefault(x => x.TypeName == eventType);
            
            if (eventType != "0" && type != null)
            {
                typeId = type.TypeId;
            } 
            
            var newEvent = new Event
            {
                EventId = generatedEventId,
                EventTitle = eventTitle,
                EventType = typeId,
                EventDescription = eventDescription,
                Repeats = repeatId,
                Duration = eventDuration,
                CreateDate = DateTime.Now,
                Status = "New",
                StartDate = startDate,
                LastDateEdited = DateTime.Now,
                Time = timeId,
                Room = roomId,
                AdditionalNotes = "None",
                LastUserEdited = userId,
                Module = moduleId,
                Course = courseId
            };

            _dBase.Events.Add(newEvent);
            _dBase.SaveChanges();

            return generatedEventId;
        }

        /// <summary>
        /// Written: 18/11/2012
        /// </summary>
        /// <param name="editedEventId"></param>
        /// <param name="userId"></param>
        /// <param name="eventTitle"></param>
        /// <param name="eventDescription"></param>
        /// <param name="eventType"></param>
        /// <param name="eventDuration"></param>
        /// <param name="startDate"></param>
        /// <param name="eventTime"></param>
        /// <param name="roomName"></param>
        /// <param name="courseName"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public bool EditEvent(int editedEventId, int userId, string eventTitle, string eventDescription,
            string eventType, int eventDuration, DateTime startDate, string eventTime,
            string roomName, string courseName, string moduleName)
        {
            var editedEvent = _dBase.Events.SingleOrDefault(x => x.EventId == editedEventId);

            if (editedEvent == null)
            {
                return false;
            }

            var repeatId = _dBase.RepeatTypes.First().RepeatTypeId;
            
            var roomId = 0;
            var room = _dBase.Rooms.SingleOrDefault(x => x.RoomName == roomName);

            if (roomName != "0" && room != null)
            {
                roomId = room.RoomId;
            }

            var timeId = 0;
            var time = _dBase.Times.SingleOrDefault(x => x.TimeLiteral == eventTime);

            if (eventTime != "0" && time != null)
            {
                timeId = time.TimeId;
            }

            var courseId = 0;
            var course = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName);

            if (courseName != "0" && course != null)
            {
                courseId = course.CourseId;
            }

            var moduleId = 0;
            var module = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName);

            if (moduleName != "0" && module != null)
            {
                moduleId = module.ModuleId;
            }

            var typeId = 0;
            var type = _dBase.EventTypes.SingleOrDefault(x => x.TypeName == eventType); 

            if (eventType != "0" && type != null)
            {
                typeId = type.TypeId;
            } 

            editedEvent.EventTitle = eventTitle;
            editedEvent.EventType = typeId;
            editedEvent.LastUserEdited = userId;
            editedEvent.EventDescription = eventDescription;
            editedEvent.Repeats = repeatId;
            editedEvent.Duration = eventDuration;
            editedEvent.CreateDate = DateTime.Now;
            editedEvent.Status = "New";
            editedEvent.StartDate = startDate;
            editedEvent.LastDateEdited = DateTime.Now;
            editedEvent.Time = timeId;
            editedEvent.Room = roomId;
            editedEvent.Module = moduleId;
            editedEvent.Course = courseId;

            _dBase.SaveChanges();

            return true;
        }


        public bool ChangeEventStatus(String status, int eventId)
        {
            if (eventId != 0)
            {
                var eventSelected = _dBase.Events.SingleOrDefault(x => x.EventId == eventId);
                if (eventSelected != null)
                {
                    if (eventSelected.Course != 0 && eventSelected.Module != 0 && eventSelected.Room != 0 && status == "Confirmed")
                    {
                        eventSelected.Status = status;
                        _dBase.SaveChanges();
                        return true;
                    }

                    eventSelected.Status = status;
                    _dBase.SaveChanges();
                    return true;
                }
                return false;
            }
            return false;
        }

        /// <summary>
        /// Written: 18/11/2012
        /// Deletes event by querying the events table using the event Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool DeleteEvent(int eventId)
        {
            var deletedEvent = _dBase.Events.SingleOrDefault(x => x.EventId == eventId);

            var newSavedId = new RecycledId
            {
                TableName = "Event",
                IdRecovered = eventId,
                DateAdded = DateTime.Now
            };

            _dBase.RecycledIds.Add(newSavedId);

            _dBase.Events.Remove(deletedEvent);

            _dBase.SaveChanges();

            return true;
        }

        #region Event Attendees

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public bool StaffEvent(int eventId, int staffId)
        {
            if (eventId != 0 && staffId != 0)
            {
                var idRecovery = _dBase.StaffEvents.SingleOrDefault(x => x.EventId == eventId);

                if (idRecovery != null)
                {
                    var recoveredId = new RecycledId
                    {
                        IdRecovered = idRecovery.StaffEventId,
                        TableName = "Staff Event",
                        DateAdded = DateTime.Now
                    };
                    _dBase.RecycledIds.Add(recoveredId);
                    _dBase.StaffEvents.Remove(idRecovery);
                    _dBase.SaveChanges();
                }

                var inviteId = StaffEventsIdGeneration();
                var staffEvent = new StaffEvent
                {
                    StaffEventId = inviteId,
                    StaffId = staffId,
                    EventId = eventId
                };
                _dBase.StaffEvents.Add(staffEvent);
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="moduleId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool ModuleEvent(int eventId, int moduleId, int courseId)
        {
            if (eventId != 0 && moduleId != 0 && courseId != 0)
            {
                var inviteId = ModuleEventsIdGeneration();

                var moduleEvent = new ModuleEvent
                {
                    EventModule = inviteId,
                    ModuleId = moduleId,
                    CourseId = courseId,
                    EventId = eventId
                };
                _dBase.ModuleEvents.Add(moduleEvent);
                _dBase.SaveChanges();

                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public bool DeleteStudentEvent(int inviteId)
        {
            var inviteSelected = _dBase.StudentEvents.SingleOrDefault(x => x.StudentEventId == inviteId);
            if (inviteSelected != null)
            {
                _dBase.StudentEvents.Remove(inviteSelected);
             
                var savedId = new RecycledId
                {
                    IdRecovered = inviteId,
                    TableName = "Student Event",
                    DateAdded = DateTime.Now
                };
                _dBase.RecycledIds.Add(savedId);
                _dBase.SaveChanges();

                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public bool DeleteStaffEvent(int inviteId)
        {
            var inviteSelected = _dBase.StaffEvents.SingleOrDefault(x => x.StaffEventId == inviteId);
            if (inviteSelected != null)
            {
                _dBase.StaffEvents.Remove(inviteSelected);

                var savedId = new RecycledId
                {
                    IdRecovered = inviteId,
                    TableName = "Staff Event",
                    DateAdded = DateTime.Now
                };
                _dBase.RecycledIds.Add(savedId);
                _dBase.SaveChanges();

                return true;
            }
            return false;
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public bool DeleteModuleEvent(int inviteId)
        {
            var inviteSelected = _dBase.StaffEvents.SingleOrDefault(x => x.StaffEventId == inviteId);
            if (inviteSelected != null)
            {
                _dBase.StaffEvents.Remove(inviteSelected);

                var savedId = new RecycledId
                {
                    IdRecovered = inviteId,
                    TableName = "Module Event",
                    DateAdded = DateTime.Now
                };
                _dBase.RecycledIds.Add(savedId);
                _dBase.SaveChanges();

                return true;
            }
            return false;
            
        }

        #endregion

        #region Event Invites
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool StudentInvite(int eventId, int studentId)
        {
            if (eventId != 0 && studentId != 0)
            {
                var inviteId = StudentInvitesIdGeneration();
                var studentEvent = new StudentInvite
                {
                    StudentInviteId = inviteId,
                    StudentId = studentId,
                    EventId = eventId,
                    Attending = true
                };
                _dBase.StudentInvites.Add(studentEvent);
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public bool StaffInvite(int eventId, int staffId)
        {
            if (eventId != 0 && staffId != 0)
            {
                var inviteId = StaffInvitesIdGeneration();
                var staffEvent = new StaffInvite
                {
                    StaffInviteId = inviteId,
                    StaffId = staffId,
                    EventId = eventId,
                    Attending = true
                };
                _dBase.StaffInvites.Add(staffEvent);
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public bool DeleteStudentInvite(int inviteId)
        {
            var inviteSelected = _dBase.StudentEvents.SingleOrDefault(x => x.StudentEventId == inviteId);
            if (inviteSelected != null)
            {
                _dBase.StudentEvents.Remove(inviteSelected);
             
                var savedId = new RecycledId
                {
                    IdRecovered = inviteId,
                    TableName = "Student Event",
                    DateAdded = DateTime.Now
                };
                _dBase.RecycledIds.Add(savedId);
                _dBase.SaveChanges();

                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public bool DeleteStaffInvite(int inviteId)
        {
            var inviteSelected = _dBase.StaffEvents.SingleOrDefault(x => x.StaffEventId == inviteId);
            if (inviteSelected != null)
            {
                _dBase.StaffEvents.Remove(inviteSelected);

                var savedId = new RecycledId
                {
                    IdRecovered = inviteId,
                    TableName = "Staff Event",
                    DateAdded = DateTime.Now
                };
                _dBase.RecycledIds.Add(savedId);
                _dBase.SaveChanges();

                return true;
            }
            return false;
        
        }

        #endregion

        #endregion

        #region Listed Types

        /// <summary>
        /// Writen: 02/12/13
        /// </summary>
        /// <returns></returns>
        public List<Event> ReturnEvents()
        {
            if (_dBase.Events.Count() != 0)
            {
                return _dBase.Events.ToList();
            }
            return null;
        }

        /// <summary>
        /// Writen: 02/12/13
        /// </summary>
        /// <returns></returns>
        public Event ReturnEventDetails(int eventId)
        {
            var eventSelected = _dBase.Events.SingleOrDefault(x => x.EventId == eventId);
            if (eventSelected != null)
            {
                return eventSelected;
            }
            return null;
        }
        
        /// <summary>
        /// Writen: 02/12/13
        /// </summary>
        /// <returns></returns>
        public List<Event> ReturnEventsWithModules()
        {
            var eventsWithModules = _dBase.Events.Where(x => x.Module > 0).ToList();
            if (eventsWithModules.Any())
            {
                return eventsWithModules;
            }
            return null;
        }
        
        /// <summary>
        /// Writen: 02/12/13
        /// </summary>
        /// <returns></returns>
        public List<Event> ReturnEventsWithRooms()
        {
            var eventsWithRooms = _dBase.Events.Where(x => x.Room > 0).ToList();
            if (eventsWithRooms.Any())
            {
                return eventsWithRooms;
            }
            return null;
        }
        
        /// <summary>
        /// Writen: 02/12/13
        /// </summary>
        /// <returns></returns>
        public List<Event> ReturnEventsWithRoomsNoModules()
        {
            var eventsWithRoomsNoModules = _dBase.Events.Where(x => x.Room > 0 && x.Module == 0 && x.Course == 0).ToList();
            if (eventsWithRoomsNoModules.Any())
            {
                return eventsWithRoomsNoModules;
            }
            return null;
        }

        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<EventType> ReturnEventTypes()
        {
            if (_dBase.EventTypes.Count() != 0)
            {
                return _dBase.EventTypes.ToList();
            }
            return null;
        }
      
        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public Staff ReturnEventStaff(int eventId)
        {
            var staffEvent = _dBase.StaffEvents.SingleOrDefault(x => x.EventId == eventId);
            if (staffEvent != null)
            {
                var staffMember = _dBase.Staffs.SingleOrDefault(x => x.StaffId == staffEvent.StaffId);

                if (staffMember != null)
                {
                    return staffMember;
                }
                return null;
            }
            return null;
        }
        
        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<RoomType> ReturnRoomTypes()
        {
            if (_dBase.RoomTypes.Count() != 0)
            {
                return _dBase.RoomTypes.ToList();
            }
            return null;
        }

        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<Course> ReturnCourses()
        {
            if (_dBase.Courses.Count() != 0)
            {
                return _dBase.Courses.ToList();
            }
            return null;
        }
        
        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<Module> ReturnModules()
        {
            if (_dBase.Modules.Count() != 0)
            {
                return _dBase.Modules.ToList();
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Student> ReturnModuleStudents(int moduleId)
        {
            if (moduleId != 0)
            {
                var moduleStudents = _dBase.StudentModules.Where(x => x.ModuleId == moduleId);
                var returnStudents = new List<Student>(); 
                
                foreach (var s in moduleStudents)
                {
                    returnStudents.Add(_dBase.Students.SingleOrDefault(x => x.StudentId == s.StudentId));
                }

                return returnStudents;
            }
            return null;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Staff> ReturnCourseStaff(int courseId)
        {
            if (courseId != 0)
            {
                var courseStaff = _dBase.Staffs.Where(x => x.Course == courseId);
                var returnStaff = new List<Staff>();

                foreach (var s in courseStaff)
                {
                    var returnedStaff = _dBase.Staffs.SingleOrDefault(x => x.Course == s.Course);
                    returnStaff.Add(returnedStaff);
                }

                return returnStaff;
            }
            return null;
        }

        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<Building> ReturnBuildings()
        {
            if (_dBase.Buildings.Count() != 0)
            {
                return _dBase.Buildings.ToList();
            }
            return null;
        }

        /// <summary>
        /// Written: 09/12/13
        /// Returns Id of the building Selected by searching by the name of the building
        /// </summary>
        /// <param name="buildingName"></param>
        /// <returns></returns>
        public int ReturnBuildingIdFromBuildingName(string buildingName)
        {
            if (!String.IsNullOrEmpty(buildingName))
            {
                var returnName = _dBase.Buildings.SingleOrDefault(x => x.BuildingName == buildingName);
                
                if (returnName != null)
                {
                    return returnName.BuildingId;
                }
            }
            return 0;
        }
        
        /// <summary>
        /// Written: 09/12/13
        /// 
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public int ReturnRoomTypeIdFromTypeName(string typeName)
        {
            if (!String.IsNullOrEmpty(typeName))
            {
                var returnName = _dBase.RoomTypes.SingleOrDefault(x => x.RoomeTypeDescription == typeName);
                
                if (returnName != null)
                {
                    return returnName.RoomTypeId;
                }
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public int ReturnRoomId(int buildingId, string roomName)
        {
            if (buildingId != 0)
            {
                var returnRoom = _dBase.Rooms.Where(x => x.Building == buildingId).SingleOrDefault(x => x.RoomName == roomName);
                if (returnRoom != null)
                {
                    return returnRoom.RoomId;
                }
            }

            return 0;
        }

        /// <summary>
        /// Written: 09/12/13
        /// Returns Id of the Course Selected by searching by the name of the course
        /// </summary>
        /// <param name="courseName"></param>
        /// <returns></returns>
        public int ReturnCourseIdFromCourseName(string courseName)
        {
            if (!String.IsNullOrEmpty(courseName))
            {
                var returnCourse = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName);
                if (returnCourse != null)
                {
                    return returnCourse.CourseId;
                }
            }

            return 0;
        }
        
        /// <summary>
        /// Written: 09/12/13
        /// Returns Id of the Course Selected by searching by the name of the course
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public int ReturnModuleIdFromModuleName(string moduleName)
        {
            if (!String.IsNullOrEmpty(moduleName))
            {
                var returnModule = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName);
                if (returnModule != null)
                {
                    return returnModule.ModuleId;
                }
            }

            return 0;
        }

        /// <summary>
        /// Written: 11/12/13
        /// Returns Modules according to the Course Id associated with them
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<Module> ReturnCourseModules(int courseId)
        {
            if (_dBase.CourseModules.Count(x => x.Course == courseId) != 0)
            {
                var moduleIdList = _dBase.CourseModules.Where(x => x.Course == courseId).Select(x => x.Module).ToList();
                var moduleList = new List<Module>();
                
                foreach(var moduleId in moduleIdList)
                {
                    moduleList.Add(_dBase.Modules.SingleOrDefault(x => x.ModuleId == moduleId));
                }

                if (moduleList.Count() != 0)
                {
                    return moduleList;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public List<Event> ReturnRoomEvents(int roomName)
        {
            if (roomName != 0)
            {
                return _dBase.Events.Where(x => x.Room == roomName).ToList();
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public List<Event> ReturnBuildingEvents(int buildingId)
        {
            if (buildingId != 0)
            {
                var buildingRooms = _dBase.Rooms.Where(x => x.Building == buildingId);
                
                var events = new List<Event>();

                foreach (var r in buildingRooms)
                {
                    events.AddRange(_dBase.Events.Where(x => x.Room == r.RoomId).ToList());
                }

                return events;
            }
            return null;
        }

        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<Room> ReturnBuildingRooms(int buildingId)
        {
            if (_dBase.Rooms.Count(x => x.Building == buildingId) != 0)
            {
                return _dBase.Rooms.Where(x => x.Building == buildingId).ToList();
            }
            return null;
        }

        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public int ReturnRoomBuilding(int roomId)
        {
            var returnBuilding = _dBase.Rooms.SingleOrDefault(x => x.RoomId == roomId);

            if (returnBuilding != null)
            {
                return returnBuilding.Building;
            }
            return 0;
        }

        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<Time> ReturnTimes()
        {
            if (_dBase.Times.Count() != 0)
            {
                return _dBase.Times.ToList();
            }
            return null;
        }

        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
         public List<RepeatType> ReturnRepeatTypes()
        {
            if (_dBase.RepeatTypes.Count() != 0)
            {
                return _dBase.RepeatTypes.ToList();
            }
            return null;
        }
        
        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<Staff> ReturnStaff()
        {
            if (_dBase.Staffs.Count() != 0)
            {
                return _dBase.Staffs.ToList();
            }
            return null;
        }
        
        /// <summary>
        /// Written: 21/11/2013
        /// </summary>
        /// <returns></returns>
        public List<Student> ReturnStudents()
        {
            if (_dBase.Students.Count() != 0)
            {
                return _dBase.Students.ToList();
            }
            return null;
        }

        public List<Student> ReturnCourseStudents(int courseId)
        {
            if(courseId != 0)
            {
                return _dBase.Students.Where(x=>x.Course ==  courseId).ToList();
            }
            return null;
        }

        public List<CourseModule> ReturnCoursesModules(int courseId)
        {
            if (courseId != 0)
            {
                var courseModules = _dBase.CourseModules.Where(x => x.Course == courseId).ToList();
                if (courseModules.Count() != 0)
                {
                    return courseModules;
                }
                return null;
            }
            return null;
        }

        public List<StudentEvent> ReturnEventsStudentsAttendees(int eventId)
        {
            if (eventId != 0)
            {
                var studentEvents = _dBase.StudentEvents.Where(x => x.EventId == eventId).ToList();
                if (studentEvents.Count() != 0)
                {
                    return studentEvents;
                }
                return null;
            }
            return null; 
        }

        public List<StaffEvent> ReturnEventsStaffAttendees(int eventId)
        {
            if (eventId != 0)
            {
                var staffEvents = _dBase.StaffEvents.Where(x => x.EventId == eventId).ToList();
                if (staffEvents.Count() != 0)
                {
                    return staffEvents;
                }
                return null;
            }
            return null;
        }
        
        public List<StudentInvite> ReturnEventsStudentInvites(int eventId)
        {
            if (eventId != 0)
            {
                var studentInvites = _dBase.StudentInvites.Where(x => x.EventId == eventId).ToList();
                if (studentInvites.Count() != 0)
                {
                    return studentInvites;
                }
                return null;
            }
            return null;
        }
        
        public List<StaffInvite> ReturnEventsStaffInvites(int eventId)
        {
            if (eventId != 0)
            {
                var staffInvites = _dBase.StaffInvites.Where(x => x.EventId == eventId).ToList();
                if (staffInvites.Count() != 0)
                {
                    return staffInvites;
                }
                return null;
            }
            return null;
        }

        #endregion

        #region Resource Creation and Maintanence 

        #region Create

        public bool CheckBuildingExists(string buildingName)
        {
            var buildingExists = _dBase.Buildings.SingleOrDefault(x => x.BuildingName == buildingName);

            if (buildingExists == null)
            {
                return false;
            }

            return true;
        }   
 
        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="buildingName"></param>
        /// <param name="buildingNumber"></param>
        /// <param name="addressLine1"></param>
        /// <param name="addressLine2"></param>
        /// <param name="postCode"></param>
        /// <param name="buildingCity"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int CreateNewBuilding(string buildingName, int buildingNumber, string addressLine1, string addressLine2, string postCode, string buildingCity, int creatorId)
        {
            var buildingId = BuildingIdGeneration();

            if (buildingId == 0)
            {
                return 0;
            }

            var newBuilding = new Building
            {
                BuildingId = buildingId,
                BuildingName = buildingName,
                BuildingNumber = buildingNumber,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                PostalCode = postCode,
                City = buildingCity,
                CreateDate = DateTime.Now,
                Creator = creatorId

            };

            _dBase.Buildings.Add(newBuilding);
            _dBase.SaveChanges();

            return buildingId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public Building ReturnBuildingDetail(int buildingId)
        {
            var buildingExists = _dBase.Buildings.SingleOrDefault(x => x.BuildingId == buildingId);

            if (buildingExists == null)
            {
                return null;
            }

            return buildingExists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public bool CheckRoomExists(string roomName)
        {
            var roomExists = _dBase.Rooms.SingleOrDefault(x => x.RoomName == roomName);

            if (roomExists == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="roomName"></param>
        /// <param name="roomDescription"></param>
        /// <param name="roomCapacity"></param>
        /// <param name="roomTypeId"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int CreateNewRoom(int buildingId, string roomName, string roomDescription, int roomCapacity, int roomTypeId, int creatorId)
        {
            var roomId = RoomIdGeneration();

            if (roomId == 0)
            {
                return 0;
            }

            var newRoom = new Room
            {
                RoomId = roomId,
                RoomName = roomName,
                Capacity = roomCapacity,
                RoomDescription = roomDescription,
                RoomType = roomTypeId,
                Building = buildingId,
                CreateDate = DateTime.Now,
                Creator = creatorId
            };

            _dBase.Rooms.Add(newRoom);
            _dBase.SaveChanges();

            return roomId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public Room ReturnRoomDetail(int roomId)
        {
            var roomExists = _dBase.Rooms.SingleOrDefault(x => x.RoomId == roomId);

            if (roomExists == null)
            {
                return null;
            }

            return roomExists;
        }

        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="roomTypeDescription"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int CreateNewRoomType(string roomTypeDescription, int creatorId)
        {
            var roomTypeId = RoomTypeIdGeneration();

            if (roomTypeId == 0)
            {
                return 0;
            }

            var newRoom = new RoomType
            {
                RoomTypeId = roomTypeId,
                RoomeTypeDescription = roomTypeDescription,
                CreateDate = DateTime.Now,
                Creator = creatorId
            };

            _dBase.RoomTypes.Add(newRoom);
            _dBase.SaveChanges();

            return roomTypeId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseName"></param>
        /// <returns></returns>
        public bool CheckCourseExists(string courseName)
        {
            var courseExists = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName);

            if (courseExists == null)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="courseDescription"></param>
        /// <param name="creatorId"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public int CreateCourse(string courseName, string courseDescription, int creatorId, int duration)
        {
             var courseId = CourseIdGeneration();

            if (courseId == 0)
            {
                return 0;
            }

            var createdCourse = new Course
            {
                CourseId = courseId,
                CourseName = courseName,
                CourseDescription = courseDescription,
                CreateDate = DateTime.Now,
                Creator = creatorId,
                Duration = duration
            };
            _dBase.Courses.Add(createdCourse);
            _dBase.SaveChanges();

            return courseId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public Course ReturnCourseDetail(int courseId)
        {
            var courseExists = _dBase.Courses.SingleOrDefault(x => x.CourseId == courseId);

            if (courseExists == null)
            {
                return null;
            }

            return courseExists;
        }

        public int ReturnModuleStudentsNumbers(int moduleId)
        {
            if (moduleId != 0)
            {
                var moduleStudents = _dBase.StudentModules.Where(x => x.ModuleId == moduleId).ToList();

                return moduleStudents.Count();
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public bool CheckModuleExists(string moduleName)
        {
            var courseExists = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName);

            if (courseExists == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="moduleDescription"></param>
        /// <param name="creatorId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public int CreateModule(string moduleName, string moduleDescription, int creatorId, int staffId)
        {
            var moduleId = ModuleIdGeneration();

            if (moduleId == 0)
            {
                return 0;
            }
            var newModule = new Module
            {
                CreateDate = DateTime.Now,
                Creator = creatorId,
                ModuleDescription = moduleDescription,
                ModuleName = moduleName,
                ModuleId = moduleId,
                Staff = staffId
            };
            _dBase.Modules.Add(newModule);
            _dBase.SaveChanges();

            return moduleId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public Module ReturnModuleDetail(int moduleId)
        {
            var moduleExists = _dBase.Modules.SingleOrDefault(x => x.ModuleId == moduleId);

            if (moduleExists == null)
            {
                return null;
            }

            return moduleExists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public int AddModuleToCourse(int courseId, int moduleId)
        {
            var courseModuleId = CourseModuleIdGeneration();

            if (courseId != 0 && moduleId != 0 && moduleId != 0)
            {
                var courseModule = new CourseModule
                {
                    CourseModuleId = courseModuleId,
                    Course = courseId,
                    Module = moduleId
                };

                _dBase.CourseModules.Add(courseModule);
                _dBase.SaveChanges();
                return courseModuleId;
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Check_Staff_Email_Exists(string email)
        {
            var emailCheck = _dBase.Staffs.SingleOrDefault(x => x.StaffEmail == email);
            if (emailCheck == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffEmail"></param>
        /// <returns></returns>
        public bool CheckStaffExists(string staffEmail)
        {
            var staffExists = _dBase.Staffs.SingleOrDefault(x => x.StaffEmail == staffEmail);

            if (staffExists == null)
            {
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffTitle"></param>
        /// <param name="staffForename"></param>
        /// <param name="staffSurname"></param>
        /// <param name="staffEmail"></param>
        /// <param name="staffPassword"></param>
        /// <param name="courseId"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int CreateStaff(string staffTitle, string staffForename, string staffSurname, string staffEmail, string staffPassword, int courseId, int creatorId)
        {
            var staffId = StaffIdGeneration();

            if (staffId == 0)
            {
                return 0;
            }

            var newStaff = new Staff
            {
                StaffId = staffId,
                Course = courseId,
                CreateDate = DateTime.Now,
                LastActivity = DateTime.Now,
                Password = staffPassword,
                StaffEmail = staffEmail,
                StaffForename = staffForename,
                StaffSurname =  staffSurname,
                StaffTitle = staffTitle,
                Creator = creatorId
            };

            _dBase.Staffs.Add(newStaff);
            _dBase.SaveChanges();

            return staffId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public Staff ReturnStaffDetail(int staffId)
        {
            var staffExists = _dBase.Staffs.SingleOrDefault(x => x.StaffId == staffId);

            if (staffExists == null)
            {
                return null;
            }

            return staffExists;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Check_Student_Email_Exists(string email)
        {
            var emailCheck = _dBase.Students.SingleOrDefault(x => x.StudentEmail == email);
            if (emailCheck == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentEmail"></param>
        /// <returns></returns>
        public bool CheckStudentExists(string studentEmail)
        {
            var staffExists = _dBase.Students.SingleOrDefault(x => x.StudentEmail == studentEmail);

            if (staffExists == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentTitle"></param>
        /// <param name="studentForename"></param>
        /// <param name="studentSurname"></param>
        /// <param name="studentEmail"></param>
        /// <param name="studentPassword"></param>
        /// <param name="courseId"></param>
        /// <param name="yearStarted"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int CreateStudent(string studentTitle, string studentForename, string studentSurname, string studentEmail, string studentPassword,int courseId, int yearStarted, int creatorId)
        {
            var studentId = StudentIdGeneration();

            if (studentId == 0)
            {
                return 0;
            }

            var newStudent = new Student
            {
                StudentId = studentId,
                Course = courseId,
                CreateDate = DateTime.Now,
                LastActivity = DateTime.Now,
                Password = studentPassword,
                StudentEmail = studentEmail,
                StudentForename = studentForename,
                StudentSurname = studentSurname,
                StudentTitle = studentTitle,
                Year = yearStarted,
                Creator = creatorId
            };

            _dBase.Students.Add(newStudent);
            _dBase.SaveChanges();

            return studentId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Student ReturnStudentDetail(int studentId)
        {
            var staffExists = _dBase.Students.SingleOrDefault(x => x.StudentId == studentId);

            if (staffExists == null)
            {
                return null;
            }

            return staffExists;
        }

        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="repeatTypeName"></param>
        /// <param name="repeatTypeDescription"></param>
        /// <returns></returns>
        public int CreateNewRepeat(string repeatTypeName, string repeatTypeDescription)
        {
            var repeatTypeId = RepeatTypesIdGeneration();

            if (repeatTypeId == 0)
            {
                return 0;
            }

            var newRepeatType = new RepeatType
            {
                RepeatTypeId = repeatTypeId,
                RepeatTypeName = repeatTypeName,
                RepeatDescription = repeatTypeDescription,
                CreateDate = DateTime.Now
            };

            _dBase.RepeatTypes.Add(newRepeatType);
            _dBase.SaveChanges();

            return repeatTypeId;
        }

        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="settingDescription"></param>
        /// <returns></returns>
        public int CreateSetting(string settingName, string settingDescription)
        {
            var newSetting = new Setting
            {
                SettingsId = 2,
                SettingName = settingName,
                SettingDescription = settingDescription,
                CreateDate = DateTime.Now,
                Creator = 1,
                Active = "Always"

            };

            _dBase.Settings.Add(newSetting);
            _dBase.SaveChanges();

            return 2;
        }
        #endregion

        #region Edit

        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="buildingId"></param>
        /// <param name="roomName"></param>
        /// <param name="roomDescription"></param>
        /// <param name="roomCapacity"></param>
        /// <param name="roomTypeId"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public bool EditRoom(int roomId, int buildingId, string roomName, string roomDescription, int roomCapacity, int roomTypeId, int creatorId)
        {
            var roomEdited = _dBase.Rooms.SingleOrDefault(x=>x.RoomId==roomId);

            if (roomEdited == null)
            {
                return false;
            }


            roomEdited.RoomId = roomId;
            roomEdited.RoomName = roomName;
            roomEdited.Capacity = roomCapacity;
            roomEdited.RoomDescription = roomDescription;
            roomEdited.RoomType = roomTypeId;
            roomEdited.Building = buildingId;
        
            _dBase.SaveChanges();

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="buildingName"></param>
        /// <param name="buildingNumber"></param>
        /// <param name="buildingAddress1"></param>
        /// <param name="buildingAddress2"></param>
        /// <param name="city"></param>
        /// <param name="postcode"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool EditBuilding(int buildingId, string buildingName, int buildingNumber, string buildingAddress1, string buildingAddress2, string city, string postcode, int userId)
        {
            var buildingEdited = _dBase.Buildings.SingleOrDefault(x => x.BuildingId == buildingId);

            if (buildingEdited == null)
            {
                return false;
            }


            buildingEdited.BuildingName = buildingName;
            buildingEdited.BuildingNumber = buildingNumber;
            buildingEdited.AddressLine1 = buildingAddress1;
            buildingEdited.AddressLine2 = buildingAddress2;
            buildingEdited.City = city;
            buildingEdited.PostalCode = postcode;
            buildingEdited.Creator = userId;
            
            _dBase.SaveChanges();

            return true;
        }

        public bool EditCourse(int courseId, string courseName, int courseDuration, string courseDescription, int userId)
        {
            var courseEdited = _dBase.Courses.SingleOrDefault(x => x.CourseId == courseId);

            if (courseEdited == null)
            {
                return false;
            }
            
            courseEdited.CourseName = courseName;
            courseEdited.CourseDescription = courseDescription;
            courseEdited.Duration = courseDuration;
            courseEdited.Creator = userId;
            _dBase.SaveChanges();

            return true;
        }

        public bool EditStaff(int staffId, string staffTitle, string staffForename, string staffSurname,
            string staffEmail, string staffPassword, int courseId)
        {
            var staffEdited = _dBase.Staffs.SingleOrDefault(x => x.StaffId == staffId);

            if (staffEdited == null)
            {
                return false;
            }

            staffEdited.StaffTitle = staffTitle;
            staffEdited.StaffForename = staffForename;
            staffEdited.StaffSurname = staffSurname;
            staffEdited.StaffEmail = staffEmail;
            staffEdited.Password = staffPassword;
            staffEdited.Course = courseId;

            _dBase.SaveChanges();
            return true;
        }


        public bool EditStudent(int studentId, string studenttitle, string studentForeame, string studentSurname,
            string studentEmail, string studentPassword, int courseId, int yearStarted)
        {
            var studentEdited = _dBase.Students.SingleOrDefault(x => x.StudentId == studentId);

            if (studentEdited == null)
            {
                return false;
            }

            studentEdited.StudentTitle = studenttitle;
            studentEdited.StudentForename = studentForeame;
            studentEdited.StudentSurname = studentSurname;
            studentEdited.StudentEmail = studentEmail;
            studentEdited.Password = studentPassword;
            studentEdited.Course = courseId;
            studentEdited.Year = yearStarted;

            _dBase.SaveChanges();
            return true;
        }
            
        #endregion

        #region Delete

        public bool DeleteRoom(int roomId)
        {
            if (roomId != 0)
            {
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = roomId,
                    TableName = "Room"
                };

                var room = _dBase.Rooms.SingleOrDefault(x => x.RoomId == roomId);
                var roomsEvents = _dBase.Events.Where(x => x.Room == roomId).ToList();

                if (room != null)
                {
                    _dBase.Rooms.Remove(room);
                    _dBase.RecycledIds.Add(recoveredId);
                }

                if (roomsEvents.Count() != 0)
                {
                    foreach (var e in roomsEvents)
                    {
                        e.Room = 0;
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false;
        }
        
        public bool DeleteBuilding(int buildingId)
        {
            if (buildingId != 0)
            {
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = buildingId,
                    TableName = "Building"
                };

                var building = _dBase.Buildings.SingleOrDefault(x => x.BuildingId == buildingId);
                var buildingRooms = _dBase.Rooms.Where(x => x.Building == buildingId).ToList();

                if (building != null)
                {
                    _dBase.Buildings.Remove(building);
                    _dBase.RecycledIds.Add(recoveredId);
                }


                if (buildingRooms.Count() != 0)
                {
                    foreach (var r in buildingRooms)
                    {
                        _dBase.Rooms.Remove(r);

                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = r.RoomId,
                            TableName = "Room"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);

                        var roomEvents = _dBase.Events.Where(x => x.Room == r.RoomId).ToList();

                        foreach (var e in roomEvents)
                        {
                           e.Room = 0;
                        }
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteCourse(int courseId)
        {
            if (courseId != 0)
            {
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = courseId,
                    TableName = "Course"
                };

                var course = _dBase.Courses.SingleOrDefault(x => x.CourseId == courseId);
                var courseModules = _dBase.CourseModules.Where(x => x.Course == courseId).ToList();

                if (course != null)
                {
                    _dBase.Courses.Remove(course);
                    _dBase.RecycledIds.Add(recoveredId);
                }


                if (courseModules.Count() != 0)
                {
                    foreach (var m in courseModules)
                    {
                        _dBase.CourseModules.Remove(m);

                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.Module,
                            TableName = "Module"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);

                        var moduleEvents = _dBase.Events.Where(x => x.Module == m.Module).ToList();

                        foreach (var e in moduleEvents)
                        {
                            e.Module = 0;
                        }
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false;   
        }

        public bool DeleteModule(int moduleId)
        {
            if (moduleId != 0)
            {
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = moduleId,
                    TableName = "Module"
                };

                var module = _dBase.Modules.SingleOrDefault(x => x.ModuleId == moduleId);
                var moduleEvents = _dBase.Events.Where(x => x.Module == moduleId).ToList();

                if (module != null)
                {
                    _dBase.Modules.Remove(module);
                    _dBase.RecycledIds.Add(recoveredId);
                }

                if (moduleEvents.Count() != 0)
                {
                    foreach (var e in moduleEvents)
                    {
                        e.Module = 0;
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false; 
        }

        public bool DeleteStudent(int studentId)
        {
            if (studentId != 0)
            {
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = studentId,
                    TableName = "Student"
                };

                var student = _dBase.Students.SingleOrDefault(x => x.StudentId == studentId);
                var studentModules = _dBase.StudentModules.Where(x => x.StudentId == studentId).ToList();
                var studentEvents = _dBase.StudentEvents.Where(x => x.StudentId == studentId).ToList();
                var studentInvites = _dBase.StudentInvites.Where(x => x.StudentId == studentId).ToList();

                if (student != null)
                {
                    _dBase.Students.Remove(student);
                    _dBase.RecycledIds.Add(recoveredId);
                }


                if (studentModules.Count() != 0)
                {
                    foreach (var m in studentModules)
                    {
                        _dBase.StudentModules.Remove(m);

                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.StudentModuleId,
                            TableName = "Student Module"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);                      
                    }
                }
                
                if (studentEvents.Count() != 0)
                {
                    foreach (var m in studentEvents)
                    {
                        _dBase.StudentEvents.Remove(m);

                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.StudentEventId,
                            TableName = "Student Event"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);                      
                    }
                }

                if (studentInvites.Count() != 0)
                {
                    foreach (var m in studentInvites)
                    {
                        _dBase.StudentInvites.Remove(m);
                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.StudentInviteId,
                            TableName = "Student Invite"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);                      
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false;   
        }

        public bool DeleteStaff(int staffId)
        {
            if(staffId != 0)
            {
            var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = staffId,
                    TableName = "Staff"
                };

                var staff = _dBase.Staffs.SingleOrDefault(x => x.StaffId == staffId);
                var staffModules = _dBase.Modules.Where(x => x.Staff == staffId);
                var staffEvents = _dBase.StaffEvents.Where(x => x.StaffId == staffId).ToList();
                var staffInvites = _dBase.StaffInvites.Where(x => x.StaffId == staffId).ToList();

                if (staff != null)
                {
                    _dBase.Staffs.Remove(staff);
                    _dBase.RecycledIds.Add(recoveredId);
                }


                if (staffModules.Count() != 0)
                {
                    foreach (var m in staffModules)
                    {
                        m.Staff = 0;
                    }
                }
                
                if (staffEvents.Count() != 0)
                {
                    foreach (var m in staffEvents)
                    {
                        _dBase.StaffEvents.Remove(m);

                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.StaffId,
                            TableName = "Staff Event"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);                      
                    }
                }

                if (staffInvites.Count() != 0)
                {
                    foreach (var m in staffInvites)
                    {
                        _dBase.StaffInvites.Remove(m);
                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.StaffId,
                            TableName = "Staff Invite"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);                      
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false;   
        }

        public bool DeleteUser(int userdId)
        {
            var usersCount = _dBase.Users.Count();

            if (usersCount > 1)
            {
                var selectedUser = _dBase.Users.SingleOrDefault(x => x.UserId == userdId);
                if (selectedUser != null)
                {
                    _dBase.Users.Remove(selectedUser);
                    return true;
                }
                return false;
            }

            return false;
        }

        #endregion

        #endregion
        
        #region Invites and Attendees


        public bool AddModulesToCourse(List<Module> modules, int courseId)
        {
            if (modules.Count != 0 && courseId != 0)
            {
                var allocatedCourseModules = _dBase.CourseModules.Where(x => x.Course == courseId);

                foreach (var cm in allocatedCourseModules)
                {
                    _dBase.CourseModules.Remove(cm);
                }

                foreach (var m in modules)
                {
                    var courseModuleId = CourseModuleIdGeneration();

                    var courseModule = new CourseModule
                    {
                        Course = courseId,
                        Module = m.ModuleId,
                        CourseModuleId = courseModuleId
                    };
                    _dBase.CourseModules.Add(courseModule);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public bool AddModulesToEvent(List<CourseModule> modules, int eventId)
        {
            if (modules.Count != 0 && eventId != 0)
            {
                foreach (var m in modules)
                {
                    var eventModuleId = ModuleEventsIdGeneration() ;

                    var eventModule = new ModuleEvent
                    {
                        EventId = eventId,
                        ModuleId = m.Module,
                        EventModule = eventModuleId,
                        CourseId = m.Course
                    };
                    _dBase.ModuleEvents.Add(eventModule);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public bool AddStaffAttendentsToEvent(List<Staff> staff, int eventId)
        {
            if (staff.Count != 0 && eventId != 0)
            {
                foreach (var s in staff)
                {
                    var staffEventId = StaffEventsIdGeneration();

                    var eventModule = new StaffEvent
                    {
                        EventId = eventId,
                        StaffId = s.StaffId,
                        StaffEventId = staffEventId
                    };
                    _dBase.StaffEvents.Add(eventModule);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public bool AddStudentsToModule(List<StudentModule> students, int moduleId)
        {
            if (students.Any())
            {
                var allAttendees = _dBase.StudentModules.Where(x => x.ModuleId == moduleId).ToList();

                foreach (var sm in allAttendees)
                {
                    _dBase.StudentModules.Remove(sm);
                }

                foreach (var s in students)
                {
                    var studentsEventId = StudentModulesIdGeneration();
                    if (studentsEventId != 0)
                    {
                        s.StudentModuleId = studentsEventId;
                        _dBase.StudentModules.Add(s);
                        _dBase.SaveChanges();
                    }
                }
                return true;
            }
            else
            {
                var allAttendees = _dBase.StudentModules.Where(x => x.ModuleId == moduleId).ToList();

                foreach (var sm in allAttendees)
                {
                    _dBase.StudentModules.Remove(sm);
                }
                _dBase.SaveChanges();
            }
            return true;
        }

        public bool AddStaffInvitesToEvent(List<Staff> staff, int eventId)
        {
            if (eventId != 0)
            {
                var allInvites = _dBase.StaffInvites.Where(x => x.EventId == eventId).ToList();

                if (allInvites.Count > 0)
                {
                    foreach (var i in allInvites)
                    {
                        var recycledId = new RecycledId
                        {
                            IdRecovered = i.StaffInviteId,
                            DateAdded = DateTime.Now.AddSeconds(1),
                            TableName = "Staff Invite"
                        };
                        _dBase.StaffInvites.Remove(i);
                        _dBase.RecycledIds.Add(recycledId);
                        _dBase.SaveChanges();

                    }
                }
                
                foreach (var s in staff)
                {
                    var staffInviteId = StaffInvitesIdGeneration();

                    var staffInvite = new StaffInvite
                    {
                        EventId = eventId,
                        StaffId = s.StaffId,
                        StaffInviteId = staffInviteId,
                        Attending = false
                    };
                    _dBase.StaffInvites.Add(staffInvite);
                    _dBase.SaveChanges();
                 }
                
                return true;
            }
            return false;
        }

        public bool AddStudentInvitesToEvent(List<Student> students, int eventId)
        {
            if (eventId != 0)
            {
                var allInvites = _dBase.StudentInvites.Where(x => x.EventId == eventId).ToList();

                if (allInvites.Count > 0)
                {
                    foreach (var i in allInvites)
                    {
                        var recycledId = new RecycledId
                        {
                            IdRecovered = i.StudentInviteId,
                            DateAdded = DateTime.Now.AddSeconds(1),
                            TableName = "Student Invite"
                        };
                        _dBase.StudentInvites.Remove(i);
                        _dBase.RecycledIds.Add(recycledId);
                        _dBase.SaveChanges();

                    }
                }
                
                foreach (var s in students)
                {
                    var studentInviteId = StudentInvitesIdGeneration();

                    var studentInvite = new StudentInvite
                    {
                        EventId = eventId,
                        StudentId = s.StudentId,
                        StudentInviteId = studentInviteId,
                        Attending = false
                    };
                    _dBase.StudentInvites.Add(studentInvite);
                    _dBase.SaveChanges();
                 }
                
                return true;
            }
            return false;
        }

        #endregion

        #region Remove Invites and Attendants

        public bool RemoveModulesFromCourse(List<CourseModule> coursemodules)
        {
            if (coursemodules.Count != 0)
            {
                foreach (var cm in coursemodules)
                {
                    var recoveredId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = cm.CourseModuleId,
                        TableName = "Course Module"
                    };

                    var deletedItem = _dBase.CourseModules.SingleOrDefault(x => x.CourseModuleId == cm.CourseModuleId);
                    _dBase.CourseModules.Remove(deletedItem);
                    _dBase.RecycledIds.Add(recoveredId);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;
        }
        
        public bool RemoveModulesFromEvent(List<ModuleEvent> moduleEvents)
        {
            if (moduleEvents.Count != 0)
            {
                foreach (var me in moduleEvents)
                {
                    var recoveredId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = me.EventModule,
                        TableName = "Event Module"
                    };

                    var deletedItem = _dBase.ModuleEvents.SingleOrDefault(x => x.EventModule == me.EventModule);
                    _dBase.ModuleEvents.Remove(deletedItem);
                    _dBase.RecycledIds.Add(recoveredId);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public bool RemoveStaffAttendentsFromEvent(List<StaffEvent> staff)
        {
            if (staff.Count != 0)
            {
                foreach (var me in staff)
                {
                    var recoveredId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = me.StaffEventId,
                        TableName = "Event Staff"
                    };

                    var deletedItem = _dBase.StaffEvents.SingleOrDefault(x => x.StaffEventId == me.StaffEventId);
                    _dBase.StaffEvents.Remove(deletedItem);
                    _dBase.RecycledIds.Add(recoveredId);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;
        }

        public bool RemoveStaffInvitesFromEvent(List<StaffInvite> staff)
        {
            if (staff.Count != 0)
            {
                foreach (var me in staff)
                {
                    var recoveredId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = me.StaffInviteId,
                        TableName = "Staff Invite"
                    };

                    var deletedItem = _dBase.StaffInvites.SingleOrDefault(x => x.StaffInviteId == me.StaffInviteId);
                    _dBase.StaffInvites.Remove(deletedItem);
                    _dBase.RecycledIds.Add(recoveredId);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;  
        }

        public bool RemoveStudentInvitesFromEvent(List<StudentInvite> students)
        {
            if (students.Count != 0)
            {
                foreach (var s in students)
                {
                    var recoveredId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = s.StudentInviteId,
                        TableName = "Student Invite"
                    };

                    var deletedItem = _dBase.StudentInvites.SingleOrDefault(x => x.StudentInviteId == s.StudentInviteId);
                    _dBase.StudentInvites.Remove(deletedItem);
                    _dBase.RecycledIds.Add(recoveredId);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;  
        }
        
        #endregion
        
        #region Search Functions

        /// <summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="appliedFilter"></param>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Event> SearchFunction(string appliedFilter, string searchItem)
        {
            if (!String.IsNullOrEmpty(appliedFilter))
            {
                if (String.IsNullOrEmpty(searchItem))
                {
                    return _dBase.Events.ToList();
                }

                var eventList = new List<Event>();

                if (appliedFilter == "Event Title")
                {
                    eventList.AddRange(_dBase.Events.Where(x => x.EventTitle.Contains(searchItem)).ToList());

                    return eventList;
                }
                if (appliedFilter == "Event Description")
                {
                    eventList.AddRange(_dBase.Events.Where(x => x.EventDescription.Contains(searchItem)).ToList());

                    return eventList;
                }

            }
            return null;
        }
        
        /// <summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Room> SearchRoomFunction(int buildingId, string searchItem)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var roomList = _dBase.Rooms.Where(x => x.RoomName.Contains(searchItem) || x.RoomDescription.Contains(searchItem)).ToList().Distinct();

                var returnRooms = new List<Room>();
                returnRooms.AddRange(roomList.Where(x => x.Building == buildingId).ToList());

                if (returnRooms.Any())
                {
                    return returnRooms;
                }
               
                    return null;
                
            }
            return _dBase.Rooms.Where(x=>x.Building == buildingId).ToList();
        }
        
        /// <summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Course> SearchCourseFunction(string searchItem)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var courseList = _dBase.Courses.Where(x => x.CourseName.Contains(searchItem) || x.CourseDescription.Contains(searchItem)).ToList().Distinct();

                var returnCourses = new List<Course>();
                returnCourses.AddRange(courseList);

                if (returnCourses.Any())
                {
                    return returnCourses;
                }
                return null;
            }
            return _dBase.Courses.ToList();
        }
        
        ///<summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Building> SearchBuildingFunction(string searchItem)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var buildingList = _dBase.Buildings.Where(x => x.BuildingName.Contains(searchItem) || x.AddressLine1.Contains(searchItem)|| x.AddressLine2.Contains(searchItem)|| x.City.Contains(searchItem)|| x.PostalCode.Contains(searchItem)).ToList().Distinct();

                var returnBuildings = new List<Building>();
                returnBuildings.AddRange(buildingList.ToList());

                if (returnBuildings.Any())
                {
                    return returnBuildings;
                }
               
                return null;
                
            }
            return _dBase.Buildings.ToList();
        }
        
        ///<summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Staff> SearchStaffFunction(string searchItem)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var staffList = _dBase.Staffs.Where(x => x.StaffForename.Contains(searchItem) || x.StaffSurname.Contains(searchItem)|| x.StaffEmail.Contains(searchItem));

                var returnStaff = new List<Staff>();
                returnStaff.AddRange(staffList.ToList());

                if (returnStaff.Any())
                {
                    return returnStaff;
                }
               
                return null;
                
            }
            return _dBase.Staffs.ToList();
        }
        
        ///<summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Student> SearchStudentFunction(string searchItem)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var studentList = _dBase.Students.Where(x => x.StudentForename.Contains(searchItem) || x.StudentSurname.Contains(searchItem)|| x.StudentEmail.Contains(searchItem));

                var returnStaff = new List<Student>();
                returnStaff.AddRange(studentList.ToList());

                if (returnStaff.Any())
                {
                    return returnStaff;
                }
               
                return null;
            }
            return _dBase.Students.ToList();
        }
        
        ///<summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<Student> SearchCourseStudentsFunction(string searchItem, int courseId)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var studentList = _dBase.Students.Where(x => x.StudentForename.Contains(searchItem) || x.StudentSurname.Contains(searchItem)|| x.StudentEmail.Contains(searchItem) && x.Course == courseId);

                var returnStaff = new List<Student>();
                returnStaff.AddRange(studentList.ToList());

                if (returnStaff.Any())
                {
                    return returnStaff;
                }
               
                return null;
            }
            return _dBase.Students.Where(x=>x.Course == courseId).ToList();
        }
        
        public List<Module> SearchCourseModulesFunction(string searchItem, int courseId)
        {
            var moduleList = _dBase.CourseModules.Where(x => x.Course == courseId).ToList();

            var returnModules = new List<Module>();

            if (!String.IsNullOrEmpty(searchItem))
            {

                foreach (var m in moduleList)
                {
                    returnModules.Add(_dBase.Modules.SingleOrDefault(x => x.ModuleId == m.CourseModuleId && (x.ModuleName.Contains(searchItem) || x.ModuleDescription.Contains(searchItem))));
                }
                
                if (returnModules.Any())
                {
                    return returnModules;
                }
               
                return null;
            }

            foreach (var m in moduleList)
                {
                    returnModules.Add(_dBase.Modules.SingleOrDefault(x => x.ModuleId == m.CourseModuleId));
                }
            return returnModules;
        }
        
        ///<summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Module> SearchModulesFunction(string searchItem)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var moduleList = _dBase.Modules.Where(x => x.ModuleName.Contains(searchItem) || x.ModuleDescription.Contains(searchItem));

                var returnModules = new List<Module>();
                returnModules.AddRange(moduleList.ToList());

                if (returnModules.Any())
                {
                    return returnModules;
                }
               
                return null;
            }
            return _dBase.Modules.ToList();
        }
        
        ///<summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Event> SearchEventsWithModulesFunction(string searchItem)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var eventList = _dBase.Events.Where(x => x.Module != 0 && (x.EventTitle.ToLower().Contains(searchItem.ToLower()) || x.EventDescription.ToLower().Contains(searchItem))).ToList();

                if (eventList.Any())
                {
                    return eventList;
                }
               
                return null;
            }
            return _dBase.Events.Where(x => x.Module != 0).ToList();
        }
        
        ///<summary>
        /// Written: 10/12/13
        /// Searches by aplpying a filer to the field searched for within the event
        /// then applaies a similar fitler of user input to select the events
        /// </summary>
        /// <param name="searchItem"></param>
        /// <returns></returns>
        public List<Event> SearchEventsWithRoomsOnlyFunction(string searchItem)
        {
            if (!String.IsNullOrEmpty(searchItem))
            {
                var eventList = _dBase.Events.Where(x => x.Room > 0 && x.Module == 0 && x.Course == 0).ToList();
                
                if (eventList.Any())
                {
                    var returnedEvents = eventList.Where(x => x.EventTitle.ToLower().Contains(searchItem.ToLower()) || x.EventDescription.ToLower().Contains(searchItem.ToLower())).ToList();
                    if (returnedEvents.Any())
                    {
                        return returnedEvents;
                    }
                    return null;
                }
               
                return null;
            }
            return _dBase.Events.Where(x => x.Room > 0 && x.Module == 0 && x.Course == 0).ToList();
        }

        #endregion

        #region Timetable Methods

        public List<Event> ReturnWeeksEvents(DateTime dateRequested, int roomId)
        {
            if (roomId != 0)
            {
                var dayRequested = dateRequested.DayOfWeek.ToString();
                var date = dateRequested.Day;
                var monthrequested = _currentculture.Calendar.GetMonth(dateRequested);
                var yearRequested = _currentculture.Calendar.GetYear(dateRequested);

                var newDate = new DateTime(yearRequested, monthrequested, date);

                var startDate = new DateTime();

                switch (dayRequested)
                {
                    case "Monday":
                        startDate = newDate.AddDays(0);
                        break;
                    case "Tuesday":
                        startDate = newDate.AddDays(-1);
                        break;
                    case "Wednesday":
                        startDate = newDate.AddDays(-2);
                        break;
                    case "Thursday":
                        startDate = newDate.AddDays(-3);
                        break;
                    case "Friday":
                        startDate = newDate.AddDays(-4);
                        break;
                    case "Saturday":
                        startDate = newDate.AddDays(-5);
                        break;
                    case "Sunday":
                        startDate = newDate.AddDays(-6);
                        break;
                }

                DateTime weekEnd = startDate.AddDays(7);

                var eventsList =
                    _dBase.Events.Where(x => x.StartDate >= startDate && x.StartDate < weekEnd && x.Room == roomId).ToList();

                return eventsList;
            }
            return null;

        }

        public TimetableEventsListObject ReturnWeeksEventsWithFilters(DateTime dateRequested, int roomId, int moduleId)
        {
            if (roomId != 0 && moduleId != 0)
            {
                var dayRequested = dateRequested.DayOfWeek.ToString();
                var date = dateRequested.Day;
                var monthrequested = _currentculture.Calendar.GetMonth(dateRequested);
                var yearRequested = _currentculture.Calendar.GetYear(dateRequested);

                var newDate = new DateTime(yearRequested, monthrequested, date);

                var startDate = new DateTime();

                switch (dayRequested)
                {
                    case "Monday":
                        startDate = newDate.AddDays(0);
                        break;
                    case "Tuesday":
                        startDate = newDate.AddDays(-1);
                        break;
                    case "Wednesday":
                        startDate = newDate.AddDays(-2);
                        break;
                    case "Thursday":
                        startDate = newDate.AddDays(-3);
                        break;
                    case "Friday":
                        startDate = newDate.AddDays(-4);
                        break;
                    case "Saturday":
                        startDate = newDate.AddDays(-5);
                        break;
                    case "Sunday":
                        startDate = newDate.AddDays(-6);
                        break;
                }

                DateTime weekEnd = startDate.AddDays(7);

                var eventsList =
                    _dBase.Events.Where(x => x.StartDate >= startDate && x.StartDate < weekEnd && x.Room == roomId && x.Module == moduleId).ToList();

                var timetableResult = new TimetableEventsListObject
                {
                    MondayList = new List<TimetableEventObject>(),
                    TuesdayList = new List<TimetableEventObject>(),
                    WednesdayList = new List<TimetableEventObject>(),
                    ThursdayList = new List<TimetableEventObject>(),
                    FridayList = new List<TimetableEventObject>(),
                    SaturdayList = new List<TimetableEventObject>(),
                    SundayList = new List<TimetableEventObject>()
                };

                
                var timeslot = _dBase.Times.ToList().OrderBy(x => x.TimeId);
                var confirmedEvents = new List<Event>();

                confirmedEvents.AddRange(eventsList.Where(x=>x.Status=="Confirmed").ToList());
                
                foreach (var t in timeslot)
                    {
                        var eventSelected = new Event();
                        foreach (Event e in confirmedEvents)
                        {
                            var tempDay = Convert.ToDateTime(e.StartDate).DayOfWeek.ToString();

                            var timeEventObject = new TimetableEventObject
                            {
                                Event = eventSelected,
                                Time = t
                            };

                            switch (tempDay)
                            {
                                case "Monday":
                                    timetableResult.MondayList.Add(timeEventObject);
                                    break;
                                case "Tuesday":
                                    timetableResult.TuesdayList.Add(timeEventObject);
                                    break;
                                case "Wednesday":
                                    timetableResult.WednesdayList.Add(timeEventObject);
                                    break;
                                case "Thursday":
                                    timetableResult.ThursdayList.Add(timeEventObject);
                                    break;
                                case "Friday":
                                    timetableResult.FridayList.Add(timeEventObject);
                                    break;
                                case "Saturday":
                                    timetableResult.SaturdayList.Add(timeEventObject);
                                    break;
                                case "Sunday":
                                    timetableResult.SundayList.Add(timeEventObject);
                                    break;
                            }
                        }
                    
                }
                return timetableResult;
            }
            return null;

        }
      
        public TimetableDisplayListObject ReturnTimetableToolListObject()
        {
            var startDate = _dBase.Settings.SingleOrDefault(x => x.SettingName == "StartDate");
            var endDate = _dBase.Settings.SingleOrDefault(x => x.SettingName == "EndDate");

            if (startDate != null && endDate != null)
            {
                if (!String.IsNullOrEmpty(startDate.SettingDescription) && !String.IsNullOrEmpty(endDate.SettingDescription))
                {
                    var beginDate = Convert.ToDateTime(startDate.SettingDescription);
                    var finalDate = Convert.ToDateTime(endDate.SettingDescription);

                    int totalDays = Convert.ToInt32((finalDate.Date - beginDate).TotalDays);
                    totalDays = totalDays/7;

                    var timetableResult = new TimetableDisplayListObject
                    {
                        MondayList = new List<TimetableObject>(),
                        TuesdayList = new List<TimetableObject>(),
                        WednesdayList = new List<TimetableObject>(),
                        ThursdayList = new List<TimetableObject>(),
                        FridayList = new List<TimetableObject>(),
                        SaturdayList = new List<TimetableObject>(),
                        SundayList = new List<TimetableObject>()
                    };

                    var daysList = new List<String>();

                    daysList.Add("Monday");
                    daysList.Add("Tuesday");
                    daysList.Add("Wednesday");
                    daysList.Add("Thursday");
                    daysList.Add("Friday");
                    daysList.Add("Saturday");
                    daysList.Add("Sunday");

                    var eventsAvailable = _dBase.Events.ToList().OrderBy(x => x.Time);
                    var timeslot = _dBase.Times.ToList().OrderBy(x => x.TimeId);


                    foreach (var d in daysList)
                    {
                        foreach (var t in timeslot)
                        {

                            var count = 0;

                            foreach (Event e in eventsAvailable)
                            {
                                var tempDay = e.StartDate;

                                if (tempDay.DayOfWeek.ToString() == d && e.Time == t.TimeId)
                                {
                                    count += 1;
                                }
                            }

                            var timeObject = new TimetableObject
                            {
                                Timeslot = t.TimeId,
                                Enumerations = count,
                                Percentage = 0
                            };

                            switch (d)
                            {
                                case "Monday":
                                    timetableResult.MondayList.Add(timeObject);
                                    break;
                                case "Tuesday":
                                    timetableResult.TuesdayList.Add(timeObject);
                                    break;
                                case "Wednesday":
                                    timetableResult.WednesdayList.Add(timeObject);
                                    break;
                                case "Thursday":
                                    timetableResult.ThursdayList.Add(timeObject);
                                    break;
                                case "Friday":
                                    timetableResult.FridayList.Add(timeObject);
                                    break;
                                case "Saturday":
                                    timetableResult.SaturdayList.Add(timeObject);
                                    break;
                                case "Sunday":
                                    timetableResult.SundayList.Add(timeObject);
                                    break;
                            }
                        }
                    }

                    CalculatePercentages(timetableResult.MondayList, totalDays);
                    CalculatePercentages(timetableResult.TuesdayList, totalDays);
                    CalculatePercentages(timetableResult.WednesdayList, totalDays);
                    CalculatePercentages(timetableResult.ThursdayList, totalDays);
                    CalculatePercentages(timetableResult.FridayList, totalDays);
                    CalculatePercentages(timetableResult.SaturdayList, totalDays);
                    CalculatePercentages(timetableResult.SundayList, totalDays);

                    return timetableResult;
                }
            }

            return null;
        }

        public List<TimetableObject> CalculatePercentages(List<TimetableObject> dayList, int totalDays)
        {
            foreach (TimetableObject t in dayList)
            {
                t.Percentage = Convert.ToInt32(t.Enumerations / totalDays);
            } 

            return dayList;
        }

        #endregion
    }

}
