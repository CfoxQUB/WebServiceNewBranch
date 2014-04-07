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
        public interface ITimetablingService
        {
            #region Client Users Functions
            [OperationContract]
            bool Check_Email_Not_Exist(string email);
            [OperationContract]
            bool Register_User(User newUser);
            [OperationContract]
            int Login(string userName, string userPassword);
            #endregion

            #region Event Actions

            [OperationContract]
            int CreateEvent(string eventTitle, int userId, string eventDescription, string eventType, string repeatType, int eventDuration, DateTime startDate, string eventTime, string roomName, string courseName, string moduleName);
            [OperationContract]
            bool EditEvent(int editedEventId, int userId, string eventTitle, string eventDescription, string eventType, string repeatType, int eventDuration, DateTime startDate, string eventTime, string roomName, string courseName, string moduleName);
            [OperationContract]
            bool DeleteEvent(int eventId);

            #endregion

            #region Timetabling Actions
            [OperationContract]
            List<Event> ReturnWeeksEvents(DateTime weekBeginning, int roomId);
            [OperationContract]
            TimetableDisplayListObject ReturnTimetableDisplayListObject();
            
            #endregion 

            #region Listed Types
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
            List<Module> ReturnCourseModules(int courseId);
            [OperationContract]
            int ReturnBuildingIdFromBuildingName(string buildingName);
            [OperationContract]
            int ReturnCourseIdFromCourseName(string courseName);
            [OperationContract]
            int ReturnRoomId(int buildingId, string roomName);
            [OperationContract]
            List<Room> ReturnBuildingRooms(int buildingId);
            [OperationContract]
            int ReturnRoomBuilding(int roomId);
            [OperationContract]
            List<Event> ReturnRoomEvents(int roomName);
            [OperationContract]
            List<Time> ReturnTimes();
            [OperationContract]
            List<RepeatType> ReturnRepeatTypes();

        [OperationContract]
        List<Event> ReturnWeeksEventsiWithFilters(DateTime dateRequested, int roomId, int moduleId);
            
            #endregion

            #region Resource Creation

            [OperationContract]
            int CreateNewBuilding(string buildingName, int buildingNumber, string addressLine1, string addressLine2, string postCode, string buildingCity);
            [OperationContract]
            int CreateNewRoom(int buildingId, string roomName, string roomDescription, int roomTypeId);
            [OperationContract]
            int CreateNewRoomType(string roomTypeDescription);
            [OperationContract]
            int CreateNewRepeat(string repeatTypeName, string repeatTypeDescription);
            [OperationContract]
            int CreateSetting(string settingName, string settingDescription);
            #endregion

            #region Search Function
            [OperationContract]
            List<Event> SearchFunction(string appliedFilter, string searchItem);
            #endregion

        }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TimetablingService : ITimetablingService
    {
        private TimetableDatabase _dBase = new TimetableDatabase();
        private readonly CultureInfo _currentculture = CultureInfo.CurrentCulture;

        #region Client User Registration

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
                newUser.UserId = genId;
                newUser.CreateDate = DateTime.Now;
                newUser.UserType = 1;
                newUser.UserEmail = newUser.UserEmail;

                _dBase.Users.Add(newUser);
                _dBase.SaveChanges();
                return true;
            }
            return false;
        }

        #endregion

        #region Client Login

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
            var temp = _dBase.Users.SingleOrDefault(x => x.UserEmail == userEmail);
            if (temp != null && temp.Password == userPassword)
            {
                return temp.UserId;
            }
            return 0;
        }

        #endregion

        #region Id Maintainance

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
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;

            if (!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }
                
            var buildingCheck = _dBase.Buildings.Count();

            if (recycledIdCount == 0 && buildingCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && buildingCheck > 0)
            {
                var largestId = _dBase.Buildings.OrderByDescending(x => x.BuildingId).First().BuildingId;

                if (largestId != maxIdValue)
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
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;

            if (!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }
            
            var courseCheck = _dBase.Courses.Count();

            if (recycledIdCount == 0 && courseCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && courseCheck != 0)
            {
                var largestId = _dBase.Courses.OrderByDescending(x => x.CourseId).First().CourseId;

                if (largestId != maxIdValue)
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
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;
            
            if(!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }

            var eventCheck = _dBase.Events.Count();

            if (recycledIdCount == 0 && eventCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && eventCheck != 0)
            {
                var largestId = _dBase.Events.OrderByDescending(x => x.EventId).First().EventId;

                if (largestId != maxIdValue)
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
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;

            if (!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }

            var moduleCheck = _dBase.Modules.Count();

            if (recycledIdCount == 0 && moduleCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && moduleCheck != 0)
            {
                var largestId = _dBase.Modules.OrderByDescending(x => x.ModuleId).First().ModuleId;

                if (largestId != maxIdValue)
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
        /// Written: 18/11/2013
        /// Generates a new Id from the Ids that are stored in the Recycled 
        /// Ids table for the Repeat types table or the current highest valued 
        /// Id in the Repeat type table.
        /// </summary>
        /// <returns></returns>
        private int RepeatTypesIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "RepeatType");
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;

            if (!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }

            var repeatsCheck = _dBase.RepeatTypes.Count();

            if (recycledIdCount == 0 && repeatsCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && repeatsCheck != 0)
            {
                var largestId = _dBase.RepeatTypes.OrderByDescending(x => x.RepeatTypeId).First().RepeatTypeId;

                if (largestId != maxIdValue)
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
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;

            if (!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }

            var roomCheck = _dBase.Rooms.Count();

            if (recycledIdCount == 0 && roomCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && roomCheck != 0)
            {
                var largestId = _dBase.Rooms.OrderByDescending(x => x.RoomId).First().RoomId;

                if (largestId != maxIdValue)
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
        /// Ids in the Staff table or the current highest valued Id in the 
        /// Staff table. 
        /// </summary>
        /// <returns></returns>
        private int StaffIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Staff");
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;

            if (!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }

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
        private int StudentIdGeneration()
        {
            var recycledIdCount = _dBase.RecycledIds.Count(x => x.TableName == "Student");
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;

            if (!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }

            var studentCheck = _dBase.Students.Count();

            if (recycledIdCount == 0 && studentCheck == 0)
            {
                return 1;
            }
            
            if (recycledIdCount == 0 && studentCheck != 0)
            {
                var largestId = _dBase.Students.OrderByDescending(x => x.StudentId).First().StudentId;

                if (largestId != maxIdValue)
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
            var maxIdString = _dBase.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription;
            int maxIdValue;

            if (!String.IsNullOrEmpty(maxIdString))
            {
                maxIdValue = Convert.ToInt32(maxIdString);
            }
            else
            {
                maxIdValue = 10000;
            }

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

            var repeatId = _dBase.RepeatTypes.SingleOrDefault(x => x.RepeatTypeName == repeatType).RepeatTypeId;
            var timeId = _dBase.Times.SingleOrDefault(x => x.TimeLiteral == eventTime).TimeId;
            var roomId = _dBase.Rooms.SingleOrDefault(x => x.RoomName == roomName).RoomId;
            var courseId = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName).CourseId;
            var moduleId = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName).ModuleId;
            var typeId = _dBase.EventTypes.SingleOrDefault(x => x.TypeName == eventType).TypeId;

            var newEvent = new Event()
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
        /// <param name="repeatType"></param>
        /// <param name="eventDuration"></param>
        /// <param name="startDate"></param>
        /// <param name="eventTime"></param>
        /// <param name="roomName"></param>
        /// <param name="courseName"></param>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        public bool EditEvent(int editedEventId, int userId, string eventTitle, string eventDescription,
            string eventType, string repeatType, int eventDuration, DateTime startDate, string eventTime,
            string roomName, string courseName, string moduleName)
        {
            var editedEvent = _dBase.Events.SingleOrDefault(x => x.EventId == editedEventId);

            if (editedEvent == null)
            {
                return false;
            }

            var repeatId = _dBase.RepeatTypes.SingleOrDefault(x => x.RepeatTypeName == repeatType).RepeatTypeId;
            var timeId = _dBase.Times.SingleOrDefault(x => x.TimeLiteral == eventTime).TimeId;
            var typeId = _dBase.EventTypes.SingleOrDefault(x => x.TypeName == eventType).TypeId;
            var roomId = _dBase.Rooms.SingleOrDefault(x => x.RoomName == roomName).RoomId;
            var courseId = _dBase.Courses.SingleOrDefault(x => x.CourseName == courseName).CourseId;
            var moduleId = _dBase.Modules.SingleOrDefault(x => x.ModuleName == moduleName).ModuleId;



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

        /// <summary>
        /// Written: 18/11/2012
        /// Deletes event by querying the events table using the event Id
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        public bool DeleteEvent(int eventId)
        {
            var deletedEvent = _dBase.Events.SingleOrDefault(x => x.EventId == eventId);

            var newSavedId = new RecycledId()
            {
                TableName = "Event",
                IdRecovered = eventId
            };

            _dBase.RecycledIds.Add(newSavedId);

            _dBase.Events.Remove(deletedEvent);

            _dBase.SaveChanges();

            return true;
        }

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
                var returnName = _dBase.Buildings.SingleOrDefault(x => x.BuildingName == buildingName).BuildingId;
                
                if (returnName != null)
                {
                    return returnName;
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
                var returnRoom = _dBase.Rooms.Where(x => x.Building == buildingId).SingleOrDefault(x => x.RoomName == roomName).RoomId;
                if (returnRoom != null)
                {
                    return returnRoom;
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
                
                foreach (var moduleId in moduleIdList)
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
            var roomList = _dBase.Rooms.ToList();
            if (roomList.Exists(x => x.RoomId == roomId))
            {
                var returnBuilding = roomList.SingleOrDefault(x => x.RoomId == roomId);
                return (int)returnBuilding.Building;
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



        #endregion

        #region Resource Creation and Maintanence 

        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="buildingName"></param>
        /// <param name="buildingNumber"></param>
        /// <param name="addressLine1"></param>
        /// <param name="addressLine2"></param>
        /// <param name="postCode"></param>
        /// <param name="buildingCity"></param>
        /// <returns></returns>
        public int CreateNewBuilding(string buildingName, int buildingNumber, string addressLine1, string addressLine2,
            string postCode, string buildingCity)
        {
            var buildingId = BuildingIdGeneration();

            if (buildingId == 0)
            {
                return 0;
            }

            var newBuilding = new Building()
            {
                BuildingId = buildingId,
                BuildingName = buildingName,
                BuildingNumber = buildingNumber,
                AddressLine1 = addressLine1,
                AddressLine2 = addressLine2,
                PostalCode = postCode,
                City = buildingCity

            };

            _dBase.Buildings.Add(newBuilding);
            _dBase.SaveChanges();

            return buildingId;
        }

        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="buildingId"></param>
        /// <param name="roomName"></param>
        /// <param name="roomDescription"></param>
        /// <param name="roomTypeId"></param>
        /// <returns></returns>
        public int CreateNewRoom(int buildingId, string roomName, string roomDescription, int roomTypeId)
        {
            var roomId = RoomIdGeneration();

            if (roomId == 0)
            {
                return 0;
            }

            var newRoom = new Room()
            {
                RoomId = roomId,
                RoomName = roomName,
                RoomDescription = roomDescription,
                RoomType = roomTypeId,
                Building = buildingId
            };

            _dBase.Rooms.Add(newRoom);
            _dBase.SaveChanges();

            return roomId;
        }

        /// <summary>
        /// Written: 02/12/2013
        /// </summary>
        /// <param name="roomTypeDescription"></param>
        /// <returns></returns>
        public int CreateNewRoomType(string roomTypeDescription)
        {
            var roomTypeId = RepeatTypesIdGeneration();

            if (roomTypeId == 0)
            {
                return 0;
            }

            var newRoom = new RoomType()
            {
                RoomTypeId = roomTypeId,
                RoomeTypeDescription = roomTypeDescription,
                CreateDate = DateTime.Now
            };

            _dBase.RoomTypes.Add(newRoom);
            _dBase.SaveChanges();

            return roomTypeId;
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

            var newRepeatType = new RepeatType()
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
            var newSetting = new Setting()
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

        #region Search Function

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

                List<Event> eventList = new List<Event>();

                if (appliedFilter == "Event Title")
                {
                    eventList.AddRange(_dBase.Events.Where(x => x.EventTitle.Contains(searchItem)).ToList());

                    return eventList;
                }
                else if (appliedFilter == "Event Description")
                {
                    eventList.AddRange(_dBase.Events.Where(x => x.EventDescription.Contains(searchItem)).ToList());

                    return eventList;
                }

            }
            return null;
        }

        #endregion

        #region Time Table Methods

        public List<Event> ReturnWeeksEvents(DateTime dateRequested, int roomId)
        {
            if (dateRequested != null)
            {
                var dayRequested = dateRequested.DayOfWeek.ToString();
                var date = dateRequested.Day;
                var monthrequested = _currentculture.Calendar.GetMonth(dateRequested);
                var yearRequested = _currentculture.Calendar.GetYear(dateRequested);

                var newDate = new DateTime(yearRequested, monthrequested, date);

                DateTime startDate = new DateTime();

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


        public List<Event> ReturnWeeksEventsiWithFilters(DateTime dateRequested, int roomId, int moduleId)
        {
            if (dateRequested != null)
            {
                var dayRequested = dateRequested.DayOfWeek.ToString();
                var date = dateRequested.Day;
                var monthrequested = _currentculture.Calendar.GetMonth(dateRequested);
                var yearRequested = _currentculture.Calendar.GetYear(dateRequested);

                var newDate = new DateTime(yearRequested, monthrequested, date);

                DateTime startDate = new DateTime();

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

                var timetableResult = new TimetableEventsListObject()
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

                            var timeEventObject = new TimetableEventObject()
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
                return eventsList;
            }
            return null;

        }
        /// <summary>
        /// Written:13/03/2014
        /// </summary>
        /// <returns></returns>
        public TimetableDisplayListObject ReturnTimetableDisplayListObject()
        {
            var startDate = _dBase.Settings.SingleOrDefault(x => x.SettingName == "StartDate").SettingDescription;
            var endDate = _dBase.Settings.SingleOrDefault(x => x.SettingName == "EndDate").SettingDescription;

            if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                var beginDate = Convert.ToDateTime(startDate);
                var finalDate = Convert.ToDateTime(endDate);

        

                int totalDays = Convert.ToInt32((finalDate.Date - beginDate).TotalDays);
                totalDays = totalDays / 7;
                
                var timetableResult = new TimetableDisplayListObject()
                {
                    MondayList = new List<TimetableObject>(),
                    TuesdayList = new List<TimetableObject>(),
                    WednesdayList = new List<TimetableObject>(),
                    ThursdayList = new List<TimetableObject>(),
                    FridayList = new List<TimetableObject>(),
                    SaturdayList = new List<TimetableObject>(),
                    SundayList = new List<TimetableObject>()
                };

                var daysList = new List<string>();

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
                            DateTime tempDay = (DateTime) e.StartDate;

                            if (tempDay.DayOfWeek.ToString() == d && e.Time == t.TimeId)
                            {
                                count += 1;
                            }
                        }

                        var timeObject = new TimetableObject()
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
