/* IMDB Data Extractor from .csv Input File */

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;


namespace IMDBDataExtractor
{
    class Extract
    {
        public int num_rate;
        public string title;
        public string year;
        public double rate;
        public double value;

        public Extract(int Num_Rate, double Rate, string Title, string Year)
        {
            num_rate = Num_Rate;
            title = Title;
            year = Year;
            rate = Rate;
            value = Math.Sqrt(num_rate) * Math.Sqrt(rate);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //Console.Write("Filename: ");
            //string filename = Console.ReadLine();             // Use If the Filename will Be Given as An Input String at Runtime
            string filename = "watchlist.csv";
            string[] writefile = filename.Split('.');

            List<string> data = new List<string>();
            data = File.ReadAllLines(filename).ToList();        // All Lines from the Input .csv File are Assigned to the "data" List

            string[] head = data[0].Trim('\"').Split(new string[] { "\",\"" }, StringSplitOptions.None);
            
            List<string> detect = new List<string>();
            detect = head.ToList();
            int title = detect.FindIndex(item => item == "Title");
            int year = detect.FindIndex(item => item == "Year");
            int ratings = detect.FindIndex(item => item == "Num. Votes");
            int rateval = detect.FindIndex(item => item == "IMDb Rating");


            List<Extract> FilmList = new List<Extract>();

            for (int i = 1; i < data.Count; i++)
            {
                string[] temp = data[i].Trim('\"').Split(new string[] { "\",\"" }, StringSplitOptions.None);

                if (temp[ratings] == "" || temp[ratings] == " ")
                {
                    temp[ratings] = "0";
                }

                if (temp[rateval] == "" || temp[rateval] == " ")
                {
                    temp[rateval] = "0";
                }

                // Each Line = Ratings: Integer || Rate Numbers: Double || Title: String || Year: String
                Extract exTemp = new Extract(int.Parse(temp[ratings]), double.Parse(temp[rateval]), temp[title], temp[year]);
                FilmList.Add(exTemp);
            }


            FilmList = FilmList.Distinct().OrderByDescending(x => x.num_rate).ToList();     // Removing Redundant Lines and Ordering By Descending Numbers of Rates
            string[] tofile = new string[FilmList.Count + 1];

            Extract film;
            Console.WriteLine();
            tofile[0] = "*RT/100 (RateNum): Title (Year)";
            Console.WriteLine(tofile[0]);
            for (int k = 0; k < FilmList.Count; k++)
            {
                film = FilmList.ElementAt(k);
                tofile[k + 1] = $"{film.rate.ToString().PadLeft(3, '0')}/100 ({film.num_rate.ToString().PadLeft(7, '0')}): {film.title} ({film.year})";
                Console.WriteLine(tofile[k + 1]);
            }
            File.WriteAllLines(writefile[0] + ".txt", tofile);


            Console.WriteLine("\n\n------------");
            Console.WriteLine($"Extracted to: \"{writefile[0]}.txt\"");

            Console.WriteLine();
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey();
        }
    }
}