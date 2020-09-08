using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Foo, string> keyValuePairs = new Dictionary<Foo, string>(30, new FooEqualityComparer());
            Foo foo1 = new Foo();
            foo1.FooId = 1;
            foo1.FooName = "foo1";
            foo1.FooDescription = "foo1descr";
            Foo foo2 = new Foo();
            foo2.FooId = 2;
            foo2.FooName = "foo2";
            foo2.FooDescription = "foo2descr";
            keyValuePairs.Add(foo1, "foo100");
            keyValuePairs.Add(foo2, "foo200");

            // here we are updating the existing obj properties which has been added as a key to the dictionary already
            foo1.FooId = 3;
            foo1.FooName = "foo3";

            // here in below line when we try to access the value of that key, it will call GetHashCode(), 
            // this time the hashcode value for the modified foo1 object will be different from the hashcode it has been generated last time while adding the object as key
            // it will throw key not found exception

            var obj = keyValuePairs[foo1];
        }
    }

    class Foo
    {
        public int FooId { get; set; }
        public string FooName { get; set; }
        public string FooDescription { get; set; }
    }

    class FooEqualityComparer : IEqualityComparer<Foo>
    {
        public bool Equals([AllowNull] Foo x, [AllowNull] Foo y)
        {
            return x.FooId == y.FooId && y.FooName == x.FooName;
        }

        public int GetHashCode([DisallowNull] Foo obj)
        {
            return new { obj.FooId, obj.FooName }.GetHashCode();
        }
    }
}
