namespace Procesor_Intel8086_1
{
    interface IRegister
    {
        public string Name { get; }
        public string Value { get; set; }
        public int Length { get; }
    }
}
