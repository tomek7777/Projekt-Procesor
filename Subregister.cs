using System;

namespace Procesor_Intel8086_1
{
    class Subregister : IRegister
    {
        const string possibleCharacters = "ABCDEF1234567890";

        private string name;
        private string _value;

        public Register ParentRegister { get; set; }

        public string Value { get => _value; set { CheckValue(value); _value = value.ToUpper(); } }

        public int Length { get; }

        public string Name { get => ParentRegister.Name + name; set => name = value; }

        public Subregister(string name, int length)
        {
            Name = name;
            Length = length;
        }


        private void CheckValue(string value)
        {
            if(value.Length != Length)
            {
                throw new ArgumentException("Trying to input value of incorrent length into a register.");
            }
            foreach (char c in value)
            {
                if (!possibleCharacters.Contains(char.ToUpper(c)))
                {
                    throw new ArgumentException("Value is using incorrect characters. Please use only \"0123456789ABCDEF\"");
                }
            }
        }
    }
}
