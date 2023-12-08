using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishFootballChampion
{
    public class Players
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public int TeamId { get; set; }
        public string Country { get; set; }
        public int JerseyNumber { get; set; }
        public int PositionId { get; set; }

        public Players() { }

        public Players(int id, string name, string surName, int teamId, string country, int jerseyNumber, int positionId)
        {
            Id = id;
            Name = name;
            SurName = surName;
            TeamId = teamId;
            Country = country;
            JerseyNumber = jerseyNumber;
            PositionId = positionId;
        }

        public void Print()
        {
            Console.WriteLine($"Id: {Id}, Name: {Name}, Surname: {SurName}, TeamId: {TeamId}, Country: {Country}, JerseyNumber: {JerseyNumber}, PositionId: {PositionId}");
        }
    }
}
