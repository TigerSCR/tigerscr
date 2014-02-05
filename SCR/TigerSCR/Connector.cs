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

        public void getEquities(List<Title> l_title)
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
                    request.Append("securities", "/isin/" + title.Isin);
                    request.Append("fields", "PX_LAST");
                    request.Append("fields", "CRNCY");
                    request.Append("fields", "COUNTRY");
                    request.Append("fields", "NAME"); 
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
                            handleResponseEvent(eventObj, l_title);
                            break;
                        case Event.EventType.PARTIAL_RESPONSE:
                            handleResponseEvent(eventObj, l_title);
                            break;
                        default:
                            handleOtherEvent(eventObj); 
                            break;
                    }
                }

                foreach (Title title in l_title)
                {
                    Console.WriteLine(title.ToString());
                }
                Console.ReadKey();
            }
        }
        

        private static void handleResponseEvent(Event eventObj, List<Title> l_title)
        {
            foreach (Message message in eventObj.GetMessages())
            {
                Element ReferenceDataResponse = message.AsElement;
                if (ReferenceDataResponse.HasElement("responseError"))
                {
                    throw new Exception("responseError " + ReferenceDataResponse.ToString());
                }
                Element securityDataArray = ReferenceDataResponse.GetElement("securityData");
                Console.WriteLine(message.ToString());
                ParseDataArray(securityDataArray, l_title);
            }
        }

        private static void ParseDataArray(Element securityDataArray, List<Title> l_title)
        {
            int numItems = securityDataArray.NumValues;
            for (int i = 0; i < numItems; ++i)
            {
                Element securityData = securityDataArray.GetValueAsElement(i);
                string security = securityData.GetElementAsString("security");
                int sequenceNumber = securityData.GetElementAsInt32("sequenceNumber");
                if (securityData.HasElement("securityError"))
                {
                    Element securityError = securityData.GetElement("securityError");
                    throw new Exception("securityError " + securityError.ToString());
                }
                else
                {
                    Element fieldData = securityData.GetElement("fieldData");
                    string country = fieldData.GetElementAsString("COUNTRY");
                    double px_last = fieldData.GetElementAsFloat64("PX_LAST");
                    string currency = fieldData.GetElementAsString("CRNCY");
                    string name = fieldData.GetElementAsString("NAME");

                    Equity equit = new Equity(security, 50, country, currency, name, px_last);
                    l_title.RemoveAt(0);
                    l_title.Add(equit);
                }
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
