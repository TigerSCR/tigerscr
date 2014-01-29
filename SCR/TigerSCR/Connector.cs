using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Bloomberglp.Blpapi;
//using BEmu;

namespace Bloomberg
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

        public void getEquity(List<Equity> l_equity)
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

                foreach (Title title in l_equity)
                {
                    request.Append("securities", title.ToSecurities()+" EQUITY");
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
                            handleResponseEvent(eventObj, l_equity);
                            break;
                        case Event.EventType.PARTIAL_RESPONSE:
                            handleResponseEvent(eventObj, l_equity);
                            break;
                        default:
                            handleOtherEvent(eventObj);
                            break;
                    }
                }

                foreach (Equity equit in l_equity)
                {
                    Console.WriteLine(equit.ToString());
                }
                Console.ReadKey();
            }
        }
        

        private static void handleResponseEvent(Event eventObj, List<Equity> l_equity)
        {
            foreach (Message message in eventObj.GetMessages())
            {
                Element ReferenceDataResponse = message.AsElement;
                if (ReferenceDataResponse.HasElement("responseError"))
                {
                    throw new Exception("responseError " + ReferenceDataResponse.ToString());
                }
                Element securityDataArray = ReferenceDataResponse.GetElement("securityData");
                ParseDataArray(securityDataArray, l_equity);
            }
        }

        private static void ParseDataArray(Element securityDataArray, List<Equity> l_equity)
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
                    
                    string[] securityTab = security.Split(' ');
                    if (securityTab.Length == 3)
                    {
                        Equity equit = new Equity(securityTab[0], securityTab[1], country, currency, name, px_last);
                        l_equity.RemoveAt(0);
                        l_equity.Add(equit);
                    }
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
