using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishFootballChampion
{
    public class Teams
    {
        public int Id { get; set; }

        public string TeamName { get; set; }
        public string Country { get; set; }

        public Teams(int id, string teamName, string country)
        {
            Id = id;
            TeamName = teamName;
            Country = country;
        }

        public void Print()
        {
            Console.WriteLine($"Id: {Id}, TeamName: {TeamName}, Country: {Country}");
        }
    }
}
