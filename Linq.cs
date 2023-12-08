using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace SpanishFootballChampion
{
    public static class Linq
    {
        public static void ShowGoalDifference(Champion context) //1
        {

                var teams = context.Teams.ToList();
                var matches = context.Matches.ToList();

                var query = from team in teams
                            select new
                            {
                                TeamName = team.TeamName,
                                GoalsScored = matches
                                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                                    .Sum(match => match.LocalTeamId == team.Id ? match.LocalGoals : match.VisitorGoals),

                                GoalsConceded = matches
                                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                                    .Sum(match => match.LocalTeamId == team.Id ? match.VisitorGoals : match.LocalGoals),
                            };
            Console.WriteLine("Разница голов всех команд:");
            Console.WriteLine(new string('-', 100));

            foreach (var item in query)
                {
                    Console.Write($"Команда: {item.TeamName}");
                    Console.WriteLine($"\tРазница голов: {item.GoalsScored - item.GoalsConceded}");
                }
            Console.WriteLine();
        }

        public static void PrintMatches(Champion context) //2
        {
            var matches = context.Matches.ToList();

            var query = from match in matches
                        select new
                        {
                            Team1 = context.Teams.FirstOrDefault(team => team.Id == match.LocalTeamId)?.TeamName,
                            Team2 = context.Teams.FirstOrDefault(team => team.Id == match.VisitorTeamId)?.TeamName,
                            MatchDate = match.Date,
                            LocalGoals = match.LocalGoals,
                            VisitorGoals = match.VisitorGoals,
                        };

            Console.WriteLine($"{"Team1",-20}{"Team2",-20}{"MatchDate",-20}{"LocalGoals",-20}{"VisitorGoals",-20}");
            Console.WriteLine(new string('-', 100));

            foreach (var item in query)
            {
                Console.WriteLine($"{item.Team1,-20}" +
                                  $"{item.Team2,-20}" +
                                  $"{item.MatchDate,-20}" +
                                  $"{item.LocalGoals,-20}" +
                                  $"{item.VisitorGoals,-20}");
            }
            Console.WriteLine();
        }

        public static void PrintMatchesOfDay(Champion context, DateTime date) //3
        {
            var matchesOfDay = context.Matches
                .Where(match => match.Date.Date == date.Date)
                .ToList();

            Console.WriteLine($"Matches on {date.ToShortDateString()}");
            Console.WriteLine($"{"Team1",-20}{"Team2",-20}{"MatchDate",-20}{"LocalGoals",-20}{"VisitorGoals",-20}");
            Console.WriteLine(new string('-', 100));

            foreach (var match in matchesOfDay)
            {
                var team1 = context.Teams.FirstOrDefault(team => team.Id == match.LocalTeamId)?.TeamName;
                var team2 = context.Teams.FirstOrDefault(team => team.Id == match.VisitorTeamId)?.TeamName;

                Console.WriteLine($"{team1,-20}" +
                                  $"{team2,-20}" +
                                  $"{match.Date,-20}" +
                                  $"{match.LocalGoals,-20}" +
                                  $"{match.VisitorGoals,-20}");
            }

            Console.WriteLine();
        }

        public static void PrintMatchesForTeam(Champion context, int teamId) //4
        {
            var matchesForTeam = context.Matches
                .Where(match => match.LocalTeamId == teamId || match.VisitorTeamId == teamId)
                .ToList();

            Console.WriteLine($"Matches for Team with Id {teamId}");
            Console.WriteLine($"{"MatchId",-10}{"LocalTeam",-20}{"VisitorTeam",-20}{"MatchDate",-20}{"LocalGoals",-20}{"VisitorGoals",-20}");
            Console.WriteLine(new string('-', 100));

            foreach (var match in matchesForTeam)
            {
                var localTeam = context.Teams.FirstOrDefault(team => team.Id == match.LocalTeamId)?.TeamName;
                var visitorTeam = context.Teams.FirstOrDefault(team => team.Id == match.VisitorTeamId)?.TeamName;

                Console.WriteLine($"{match.Id,-10}" +
                                  $"{localTeam,-20}" +
                                  $"{visitorTeam,-20}" +
                                  $"{match.Date,-20}" +
                                  $"{match.LocalGoals,-20}" +
                                  $"{match.VisitorGoals,-20}");
            }

            Console.WriteLine();
        }


        public static void PrintScoringPlayersByDate(Champion context, DateTime date) //5
        {
            var matches = context.Matches
                .Where(match => match.Date.Date == date.Date)
                .ToList();

            var players = context.Players.ToList();

            var query = from match in matches
                        join player in players on match.LocalTeamId equals player.TeamId
                        where match.LocalGoals > 0
                        select new
                        {
                            PlayerName = player.Name,
                            TeamName = context.Teams.FirstOrDefault(team => team.Id == match.LocalTeamId)?.TeamName,
                            Goals = match.LocalGoals,
                        };

            Console.WriteLine($"{"PlayerName",-20}{"TeamName",-20}{"Goals",-20}");
            Console.WriteLine(new string('-', 100));

            foreach (var item in query)
            {
                Console.WriteLine($"{item.PlayerName,-20}" +
                                  $"{item.TeamName,-20}" +
                                  $"{item.Goals,-20}");
            }

            Console.WriteLine();
        }

    }
}
