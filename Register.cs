using System;

namespace Procesor_Intel8086_1
{
    class Register : IRegister
    {
        const string possibleCharacters = "0123456789ABCDEF";

        public readonly Subregister[] Subregisters;

        public string Value { get => GetValue(); set => SetRegister(value); }

        public int Length { get; }

        public string Name { get; }

        public Register(string name, string value, Subregister[] subRegisters)
        {
            Name = name;
            Subregisters = new Subregister[subRegisters.Length];

            for (int i = 0; i < subRegisters.Length; i++)
            {
                Subregisters[i] = subRegisters[i];
                subRegisters[i].ParentRegister = this;
            }

            Length = value.Length;
            Value = value.ToUpper();
        }

        private void SetRegister(string value)
        {
            if (value.Length != Length)
                throw new ArgumentException("Trying to input value of incorrent length into a register.");

            CheckValue(value);

            int subRegisterIndex = 0;
            for (int i = 0; i < value.Length; )
            {
                int subRegisterLength = Subregisters[subRegisterIndex].Length;
                SetSubregister(subRegisterIndex++, value.Substring(i, subRegisterLength));
                i += subRegisterLength;
            }
        }

        public void SetSubregister(int index, string value)
        {
            CheckValue(value);
            Subregisters[index].Value = value.ToUpper();
        }

        public void Reset()
        {
            foreach (var subRegister in Subregisters)
            {
                string newValue = "";
                for (int i = 0; i < subRegister.Length; i++)
                {
                    newValue += GetDefaultCharacter();
                }
                subRegister.Value = newValue;
            }
        }

        public void Random()
        {
            foreach (var subRegister in Subregisters)
            {
                string newValue = "";
                for (int i = 0; i < subRegister.Length; i++)
                {
                    newValue += GetRandomAvaliableCharacter();
                }
                subRegister.Value = newValue;
            }
        }

        private string GetValue()
        {
            string ret = "";

            foreach (var subRegister in Subregisters)
            {
                ret += subRegister.Value;
            }

            return ret;
        }

        private char GetRandomAvaliableCharacter()
        {
            return possibleCharacters[new Random().Next(0, possibleCharacters.Length)];
        }

        private char GetDefaultCharacter()
        {
            return possibleCharacters[0];
        }

        private void CheckValue(string value)
        {
            foreach(char c in value)
            {
                if (!possibleCharacters.Contains(char.ToUpper(c)))
                {
                    throw new ArgumentException("Value is using incorrect characters. Please use only \"0123456789ABCDEF\"");
                }
            }
        }
    }
}
