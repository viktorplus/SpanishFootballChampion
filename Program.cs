namespace SpanishFootballChampion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateTime dateToPrint = new DateTime(2023, 12, 07);
            using (var db = new Champion())
            {
                while (true)
                {
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1. Показать разницу голов команд");
                    Console.WriteLine("2. Показать все матчи");
                    Console.WriteLine("3. Показать матчи за определенную дату");
                    Console.WriteLine("4. Показать матчи команды");
                    Console.WriteLine("5. Показать игроков, забивших голы за определенную дату");
                    Console.WriteLine("6. Добавить информацию о матче");
                    Console.WriteLine("7. Обновить информацию о матче");
                    Console.WriteLine("8. Удалить матч");
                    Console.WriteLine("9. Добавить случайный матч");
                    Console.WriteLine("10. Топ-3 бомбардиры команды");
                    Console.WriteLine("11. Топ-1 бомбардир команды");
                    Console.WriteLine("12. Топ-3 бомбардиры чемпионата");
                    Console.WriteLine("13. Топ-1 бомбардир чемпионата");
                    Console.WriteLine("14. Топ-3 команды по забитым голам");
                    Console.WriteLine("15. Топ-1 команда по забитым голам");
                    Console.WriteLine("16. Топ-3 команды по пропущенным голам");
                    Console.WriteLine("17. Топ-1 команда по пропущенным голам");
                    Console.WriteLine("18. Топ-3 команды по набранным очкам");
                    Console.WriteLine("19. Топ-1 команда по набранным очкам");
                    Console.WriteLine("20. Топ-3 команды по набранным наименьшим очкам");
                    Console.WriteLine("21. Топ-1 команда по набранным наименьшим очкам");
                    Console.WriteLine("22. Выйти");

                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Linq.ShowGoalDifference(db);
                            break;
                        case "2":
                            Linq.PrintMatches(db);
                            break;
                        case "3":
                            Linq.PrintMatchesOfDay(db, dateToPrint);
                            break;
                        case "4":
                            Linq.PrintMatchesForTeam(db, 1);
                            break;
                        case "5":
                            Linq.PrintScoringPlayersByDate(db, dateToPrint);
                            break;
                        case "6":
                            Linq.AddMatchInfo(db);
                            break;
                        case "7":
                            Linq.UpdateMatchInfo(db);
                            break;
                        case "8":
                            Linq.DeleteMatch(db);
                            break;
                        case "9":
                            L2.AddRandomMatch(db);
                            break;
                        case "10":
                            L2.ShowTop3ScorersForTeam(db, 1);
                            break;
                        case "11":
                            L2.ShowTopScorerForTeam(db, 1);
                            break;
                        case "12":
                            L2.ShowTop3OverallScorers(db);
                            break;
                        case "13":
                            L2.ShowTopOverallScorer(db);
                            break;
                        case "14":
                            L2.ShowTop3TeamsByGoalsScored(db);
                            break;
                        case "15":
                            L2.ShowTopTeamByGoalsScored(db);
                            break;
                        case "16":
                            L2.ShowTop3TeamsByGoalsConceded(db);
                            break;
                        case "17":
                            L2.ShowTopTeamByGoalsConceded(db);
                            break;
                        case "18":
                            L2.ShowTop3TeamsByPoints(db);
                            break;
                        case "19":
                            L2.ShowTopTeamByPoints(db);
                            break;
                        case "20":
                            L2.ShowTop3TeamsByLowestPoints(db);
                            break;
                        case "21":
                            L2.ShowTopTeamByLowestPoints(db);
                            break;
                        case "22":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Пожалуйста, выберите от 1 до 22.");
                            break;
                    }
                }
            }
        }
    }
}
