using System;
using System.Collections.Generic;
using System.Text;

namespace ComparableEquatableBase.Tests
{
    public class SomestuffBroken : ISomestuff
    {
        public int A { get; set; }
        public int B { get; set; }

        public int Expanded => A * 10 + B;
    }
}
