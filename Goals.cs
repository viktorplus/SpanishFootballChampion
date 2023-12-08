using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishFootballChampion
{
    public class Goals
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int MatchId { get; set; }
        public int Minute { get; set; }

       public Goals(int id, int playerId, int matchId, int minute)
        {
            Id = id;
            PlayerId = playerId;
            MatchId = matchId;
            Minute = minute;
        }
        public void Print()
        {
            Console.WriteLine($"Id: {Id}, PlayerId: {PlayerId}, MatchId: {MatchId}, Minute: {Minute}");
        }
    }
}
