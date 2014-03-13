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
            #endregion 

            #region Listed Types
            [OperationContract]
            List<Event> ReturnEvents();
            [OperationContract]
            List<EventType> ReturnEventTypes();
            [OperationContract]
            List<UserType> ReturnUserTypes();
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
            List<TimetableObject> ReturnTimetableDisplay();

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
            LocalDatabase db = new LocalDatabase();
            CultureInfo currentculture = CultureInfo.CurrentCulture;

            #region Client User Registration
            public bool Check_Email_Not_Exist(string email)
            {
                var emailList = db.Users.Select(x => x.UserEmail).ToList();
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
                    newUser.CurrentlyLoggedIn = "false";
                    newUser.UserEmail = newUser.UserEmail;

                    db.Users.Add(newUser);
                    db.SaveChanges();
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
            /// <param name="userName"></param>
            /// <param name="userPassword"></param>
            /// <returns></returns>
            public int Login(string userEmail, string userPassword)
            {
                var temp = db.Users.SingleOrDefault(x => x.UserEmail == userEmail);
                if (temp != null && temp.Password == userPassword)
                {
                    temp.CurrentlyLoggedIn = "true";
                    db.SaveChanges();

                    return temp.UserId;
                }
                    return 0;
            }
            #endregion

            #region Id Maintainance

            /// <summary>
            /// Written: 18/11/2013
            /// Generates a new Id from the Ids that are stored in the Recycled 
            /// Ids table for the building table or the current highest valued 
            /// Id in the Building table.
            /// </summary>
            /// <returns></returns>
            private int BuildingIdGeneration()
            {
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "Building").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);

                var buildingCheck = db.Buildings.Count();

                if (recycledIdCount == 0 && buildingCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && buildingCheck != 0)
                {
                    var largestId = db.Buildings.OrderByDescending(x => x.BuildingId).First().BuildingId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0;
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Building");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "Course").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);

                var courseCheck = db.Courses.Count();

                if (recycledIdCount == 0 && courseCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && courseCheck != 0)
                {
                    var largestId = db.Courses.OrderByDescending(x => x.CourseId).First().CourseId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0;
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Course");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "Event").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);

                var eventCheck = db.Events.Count();

                if (recycledIdCount == 0 && eventCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && eventCheck != 0)
                {
                    var largestId = db.Events.OrderByDescending(x => x.EventId).First().EventId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0;
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Event");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "Module").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);

                var moduleCheck = db.Modules.Count();

                if (recycledIdCount == 0 && moduleCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && moduleCheck != 0)
                {
                    var largestId = db.Modules.OrderByDescending(x => x.ModuleId).First().ModuleId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0;
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Module");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "RepeatType").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);

                var repeatsCheck = db.RepeatTypes.Count();

                if (recycledIdCount == 0 && repeatsCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && repeatsCheck != 0)
                {
                    var largestId = db.RepeatTypes.OrderByDescending(x => x.RepeatTypeId).First().RepeatTypeId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0;
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "RepeatType");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "Room").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);

                var roomCheck = db.Rooms.Count();

                if (recycledIdCount == 0 && roomCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && roomCheck != 0)
                {
                    var largestId = db.Rooms.OrderByDescending(x => x.RoomId).First().RoomId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0;
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Room");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "Staff").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);

                var staffCheck = db.Staffs.Count();

                if (recycledIdCount == 0 && staffCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && staffCheck != 0)
                {
                    var largestId = db.Staffs.OrderByDescending(x => x.StaffId).First().StaffId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0;
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Staff");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "Student").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);

                var studentCheck = db.Students.Count();

                if (recycledIdCount == 0 && studentCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && studentCheck != 0)
                {
                    var largestId = db.Students.OrderByDescending(x => x.StudentId).First().StudentId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0;
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "Student");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
                var recycledIdCount = db.RecycledIds.Where(x => x.TableName == "User").Count();

                var maxIdValue = Convert.ToInt32(db.Settings.SingleOrDefault(x => x.SettingName == "Max Ids Value").SettingDescription);
                
                var userCheck = db.Users.Count();

                if (recycledIdCount == 0 && userCheck == 0)
                {
                    return 1;
                }
                else if (recycledIdCount == 0 && userCheck != 0)
                {
                    var largestId = db.Users.OrderByDescending(x => x.UserId).First().UserId;

                    if (largestId != maxIdValue)
                    {
                        return largestId + 1;
                    }

                    return 0; 
                }
                else if (recycledIdCount != 0)
                {
                    var recoveredId = db.RecycledIds.OrderByDescending(x => x.IdRecovered).First(x => x.TableName == "User");
                    db.RecycledIds.Remove(recoveredId);
                    db.SaveChanges();

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
            /// <param name="eventDescription"></param>
            /// <param name="eventType"></param>
            /// <param name="eventDate"></param>
            /// <param name="eventTime"></param>
            /// <param name="eventDuration"></param>
            /// <param name="eventRepeats"></param>
            /// <returns></returns>
            public int CreateEvent(string eventTitle, int userId, string eventDescription, string eventType, string repeatType, int eventDuration, DateTime startDate, string eventTime, string roomName, string courseName, string moduleName)
            {
                var generatedEventId = EventIdGeneration();

                if (generatedEventId == 0) 
                {
                    return 0;
                }

                var repeatId = db.RepeatTypes.SingleOrDefault(x=>x.RepeatTypeName == repeatType).RepeatTypeId;
                var timeId = db.Times.SingleOrDefault(x => x.TimeLiteral == eventTime).TimeId;
                var roomId = db.Rooms.SingleOrDefault(x=>x.RoomName == roomName).RoomId;
                var courseId = db.Courses.SingleOrDefault(x => x.CourseName == courseName).CourseId;
                var moduleId = db.Modules.SingleOrDefault(x => x.ModuleName == moduleName).ModuleId;
                var typeId = db.EventTypes.SingleOrDefault(x => x.TypeName == eventType).TypeId;

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
                                                             
                db.Events.Add(newEvent);
                db.SaveChanges();

                return generatedEventId;
            }

            /// <summary>
            /// Written: 18/11/2012
            /// </summary>
            /// <param name="editedEvent"></param>
            /// <returns></returns>
            public bool EditEvent(int editedEventId, int userId, string eventTitle, string eventDescription, string eventType, string repeatType, int eventDuration, DateTime startDate, string eventTime, string roomName, string courseName, string moduleName)
            {
                var editedEvent = db.Events.SingleOrDefault(x => x.EventId == editedEventId);

                if ( editedEvent == null)
                {
                    return false;
                }

                var repeatId = db.RepeatTypes.SingleOrDefault(x => x.RepeatTypeName == repeatType).RepeatTypeId;
                var timeId = db.Times.SingleOrDefault(x => x.TimeLiteral == eventTime).TimeId;
                var typeId = db.EventTypes.SingleOrDefault(x=>x.TypeName == eventType).TypeId;
                var roomId = db.Rooms.SingleOrDefault(x => x.RoomName == roomName).RoomId;
                var courseId = db.Courses.SingleOrDefault(x => x.CourseName == courseName).CourseId;
                var moduleId = db.Modules.SingleOrDefault(x => x.ModuleName == moduleName).ModuleId;

                
                  
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
                            
                    db.SaveChanges();

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
                var deletedEvent = db.Events.SingleOrDefault(x =>x.EventId == eventId);

                var newSavedId = new RecycledId()
                {
                    TableName = "Event",
                    IdRecovered = eventId
                };

                db.RecycledIds.Add(newSavedId);
                                
                db.Events.Remove(deletedEvent);

                db.SaveChanges();

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
                if (db.Events.Count() != 0)
                {
                    return db.Events.ToList();
                }
                return null;
            }

            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public List<EventType> ReturnEventTypes()
            {
                if (db.EventTypes.Count() != 0)
                {
                    return db.EventTypes.ToList(); 
                }
                return null;
            }

            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public List<UserType> ReturnUserTypes()
            {
                if (db.UserTypes.Count() != 0)
                {
                    return db.UserTypes.ToList();              
                }
                return null;
            }

            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public List<RoomType> ReturnRoomTypes()
            {
                if (db.RoomTypes.Count() != 0)
                {
                  return db.RoomTypes.ToList();
                }
                return null;
            }

            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public List<Course> ReturnCourses()
            {
                if (db.Courses.Count() != 0)
                {
                    return db.Courses.ToList();
                }
                return null;
            }

            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public List<Building> ReturnBuildings()
            {
                if (db.Buildings.Count() != 0)
                {
                    return db.Buildings.ToList();
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
                    return db.Buildings.SingleOrDefault(x => x.BuildingName == buildingName).BuildingId;
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
                   return db.Rooms.Where(x => x.Building == buildingId).SingleOrDefault(x=>x.RoomName == roomName).RoomId;
                }

                return 0;
            }


            /// <summary>
            /// Written: 09/12/13
            /// Returns Id of the Course Selected by searching by the name of the course
            /// </summary>
            /// <param name="buildingName"></param>
            /// <returns></returns>
            public int ReturnCourseIdFromCourseName(string courseName)
            {
                if (!String.IsNullOrEmpty(courseName))
                {
                    return db.Courses.SingleOrDefault(x => x.CourseName == courseName).CourseId;
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
                if (db.CourseModules.Where(x => x.Course == courseId).Count() != 0)
                {
                    var moduleIdList =  db.CourseModules.Where(x => x.Course == courseId).Select(x => x.Module).ToList();
                    List<Module> moduleList = new List<Module>();
                    foreach (var moduleId in moduleIdList)
                    {
                        moduleList.Add(db.Modules.SingleOrDefault(x => x.ModuleId == moduleId));
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
            /// <param name="RoomName"></param>
            /// <returns></returns>
            public List<Event> ReturnRoomEvents(int roomName)
            {
                if (roomName != 0)
                {
                   return db.Events.Where(x => x.Room == roomName).ToList();
                }
                return null;
            }

            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public List<Room> ReturnBuildingRooms(int buildingId)
            {
                if (db.Rooms.Where(x=>x.Building == buildingId).Count() != 0)
                {
                    return  db.Rooms.Where(x=>x.Building == buildingId).ToList();
                }
                return null;
            }

            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public int ReturnRoomBuilding(int roomId)
            {
                if (db.Rooms.Count() != 0)
                {
                    try
                    {
                        return (int)db.Rooms.SingleOrDefault(x => x.RoomId == roomId).Building;
                    }
                    catch (NullReferenceException)
                    {
                        return 0;
                    }
                    
                }
                return 0;
            }
            
            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public List<Time> ReturnTimes()
            {
                if (db.Times.Count() != 0)
                {
                    return db.Times.ToList();
                }
                return null;
            }

            /// <summary>
            /// Written: 21/11/2013
            /// </summary>
            /// <returns></returns>
            public List<RepeatType> ReturnRepeatTypes()
            {
                if(db.RepeatTypes.Count() != 0)
                {
                    return db.RepeatTypes.ToList();
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
            public int CreateNewBuilding(string buildingName, int buildingNumber, string addressLine1, string addressLine2, string postCode, string buildingCity)
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

                db.Buildings.Add(newBuilding);
                db.SaveChanges();

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

                db.Rooms.Add(newRoom);
                db.SaveChanges();

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

                db.RoomTypes.Add(newRoom);
                db.SaveChanges();

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

                db.RepeatTypes.Add(newRepeatType);
                db.SaveChanges();

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
                    SettingName = settingName,
                    SettingDescription = settingDescription,
                    CreateDate = DateTime.Now,
                    Creator = 1,
                    Active = "Always"

                };

                db.Settings.Add(newSetting);
                db.SaveChanges();

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
                if (appliedFilter != "")
                {
                    if (searchItem == "")
                    {
                        return db.Events.ToList();
                    }

                    List<Event> eventList = new List<Event>();
                    
                    if (appliedFilter == "Event Title")
                    {
                        eventList.AddRange(db.Events.Where(x => x.EventTitle.Contains(searchItem)).ToList());
                       
                        return eventList;
                    }
                    else if(appliedFilter == "Event Description")
                    {
                        eventList.AddRange(db.Events.Where(x => x.EventDescription.Contains(searchItem)).ToList());
                        
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
                    var monthrequested = currentculture.Calendar.GetMonth(dateRequested);
                    var yearRequested = currentculture.Calendar.GetYear(dateRequested);

                    var newDate = new DateTime(yearRequested, monthrequested, date);

                    DateTime startDate = new DateTime();

                    switch (dayRequested)
                    {
                        case "Sunday": startDate = newDate.AddDays(0);
                            break;
                        case "Monday": startDate = newDate.AddDays(-1);
                            break;
                        case "Tuesday": startDate = newDate.AddDays(-2);
                            break;
                        case "Wednesday": startDate = newDate.AddDays(-3);
                            break;
                        case "Thursday": startDate = newDate.AddDays(-4);
                            break;
                        case "Friday": startDate = newDate.AddDays(-5);
                            break;
                        case "Saturday": startDate = newDate.AddDays(-6);
                            break;
                    }

                    DateTime weekEnd = startDate.AddDays(7);

                    var eventsList = db.Events.Where(x => x.StartDate >= startDate && x.StartDate < weekEnd && x.Room == roomId).ToList();

                    return eventsList;
                }
                return null;

            }

            /// <summary>
            /// Written:13/03/2014
            /// </summary>
            /// <returns></returns>
            public List<TimetableObject> ReturnTimetableDisplay()
            {
                var timetableResult = new List<TimetableObject>();
                var eventsAvailable = db.Events.ToList().OrderBy(x => x.Time);
                var timeslot = db.Times.ToList().OrderBy(x => x.TimeId);

                foreach (Time e in timeslot)
                {
                    var newTimtableObject = new TimetableObject()
                    {
                        Timeslot = e.TimeId,
                        Enumerations = eventsAvailable.Count(x => x.Time == e.TimeId)
                    };

                    timetableResult.Add(newTimtableObject);
                }

                return timetableResult;

            }
            #endregion

        }


        
}
