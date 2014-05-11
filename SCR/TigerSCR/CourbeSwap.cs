using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bloomberglp.Blpapi;
using System.IO;


public class CourbeSwap
{
    public SortedList<DateTime, double> pointsCourbe = new SortedList<DateTime, double>();
    public string name { get; private set; }
    public string isin { get; private set; }
    private string security;
    private DateTime dtNow = DateTime.Now;
    private string text_tenor = "1D 2D 1W 2W 1M 2M 3M 4M 5M 6M 7M 8M 9M 10M 11M 1Y 15M 18M 21M 2Y 27M 30M 33M 3Y 39M 42M 45M 4Y 5Y 6Y " + //1I 2I 3I 4I
        "7Y 8Y 9Y 10Y 11Y 12Y 13Y 14Y 15Y 16Y 17Y 18Y 19Y 20Y 21Y 22Y 23Y 24Y 25Y 26Y 27Y 28Y 29Y 30Y 31Y 32Y 33Y 34Y 35Y 36Y 37Y 38Y 39Y 40Y 41Y 42Y "+ 
        "43Y 44Y 45Y 46Y 47Y 48Y 49Y 50Y";
    private string[] t_tenor;

	public CourbeSwap(string _nom, string _isin, string _security)
	{
        this.name = _nom;
        this.isin = _isin;
        this.t_tenor = text_tenor.Split(' ');
        this.security = "BLC2 Curncy";
	}

    public Request SetRequest(Request request)
    {
        switch (name)
        {
            case "EuroSwap":

                for (int i = 0; i < t_tenor.Length; i++)
                {
                    request.Append("securities", isin + " "+t_tenor[i]+" "+ security);
                    request.Append("fields", "PRIOR_CLOSE_MID");
                    request.Append("fields", "ID_BB_SEC_NUM_DES");
                }

                break;

            default:
                throw new FormatException("Invalid name : " + name);
        }
        return request;
    }

    public void ParseEquity(Element fieldData)
    {
        string tenor = fieldData.GetElementAsString("ID_BB_SEC_NUM_DES");
        double px_mid = fieldData.GetElementAsFloat64("PRIOR_CLOSE_MID");
        tenor = tenor.Replace(isin,"");
        pointsCourbe.Add(TenorToDate(tenor), px_mid);
    }

    public override string ToString()
    {
        string s = "";
        foreach (var t in pointsCourbe)
        {
            s += t.Key + " " + t.Value.ToString() + "\n";
        }
        return s;
    }

    private DateTime TenorToDate(string tenor)
    {
        DateTime date_debut = dtNow;

        if (tenor.Contains('D'))
        {
            tenor = tenor.Replace("D", "");
            date_debut = DateTime.Now.AddDays(int.Parse(tenor));
        }

        else if (tenor.Contains('W'))
        {
            tenor = tenor.Replace("W", "");
            int ajout = int.Parse(tenor) * 7;
            date_debut = DateTime.Now.AddDays(ajout);
        }

        else if (tenor.Contains('M'))
        {
            tenor = tenor.Replace("M", "");
            date_debut = DateTime.Now.AddMonths(int.Parse(tenor));
        }

        else if (tenor.Contains('I'))
        {
            //tenor = tenor.Replace("I", "");
            //int ajout = int.Parse(tenor) * 3;
            //date_debut = DateTime.Now.AddMonths(ajout);
        }

        else if (tenor.Contains('Y'))
        {
            tenor = tenor.Replace("Y", "");
            date_debut = DateTime.Now.AddYears(int.Parse(tenor));
        }

        else
        {
            throw new ArgumentException(tenor + " pas de type D,W,M,I,Y");
        }

        return date_debut;

    }

    public void WriteCSV()
    {
        using (StreamWriter sw = new StreamWriter(@"CSV\" + name + ".csv"))
        {
            foreach (var point in pointsCourbe)
            {
                sw.WriteLine(point.Key + ";" + point.Value);
            }
            sw.Close();
        }
    }

    public void ReadCSV()
    {
        // Read and show each line from the file.
        string line = "";
        string[] values;
        Console.WriteLine("Lecture CSV, ancienne courbe ecrasée");
        pointsCourbe.Clear();
        try
        {
            using (StreamReader sr = new StreamReader(@"CSV\" + name + ".csv"))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    values = line.Split(';');
                    pointsCourbe.Add(Convert.ToDateTime(values[0]), Convert.ToDouble(values[1]));
                }
                sr.Close();
            }
        }

        catch (Exception)
        {

        }
    }

    
    /// <summary>
    /// Donne la valeur d'une date données à partir de la courbe
    /// </summary>
    /// <param name="date"></param>
    /// <returns></returns>
    public double GetValue(string date)
    {
        //ex : 2011-06-30
        DateTime dt_emit = Convert.ToDateTime(date);
        DateTime dt_last = DateTime.Now;
        double value;

        foreach (var dt in pointsCourbe)
        {
            if (dt_emit < dt.Key && dt_emit > dt_last)
            {
                //interpolation linéaire entre dt et dt_last
                double p = (dt.Value - pointsCourbe[dt_last])/ (dt.Key - dt_last).TotalDays; // pente
                value = p * (dt_emit - dt_last).TotalDays + pointsCourbe[dt_last];
                return value;
            }
            dt_last = dt.Key;
        }
        return pointsCourbe[dt_last];


    }
}
