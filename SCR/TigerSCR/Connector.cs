using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bloomberglp.Blpapi;
//using BEmu;

namespace TigerSCR
{
    class Connector
    {
        private static Connector _instance;
        static readonly object instanceLock = new object();

        private Session session;
        private SessionOptions sessionOptions;
        private Request request;

        private Connector()
        {
            OpenSession();
        }

        public static Connector getConnector()
        {
            lock (instanceLock)
            {
                if (_instance == null)
                {
                    _instance = new Connector();
                }
            }
            return _instance;
        }

        public void OpenSession()
        {
            sessionOptions = new SessionOptions();
            sessionOptions.ServerHost = "localhost";
            sessionOptions.ServerPort = 8194;
            session = new Session(sessionOptions);
            if (!session.Start())
            {
                System.Console.WriteLine("Could not start session.");
                Console.WriteLine(session.ToString());
                Console.ReadKey();
                System.Environment.Exit(1);
            }
            if (!session.OpenService("//blp/refdata"))
            {
                System.Console.WriteLine("Could not open service " +
                "//blp/refdata");
                System.Environment.Exit(1);
            }
        }

        public void getEquity(List<Title> l_title)
        {
            CorrelationID requestID = new CorrelationID(1);
            Service refDataSvc = session.GetService("//blp/refdata");
            if (refDataSvc == null)
            {
                Console.WriteLine("Cannot get service");
            }
            else
            {
                request = refDataSvc.CreateRequest("ReferenceDataRequest");

                foreach (Title title in l_title)
                {
                    request.Append("securities", title.ToSubricption());
                    request.Append("fields", "PX_LAST");
                }

                session.SendRequest(request, requestID);
                bool continueToLoop = true;
                while (continueToLoop)
                {
                    Event eventObj = session.NextEvent();
                    switch (eventObj.Type)
                    {
                        case Event.EventType.RESPONSE: // final event
                            continueToLoop = false;
                            handleResponseEvent(eventObj);
                            break;
                        case Event.EventType.PARTIAL_RESPONSE:
                            handleResponseEvent(eventObj);
                            break;
                        default:
                            handleOtherEvent(eventObj);
                            break;
                    }
                }
                Console.ReadKey();
            }
        }
        

        private static void handleResponseEvent(Event eventObj)
        {
            System.Console.WriteLine("EventType =" + eventObj.Type);
            foreach (Message message in eventObj.GetMessages())
            {
                System.Console.WriteLine("correlationID=" +
                message.CorrelationID);
                System.Console.WriteLine("messageType =" +
                message.MessageType);
                Console.WriteLine(message.AsElement);
            }
            
        }
        private static void handleOtherEvent(Event eventObj)
        {
            System.Console.WriteLine("EventType=" + eventObj.Type);
            foreach (Message message in eventObj.GetMessages())
            {
                System.Console.WriteLine("correlationID=" +
                message.CorrelationID);
                System.Console.WriteLine("messageType=" +
                message.MessageType);
                Console.WriteLine(message.ToString());
                if (Event.EventType.SESSION_STATUS == eventObj.Type
                && message.MessageType.Equals("SessionTerminated"))
                {
                    System.Console.WriteLine("Terminating: " +
                    message.MessageType);
                    System.Environment.Exit(1);
                }
            }
        }


    }
}
