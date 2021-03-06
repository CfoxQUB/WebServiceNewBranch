﻿using System;
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
            String ChangeEventStatus(String status, int eventId);

            #region Event Attendees
            
            [OperationContract]
            bool StaffEvent(int eventId, int staffId);

            [OperationContract]
            bool ModuleEvent(int eventId, int moduleId, int courseId);
            
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
            TimetableEventsListObject ReturnWeeksEventsWithFilters(DateTime dateRequested, int roomId);

            [OperationContract]
            TimetableEventsListObject ReturnWeeksEventsForCourses(DateTime dateRequested, int courseId);
            
            [OperationContract]
            TimetableDisplayListObject ReturnTimetableToolListObject();
            
            #endregion 

            #region Listed Types

            #region Basic Types

            [OperationContract]
            List<Event> ReturnEvents();

            [OperationContract]
            List<EventType> ReturnEventTypes();

            [OperationContract]
            List<RoomType> ReturnRoomTypes();

            [OperationContract]
            List<Building> ReturnBuildings();

            [OperationContract]
            List<Course> ReturnCourses();

            [OperationContract]
            List<Module> ReturnModules();

            [OperationContract]
            List<Time> ReturnTimes();

            [OperationContract]
            List<RepeatType> ReturnRepeatTypes();

            [OperationContract]
            List<Staff> ReturnStaff();

            [OperationContract]
            List<Student> ReturnStudents();

            #endregion


            #region Return Event Associated Information
            [OperationContract]
            List<Event> ReturnEventsWithModules();
            
            [OperationContract]
            List<Event> ReturnEventsWithRooms();
            
            [OperationContract]
            List<Event> ReturnEventsWithRoomsNoModules();

            [OperationContract]
            Staff ReturnEventStaff(int eventId);
            #endregion

            #region Return Building Associated Information
            
            [OperationContract]
            int ReturnBuildingIdFromBuildingName(string buildingName);

            [OperationContract]
            List<Room> ReturnBuildingRooms(int buildingId);

            [OperationContract]
            List<Event> ReturnBuildingEvents(int buildingId);

            #endregion

            #region Return Room Associated Information

            [OperationContract]
            int ReturnRoomTypeIdFromTypeName(string typeName);

            [OperationContract]
            int ReturnRoomId(int buildingId, string roomName);

            [OperationContract]
            int ReturnRoomBuilding(int roomId);

            [OperationContract]
            List<Event> ReturnRoomEvents(int roomName);

            #endregion

            #region Return Course Associated Information

            [OperationContract]
            int ReturnCourseIdFromCourseName(string courseName);
            
            [OperationContract]
            List<Staff> ReturnCourseStaff(int courseId);
            
            [OperationContract]
            List<Module> ReturnCourseModules(int courseId);

            [OperationContract]
            List<Student> ReturnCourseStudents(int courseId);

            #endregion

            #region Return Module Associated Information

            [OperationContract]
            int ReturnModuleIdFromModuleName(string moduleName);
            
            [OperationContract]
            int ReturnModuleStudentsNumbers(int moduleId);
            
            [OperationContract]
            List<Student> ReturnModuleStudents(int moduleId);

            #endregion

            #endregion

            #region Resource Creation

            #region Checks for Existing records

            [OperationContract]
            bool CheckBuildingExists(string buildingName);

            [OperationContract]
            bool CheckRoomExists(string roomName);

            [OperationContract]
            bool CheckCourseExists(string courseName);

            [OperationContract]
            bool CheckModuleExists(string moduleName);

            [OperationContract]
            bool CheckStaffExists(string staffName);

            [OperationContract]
            bool Check_Staff_Email_Exists(string email);

            [OperationContract]
            bool CheckStudentExists(string studentName);

            [OperationContract]
            bool Check_Student_Email_Exists(string email);

            #endregion
            
            #region Creation of Records
            [OperationContract]
            int CreateNewBuilding(string buildingName, int buildingNumber, string addressLine1, string addressLine2, string postCode, string buildingCity, int creatorId);
            
            [OperationContract]
            int CreateNewRoom(int buildingId, string roomName, string roomDescription, int roomCapacity, int roomTypeId, int creatorId);

            [OperationContract]
            int CreateCourse(string courseName, string courseDescription, int creatorId, int duration);

            [OperationContract]
            int CreateModule(string moduleName, string moduleDescription, int creatorId, int staffId);

            [OperationContract]
            int CreateStaff(string staffTitle, string staffForename, string staffSurname, string staffEmail, string staffPassword, int courseId, int creatorId);

            [OperationContract]
            int CreateStudent(string studenttitle, string studentForeame, string studentSurname, string studentEmail, string studentPassword, int courseId, int yearStarted, int creatorId);

            //Not Used - area for further development
            [OperationContract]
            int CreateNewRoomType(string roomTypeDescription, int creatorId);
            //Not Used - area for further development
            [OperationContract]
            int CreateNewRepeat(string repeatTypeName, string repeatTypeDescription);
            //Not Used - area for further development
            [OperationContract]
            int CreateSetting(string settingName, string settingDescription);
            #endregion

            #region Returning Record Details
            [OperationContract]
            Event ReturnEventDetails(int eventId);
            
            [OperationContract]
            Building ReturnBuildingDetail(int buildingId);

            [OperationContract]
            Room ReturnRoomDetail(int roomId);

            [OperationContract]
            Course ReturnCourseDetail(int courseId);

            [OperationContract]
            Module ReturnModuleDetail(int moduleId);

            [OperationContract]
            Staff ReturnStaffDetail(int staffId);

            [OperationContract]
            Student ReturnStudentDetail(int studentId);
            #endregion
            
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

            


            #region Return Lists
            [OperationContract]
            List<CourseModule> ReturnCoursesModules(int courseId);

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

        #region Client User Functions

        #region Encryption
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
                //String passed in hashed into array of bytes
                byte[] data = md5Hash.ComputeHash(Encoding.ASCII.GetBytes(unencryptedString));
                
                //Array of bytes concatenated into a string
                var sBuilder = new StringBuilder();
                foreach (var b in data)
                {
                    sBuilder.Append(b.ToString("x2"));
                }
                //return hased string
                return sBuilder.ToString();
            }
        }
        
        #endregion

        #region Registration

        /// <summary>
        /// Client Application passes across User object to be added to the database.
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        public bool Register_User(User newUser)
        {
            //New User Id generated
            var genId = UserIdGeneration();
            if (newUser != null && genId != 0)
            {   // new user information added to a new user object
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
                //User added to the database
                _dBase.Users.Add(generateUser);
                _dBase.SaveChanges();
                return true;
            }
            //Either Id generate 0 or user passed in is null
            return false;
        }

        /// <summary>
        ///  Check Made to ensure email address does not already
        /// exist in another registered users information
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Check_Email_Not_Exist(string email)
        {
            //List of registered emails returned
            var emailList = _dBase.Users.Select(x => x.UserEmail).ToList();
            //If email already exists false returned
            if (emailList.Contains(email))
            {
                return false;
            }
            //If user email does not exist true returned
            return true;
        }
        
        #endregion

        #region Login

        /// <summary>
        /// Client passes across username and password fields, method below 
        /// checks the information and affirms or denies login request
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public int Login(string userEmail, string userPassword)
        {
            //Checks made to ensure emai; and password arent empty strings or null values
            if (!String.IsNullOrEmpty(userEmail) && !String.IsNullOrEmpty(userPassword))
            {
                //Check to see if user exists
                var temp = _dBase.Users.SingleOrDefault(x => x.UserEmail == userEmail);

                if (temp != null && temp.Password == userPassword)
                {
                    //Returns Id if user exists and password matches
                    return temp.UserId;
                }
            }
            //Login failed 0 returned
            return 0;
        }

        #endregion
        
        #endregion
        
        #region Id Generation

        /// <summary>
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table for the building table or the current highest valued 
        /// Id in the Building table.
        /// </summary>
        /// <returns></returns>
        private int BuildingIdGeneration()
        {
            //Check for values in the recycled Ids table for type Builing
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Building");
            //Setting Max Id
            const int maxIdValue = MaxId;
            //returning count of current buildings in List    
            var buildingCheck = _dBase.Buildings.Count();
            //First default value is 1
            if (recycledIdCount == 0 && buildingCheck == 0)
            {
                return 1;
            }
            //If there are  no recycled Ids and buildings exist new id generated from Database values
            if (recycledIdCount == 0 && buildingCheck > 0)
            {
                var largestId = _dBase.Buildings.OrderByDescending(x => x.BuildingId).First().BuildingId;
                //Largest Id found +1 
                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }

                return 0;
            }
            
            //If Ids exist that are to be recycled Lowest value selected
            if (recycledIdCount > 0)
            {
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Building");
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                return recoveredId.IdRecovered;
            }
            //no further Ids can be generated 0 returned
            return 0;

        }

        /// <summary>
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table for the Course table or the current highest valued Id 
        /// in the Course table.
        /// </summary>
        /// <returns></returns>
        private int CourseIdGeneration()
        {   //check for recyled course Ids
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Course");
            //Max id set
            const int maxIdValue = MaxId;
            //Check for courses currently in database
            var courseCheck = _dBase.Courses.Count();

            //If first entry 1 returned
            if (recycledIdCount == 0 && courseCheck == 0)
            {
                return 1;
            }
            
            //If courses exist and no recycled Ids Highest id selected +1
            if (recycledIdCount == 0 && courseCheck != 0)
            {
                //Largest value returned
                var largestId = _dBase.Courses.OrderByDescending(x => x.CourseId).First().CourseId;

                //as long as id generated below the highest Id value +1 to id returned
                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }
                //No furthe ids can be generate 0 returned
                return 0;
            }
            //Recycled Ids selected and returned
            if (recycledIdCount != 0)
            {
                //recycled Id selected
                var recoveredId =
                    _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Course");
                
                //recycled Id removed
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                //recycled id returned
                return recoveredId.IdRecovered;
            }
            // No recycled Ids returned 0 returned
            return 0;

        }

        /// <summary>
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table in the Event table or the current highest valued Id in 
        /// the Event table.
        /// </summary>
        /// <returns></returns>
        private int EventIdGeneration()
        {   //Check for recycled event Ids
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Event");
            //Max Id set
            const int maxIdValue = MaxId;
            //Return number of events that currently exist
            var eventCheck = _dBase.Events.Count();

            //If first event entry 1 returned
            if (recycledIdCount == 0 && eventCheck == 0)
            {
                return 1;
            }
            
            //if no recycled ids and events exist highest value selected and +1 returned
            if (recycledIdCount == 0 && eventCheck != 0)
            {
                //highest id returned
                var largestId = _dBase.Events.OrderByDescending(x => x.EventId).First().EventId;

                //as long as max id not met id +1 returned
                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }
                //no further ids can be generated
                return 0;
            }
            
            //Recycled Ids exist
            if (recycledIdCount != 0)
            {
                //Recycled Id selected
                var recoveredId = _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered)
                    .First(x => x.TableName == "Event");
                
                //recycled Id removed
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();

                //recycled Id returned
                return recoveredId.IdRecovered;

            }
            //No ids available 0 returned
            return 0;
        }

        /// <summary>
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table in the module table or the current highest valued Id 
        /// in the Module table. 
        /// </summary>
        /// <returns></returns>
        private int ModuleIdGeneration()
        {
            //check for recycled Ids
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Module");
            //max id value set
            const int maxIdValue = MaxId;
            //count of modules already in database
            var moduleCheck = _dBase.Modules.Count();

            //first database value 1
            if (recycledIdCount == 0 && moduleCheck == 0)
            {
                return 1;
            }
            
            //no recycled Ids avaiabel id generated from db content
            if (recycledIdCount == 0 && moduleCheck != 0)
            {
                //largets id selected
                var largestId = _dBase.Modules.OrderByDescending(x => x.ModuleId).First().ModuleId;

                //as long as max id not met selected id +1 returned
                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }
                //if no recyeled Ids and max id met 0 returned
                return 0;
            }
            //if recycled ids available highests selected
            if (recycledIdCount != 0)
            {
                //recycled Id selcted
                var recoveredId =
                    _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Module");
                //recycled Id removed
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();
                //Id recovered returned
                return recoveredId.IdRecovered;
            }
            //no further ids available 0 returned
            return 0;
        }
        
        /// <summary>
        /// Course Module Id generation based on the contecnt of the databse
        /// and the recycled Ids which are marked by Course Module
        /// </summary>
        /// <returns></returns>
        private int CourseModuleIdGeneration()
        {
            //recycled Ids checked
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Course Module");
            //max id set
           const int maxIdValue = MaxId;
            //current database content counted
            var courseModuleCheck = _dBase.CourseModules.Count();

            //first database value id always 1
            if (recycledIdCount == 0 && courseModuleCheck == 0)
            {
                return 1;
            }

            //If no recycled ids available databse values used to generate id
            if (recycledIdCount == 0 && courseModuleCheck != 0)
            {
                //largest Id selected
                var largestId = _dBase.CourseModules.OrderByDescending(x => x.CourseModuleId).First().CourseModuleId;
                //as long as max id not met id returned is +1
                if (largestId < maxIdValue)
                {
                    return largestId + 1;
                }
                //no id can be genereate 0 returned
                return 0;
            }
            //if reycled ids exist recycled ids used
            if (recycledIdCount != 0)
            {
                //Id recovered
                var recoveredId =
                _dBase.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Course Module");
                //Id removed
                _dBase.RecycledIds.Remove(recoveredId);
                _dBase.SaveChanges();
                //Id returned
                return recoveredId.IdRecovered;
            }
            //no more ids available 0 returned
            return 0;

        }
        
        /// <summary>
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table for the Repeat types table or the current highest valued 
        /// Id in the Repeat type table.
        /// Currently not used due to set repeat types not used (future developent)
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
            //Id generated
            var generatedEventId = EventIdGeneration();

            //If id value continue if not return 0
            if (generatedEventId == 0)
            {
                return 0;
            }
            //repeat Id set to default if repeats not implemented
            var repeatId = 0;

            //repeat type retunred repeat id set withni the event object
            var repeat = _dBase.RepeatTypes.SingleOrDefault(x => x.RepeatTypeName == repeatType);
            if (repeatType != "0" && repeat != null)
            {
                repeatId = repeat.RepeatTypeId;
            } 
            
            var roomId = 0;
            //default room is 0 unless a room has been passed in from the client
            var room = _dBase.Rooms.SingleOrDefault(x => x.RoomName == roomName);
            if (roomName != "0" && room != null)
            {
                roomId = room.RoomId;
            } 
            
            var timeId = 0;
            //time is by default 0 if time is not passed in (not possible in cleint however)
            var time = _dBase.Times.SingleOrDefault(x => x.TimeLiteral == eventTime);
            if (eventTime != "0" && time != null)
            {
                timeId = time.TimeId;
            }
            
            var courseId = 0;
            //course by default set to 0 unless passed in by client
            var course = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName);
            if (courseName != "0" && course != null)
            {
                courseId = course.CourseId;
            }
            
            var moduleId = 0;
            var module = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName);
            //module by default set to 0 unless passed in by client
            if (moduleName != "0" && module != null)
            {
                moduleId = module.ModuleId;
                //Module Event generated by ModuleEvent method
                ModuleEvent(generatedEventId, moduleId, courseId);
            }

            var typeId = 0;
            //event type is by default 0 if time is not passed in (not possible in cleint however)
            var type = _dBase.EventTypes.SingleOrDefault(x => x.TypeName == eventType);
            if (eventType != "0" && type != null)
            {
                typeId = type.TypeId;
            } 
            //event object created too be adde to the databse
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
            //event added to the databse
            _dBase.Events.Add(newEvent);
            _dBase.SaveChanges();
            //id of generate event returned
            return generatedEventId;
        }

        /// <summary>
        /// Edited event detials passed in from teh client and event selected
        /// from databse and changes applied
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
            //edited event seleted from databse
            var editedEvent = _dBase.Events.SingleOrDefault(x => x.EventId == editedEventId);
            //as long as edited event exists changes will be made otherwise changes saved failed
            if (editedEvent == null)
            {
                return false;
            }

            #region Removal of Previous Invites and Attendees
            //staff attendees selected to be deleted
            var staffAtendees = _dBase.StaffEvents.Where(x => x.EventId == editedEventId).ToList();
            if (staffAtendees.Any())
            {
                //each staff attendee reomved and Ids recycled
                foreach (var sa in staffAtendees)
                {//recycled id created
                    var recycledId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = sa.StaffEventId,
                        TableName = "Staff Event"
                    };
                    _dBase.StaffEvents.Remove(sa);
                    _dBase.RecycledIds.Add(recycledId);
                    _dBase.SaveChanges();
                }
            }
            //module attendees selected for deletion
            var moduleAttendees = _dBase.ModuleEvents.Where(x => x.EventId == editedEventId).ToList();
            if (moduleAttendees.Any())
            {
                //each module deleted individually
                foreach (var m in moduleAttendees)
                {//id of module event is recycled
                    var recycledId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = m.EventModule,
                        TableName = "Module Event"
                    };
                    _dBase.ModuleEvents.Remove(m);
                    _dBase.RecycledIds.Add(recycledId);
                    _dBase.SaveChanges();
                }
            }
            //staff invitations returned for deletion
            var staffInvites = _dBase.StaffInvites.Where(x => x.EventId == editedEventId).ToList();
            if (staffInvites.Any())
            { //staff Ids deleted individuallys
                foreach (var si in staffInvites)
                {//ids of each staff invited are returned and recycled 
                    var recycledId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = si.StaffInviteId,
                        TableName = "Staff Invite"
                    };
                    _dBase.StaffInvites.Remove(si);
                    _dBase.RecycledIds.Add(recycledId);
                    _dBase.SaveChanges();
                }
            }
            //student invites retunred for deletion
            var studentInvites = _dBase.StudentInvites.Where(x => x.EventId == editedEventId).ToList();
            if (studentInvites.Any())
            { //each invite is deleted individually
                foreach (var st in studentInvites)
                {// ids are recycled
                    var recycledId = new RecycledId
                    {
                        DateAdded = DateTime.Now,
                        IdRecovered = st.StudentInviteId,
                        TableName = "Student Invite"
                    };
                    _dBase.StudentInvites.Remove(st);
                    _dBase.RecycledIds.Add(recycledId);
                    _dBase.SaveChanges();
                }
            }

            #endregion

            //repeat Id set to first value as default as repeats not implemented
            var repeatId = _dBase.RepeatTypes.First().RepeatTypeId;
            
            var roomId = 0;
            //room selceted and saved to event
            var room = _dBase.Rooms.SingleOrDefault(x => x.RoomName == roomName);
            if (roomName != "0" && room != null)
            {
                roomId = room.RoomId;
            }

            var timeId = 0;
            //time of event changed according to the information passed in from client
            var time = _dBase.Times.SingleOrDefault(x => x.TimeLiteral == eventTime);
            if (eventTime != "0" && time != null)
            {
                timeId = time.TimeId;
            }

            var courseId = 0;
            //course informatino changes accoding to the clinets information passed in
            var course = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName);
            if (courseName != "0" && course != null)
            {
                courseId = course.CourseId;
            }

            var moduleId = 0;
            //module Id passed in from the client added ot the event
            var module = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName);
            if (moduleName != "0" && module != null)
            {
                moduleId = module.ModuleId;
                //new moduke event created
                ModuleEvent(editedEventId, moduleId, courseId);
            }

            var typeId = 0;
            //event type set according to type id
            var type = _dBase.EventTypes.SingleOrDefault(x => x.TypeName == eventType); 
            if (eventType != "0" && type != null)
            {
                typeId = type.TypeId;
            } 

            //details of event passed in from client added to event
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
            //change of event saved
            _dBase.SaveChanges();
            //edit changes successfull
            return true;
        }

        /// <summary>
        /// Events status for confirmation of event
        /// Status used by timetabling engine to create timetable with existing events
        /// Confirmed events can only be, both module and room, room only with 
        /// no duplicate of event in room and time or module events at same time
        /// </summary>
        /// <param name="status"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public String ChangeEventStatus(String status, int eventId)
        {
            if (eventId != 0)
            {
                //retunr details of event selected
                var eventSelected = _dBase.Events.SingleOrDefault(x => x.EventId == eventId);
                if (eventSelected != null)
                {
                    //If module and room set and status is confirmed
                    if (eventSelected.Course != 0 && eventSelected.Module != 0 && eventSelected.Room != 0 && status == "Confirmed")
                    {
                        //check if room is already in use by a confirmed event
                        var eventsAlreadyRoom =_dBase.Events.Where(x =>x.Time == eventSelected.Time &&
                            x.Room == eventSelected.Room && x.StartDate == eventSelected.StartDate && 
                            x.Status == "Confirmed" && x.EventId != eventSelected.EventId).ToList();
                        //check if event for course already confirmed
                        var eventsAlreadyCourse =_dBase.Events.Where(x =>x.Time == eventSelected.Time &&
                                     x.Course == eventSelected.Course && x.StartDate == eventSelected.StartDate 
                                     && x.Status == "Confirmed" && x.EventId != eventSelected.EventId).ToList();
                        //check for module event if they already exist
                        var eventsAlreadyModule =_dBase.Events.Where(x =>x.Time == eventSelected.Time &&
                                     x.Module == eventSelected.Module && x.StartDate == eventSelected.StartDate 
                                     && x.Status == "Confirmed" && x.EventId != eventSelected.EventId).ToList();
                        
                        
                        //return error of type course
                        if (eventsAlreadyCourse.Any())
                        {
                           return "course";
                        } 
                        //return error of type room
                        if (eventsAlreadyRoom.Any())
                        {
                           return "room";
                        }
                        //if module event already exists module error returned
                        if (eventsAlreadyModule.Any())
                        {
                            return "module";
                        }

                        //if event valid confirmed
                        if (!eventsAlreadyModule.Any() && !eventsAlreadyRoom.Any())
                        {
                            eventSelected.Status = status;
                            _dBase.SaveChanges();
                            return "success";
                        }
                        
                        return "both";
                    }

                    //if room is only selected to be confirmed
                    if (eventSelected.Course == 0 && eventSelected.Module == 0 && eventSelected.Room != 0 &&
                        status == "Confirmed")
                    {
                        //check for events in room that may already exist
                        var eventsAlreadyRoom = _dBase.Events.Where(x => x.Time == eventSelected.Time &&
                            x.Room == eventSelected.Room && x.StartDate == eventSelected.StartDate &&
                            x.Status == "Confirmed" && x.EventId != eventSelected.EventId).ToList();

                        //if there are events in this room at the time specified room error returned
                        if (eventsAlreadyRoom.Any())
                        {
                            return "room";
                        }
                        //otherwise event confirmed
                        eventSelected.Status = status;
                        _dBase.SaveChanges();
                        return "success";
                    }

                    //if status is anything but confirmed status changed
                    if (status != "Confirmed")
                    {
                        eventSelected.Status = status;
                        _dBase.SaveChanges();
                        return "success";
                    }
                    return "failed";
                }
                return "failed";
            }
            return "failed";
        }

        /// <summary>
        /// Deletes event by querying the events table using the event Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool DeleteEvent(int eventId)
        {
            //delete event selected from database
            var deletedEvent = _dBase.Events.SingleOrDefault(x => x.EventId == eventId);
            //as long as event exists
            if (deletedEvent != null)
            {
                #region Removal of Previous Invites and Attendees

                var staffAtendees = _dBase.StaffEvents.Where(x => x.EventId == eventId).ToList();

                if (staffAtendees.Count > 0)
                {
                    foreach (var sa in staffAtendees)
                    {
                        var recycledId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = sa.StaffEventId,
                            TableName = "Staff Event"
                        };
                        _dBase.StaffEvents.Remove(sa);
                        _dBase.RecycledIds.Add(recycledId);
                        _dBase.SaveChanges();
                    }
                }

                var moduleAttendees = _dBase.ModuleEvents.Where(x => x.EventId == eventId).ToList();

                if (moduleAttendees.Count > 0)
                {
                    foreach (var m in moduleAttendees)
                    {
                        var recycledId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.EventModule,
                            TableName = "Module Event"
                        };
                        _dBase.ModuleEvents.Remove(m);
                        _dBase.RecycledIds.Add(recycledId);
                        _dBase.SaveChanges();
                    }
                }
                var staffInvites = _dBase.StaffInvites.Where(x => x.EventId == eventId).ToList();

                if (staffInvites.Count > 0)
                {
                    foreach (var si in staffInvites)
                    {
                        var recycledId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = si.StaffInviteId,
                            TableName = "Staff Invite"
                        };
                        _dBase.StaffInvites.Remove(si);
                        _dBase.RecycledIds.Add(recycledId);
                        _dBase.SaveChanges();
                    }
                }

                var studentInvites = _dBase.StudentInvites.Where(x => x.EventId == eventId).ToList();

                if (studentInvites.Count > 0)
                {
                    foreach (var st in studentInvites)
                    {
                        var recycledId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = st.StudentInviteId,
                            TableName = "Student Invite"
                        };
                        _dBase.StudentInvites.Remove(st);
                        _dBase.RecycledIds.Add(recycledId);
                        _dBase.SaveChanges();
                    }
                }

                #endregion

                //event id recycled
                var newSavedId = new RecycledId
                {
                    TableName = "Event",
                    IdRecovered = eventId,
                    DateAdded = DateTime.Now
                };

                //Id recycled
                _dBase.RecycledIds.Add(newSavedId);
                //event deleted
                _dBase.Events.Remove(deletedEvent);
                _dBase.SaveChanges();

                return true;
            }
            //delete failed
            return false;
        }

        #region Event Attendees

        /// <summary>
        /// Staff added to events in general, no staff has to be adde to an event
        /// to be confirmed
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public bool StaffEvent(int eventId, int staffId)
        {
            //as long as valid ids passed in attendee added
            if (eventId != 0 && staffId != 0)
            {
                //current staff allocation removed
                var idRecovery = _dBase.StaffEvents.SingleOrDefault(x => x.EventId == eventId);
                //Ids of current staff allocation recycled
                if (idRecovery != null)
                {
                    //recycled Id maintained
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

                //new staff attendee created
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
        /// Creation of module attendees for event
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="moduleId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool ModuleEvent(int eventId, int moduleId, int courseId)
        {//check ids that have been passed in are valid
            if (eventId != 0 && moduleId != 0 && courseId != 0)
            {
                //Id generated
                var inviteId = ModuleEventsIdGeneration();
                // new module event object created
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
            //Ids passed in invalid i.e contain 0s
            return false;
        }
        
        /// <summary>
        /// Deletion of module Event allocation
        /// Used to help allocated students to events
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public bool DeleteModuleEvent(int inviteId)
        {
            //selected module event from database
            var inviteSelected = _dBase.ModuleEvents.SingleOrDefault(x => x.EventModule == inviteId);
            if (inviteSelected != null)
            {
                //module event removed
                _dBase.ModuleEvents.Remove(inviteSelected);
                //recycled Id 
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
        /// Invitation of student to events that have been assigned on a 
        /// room and not a module, as evemt with modules invitations handled 
        /// by module event records
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool StudentInvite(int eventId, int studentId)
        {
            if (eventId != 0 && studentId != 0)
            {   //Id generated
                var inviteId = StudentInvitesIdGeneration();
                var studentEvent = new StudentInvite
                {
                    StudentInviteId = inviteId,
                    StudentId = studentId,
                    EventId = eventId,
                    Attending = true
                };
                //student Invite added
                _dBase.StudentInvites.Add(studentEvent);
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Staff invite created for staff member attendance to events that
        /// have been assigned a room only
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public bool StaffInvite(int eventId, int staffId)
        {
            if (eventId != 0 && staffId != 0)
            {
                //id generated
                var inviteId = StaffInvitesIdGeneration();
                //staff invite created
                var staffEvent = new StaffInvite
                {
                    StaffInviteId = inviteId,
                    StaffId = staffId,
                    EventId = eventId,
                    Attending = true
                };
                //staff invite added to database
                _dBase.StaffInvites.Add(staffEvent);
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Deletion of a student invite
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public bool DeleteStudentInvite(int inviteId)
        {
            //selected invite selected from database
            var inviteSelected = _dBase.StudentEvents.SingleOrDefault(x => x.StudentEventId == inviteId);
            // as longa as invite exists it is removed
            if (inviteSelected != null)
            {
                //record removed from database
                _dBase.StudentEvents.Remove(inviteSelected);
             
                //Id recycled
                var savedId = new RecycledId
                {
                    IdRecovered = inviteId,
                    TableName = "Student Event",
                    DateAdded = DateTime.Now
                };
                //Id saved
                _dBase.RecycledIds.Add(savedId);
                _dBase.SaveChanges();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Removal of staff invite to event
        /// </summary>
        /// <param name="inviteId"></param>
        /// <returns></returns>
        public bool DeleteStaffInvite(int inviteId)
        {   //Invite to be deleted selected from database
            var inviteSelected = _dBase.StaffEvents.SingleOrDefault(x => x.StaffEventId == inviteId);
            //as long as invite exists it will be removed
            if (inviteSelected != null)
            {
                //Invite removed
                _dBase.StaffEvents.Remove(inviteSelected);

                //Id recycled
                var savedId = new RecycledId
                {
                    IdRecovered = inviteId,
                    TableName = "Staff Event",
                    DateAdded = DateTime.Now
                };
                //Id added to recycled ids
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
        /// Returns all events in the database
        /// </summary>
        /// <returns></returns>
        public List<Event> ReturnEvents()
        {//as long as any  events exist they are returned
            if (_dBase.Events.Any())
            {
                //events list returned
                return _dBase.Events.ToList();
            }
            return null;
        }

        /// <summary>
        /// Details of individual event returned
        /// selcetd from databse by its unique Id
        /// </summary>
        /// <returns></returns>
        public Event ReturnEventDetails(int eventId)
        {
            //event selected from databse
            var eventSelected = _dBase.Events.SingleOrDefault(x => x.EventId == eventId);
            if (eventSelected != null)
            {//as longa s event exists the details are returned
                return eventSelected;
            }
            return null;
        }
        
        /// <summary>
        /// Returns events that have modules attached to
        /// them
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
        /// Return Events with rooms attached to them
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
        ///Returns rooms with roms and no modules
        /// </summary>
        /// <returns></returns>
        public List<Event> ReturnEventsWithRoomsNoModules()
        {
            //listed events created to be returned to teh client
            var eventsWithRoomsNoModules = _dBase.Events.Where(x => x.Room > 0 && x.Module == 0 && x.Course == 0).ToList();
            if (eventsWithRoomsNoModules.Any())
            {
                return eventsWithRoomsNoModules;
            }
            return null;
        }

        /// <summary>
        /// Event types listed and returned to client
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
        /// Staff that are associated with event returned
        /// </summary>
        /// <returns></returns>
        public Staff ReturnEventStaff(int eventId)
        {
            //returns all Event staff records used to return event staff from
            var staffEvent = _dBase.StaffEvents.SingleOrDefault(x => x.EventId == eventId);
            if (staffEvent != null)
            {
                //staff members returned
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
        /// room types return to client
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
        /// All available courses returned
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
        /// all available modules returned
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
        /// Returns list of staff members taht are associated with course
        /// by which is selected by the course Id passed in
        /// </summary>
        /// <returns></returns>
        public List<Staff> ReturnCourseStaff(int courseId)
        {
            if (courseId != 0)
            {
                //Staff associated with course selected
                var courseStaff = _dBase.Staffs.Where(x => x.Course == courseId).ToList();
                if (courseStaff.Any())
                {
                    return courseStaff;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// Returns complete list of buildings that are available in the database
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
        /// Returns repeat Id from teh repeat name that is passed in from method or client
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
        /// Returns room Id from room name and associated building Id
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
        /// Return allevents that are associated with a particular
        /// room indicated by its id
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
        /// Returns all events that are associated with the building
        /// selected by its id passed in
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
        /// Returns all the rooms associated with a building
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
        /// Returns the id of the building that is associated 
        /// with a particular room
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
        /// Return full list of times associated with the system
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
        /// returns full list of repeat types
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
        /// returns all staff that exists in the database
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
        /// Return all students that exist in the database
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

        /// <summary>
        /// Return all the students that are associated with a particular course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public List<Student> ReturnCourseStudents(int courseId)
        {
            if(courseId != 0)
            {
                return _dBase.Students.Where(x=>x.Course ==  courseId).ToList();
            }
            return null;
        }

        /// <summary>
        /// returns all the modules that are associated with a particular course
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns Staff that are attending events
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// returns the information of students invites
        ///  </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// Returns staff invitations for events that have been created
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check to ensure building with same name does not exist
        /// </summary>
        /// <param name="buildingName"></param>
        /// <returns></returns>
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
        /// Creation of new building in database
        /// from details passed in from the client
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
            //New building object created to be added to database
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
            //Building added to database
            _dBase.Buildings.Add(newBuilding);
            _dBase.SaveChanges();

            return buildingId;
        }

        /// <summary>
        /// Details of building selcetd by id returned
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public Building ReturnBuildingDetail(int buildingId)
        {
            //Building selected
            var buildingExists = _dBase.Buildings.SingleOrDefault(x => x.BuildingId == buildingId);

            if (buildingExists == null)
            {
                return null;
            }

            return buildingExists;
        }

        /// <summary>
        /// Check to see if room with the same name
        /// exists already in the database
        /// </summary>
        /// <param name="roomName"></param>
        /// <returns></returns>
        public bool CheckRoomExists(string roomName)
        {
            //Room name  checked against database values
            var roomExists = _dBase.Rooms.SingleOrDefault(x => x.RoomName == roomName);

            if (roomExists == null)
            {
                //room does not exist
                return false;
            }

            return true;
        }

        /// <summary>
        /// New room object added to database
        /// by data taht has been passed in from client
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
            //room id generated
            var roomId = RoomIdGeneration();

            if (roomId == 0)
            {
                return 0;
            }
            //new room object created to be added to database
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
            //room object added to database
            _dBase.Rooms.Add(newRoom);
            _dBase.SaveChanges();
            //room id returned
            return roomId;
        }

        /// <summary>
        /// Returns details of the room that exists in the database
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public Room ReturnRoomDetail(int roomId)
        {
            //room selected
            var roomExists = _dBase.Rooms.SingleOrDefault(x => x.RoomId == roomId);

            if (roomExists == null)
            {
                //room does not exist
                return null;
            }
            //room details returned
            return roomExists;
        }

        /// <summary>
        /// New room type creation
        /// </summary>
        /// <param name="roomTypeDescription"></param>
        /// <param name="creatorId"></param>
        /// <returns></returns>
        public int CreateNewRoomType(string roomTypeDescription, int creatorId)
        {
            //room type id generation
            var roomTypeId = RoomTypeIdGeneration();

            if (roomTypeId == 0)
            {
                return 0;
            }
            //new room type object created
            var newRoom = new RoomType
            {
                RoomTypeId = roomTypeId,
                RoomeTypeDescription = roomTypeDescription,
                CreateDate = DateTime.Now,
                Creator = creatorId
            };
            //Room type added to database
            _dBase.RoomTypes.Add(newRoom);
            _dBase.SaveChanges();
            //id returned
            return roomTypeId;
        }

        /// <summary>
        /// Check made to ensure no course with teh same name
        /// exist within the current database
        /// </summary>
        /// <param name="courseName"></param>
        /// <returns></returns>
        public bool CheckCourseExists(string courseName)
        {
            //room selected according to room name
            var courseExists = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName);
            //room does not exist
            if (courseExists == null)
            {  
                return false;
            }
            //room does exist
            return true;
        }
        
        /// <summary>
        /// New course cretaion from teh details passed from the
        /// cleint
        /// </summary>
        /// <param name="courseName"></param>
        /// <param name="courseDescription"></param>
        /// <param name="creatorId"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public int CreateCourse(string courseName, string courseDescription, int creatorId, int duration)
        {
            //course Id generated
             var courseId = CourseIdGeneration();
            //course generation failed due to no ids availabel
            if (courseId == 0)
            {
                return 0;
            }
            //new course object created to be added to the database
            var createdCourse = new Course
            {
                CourseId = courseId,
                CourseName = courseName,
                CourseDescription = courseDescription,
                CreateDate = DateTime.Now,
                Creator = creatorId,
                Duration = duration
            };
            //course added to the database
            _dBase.Courses.Add(createdCourse);
            _dBase.SaveChanges();
            //course Id returned
            return courseId;
        }

        /// <summary>
        /// course details returned according to course selected by id
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public Course ReturnCourseDetail(int courseId)
        {   //course selected by id
            var courseExists = _dBase.Courses.SingleOrDefault(x => x.CourseId == courseId);
            //course does not exist
            if (courseExists == null)
            {
                return null;
            }
            //full course object returned
            return courseExists;
        }

        /// <summary>
        /// Returns the number of students that are associated with teh module 
        /// selected by the modules Id
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public int ReturnModuleStudentsNumbers(int moduleId)
        {
            //as long a module id is not 0
            if (moduleId != 0)
            {
                //selection of students associated with module
                var moduleStudents = _dBase.StudentModules.Where(x => x.ModuleId == moduleId).ToList();
                //Counts module student records selected and passed back number
                return moduleStudents.Count();
            }
            //no students due to invalid module id passed in
            return 0;
        }
        
        
        /// <summary>
        /// Returns the number of students that are associated with teh module 
        /// selected by the modules Id
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public List<Student> ReturnModuleStudents(int moduleId)
        {
            //as long a module id is not 0
            if (moduleId != 0)
            {
                //selection of students associated with module
                var moduleStudents = _dBase.StudentModules.Where(x => x.ModuleId == moduleId).ToList();
                
                //Students List returend
                var returnedStudents = new List<Student>();
                foreach (var ms in moduleStudents)
                {
                   returnedStudents.Add(_dBase.Students.SingleOrDefault(x => x.StudentId == ms.StudentId));
                }
                
                //Counts module student records selected and passed back number
                return returnedStudents;
            }
            //no students due to invalid module id passed in
            return null;
        }

        /// <summary>
        /// check to ensure module does not exists according
        /// to module name within the database
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public bool CheckModuleExists(string moduleName)
        {
            //Module selected by name
            var courseExists = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName);
            //module does not exist in database
            if (courseExists == null)
            {
                return false;
            }
            //module does exist in database
            return true;
        }

        /// <summary>
        /// Creatuion of new module according to information passed
        /// from client
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="moduleDescription"></param>
        /// <param name="creatorId"></param>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public int CreateModule(string moduleName, string moduleDescription, int creatorId, int staffId)
        {
            //module id gerneated
            var moduleId = ModuleIdGeneration();
            //module creatin failed due to id inavailability
            if (moduleId == 0)
            {
                return 0;
            }
            //new module object created
            var newModule = new Module
            {
                CreateDate = DateTime.Now,
                Creator = creatorId,
                ModuleDescription = moduleDescription,
                ModuleName = moduleName,
                ModuleId = moduleId,
                Staff = staffId
            };
            //module added to database
            _dBase.Modules.Add(newModule);
            _dBase.SaveChanges();
            //id returned of created module
            return moduleId;
        }

        /// <summary>
        /// Module detail retunred by selction of module by the 
        /// modules unique id
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public Module ReturnModuleDetail(int moduleId)
        {
            //Module selected according to the id
            var moduleExists = _dBase.Modules.SingleOrDefault(x => x.ModuleId == moduleId);
            //no module with this id exists
            if (moduleExists == null)
            {
                return null;
            }
            //module object returned to client
            return moduleExists;
        }

        /// <summary>
        /// New course module allocation
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public int AddModuleToCourse(int courseId, int moduleId)
        {//course module id generated
            var courseModuleId = CourseModuleIdGeneration();
            //as long as id passed in for generation valid object created and added to database
            if (courseId != 0 && moduleId != 0 && moduleId != 0)
            {
                //new course module object created
                var courseModule = new CourseModule
                {
                    CourseModuleId = courseModuleId,
                    Course = courseId,
                    Module = moduleId
                };
                //course module added to database
                _dBase.CourseModules.Add(courseModule);
                _dBase.SaveChanges();
                //course module id returned
                return courseModuleId;
            }
            //course module allocation failed 0 returned
            return 0;
        }

        /// <summary>
        /// Check staff exixsts according to teh email address
        /// generated by staff in the client
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Check_Staff_Email_Exists(string email)
        {
            //check to see if any record exists with eth email addres passed in
            var emailCheck = _dBase.Staffs.SingleOrDefault(x => x.StaffEmail == email);
            //no staff exists with this email
            if (emailCheck == null)
            {
                return false;
            }
            //staff memebr exists
            return true;
        }

        /// <summary>
        /// Check to see if there is a sftaff memebr that exists with same email
        /// </summary>
        /// <param name="staffEmail"></param>
        /// <returns></returns>
        public bool CheckStaffExists(string staffEmail)
        {
            //check to see if any record exists with the email address passed in
            var staffExists = _dBase.Staffs.SingleOrDefault(x => x.StaffEmail == staffEmail);
            //staff memebr does not exist
            if (staffExists == null)
            {
                return false;
            }
            //staff memeber exists
            return true;
        }
        
        /// <summary>
        /// Creation of new staff memeber
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
            //staff id generated
            var staffId = StaffIdGeneration();

            //staff generation failed due to id generation failures
            if (staffId == 0)
            {
                return 0;
            }
            //new staff object created
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
            //Staff memebr addded to the database
            _dBase.Staffs.Add(newStaff);
            _dBase.SaveChanges();
            //staff id returned
            return staffId;
        }

        /// <summary>
        /// Staff details returned by selection by Id
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public Staff ReturnStaffDetail(int staffId)
        {
            //staff memebr selelted by Id
            var staffExists = _dBase.Staffs.SingleOrDefault(x => x.StaffId == staffId);
            //Staff memebr does not exist
            if (staffExists == null)
            {
                return null;
            }
            //return staff member object
            return staffExists;
        }

        /// <summary>
        /// check to see if student email address is allocated to another
        /// student record that exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool Check_Student_Email_Exists(string email)
        {
            //student with email address allocated selected
            var emailCheck = _dBase.Students.SingleOrDefault(x => x.StudentEmail == email);
            //email not registered to a student
            if (emailCheck == null)
            {
                return false;
            }
            //email already in use by another student
            return true;
        }

        /// <summary>
        /// check to see if student email address is allocated to another
        /// student record that exists in the database
        /// </summary>
        /// <param name="studentEmail"></param>
        /// <returns></returns>
        public bool CheckStudentExists(string studentEmail)
        {
            //student with email address allocated selected
            var studentExists = _dBase.Students.SingleOrDefault(x => x.StudentEmail == studentEmail);
            //email not registered to a student
            if (studentExists == null)
            {
                return false;
            }
            //email already in use by another student
            return true;
        }

        /// <summary>
        /// New student creation according to information passed in
        /// from the client
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
            //student Id generated
            var studentId = StudentIdGeneration();
            //Student creation failed due to student id generation failed
            if (studentId == 0)
            {
                return 0;
            }
            //new stuent object created in order to be added to the database
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
            //new student added to the database
            _dBase.Students.Add(newStudent);
            _dBase.SaveChanges();
            //student id returned
            return studentId;
        }

        /// <summary>
        /// Student details returned by selection of student by ID
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public Student ReturnStudentDetail(int studentId)
        {   //Students selected by Id
            var studentExists = _dBase.Students.SingleOrDefault(x => x.StudentId == studentId);
            //student does not exist
            if (studentExists == null)
            {
                return null;
            }
            //student returned
            return studentExists;
        }

        /// <summary>
        ///New repeat type created
        /// </summary>
        /// <param name="repeatTypeName"></param>
        /// <param name="repeatTypeDescription"></param>
        /// <returns></returns>
        public int CreateNewRepeat(string repeatTypeName, string repeatTypeDescription)
        {
            //New repeat type id generated
            var repeatTypeId = RepeatTypesIdGeneration();
            //id generation failed 0 returned
            if (repeatTypeId == 0)
            {
                return 0;
            }
            //new repeat type object created
            var newRepeatType = new RepeatType
            {
                RepeatTypeId = repeatTypeId,
                RepeatTypeName = repeatTypeName,
                RepeatDescription = repeatTypeDescription,
                CreateDate = DateTime.Now
            };
            //repeat type added to database
            _dBase.RepeatTypes.Add(newRepeatType);
            _dBase.SaveChanges();
            //id returned
            return repeatTypeId;
        }

        /// <summary>
        /// Creation of a new setting
        /// NOT USED FUTURE DEVELOPEMENT
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="settingDescription"></param>
        /// <returns></returns>
        public int CreateSetting(string settingName, string settingDescription)
        {
            //NEW SETTING created Id generation not used
            var id = _dBase.Settings.Count();
            id += 1;
            var newSetting = new Setting
            {
                SettingsId = id,
                SettingName = settingName,
                SettingDescription = settingDescription,
                CreateDate = DateTime.Now,
                Creator = 1,
                Active = "Always"

            };
            //settinga added
            _dBase.Settings.Add(newSetting);
            _dBase.SaveChanges();

            return id;
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
        /// <summary>
        /// Deletion of rooom according to id passed in for cleint
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public bool DeleteRoom(int roomId)
        {
            if (roomId != 0)
            {
                //Id recycled
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = roomId,
                    TableName = "Room"
                };

                var room = _dBase.Rooms.SingleOrDefault(x => x.RoomId == roomId);
                var roomsEvents = _dBase.Events.Where(x => x.Room == roomId).ToList();
                //All rooms that have the id passed in removed
                if (room != null)
                {
                    _dBase.Rooms.Remove(room);
                    _dBase.RecycledIds.Add(recoveredId);
                }
                //all events that use this room are modified for both room and status
                if (roomsEvents.Count() != 0)
                {
                    foreach (var e in roomsEvents)
                    {
                        e.Room = 0;
                        e.Status = "Denied";
                    }
                }
                //database changes saved
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Building deleted according to the Id passed in
        /// Also cleanu of rooms associations and events managaed
        /// </summary>
        /// <param name="buildingId"></param>
        /// <returns></returns>
        public bool DeleteBuilding(int buildingId)
        {
            if (buildingId != 0)
            {//building id recycled
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = buildingId,
                    TableName = "Building"
                };
                //Building and room objects selected
                var building = _dBase.Buildings.SingleOrDefault(x => x.BuildingId == buildingId);
                var buildingRooms = _dBase.Rooms.Where(x => x.Building == buildingId).ToList();

                if (building != null)
                {//building removed and id recycled
                    _dBase.Buildings.Remove(building);
                    _dBase.RecycledIds.Add(recoveredId);
                }

                //Rooms that were reviously associcated with building are updated
                if (buildingRooms.Count() != 0)
                {
                    //Rooms taht are associated with building removed
                    foreach (var r in buildingRooms)
                    {
                        _dBase.Rooms.Remove(r);
                        //room ids recycled
                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = r.RoomId,
                            TableName = "Room"
                        };
                        _dBase.RecycledIds.Add(recoveredRoomId);
                        //evenst of associated rooms updated
                        var roomEvents = _dBase.Events.Where(x => x.Room == r.RoomId).ToList();
                        foreach (var e in roomEvents)
                        {
                           e.Room = 0;
                           e.Status = "Denied";
                        }
                    }
                }
                //changes saved
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Deletion of course seleted by Id
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
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
                //course and course modules allocations slected
                var course = _dBase.Courses.SingleOrDefault(x => x.CourseId == courseId);
                var courseModules = _dBase.CourseModules.Where(x => x.Course == courseId).ToList();

                if (course != null)
                {//removal of course
                    _dBase.Courses.Remove(course);
                    _dBase.RecycledIds.Add(recoveredId);
                }


                if (courseModules.Count() != 0)
                {
                    //removal of course moudule associations
                    foreach (var m in courseModules)
                    {
                        _dBase.CourseModules.Remove(m);
                        //course module id recycled
                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.Module,
                            TableName = "Module"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);
                        //module events updated to reflect removal of modules
                        var moduleEvents = _dBase.Events.Where(x => x.Module == m.Module).ToList();

                        foreach (var e in moduleEvents)
                        {
                            e.Module = 0;
                            e.Status = "Denied";
                        }
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false;   
        }

        /// <summary>
        /// deletion of module
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public bool DeleteModule(int moduleId)
        {
            if (moduleId != 0)
            {
                //moudle Id recovered
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = moduleId,
                    TableName = "Module"
                };
                //module and module events selected
                var module = _dBase.Modules.SingleOrDefault(x => x.ModuleId == moduleId);
                var moduleEvents = _dBase.Events.Where(x => x.Module == moduleId).ToList();

                if (module != null)
                {
                    //modue removed
                    _dBase.Modules.Remove(module);
                    _dBase.RecycledIds.Add(recoveredId);
                }

                if (moduleEvents.Count() != 0)
                {//module events updated to relect removal of module
                    foreach (var e in moduleEvents)
                    {
                        e.Module = 0;
                        e.Status = "Denied";
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false; 
        }

        /// <summary>
        /// student delete function
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        public bool DeleteStudent(int studentId)
        {
            if (studentId != 0)
            {//studnet Id recovered
                var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = studentId,
                    TableName = "Student"
                };

                //fields associated with students selected for removal or updating
                var student = _dBase.Students.SingleOrDefault(x => x.StudentId == studentId);
                var studentModules = _dBase.StudentModules.Where(x => x.StudentId == studentId).ToList();
                var studentEvents = _dBase.StudentEvents.Where(x => x.StudentId == studentId).ToList();
                var studentInvites = _dBase.StudentInvites.Where(x => x.StudentId == studentId).ToList();

                if (student != null)
                {//studnet removed
                    _dBase.Students.Remove(student);
                    _dBase.RecycledIds.Add(recoveredId);
                }

                //Student modules removed and id recycled
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
                //studnet Events updated 
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
                //studnet Invites  updated
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

        /// <summary>
        /// Deletion of selected staff member
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public bool DeleteStaff(int staffId)
        {
            if(staffId != 0)
            {
                //Staff id recovered
            var recoveredId = new RecycledId
                {
                    DateAdded = DateTime.Now,
                    IdRecovered = staffId,
                    TableName = "Staff"
                };
                //associated staff firelds collected for deletion or updating
                var staff = _dBase.Staffs.SingleOrDefault(x => x.StaffId == staffId);
                var staffModules = _dBase.Modules.Where(x => x.Staff == staffId);
                var staffEvents = _dBase.StaffEvents.Where(x => x.StaffId == staffId).ToList();
                var staffInvites = _dBase.StaffInvites.Where(x => x.StaffId == staffId).ToList();

                if (staff != null)
                {
                    //staff removed and id recovered
                    _dBase.Staffs.Remove(staff);
                    _dBase.RecycledIds.Add(recoveredId);
                }

                //staf modules updataed
                if (staffModules.Count() != 0)
                {
                    foreach (var m in staffModules)
                    {
                        m.Staff = 0;
                    }
                }
                
                //staff events updated to relect staff removal
                if (staffEvents.Count() != 0)
                {
                    foreach (var m in staffEvents)
                    {
                        _dBase.StaffEvents.Remove(m);
                        //Id recycled
                        var recoveredRoomId = new RecycledId
                        {
                            DateAdded = DateTime.Now,
                            IdRecovered = m.StaffId,
                            TableName = "Staff Event"
                        };

                        _dBase.RecycledIds.Add(recoveredRoomId);                      
                    }
                }
                //staff invitatino for events removeed
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
                        //Id reccycled
                        _dBase.RecycledIds.Add(recoveredRoomId);                      
                    }
                }

                _dBase.SaveChanges();
                return true;
            }
            return false;   
        }
       
        /// <summary>
        /// deletion of User
        /// NOT IMPLEMEMNTED FUTURE DEVELOPMENT
        /// </summary>
        /// <param name="userdId"></param>
        /// <returns></returns>
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
        
        /// <summary>
        /// addition of module to course 
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public bool AddModulesToCourse(List<Module> modules, int courseId)
        {
            if (modules.Count != 0 && courseId != 0)
            {   // modules selceted from database to create new course module records
                var allocatedCourseModules = _dBase.CourseModules.Where(x => x.Course == courseId);

                //Removal of current course modules
                foreach (var cm in allocatedCourseModules)
                {
                    _dBase.CourseModules.Remove(cm);
                }
                //new course modules generated
                foreach (var m in modules)
                {
                    //new course module  idcreated
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

        /// <summary>
        /// Module added to event for attendence
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool AddModulesToEvent(List<CourseModule> modules, int eventId)
        {
            if (modules.Count != 0 && eventId != 0)
            {
                foreach (var m in modules)
                {
                    //new event module id generted
                    var eventModuleId = ModuleEventsIdGeneration() ;
                    //new event module created
                    var eventModule = new ModuleEvent
                    {
                        EventId = eventId,
                        ModuleId = m.Module,
                        EventModule = eventModuleId,
                        CourseId = m.Course
                    };
                    //event mdouel adde to database
                    _dBase.ModuleEvents.Add(eventModule);
                    _dBase.SaveChanges();
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Add staff atteneddent to event
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
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

        /// <summary>
        /// add list of students to a module
        /// </summary>
        /// <param name="students"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public bool AddStudentsToModule(List<StudentModule> students, int moduleId)
        {
            if (students.Any())
            {   //current module attendees selected
                var allAttendees = _dBase.StudentModules.Where(x => x.ModuleId == moduleId).ToList();
                //student Modules removed
                foreach (var sm in allAttendees)
                {
                    _dBase.StudentModules.Remove(sm);
                }

                //new student moduels added
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
                //removal of all student modules
                var allAttendees = _dBase.StudentModules.Where(x => x.ModuleId == moduleId).ToList();
                foreach (var sm in allAttendees)
                {
                    _dBase.StudentModules.Remove(sm);
                }
                _dBase.SaveChanges();
            }
            return true;
        }

        /// <summary>
        /// Invitations of staff to a room only event created
        /// </summary>
        /// <param name="staff"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool AddStaffInvitesToEvent(List<Staff> staff, int eventId)
        {
            if (eventId != 0)
            {
                //selection of all current invites for the event created
                var allInvites = _dBase.StaffInvites.Where(x => x.EventId == eventId).ToList();

                if (allInvites.Count > 0)
                {
                    foreach (var i in allInvites)
                    { //old invitations removed
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
                //new invitations creatde and adde to the database
                foreach (var s in staff)
                {   //staff invite id generation 
                    var staffInviteId = StaffInvitesIdGeneration();
                    //new staff invite created
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

        /// <summary>
        /// studnet invitations for event created
        /// </summary>
        /// <param name="students"></param>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool AddStudentInvitesToEvent(List<Student> students, int eventId)
        {
            if (eventId != 0)
            {   //current invites selceted
                var allInvites = _dBase.StudentInvites.Where(x => x.EventId == eventId).ToList();
                //current invites removed
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
                //new invited created
                foreach (var s in students)
                {
                    //new invite id generated
                    var studentInviteId = StudentInvitesIdGeneration();
                    //new student invite object created
                    var studentInvite = new StudentInvite
                    {
                        EventId = eventId,
                        StudentId = s.StudentId,
                        StudentInviteId = studentInviteId,
                        Attending = false
                    };
                    //Invite added to databse
                    _dBase.StudentInvites.Add(studentInvite);
                    _dBase.SaveChanges();
                 }
                
                return true;
            }
            return false;
        }

        #endregion
        
        #region Search Functions

        /// <summary>
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
        /// Searches by aplpying a filer to the field searched for within the room
        /// then applaies a similar fitler of user input to select the room
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
        /// Searches by aplpying a filer to the field searched for within the course
        /// then applies a similar filter of user input to select the courses
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
        /// Searches by aplpying a filer to the field searched for within the building
        /// then applies a similar fitler of user input to select the building
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
        /// Searches by aplpying a filer to the field searched for within the staff members
        /// then applaies a similar fitler of user input to select the staff
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
        /// Searches by aplpying a filer to the field searched for within the students
        /// then applaies a similar fitler of user input to select the students
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
        
        /// <summary>
        /// Search function which provides a means of searching through the course selected modules
        /// the moduels are then returuned and populated into the client
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
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
        /// Searches by aplpying a filer to the field searched for within the module
        /// then applaies a similar fitler of user input to select the modules
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
        /// Searches by aplpying a filer to the field searched for within the event
        /// with only a room allocated ie. no moudle hase been added
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

        public TimetableEventsListObject ReturnWeeksEventsWithFilters(DateTime dateRequested, int roomId)
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
                
                
                        foreach (Event e in confirmedEvents)
                        {

                            foreach (var t in timeslot)
                            {
                                if (e.Time == t.TimeId)
                                {
                                    var tempDay = Convert.ToDateTime(e.StartDate).DayOfWeek.ToString();

                                    var timeEventObject = new TimetableEventObject
                                    {
                                        Event = e,
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
                        }
                return timetableResult;
            }
            return null;

        } 
        
        public TimetableEventsListObject ReturnWeeksEventsForCourses(DateTime dateRequested, int courseId)
        {
            if (courseId != 0 )
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
                    _dBase.Events.Where(x => x.StartDate >= startDate && x.StartDate < weekEnd && x.Course == courseId).ToList();

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
                
                        foreach (Event e in confirmedEvents)
                        {

                            foreach (var t in timeslot)
                            {
                                if (e.Time == t.TimeId)
                                {
                                    var tempDay = Convert.ToDateTime(e.StartDate).DayOfWeek.ToString();

                                    var timeEventObject = new TimetableEventObject
                                    {
                                        Event = e,
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
