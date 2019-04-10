﻿using System.Collections.Generic;

namespace UnitTest.UnitTestBase
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