using System;
using System.Collections.Generic;
using System.Text;

namespace ComparableEquatableBase.Tests
{
    public class Somestuff : ComparableEquatableBase<Somestuff, (int, int)>, ISomestuff
    {
        protected override (int, int) GetComparableEquatableAdaptation()
        {
            return (A, B);
        }

        public int A { get; set; }
        public int B { get; set; }

        public int Expanded => A * 10 + B;

    }
}
