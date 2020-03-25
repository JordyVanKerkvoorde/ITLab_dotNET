﻿using ITLab29.Models.Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace ITLab29.Tests.Data
{
    class DummyApplicationDbContext
    {

        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Session> Sessions { get; set; }
        public IEnumerable<UserSession> UserSessions { get; set; }
        public IEnumerable<Media> Media { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Feedback> Feedback { get; set; }
        public IEnumerable<Guest> Guests { get; set; }
        public IEnumerable<Announcement> Announcements { get; set; }


        public DummyApplicationDbContext()
        {

            User dummyUser = new User("88888", "Yorick", "Van de Woestyne", UserType.ADMIN, UserStatus.ACTIVE)
            { UserName = "yorick.vandewoestyne@student.hogent.be", Email = "yorick.vandewoestyne@student.hogent.be", EmailConfirmed = true };
            User dummyUser2 = new User("11111", "Jan", "Willem", UserType.USER, UserStatus.INACTIVE)
            { UserName = "jan.willem@student.hogent.be", Email = "jan.willem@student.hogent.be", EmailConfirmed = true };
            User dummyUser3 = new User("12345", "Sander", "Machado", UserType.RESPONSIBLE, UserStatus.BLOCKED)
            { UserName = "sander.castanheiramachado@student.hogent.be", Email = "sander.castanheiramachado@student.hogent.be", EmailConfirmed = true };
            User dummyUser4 = new User("00200", "Jordy", "Van Kerkvoorde", UserType.RESPONSIBLE, UserStatus.ACTIVE)
            { UserName = "jordy.vankerkvoorde@student.hogent.be", Email = "jordy.vankerkvoorde@student.hogent.be", EmailConfirmed = true };
            Media avatar = new Media(MediaType.IMAGE, "/photo/stock.png");
            Media = new List<Media> { avatar };
            dummyUser.Avatar = avatar;
            dummyUser2.Avatar = avatar;
            dummyUser3.Avatar = avatar;
            dummyUser4.Avatar = avatar;

            Users = new List<User> { dummyUser, dummyUser2, dummyUser3, dummyUser4 };

            Location schoonmeersen = new Location("GSCHB1.420", CampusEnum.SCHOONMEERSEN, 500);
            Location aalst = new Location("GSCHA6.099", CampusEnum.AALST, 420);
            Location mercator = new Location("GSCHM4.012", CampusEnum.MERCATOR, 200);
            Location schoonmeersen2 = new Location("GSCHB3.220", CampusEnum.SCHOONMEERSEN, 100);

            Locations = new List<Location> { schoonmeersen, schoonmeersen2, mercator, aalst };


            Session session1 = new Session("What I Wish I Had Known Before Scaling Uber to 1000 Services",
                    "Matt Ranney, Senior Staff Engineer bij Uber, vertelt over zijn ervaringen met microservices bij Uber: \"To Keep up with Uber's growth, we've embraced microservices in a big way.This has led to an explosion of new services, crossing over 1, 000 production services in early March 2016.Along the way we've learned a lot, and if we had to do it all over again, we'd do some things differently.If you are earlier along on your personal microservices journey than we are, then this talk may save you from having to learn some things learn the hard way. \"",
                        dummyUser, DateTime.Now.AddDays(-5), DateTime.Now.AddDays(-5).AddHours(2), 1, schoonmeersen);
            Session session2 =
                new Session("Life is Terrible: Let’s Talk About the Web",
                "Zij die Webapps 2 gehad hebben kennen JavaScript al, anderen beginnen nu net aan hun \"avontuur\". James Mickens kwam vorig semester al aan bod met een tirade over security en machine learning, nu is hij terug om iedereen te laten weten wat hij vindt van de nummer 1 programmeertaal voor het Web.",
                    dummyUser, DateTime.Now.AddDays(3), DateTime.Now.AddDays(3).AddHours(3), 69, aalst);
            Session session3 = new Session("De weg naar de Cloud, hoe doen bedrijven dat nu eigenlijk?", "Diederik Wyffels heeft al 20 jaar ervaring in de branche en focust zich vooral op het begeleiden van bedrijven die moeite hebben met het schalen van hun IT-infrastructuur. In deze talk bespreekt hij concreet hoe bedrijven begeleid worden in hun overstap naar de cloud.",
                        dummyUser2, DateTime.Now.AddDays(7), DateTime.Now.AddDays(7).AddHours(2), 30, mercator);
            Session session4 = new Session("TDD, Where Did It All Go Wrong", "In Ontwerpen 1 leerde je al over het testen van software, en hoe TDD vitaal is voor het afleveren van werkende software. En in de volgende semesters blijft die focus op het schrijven van testen aanwezig. Maar moet die focus op TDD er wel zo sterk zijn? Is wat nuance niet aan de orde? Dat is wat deze talk brengt. Ian Cooper zijn kennis over software testing is al even impressionant als zijn baard dus zeker een talk om niet te missen.",
                        dummyUser2, DateTime.Now.AddDays(14), DateTime.Now.AddDays(14).AddHours(1), 123, schoonmeersen2);
            Session session5 = new Session("Power Use of UNIX - Dan North", "Kennis van de commandline gecombineerd met de basis van reguliere expressies laten je toe om een hoger niveau van productiviteit te bereiken. Deze talk introduceert in een halfuur de meest bruikbare UNIX commando's om je workflow te optimaliseren.",
                        dummyUser3, DateTime.Now.AddDays(8), DateTime.Now.AddDays(8).AddHours(1), 30, schoonmeersen);

            session1.SessionId = 5;
            session2.SessionId = 4;
            session3.SessionId = 3;
            session4.SessionId = 2;
            session5.SessionId = 1;


            Sessions = new List<Session> { session1, session2, session3, session4, session5 };


            Feedback feedback1 = new Feedback(5, "Goede gastspreker!", dummyUser);
            Feedback feedback2 = new Feedback(3, "Jammer dat er op het einde geen tijd meer was voor vragen.", dummyUser2);

            Feedback = new List<Feedback> { feedback1, feedback2 };


            Guest guest1 = new Guest("Elon Musk", "elon.musk@gmail.com", "0412 12 12 12");
            Guest guest2 = new Guest("Tim Cook", "tim.cook@gmail.com", "0413 13 13 13");
            Guest guest3 = new Guest("Bill Gates", "bill.gates@gmail.com", "0414 14 14 14");
            Guest guest4 = new Guest("Mark Zuckerberg", "mark.zuckerberg@gmail.com", "0415 15 15 15");

            Guests = new List<Guest> { guest1, guest2, guest3, guest4 };
            

            Media = new List<Media>
                {
                    avatar,
                    new Media(MediaType.FILE, "/path/web_presentatie.ppt"),
                    new Media(MediaType.IMAGE, "/path/cloud_schema.jpg"),
                    new Media(MediaType.VIDEO, "/path/videos/testdrivendevelopment.mov")
                };

            session1.AddUserSession(dummyUser);
            session3.AddUserSession(dummyUser);
            session5.AddUserSession(dummyUser);


            Announcement announcement1 = new Announcement(DateTime.Now.AddDays(-2), "Geen fysieke lessen tot 18 mei.");
            Announcement announcement2 = new Announcement(DateTime.Now.AddDays(-9), "Sessies worden voorlopig uitgesteld.");
            Announcement announcement3 = new Announcement(DateTime.Now.AddDays(-6), "Lessen gaan online door.");

            Announcements = new List<Announcement> { announcement1, announcement2, announcement3 };



        }

    }
}
