namespace WEATHER.API.Tests.Utilities
{
    internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public TestAsyncEnumerator(IEnumerator<T> inner)
        {
            _enumerator = inner;
        }

        public T Current => _enumerator.Current;

        public ValueTask DisposeAsync() => new(Task.Run(() => _enumerator.Dispose()));

        public ValueTask<bool> MoveNextAsync() => new(_enumerator.MoveNext());
    }
}
