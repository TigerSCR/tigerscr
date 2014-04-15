using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bloomberglp.Blpapi;
using System.Windows;
//using BEmu;

namespace TigerAppWPF
{
    class Connector
    {
        private static Connector _instance;
        static readonly object instanceLock = new object();

        static private Session session;
        private Service refDataSvc;
        private SessionOptions sessionOptions;
        static private Request request;
        static private List<Title> l_title;
        static Dictionary<string, Tuple<int, string>> d_title;

        static private bool isGetType;

        private Connector()
        {
            OpenSession();
        }

        /// <summary>
        /// Pattern singleton : connecteur unique
        /// </summary>
        /// <returns>connecteur avec liaison BL étblie</returns>
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

        /// <summary>
        /// Etablie la connection avec Bloomberg
        /// </summary>
        private void OpenSession()
        {
            sessionOptions = new SessionOptions();
            sessionOptions.ServerHost = "localhost";
            sessionOptions.ServerPort = 8194;
            session = new Session(sessionOptions);
            l_title = new List<Title>();
            if (!session.Start())
            {
                MessageBox.Show("Could not start session.\n" + session.ToString(), "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Remplissage_Non_connection();
                return;
            }
            if (!session.OpenService("//blp/refdata"))
            {
                MessageBox.Show("Could not open service " +
                "//blp/refdata", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            refDataSvc = session.GetService("//blp/refdata");
            if (refDataSvc == null)
            {
                MessageBox.Show("Cannot get service", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                request = refDataSvc.CreateRequest("ReferenceDataRequest");
            }
        }

        /// <summary>
        /// Recupère les info specifiques des differents ISIN
        /// </summary>
        /// <param name="d_title">Isin avec leurs quantité</param>
        /// <returns>la liste de Titre avec les informations remplit</returns>
        public List<Title> getInfo(List<Tuple<string, int, string>> _d_title)
        {
            if (l_title.Count != 0)
                return l_title;

            d_title = new Dictionary<string, Tuple<int, string>>();
            foreach (var tuple in _d_title)
            {
                d_title.Add(tuple.Item1, new Tuple<int, string>(tuple.Item2, tuple.Item3));
            }
            isGetType = true;


            foreach (var title in _d_title)
            {
                request.Append("securities", "/isin/" + title.Item1);
                request.Append("fields", "MARKET_SECTOR_DES");
            }
            ResponseLoop(); // Recupère les secteurs de marché
            ResponseLoop(); // Recupère les actions propres au secteur

            int qtty;
            foreach (Title title in l_title)
            {
                qtty = d_title[title.Isin].Item1;

                if (qtty == 0)
                {
                    throw new NotFoundException(title.Isin + " not found");
                }
            }
            return l_title;
        }

        /// <summary>
        /// Remplit la liste de titre par des valeurs par defaut quand la connection à Bl à echoué
        /// </summary>
        static public void Remplissage_Non_connection()
        {
            if (l_title.Count == 0)
            {
                l_title.Add(new Equity("US03938L1044", 50, "LU", "USD", "ARCELORMITTAL-NY REGISTERED", 15.65));
                l_title.Add(new Equity("US76218Y1038", 25, "US", "USD", "RHINO RESOURCE PARTNERS LP", 13.08));
                l_title.Add(new Corp("XS0643300717", 100, "2011-06-30", "2014-07-07", "RCI BANQUE SA"));
            }
        }

        /// <summary>
        /// Envoi les requêtes PREPARES à l'avance, et recupère les infos.
        /// </summary>
        private void ResponseLoop()
        {
            session.SendRequest(request, new CorrelationID(1)); ;
            request = refDataSvc.CreateRequest("ReferenceDataRequest"); //Remet à zero la request qui sera rempli après.
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
        }

        /// <summary>
        /// Filtre la reponse pour les stocker dans des objets de types TITLE
        /// </summary>
        /// <param name="eventObj"></param>
        private static void handleResponseEvent(Event eventObj)
        {
            string marketSector;
            foreach (Message message in eventObj.GetMessages())
            {
                Element ReferenceDataResponse = message.AsElement;
                if (ReferenceDataResponse.HasElement("responseError"))
                {
                    //throw new Exception("responseError " + ReferenceDataResponse.ToString());
                    Console.WriteLine("Mode non connecté");
                    Remplissage_Non_connection();
                }
                Element securityDataArray = ReferenceDataResponse.GetElement("securityData");
                //Console.WriteLine(message.ToString());

                int numItems = securityDataArray.NumValues;
                for (int i = 0; i < numItems; ++i)
                {
                    Element securityData = securityDataArray.GetValueAsElement(i);
                    string security = securityData.GetElementAsString("security").Replace("/isin/", "");
                    //int sequenceNumber = securityData.GetElementAsInt32("sequenceNumber");
                    if (securityData.HasElement("securityError"))
                    {
                        Element securityError = securityData.GetElement("securityError");
                        throw new Exception("securityError " + securityError.ToString());
                    }
                    else
                    {
                        Element fieldData = securityData.GetElement("fieldData");

                        if (isGetType)
                            marketSector = fieldData.GetElementAsString("MARKET_SECTOR_DES");
                        else
                            marketSector = d_title[security].Item2;

                        switch (marketSector)
                        {
                            case "Equity":
                                if (isGetType)
                                    RequestEquity(security);
                                else
                                    ParseEquity(fieldData, security);
                                break;

                            case "Corp":
                                if (isGetType)
                                    RequestCorp(security);
                                else
                                    ParseCorp(fieldData, security);
                                break;

                            default:
                                throw new FormatException("market sector invalid: " + marketSector);
                        }
                    }
                }
            }
            if (isGetType)
            {
                isGetType = false;
            }
        }

        #region Sector

        static private void RequestCorp(string title)
        {
            d_title[title] = new Tuple<int, string>(d_title[title].Item1, "Corp");
            request.Append("securities", "/isin/" + title);
            //request.Append("fields", "MARKET_SECTOR_DES");
            request.Append("fields", "WORKOUT_DT_BID");
            request.Append("fields", "ISSUE_DT");
            request.Append("fields", "NAME");
        }

        private static void ParseEquity(Element fieldData, string security)
        {
            string country = fieldData.GetElementAsString("COUNTRY_ISO");
            double px_last = fieldData.GetElementAsFloat64("PX_LAST");
            string currency = fieldData.GetElementAsString("CRNCY");
            string name = fieldData.GetElementAsString("NAME");

            Equity equit = new Equity(security, 0, country, currency, name, px_last);
            l_title.Add(equit);
        }

        static private void RequestEquity(string title)
        {
            request.Append("securities", title);
            //request.Append("fields", "MARKET_SECTOR_DES");
            request.Append("fields", "PX_LAST");
            request.Append("fields", "CRNCY");
            request.Append("fields", "COUNTRY_ISO");
            request.Append("fields", "NAME");
        }

        private static void ParseCorp(Element fieldData, string security)
        {
            string dateBack = fieldData.GetElementAsString("WORKOUT_DT_BID");
            string dateEmit = fieldData.GetElementAsString("ISSUE_DT");
            string name = fieldData.GetElementAsString("NAME");

            Corp corp = new Corp(security, 0, dateEmit, dateBack, name);
            l_title.Add(corp);
        }

        #endregion


        private static void handleOtherEvent(Event eventObj)
        {
            //System.Console.WriteLine("EventType=" + eventObj.Type);
            //foreach (Message message in eventObj.GetMessages())
            //{
            //    System.Console.WriteLine("correlationID=" +
            //    message.CorrelationID);
            //    System.Console.WriteLine("messageType=" +
            //    message.MessageType);
            //    Console.WriteLine(message.ToString());
            //    if (Event.EventType.SESSION_STATUS == eventObj.Type
            //    && message.MessageType.Equals("SessionTerminated"))
            //    {
            //        System.Console.WriteLine("Terminating: " +
            //        message.MessageType);
            //        System.Environment.Exit(1);
            //    }
            //}
        }
    }
}
