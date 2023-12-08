using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishFootballChampion
{
    public class Matches
    {
        public int Id { get; set; }
        public int LocalTeamId { get; set; }
        public int VisitorTeamId { get; set; }
        public int LocalGoals { get; set; }
        public int VisitorGoals { get; set; }
        public DateTime Date { get; set; }

        public Matches() { }

        public Matches(int id, int localTeamId, int visitorTeamId, int localGoals, int visitorGoals, DateTime date)
        {
            Id = id;
            LocalTeamId = localTeamId;
            VisitorTeamId = visitorTeamId;
            LocalGoals = localGoals;
            VisitorGoals = visitorGoals;
            Date = date;
        }

        public void Print()
        {
            Console.WriteLine($"Id: {Id}, LocalTeamId: {LocalTeamId}, VisitorTeamId: {VisitorTeamId}, LocalGoals: {LocalGoals}, VisitorGoals: {VisitorGoals}, Date: {Date}");
        }
    }
}
