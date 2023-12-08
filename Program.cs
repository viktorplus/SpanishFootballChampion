namespace SpanishFootballChampion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new Champion())
            {
                string choice = Console.ReadLine(); 
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Показать разницу голов команд");
                Console.WriteLine("2. Показать все матчи");
                Console.WriteLine("3. Показать матчи за определенную дату");
                Console.WriteLine("4. Показать всех игроков");

                switch (choice)
                {
                    case "1":
                        Linq.ShowGoalDifference(db);
                        break;
                    case "2":
                        Linq.PrintMatches(db);

                        break;
                    case "3":
                        //ShowMatchOfData(db);
                        break;
                    case "4":
                        //PrintAll(db);
                        break;
                    case "5":
                        //PrintStudios(db);
                        break;
                    case "6":
                        Linq.GetPlayersFromTeamWithId1(db, 1);
                        break;
                    case "7":
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
