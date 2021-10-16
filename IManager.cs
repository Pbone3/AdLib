namespace AdLib
{
    public interface IManager<T>
    {
        public void OnGainControl(T previous) { }
        public void OnLooseControl(T newManager) { }
    }
}
