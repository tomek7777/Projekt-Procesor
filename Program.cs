using System;

namespace Procesor_Intel8086_1
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome in simulator of Intel 8086!");
            Console.WriteLine();
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  exit                           - exit program");
            Console.WriteLine("  reset                          - reset all registers");
            Console.WriteLine("  random                         - randomise all registers");
            Console.WriteLine("  set <register> <value>         - set register to value");
            Console.WriteLine("  mov <register> <register>      - move value of second register ro first register");
            Console.WriteLine("  xchg <register> <register>     - swap value between registers");

            Simulation simulation = new Simulation();
            simulation.Run();
        }
    }
}
