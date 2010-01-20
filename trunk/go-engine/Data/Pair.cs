namespace go_engine.Data
{
    public struct Pair<T1, T2>
    {
        public Pair(T1 first, T2 second) : this()
        {
            First = first;
            Second = second;
        }

        public T1 First { get; private set; }

        public T2 Second { get; private set; }
    }
}