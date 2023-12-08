using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace SpanishFootballChampion
{
    public static class Linq
    {
        // 1. Get all players from the team with id 
        public static void GetPlayersFromTeamWithId1(Champion context, int id)
        {
                var players = context.Players.Where(p => p.TeamId == id).ToList();
                foreach (var player in players)
                {
                    player.Print();
                }
        }

        public static void ShowGoalDifference(Champion context)
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

                foreach (var item in query)
                {
                    Console.Write($"Команда: {item.TeamName}");
                    Console.WriteLine($"\tРазница голов: {item.GoalsScored - item.GoalsConceded}");
                    Console.WriteLine();
                }
        }

        public static void PrintMatches(Champion context)
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

                foreach (var item in query)
                {
                    Console.WriteLine($"{item.Team1}\t\t" +
                                      $"{item.Team2}\t\t" +
                                      $"{item.MatchDate}\t\t" +
                                      $"{item.LocalGoals}\t\t\t" +
                                      $"{item.VisitorGoals}\t\t\t");
                }
        }

    }
}
