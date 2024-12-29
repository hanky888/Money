using Money.Models;

namespace Money_Test.Controllers
{
    public class MockAsyncEnumerator<T> : IAsyncEnumerator<Currency>
    {
        private readonly IEnumerator<T> _inner;

        public MockAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public ValueTask DisposeAsync()
        {
            _inner.Dispose();
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new ValueTask<bool>(_inner.MoveNext());
        }

        public T Current => _inner.Current;

        Currency IAsyncEnumerator<Currency>.Current => throw new NotImplementedException();
    }
}