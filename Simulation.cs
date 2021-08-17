using System;
using System.Collections.Generic;

namespace Procesor_Intel8086_1
{
    class Simulation
    {
        List<Register> parentRegisters = new List<Register>();
        List<IRegister> registers = new List<IRegister>();

        public void Run()
        {
            #region Initialize registers
            AddRegister(
                new Register(
                        "A",
                        "0000",
                        new Subregister[] { new Subregister("H", 2), new Subregister("L", 2) }
                    ));
            AddRegister(
                new Register(
                        "B",
                        "0000",
                        new Subregister[] { new Subregister("H", 2), new Subregister("L", 2) }
                    ));
            AddRegister(
                new Register(
                        "C",
                        "0000",
                        new Subregister[] { new Subregister("H", 2), new Subregister("L", 2) }
                    ));
            AddRegister(
                new Register(
                        "D",
                        "0000",
                        new Subregister[] { new Subregister("H", 2), new Subregister("L", 2) }
                    ));

            #endregion

            bool run = true;
            while (run)
            {
                DisplayRegisters();

                Console.WriteLine();
                Console.Write("> ");
                string input = Console.ReadLine().ToLower().Trim();
                Console.WriteLine();
                string[] words = input.Split(" ");

                try
                {
                    switch (words[0])
                    {
                        case "exit":
                            run = false;
                            break;
                        case "reset":
                            Reset();
                            break;
                        case "random":
                            Random();
                            break;
                        case "set":
                            SetRegister(words);
                            break;
                        case "mov":
                            Move(words);
                            break;
                        case "xchg":
                            Swap(words);
                            break;
                        default:
                            throw new ArgumentException("Please input valid command");
                    }
                } catch(Exception ex)
                {
                    ErrorMessage(ex);
                }
            
            }
        }

        private void ErrorMessage(Exception ex)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine(ex.Message);

            Console.ForegroundColor = color;
        }

        private void AddRegister(Register register)
        {
            parentRegisters.Add(register);
            registers.Add(register);
            registers.AddRange(register.Subregisters);
        }

        private void DisplayRegisters()
        {
            Console.WriteLine();
            foreach (var register in parentRegisters)
            {
                Console.Write($"| {register.Name}X = {register.Value} \t");
                foreach(Subregister subregister in register.Subregisters)
                {
                    Console.Write($"{subregister.Name} = {subregister.Value} | ");
                }
                Console.WriteLine();
            }
        }

        private IRegister GetRegister(string name, bool throwException = true)
        {
            IRegister register = registers.Find(x => x.Name.ToLower() == name.ToLower()
                                    || x is Register && x.Name.ToLower() + "x" == name.ToLower());

            if (throwException && register == null)
                throw new ArgumentException($"There is no register with name: \"{name}\"");

            return register;
        }

        #region Commands

        private void Swap(string[] words)
        {
            if (words.Length < 3)
                throw new ArgumentException("Too few arguments.");

            IRegister registerA = GetRegister(words[1]);
            IRegister registerB = GetRegister(words[2]);

            if (registerA.Length != registerB.Length)
                throw new ArgumentException("Please select registers of the same length.");

            string temp = registerA.Value;
            registerA.Value = registerB.Value;
            registerB.Value = temp;
        }

        private void Move(string[] words)
        {
            if (words.Length < 3)
                throw new ArgumentException("Too few arguments.");

            IRegister registerA = GetRegister(words[1]);
            IRegister registerB = GetRegister(words[2]);

            if(registerA.Length != registerB.Length)
                throw new ArgumentException("Please select registers of the same length.");

            registerA.Value = registerB.Value;
        }

        private void SetRegister(string[] words)
        {
            if (words.Length < 3)
                throw new ArgumentException("Too few arguments.");

            IRegister register = GetRegister(words[1]);

            register.Value = words[2];
        }

        private void Random()
        {
            foreach (Register register in parentRegisters)
            {
                register.Random();
            }
        }

        private void Reset()
        {
            foreach(Register register in parentRegisters)
            {
                register.Reset();
            }
        }

        #endregion
    }
}
