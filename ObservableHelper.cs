using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reactive.Linq;

namespace BasicRxSearch
{
    public class ObservableHelper<T> : IObservableHelper<T, Database1Entities>
        where T : class //or EntityObject 
    {
        public ObservableHelper()
        {
            _dat = new Database1Entities();
        }
        Database1Entities _dat;
        public IObservable<IList<T>> GetAllAsObservables(Func<Database1Entities, IQueryable<T>> funcquery)
        {
            var getall = Observable.ToAsync<Database1Entities, IQueryable<T>>(funcquery);
            return getall(_dat).Select(x => x.ToList());
        }
    }
}
