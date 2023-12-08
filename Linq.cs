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
        // Задание 2
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

        //Показать все матчи
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

        //Показать матчи за определенную дату
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
        //Показать матчи команды
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

        //Показать игроков, забивших голы за определенную дату
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
        //Задание 3
        //добавление нового матча
        public static void AddMatchInfo(Champion context)  //6
        {
            Console.Write("Введите дату матча (формат: YYYY-MM-DD HH:mm:ss): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Некорректный формат даты.");
                return;
            }

            Console.Write("Введите ID локальной команды: ");
            if (!int.TryParse(Console.ReadLine(), out int localTeamId))
            {
                Console.WriteLine("Некорректный формат ID команды.");
                return;
            }

            Console.Write("Введите ID гостевой команды: ");
            if (!int.TryParse(Console.ReadLine(), out int visitorTeamId))
            {
                Console.WriteLine("Некорректный формат ID команды.");
                return;
            }

            Console.Write("Введите количество голов локальной команды: ");
            if (!int.TryParse(Console.ReadLine(), out int localGoals))
            {
                Console.WriteLine("Некорректный формат количества голов.");
                return;
            }

            Console.Write("Введите количество голов гостевой команды: ");
            if (!int.TryParse(Console.ReadLine(), out int visitorGoals))
            {
                Console.WriteLine("Некорректный формат количества голов.");
                return;
            }

            var newMatch = new Matches
            {
                Date = date,
                LocalTeamId = localTeamId,
                VisitorTeamId = visitorTeamId,
                LocalGoals = localGoals,
                VisitorGoals = visitorGoals
            };

            context.Matches.Add(newMatch);
            context.SaveChanges();

            Console.WriteLine("Информация о матче успешно добавлена."); 
        }

        // Изменение информации о матче
        public static void UpdateMatchInfo(Champion context) //7
        {
            Console.Write("Введите ID матча, который вы хотите изменить: ");
            if (!int.TryParse(Console.ReadLine(), out int matchId))
            {
                Console.WriteLine("Некорректный формат ID матча.");
                return;
            }

            var existingMatch = context.Matches.Find(matchId);

            if (existingMatch == null)
            {
                Console.WriteLine("Матч с указанным ID не найден.");
                return;
            }

            Console.WriteLine("Выберите параметр для изменения:");
            Console.WriteLine("1. Дата матча");
            Console.WriteLine("2. ID локальной команды");
            Console.WriteLine("3. ID гостевой команды");
            Console.WriteLine("4. Количество голов локальной команды");
            Console.WriteLine("5. Количество голов гостевой команды");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Некорректный выбор.");
                return;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Введите новую дату матча (формат: YYYY-MM-DD HH:mm:ss): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime newDate))
                    {
                        Console.WriteLine("Некорректный формат даты.");
                        return;
                    }

                    existingMatch.Date = newDate;
                    break;

                case 2:
                    Console.Write("Введите новый ID локальной команды: ");
                    if (!int.TryParse(Console.ReadLine(), out int newLocalTeamId))
                    {
                        Console.WriteLine("Некорректный формат ID команды.");
                        return;
                    }

                    existingMatch.LocalTeamId = newLocalTeamId;
                    break;

                case 3:
                    Console.Write("Введите новый ID гостевой команды: ");
                    if (!int.TryParse(Console.ReadLine(), out int newVisitorTeamId))
                    {
                        Console.WriteLine("Некорректный формат ID команды.");
                        return;
                    }

                    existingMatch.VisitorTeamId = newVisitorTeamId;
                    break;

                case 4:
                    Console.Write("Введите новое количество голов локальной команды: ");
                    if (!int.TryParse(Console.ReadLine(), out int newLocalGoals))
                    {
                        Console.WriteLine("Некорректный формат количества голов.");
                        return;
                    }

                    existingMatch.LocalGoals = newLocalGoals;
                    break;

                case 5:
                    Console.Write("Введите новое количество голов гостевой команды: ");
                    if (!int.TryParse(Console.ReadLine(), out int newVisitorGoals))
                    {
                        Console.WriteLine("Некорректный формат количества голов.");
                        return;
                    }

                    existingMatch.VisitorGoals = newVisitorGoals;
                    break;

                default:
                    Console.WriteLine("Некорректный выбор.");
                    return;
            }

            context.SaveChanges();

            Console.WriteLine("Информация о матче успешно обновлена.");
        }

        // Удаление матча
        public static void DeleteMatch(Champion context) //8
        {
            Console.Write("Введите название локальной команды: ");
            string localTeamName = Console.ReadLine();

            Console.Write("Введите название гостевой команды: ");
            string visitorTeamName = Console.ReadLine();

            Console.Write("Введите дату матча (формат: YYYY-MM-DD HH:mm:ss): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.WriteLine("Некорректный формат даты.");
                return;
            }

            var matchesToDelete = context.Matches
                .Where(match => match.Date.Date == date.Date &&
                                context.Teams.Any(team => team.Id == match.LocalTeamId && team.TeamName == localTeamName) &&
                                context.Teams.Any(team => team.Id == match.VisitorTeamId && team.TeamName == visitorTeamName))
                .ToList();

            if (matchesToDelete.Count == 0)
            {
                Console.WriteLine("Матч с указанными параметрами не найден.");
                return;
            }

            Console.WriteLine("Найдены следующие матчи:");

            foreach (var match in matchesToDelete)
            {
                Console.WriteLine($"ID матча: {match.Id}, Дата: {match.Date.ToShortDateString()}, Локальная команда: {localTeamName}, Гостевая команда: {visitorTeamName}");
            }

            Console.Write("Хотите удалить один из этих матчей? (Y/N): ");
            string userInput = Console.ReadLine();

            if (userInput.ToUpper() == "Y")
            {
                Console.Write("Введите ID матча, который вы хотите удалить: ");
                if (!int.TryParse(Console.ReadLine(), out int matchId))
                {
                    Console.WriteLine("Некорректный формат ID матча.");
                    return;
                }

                var matchToDelete = context.Matches.Find(matchId);

                if (matchToDelete == null)
                {
                    Console.WriteLine("Матч с указанным ID не найден.");
                    return;
                }

                context.Matches.Remove(matchToDelete);
                context.SaveChanges();

                Console.WriteLine("Матч успешно удален.");
            }
            else
            {
                Console.WriteLine("Удаление отменено.");
            }
        }


    }
}
