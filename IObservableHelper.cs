using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Reactive.Linq;

namespace BasicRxSearch
{
    public interface IObservableHelper<T, TContext>
        where T : class
        where TContext : ObjectContext
    {
        IObservable<IList<T>> GetAllAsObservables(Func<TContext, IQueryable<T>> funcquery);
    }
}
