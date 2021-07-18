namespace AdLib.DataStructures
{
    public struct Stat
    {
        public float Get => (Base + Add) * Multiplier;

        public float Base;
        public float Add;
        public float Multiplier;

        public void Reset()
        {
            Add = 0f;
            Multiplier = 1f;
        }
    }
}
