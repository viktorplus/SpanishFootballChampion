using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpanishFootballChampion
{
    public class Positions
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Positions(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public void Print()
        {
            Console.WriteLine($"Id: {Id}, Name: {Name}");
        }
    }
}
