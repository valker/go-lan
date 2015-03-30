namespace Valker.PlayOnLan.GoPlugin
{
    public struct Pair<T1, T2>
    {
        public Pair(T1 first, T2 second)
            : this()
        {
            Item1 = first;
            Item2 = second;
        }

        public T1 Item1 { get; private set; }

        public T2 Item2 { get; private set; }

    }
}