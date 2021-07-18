namespace AdLib.DataStructures
{
    public struct StatModifier
    {
        public float Add;
        public float Multiplier;

        public StatModifier(float add, float multiply)
        {
            Add = add;
            Multiplier = multiply;
        }

        public void Apply(Stat stat)
        {
            stat.Add += Add;
            stat.Multiplier += Multiplier;
        }
    }
}
