﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITLab29.Models.Domain
{
    public class User : IdentityUser
    {

        #region Properties
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType UserType { get; set; }
        public UserStatus UserStatus { get; set; }
        public ICollection<UserSession> UserSessions { get; set; }
        public Media Avatar { get; set; }

        #endregion 

        public User(string userId, string firstName, string lastName, UserType userType, UserStatus userStatus) : 
            base()
        {
            if (userId == null || firstName == null || lastName == null)
            {
                throw new ArgumentNullException();
            }

            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            UserType = userType;
            UserStatus = userStatus;
            UserSessions = new List<UserSession>();
        }

        public User()
        {
            UserSessions = new List<UserSession>();
        }

        //public Session AddSession(int id, string title, string description, User responsible, DateTime start, DateTime end, int capacity, Location location) {
        //    if (UserSessions.Any(e => e.SessionId == id))
        //    {
        //        throw new ArgumentException($"User {FirstName} {LastName} has already an event with title: {title}");
        //    }
        //    Session newSession = new Session(id, title, description, responsible, start, end, capacity, location);
        //    UserSessions.Add(newSession);
        //    return newSession;
        //}

        public bool IsActive()
        {
            return UserStatus == UserStatus.ACTIVE;

        }

        public void AddUserSession(Session session) {
            UserSessions.Add(new UserSession { 
                Session = session,
                User = this,
                UserId = UserId,
                SessionId = session.SessionId
            });
        }

        public void RemoveUserSession(Session session) {
            UserSessions.Remove(UserSessions.Where(u => u.Session == session && u.User == this).FirstOrDefault());
        }

        public bool IsRegistered(int sessionId) {
            return UserSessions.Where(us => us.UserId == Id && us.SessionId == sessionId).Any();
        }
    }
}
