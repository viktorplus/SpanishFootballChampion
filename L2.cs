using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishFootballChampion
{
    public static class L2
    {
        public static void AddRandomMatch(Champion context)
        {
            // Генерация случайных данных для нового матча
            Random random = new Random();
            int localTeamId = random.Next(1, 5); // диапазон ID команд
            int visitorTeamId;
            do
            {
                visitorTeamId = random.Next(1, 5); // диапазон ID команд
            } while (visitorTeamId == localTeamId); // Гарантия, что ID гостевой команды не совпадает с локальной

            DateTime date = DateTime.Now.AddHours(random.Next(1, 48)); // Генерация случайной даты

            int localGoals = random.Next(0, 5); //  диапазон голов
            int visitorGoals = random.Next(0, 5); // диапазон голов

            // Создание нового матча
            var newMatch = new Matches
            {
                Date = date,
                LocalTeamId = localTeamId,
                VisitorTeamId = visitorTeamId,
                LocalGoals = localGoals,
                VisitorGoals = visitorGoals
            };

            // Добавление матча в базу данных
            context.Matches.Add(newMatch);
            context.SaveChanges();

            Console.WriteLine("Информация о случайном матче успешно добавлена.");
        }

        // Топ-3 найкращих бомбардирів конкретної команди
        public static void ShowTop3ScorersForTeam(Champion context, int teamId)
        {
            var topScorers = context.Players
                .Where(player => player.TeamId == teamId)
                .OrderByDescending(player => context.Goals.Count(goal => goal.PlayerId == player.Id))
                .Take(3)
                .Select(player => new
                {
                    PlayerName = $"{player.Name} {player.SurName}",
                    GoalsScored = context.Goals.Count(goal => goal.PlayerId == player.Id)
                });

            Console.WriteLine($"Топ-3 найкращих бомбардирів команди з ID {teamId}:");
            Console.WriteLine(new string('-', 50));

            foreach (var scorer in topScorers)
            {
                Console.WriteLine($"Гравець: {scorer.PlayerName}, Забиті голи: {scorer.GoalsScored}");
            }

            Console.WriteLine();
        }

        //Покажіть найкращого бомбардира конкретної команди:
        public static void ShowTopScorerForTeam(Champion context, int teamId)
        {
            var topScorer = context.Players
                .Where(player => player.TeamId == teamId)
                .OrderByDescending(player => context.Goals.Count(goal => goal.PlayerId == player.Id))
                .FirstOrDefault();

            if (topScorer != null)
            {
                Console.WriteLine($"Найкращий бомбардир команди з ID {teamId}:");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Гравець: {topScorer.Name} {topScorer.SurName}");
                Console.WriteLine($"Забиті голи: {context.Goals.Count(goal => goal.PlayerId == topScorer.Id)}");
            }
            else
            {
                Console.WriteLine($"Немає даних про бомбардирів для команди з ID {teamId}.");
            }

            Console.WriteLine();
        }


        //Покажіть Топ-3 найкращих бомбардирів усього чемпіонату:

        public static void ShowTop3OverallScorers(Champion context)
        {
            var topOverallScorers = context.Players
                .OrderByDescending(player => context.Goals.Count(goal => goal.PlayerId == player.Id))
                .Take(3)
                .Select(player => new
                {
                    PlayerName = $"{player.Name} {player.SurName}",
                    GoalsScored = context.Goals.Count(goal => goal.PlayerId == player.Id)
                });

            Console.WriteLine($"Топ-3 найкращих бомбардирів усього чемпіонату:");
            Console.WriteLine(new string('-', 50));

            foreach (var scorer in topOverallScorers)
            {
                Console.WriteLine($"Гравець: {scorer.PlayerName}, Забиті голи: {scorer.GoalsScored}");
            }

            Console.WriteLine();
        }


        // Покажіть найкращого бомбардира усього чемпіонату:

        public static void ShowTopOverallScorer(Champion context)
        {
            var topOverallScorer = context.Players
                .OrderByDescending(player => context.Goals.Count(goal => goal.PlayerId == player.Id))
                .FirstOrDefault();

            if (topOverallScorer != null)
            {
                Console.WriteLine($"Найкращий бомбардир усього чемпіонату:");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Гравець: {topOverallScorer.Name} {topOverallScorer.SurName}");
                Console.WriteLine($"Забиті голи: {context.Goals.Count(goal => goal.PlayerId == topOverallScorer.Id)}");
            }
            else
            {
                Console.WriteLine("Немає даних про бомбардирів усього чемпіонату.");
            }

            Console.WriteLine();
        }


        //Покажіть Топ-3 команди, які забили найбільше голів:

        public static void ShowTop3TeamsByGoalsScored(Champion context)
        {
            var topTeamsByGoalsScored = context.Teams
                .OrderByDescending(team => context.Matches
                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                    .Sum(match => match.LocalTeamId == team.Id ? match.LocalGoals : match.VisitorGoals))
                .Take(3)
                .Select(team => new
                {
                    TeamName = team.TeamName,
                    GoalsScored = context.Matches
                        .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                        .Sum(match => match.LocalTeamId == team.Id ? match.LocalGoals : match.VisitorGoals)
                });

            Console.WriteLine($"Топ-3 команди, які забили найбільше голів:");
            Console.WriteLine(new string('-', 50));

            foreach (var team in topTeamsByGoalsScored)
            {
                Console.WriteLine($"Команда: {team.TeamName}, Забиті голи: {team.GoalsScored}");
            }

            Console.WriteLine();
        }
       
        // Покажіть команду, яка забила найбільше голів:

        public static void ShowTopTeamByGoalsScored(Champion context)
        {
            var topTeamByGoalsScored = context.Teams
                .OrderByDescending(team => context.Matches
                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                    .Sum(match => match.LocalTeamId == team.Id ? match.LocalGoals : match.VisitorGoals))
                .FirstOrDefault();

            if (topTeamByGoalsScored != null)
            {
                Console.WriteLine($"Команда, яка забила найбільше голів:");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Команда: {topTeamByGoalsScored.TeamName}, Забиті голи: {context.Matches.Sum(match => match.LocalTeamId == topTeamByGoalsScored.Id ? match.LocalGoals : match.VisitorGoals)}");
            }
            else
            {
                Console.WriteLine("Немає даних про команди, які забили голі.");
            }

            Console.WriteLine();
        }
        // Покажіть Топ-3 команди, які пропустили найменше голів:

        public static void ShowTop3TeamsByGoalsConceded(Champion context)
        {
            var topTeamsByGoalsConceded = context.Teams
                .OrderBy(team => context.Matches
                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                    .Sum(match => match.LocalTeamId == team.Id ? match.VisitorGoals : match.LocalGoals))
                .Take(3)
                .Select(team => new
                {
                    TeamName = team.TeamName,
                    GoalsConceded = context.Matches
                        .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                        .Sum(match => match.LocalTeamId == team.Id ? match.VisitorGoals : match.LocalGoals)
                });

            Console.WriteLine($"Топ-3 команди, які пропустили найменше голів:");
            Console.WriteLine(new string('-', 50));

            foreach (var team in topTeamsByGoalsConceded)
            {
                Console.WriteLine($"Команда: {team.TeamName}, Пропущені голи: {team.GoalsConceded}");
            }

            Console.WriteLine();
        }
       // Покажіть команду, яка пропустила найменше голів:

        public static void ShowTopTeamByGoalsConceded(Champion context)
        {
            var topTeamByGoalsConceded = context.Teams
                .OrderBy(team => context.Matches
                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                    .Sum(match => match.LocalTeamId == team.Id ? match.VisitorGoals : match.LocalGoals))
                .FirstOrDefault();

            if (topTeamByGoalsConceded != null)
            {
                Console.WriteLine($"Команда, яка пропустила найменше голів:");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Команда: {topTeamByGoalsConceded.TeamName}, Пропущені голі: {context.Matches.Sum(match => match.LocalTeamId == topTeamByGoalsConceded.Id ? match.VisitorGoals : match.LocalGoals)}");
            }
            else
            {
                Console.WriteLine("Немає даних про команди, які пропустили голі.");
            }

            Console.WriteLine();
        }



        //Покажіть Топ-3 команди, які набрали найбільше очок:

        public static void ShowTop3TeamsByPoints(Champion context)
        {
            var topTeamsByPoints = context.Teams
                .OrderByDescending(team => context.Matches
                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                    .Sum(match =>
                        match.LocalTeamId == team.Id
                            ? (match.LocalGoals > match.VisitorGoals ? 3 : match.LocalGoals == match.VisitorGoals ? 1 : 0)
                            : (match.VisitorGoals > match.LocalGoals ? 3 : match.VisitorGoals == match.LocalGoals ? 1 : 0)))
                .Take(3)
                .Select(team => new
                {
                    TeamName = team.TeamName,
                    Points = context.Matches
                        .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                        .Sum(match =>
                            match.LocalTeamId == team.Id
                                ? (match.LocalGoals > match.VisitorGoals ? 3 : match.LocalGoals == match.VisitorGoals ? 1 : 0)
                                : (match.VisitorGoals > match.LocalGoals ? 3 : match.VisitorGoals == match.LocalGoals ? 1 : 0))
                });

            Console.WriteLine($"Топ-3 команди, які набрали найбільше очок:");
            Console.WriteLine(new string('-', 50));

            foreach (var team in topTeamsByPoints)
            {
                Console.WriteLine($"Команда: {team.TeamName}, Очки: {team.Points}");
            }

            Console.WriteLine();
        }
        //Покажіть команду, яка набрала найбільше очок:

        public static void ShowTopTeamByPoints(Champion context)
        {
            var topTeamByPoints = context.Teams
                .OrderByDescending(team => context.Matches
                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                    .Sum(match =>
                        match.LocalTeamId == team.Id
                            ? (match.LocalGoals > match.VisitorGoals ? 3 : match.LocalGoals == match.VisitorGoals ? 1 : 0)
                            : (match.VisitorGoals > match.LocalGoals ? 3 : match.VisitorGoals == match.LocalGoals ? 1 : 0)))
                .FirstOrDefault();

            if (topTeamByPoints != null)
            {
                Console.WriteLine($"Команда, яка набрала найбільше очок:");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Команда: {topTeamByPoints.TeamName}, Очки: {context.Matches.Sum(match => match.LocalTeamId == topTeamByPoints.Id ? (match.LocalGoals > match.VisitorGoals ? 3 : match.LocalGoals == match.VisitorGoals ? 1 : 0) : (match.VisitorGoals > match.LocalGoals ? 3 : match.VisitorGoals == match.LocalGoals ? 1 : 0))}");
            }
            else
            {
                Console.WriteLine("Немає даних про команди, які набрали очки.");
            }

            Console.WriteLine();
        }
       // Покажіть Топ-3 команди, які набрали найменшу кількість очок:

        public static void ShowTop3TeamsByLowestPoints(Champion context)
        {
            var topTeamsByLowestPoints = context.Teams
                .OrderBy(team => context.Matches
                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                    .Sum(match =>
                        match.LocalTeamId == team.Id
                            ? (match.LocalGoals > match.VisitorGoals ? 3 : match.LocalGoals == match.VisitorGoals ? 1 : 0)
                            : (match.VisitorGoals > match.LocalGoals ? 3 : match.VisitorGoals == match.LocalGoals ? 1 : 0)))
                .Take(3)
                .Select(team => new
                {
                    TeamName = team.TeamName,
                    Points = context.Matches
                        .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                        .Sum(match =>
                            match.LocalTeamId == team.Id
                                ? (match.LocalGoals > match.VisitorGoals ? 3 : match.LocalGoals == match.VisitorGoals ? 1 : 0)
                                : (match.VisitorGoals > match.LocalGoals ? 3 : match.VisitorGoals == match.LocalGoals ? 1 : 0))
                });

            Console.WriteLine($"Топ-3 команди, які набрали найменшу кількість очок:");
            Console.WriteLine(new string('-', 50));

            foreach (var team in topTeamsByLowestPoints)
            {
                Console.WriteLine($"Команда: {team.TeamName}, Очки: {team.Points}");
            }

            Console.WriteLine();
        }
       
        
        // Покажіть команду, яка набрала найменшу кількість очок:

        public static void ShowTopTeamByLowestPoints(Champion context)
        {
            var topTeamByLowestPoints = context.Teams
                .OrderBy(team => context.Matches
                    .Where(match => match.LocalTeamId == team.Id || match.VisitorTeamId == team.Id)
                    .Sum(match =>
                        match.LocalTeamId == team.Id
                            ? (match.LocalGoals > match.VisitorGoals ? 3 : match.LocalGoals == match.VisitorGoals ? 1 : 0)
                            : (match.VisitorGoals > match.LocalGoals ? 3 : match.VisitorGoals == match.LocalGoals ? 1 : 0)))
                .FirstOrDefault();

            if (topTeamByLowestPoints != null)
            {
                Console.WriteLine($"Команда, яка набрала найменшу кількість очок:");
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"Команда: {topTeamByLowestPoints.TeamName}, Очки: {context.Matches.Sum(match => match.LocalTeamId == topTeamByLowestPoints.Id ? (match.LocalGoals > match.VisitorGoals ? 3 : match.LocalGoals == match.VisitorGoals ? 1 : 0) : (match.VisitorGoals > match.LocalGoals ? 3 : match.VisitorGoals == match.LocalGoals ? 1 : 0))}");
            }
            else
            {
                Console.WriteLine("Немає даних про команди, які набрали очки.");
            }

            Console.WriteLine();
        }



    }
}
