using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace WEATHER.API.Tests.Utilities
{
    internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider _innerQueryProvider;

        internal TestAsyncQueryProvider(IQueryProvider inner)
        {
            _innerQueryProvider = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression) => _innerQueryProvider.Execute(expression);

        public TResult Execute<TResult>(Expression expression) => _innerQueryProvider.Execute<TResult>(expression);

        TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            Type expectedResultType = typeof(TResult).GetGenericArguments()[0];
            object? executionResult = ((IQueryProvider)this).Execute(expression);

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                .MakeGenericMethod(expectedResultType)
                .Invoke(null, new[] { executionResult });
        }
    }
}
