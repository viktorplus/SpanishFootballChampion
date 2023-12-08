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
                            Linq.PrintMatchesForTeam(db,1);
                            break;
                        case "5":
                            Linq.PrintScoringPlayersByDate(db, dateToPrint);
                            break;
                        case "6":

                            break;
                        case "7":
                            
                            break;
                        case "8":

                            break;
                        case "9":

                            break;
                        case "10":

                            break;
                        case "11":

                            break;
                        case "12":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Неверный выбор. Пожалуйста, выберите от 1 до 7.");
                            break;
                    }
                    
                }
            }
        }
    }
}
