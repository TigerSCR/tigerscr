using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bloomberglp.Blpapi;


public class CourbeSwap
{
    private List<Tuple<string, double>> pointsCourbe = new List<Tuple<string, double>>();
    private string nom;

	public CourbeSwap(string _nom)
	{
        this.nom = _nom;
	}

    public Request SetRequest(Request request)
    {
        switch (nom)
        {
            case "EuroSwap":

                request.Append("securities", "EONIA Index"); //1 Day
                request.Append("fields", "ID_BB_SEC_NUM_DES");
                request.Append("fields", "PRIOR_CLOSE_MID");

                request.Append("securities", "EUR001W Index");// 1 Week
                request.Append("fields", "ID_BB_SEC_NUM_DES");
                request.Append("fields", "PRIOR_CLOSE_MID");

                request.Append("securities", "EUR001M Index");// 1 Month
                request.Append("fields", "ID_BB_SEC_NUM_DES");
                request.Append("fields", "PRIOR_CLOSE_MID");

                request.Append("securities", "EUR003M Index");// 3 Month
                request.Append("fields", "ID_BB_SEC_NUM_DES");
                request.Append("fields", "PRIOR_CLOSE_MID");

                break;

            default:
                throw new FormatException("Invalid name : " + nom);
        }

        return request;
    }

    public void ParseEquity(Element fieldData)
    {
        string tenor = fieldData.GetElementAsString("ID_BB_SEC_NUM_DES");
        double px_mid = fieldData.GetElementAsFloat64("PRIOR_CLOSE_MID");

        pointsCourbe.Add(new Tuple<string, double>(tenor, px_mid));
    }

    public override string ToString()
    {
        string s = "";
        foreach (var t in pointsCourbe)
        {
            s += t.Item1 + " " + t.Item2.ToString() + "\n";
        }
        return s;
    }
}
