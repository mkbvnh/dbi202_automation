using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{
    class CompareUnitTest
    {
        public bool CompareDictionary<TKey, TValue>(IDictionary<TKey, TValue> x, IDictionary<TKey, TValue> y)
        {
            // early-exit checks
            if (null == y)
                return null == x;
            if (null == x)
                return false;
            if (object.ReferenceEquals(x, y))
                return true;
            if (x.Count != y.Count)
                return false;

            // check keys are the same
            foreach (TKey k in x.Keys)
                if (!y.ContainsKey(k))
                    return false;

            // check values are the same
            foreach (TKey k in x.Keys)
                if (!x[k].Equals(y[k]))
                    return false;
            return true;
        }
    }
}
