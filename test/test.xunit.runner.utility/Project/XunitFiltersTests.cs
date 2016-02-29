using Xunit;

public class XunitFiltersTests
{
    public static class ExcludedTraits
    {
        [Fact]
        public static void EmptyFiltersListAlwaysPasses()
        {
            var filters = new XunitFilters();
            var method = Mocks.TestCase<ClassUnderTest>("MethodWithNoTraits");

            var result = filters.Filter(method);

            Assert.True(result);
        }

        [Fact]
        public static void CanFilterItemsByTrait()
        {
            var filters = new XunitFilters();
            var methodWithFooBar = Mocks.TestCase<ClassUnderTest>("MethodWithFooBar");
            var methodWithBazBiff = Mocks.TestCase<ClassUnderTest>("MethodWithBazBiff");
            var methodWithNoTraits = Mocks.TestCase<ClassUnderTest>("MethodWithNoTraits");
            filters.ExcludedTraits.Add("foo", "bar");

            Assert.False(filters.Filter(methodWithFooBar));
            Assert.True(filters.Filter(methodWithBazBiff));
            Assert.True(filters.Filter(methodWithNoTraits));
        }

        [Fact]
        public static void MultipleTraitFiltersAreAnAndOperation()
        {
            var filters = new XunitFilters();
            var methodWithFooBar = Mocks.TestCase<ClassUnderTest>("MethodWithFooBar");
            var methodWithBazBiff = Mocks.TestCase<ClassUnderTest>("MethodWithBazBiff");
            var methodWithNoTraits = Mocks.TestCase<ClassUnderTest>("MethodWithNoTraits");
            filters.ExcludedTraits.Add("fOo", "bAr");
            filters.ExcludedTraits.Add("bAz", "bIff");

            Assert.False(filters.Filter(methodWithFooBar));
            Assert.False(filters.Filter(methodWithBazBiff));
            Assert.True(filters.Filter(methodWithNoTraits));
        }

        class ClassUnderTest
        {
            [Fact]
            public static void MethodWithNoTraits() { }

            [Fact]
            [Trait("foo", "bar")]
            public static void MethodWithFooBar() { }

            [Fact]
            [Trait("baz", "biff")]
            public static void MethodWithBazBiff() { }
        }
    }

    public static class IncludedTraits
    {
        [Fact]
        public static void EmptyFiltersListAlwaysPasses()
        {
            var filters = new XunitFilters();
            var method = Mocks.TestCase<ClassUnderTest>("MethodWithNoTraits");

            var result = filters.Filter(method);

            Assert.True(result);
        }

        [Fact]
        public static void CanFilterItemsByTrait()
        {
            var filters = new XunitFilters();
            var methodWithFooBar = Mocks.TestCase<ClassUnderTest>("MethodWithFooBar");
            var methodWithBazBiff = Mocks.TestCase<ClassUnderTest>("MethodWithBazBiff");
            var methodWithNoTraits = Mocks.TestCase<ClassUnderTest>("MethodWithNoTraits");
            filters.IncludedTraits.Add("foo", "bar");

            Assert.True(filters.Filter(methodWithFooBar));
            Assert.False(filters.Filter(methodWithBazBiff));
            Assert.False(filters.Filter(methodWithNoTraits));
        }

        [Fact]
        public static void MultipleTraitFiltersAreAnAndOperation()
        {
            var filters = new XunitFilters();
            var methodWithFooBar = Mocks.TestCase<ClassUnderTest>("MethodWithFooBar");
            var methodWithBazBiff = Mocks.TestCase<ClassUnderTest>("MethodWithBazBiff");
            var methodWithNoTraits = Mocks.TestCase<ClassUnderTest>("MethodWithNoTraits");
            filters.IncludedTraits.Add("fOo", "bAr");
            filters.IncludedTraits.Add("bAz", "bIff");

            Assert.True(filters.Filter(methodWithFooBar));
            Assert.True(filters.Filter(methodWithBazBiff));
            Assert.False(filters.Filter(methodWithNoTraits));
        }

        class ClassUnderTest
        {
            [Fact]
            public static void MethodWithNoTraits() { }

            [Fact]
            [Trait("foo", "bar")]
            public static void MethodWithFooBar() { }

            [Fact]
            [Trait("baz", "biff")]
            public static void MethodWithBazBiff() { }
        }
    }

    public static class IncludedClasses
    {
        [Fact]
        public static void EmptyFiltersListAlwaysPasses()
        {
            var filters = new XunitFilters();
            var method = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");

            var result = filters.Filter(method);

            Assert.True(result);
        }

        [Fact]
        public static void CanFilterFactsByFullName()
        {
            var filters = new XunitFilters();
            var method1 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method2 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name2");
            var method3 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass2>("Name3");
            filters.IncludedClasses.Add("Namespace1.ClassInNamespace1+InnerClass1");

            Assert.True(filters.Filter(method1));
            Assert.True(filters.Filter(method2));
            Assert.False(filters.Filter(method3));
        }

        [Fact]
        public static void MultipleNameFiltersAreAnOrOperation()
        {
            var filters = new XunitFilters();
            var method1 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method2 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name2");
            var method3 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass2>("Name3");
            filters.IncludedClasses.Add("Namespace1.ClassInNamespace1+InnerClass1");
            filters.IncludedClasses.Add("namespace1.classinnamespace1+InnerClass2");

            Assert.True(filters.Filter(method1));
            Assert.True(filters.Filter(method2));
            Assert.True(filters.Filter(method3));
        }
    }

    public static class IncludedMethods
    {
        [Fact]
        public static void EmptyFiltersListAlwaysPasses()
        {
            var filters = new XunitFilters();
            var method = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");

            var result = filters.Filter(method);

            Assert.True(result);
        }

        [Fact]
        public static void CanFilterFactsByFullName()
        {
            var filters = new XunitFilters();
            var method1 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method2 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name2");
            var method3 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name3");
            filters.IncludedMethods.Add("Namespace1.ClassInNamespace1+InnerClass1.Name1");

            Assert.True(filters.Filter(method1));
            Assert.False(filters.Filter(method2));
            Assert.False(filters.Filter(method3));
        }

        [Fact]
        public static void MultipleNameFiltersAreAnOrOperation()
        {
            var filters = new XunitFilters();
            var method1 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method2 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name2");
            var method3 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name3", displayName: "Namespace1.ClassInNamespace1+InnerClass1.Name1");
            filters.IncludedMethods.Add("Namespace1.ClassInNamespace1+InnerClass1.Name1");
            filters.IncludedMethods.Add("namespace1.classinnamespace1+InnerClass1.name2");

            Assert.True(filters.Filter(method1));
            Assert.True(filters.Filter(method2));
            Assert.False(filters.Filter(method3));
        }
    }

    public static class IncludedClassesAndMethods
    {
        [Fact]
        public static void MultipleNameFiltersAreAnOrOperation()
        {
            var filters = new XunitFilters();
            var method1 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method2 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name2");
            var method3 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass2>("Name3");
            filters.IncludedMethods.Add("Namespace1.ClassInNamespace1+InnerClass1.Name2");
            filters.IncludedClasses.Add("namespace1.classinnamespace1+InnerClass2");

            Assert.False(filters.Filter(method1));
            Assert.True(filters.Filter(method2));
            Assert.True(filters.Filter(method3));
        }
    }

    public static class IncludedNameSpaces
    {
        [Fact]
        public static void CanFilterFactsByNameSpace()
        {
            var filters = new XunitFilters();
            var method11 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method21 = Mocks.TestCase<Namespace2.ClassInNamespace2.InnerClass1>("Name1");
            filters.IncludedNameSpaces.Add("Namespace1");

            Assert.True(filters.Filter(method11));
            Assert.False(filters.Filter(method21));
        }

        [Fact]
        public static void MultipleFiltersAreAnOrOperation()
        {
            var filters = new XunitFilters();
            var method11 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method21 = Mocks.TestCase<Namespace2.ClassInNamespace2.InnerClass1>("Name1");
            var method31 = Mocks.TestCase<Namespace3.ClassInNamespace3.InnerClass1>("Name1");
            filters.IncludedNameSpaces.Add("Namespace1");
            filters.IncludedNameSpaces.Add("Namespace2");

            Assert.True(filters.Filter(method11));
            Assert.True(filters.Filter(method21));
            Assert.False(filters.Filter(method31));
        }
    }

    public static class ExcludedNameSpaces
    {
        [Fact]
        public static void CanFilterFactsByNameSpace()
        {
            var filters = new XunitFilters();
            var method11 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method21 = Mocks.TestCase<Namespace2.ClassInNamespace2.InnerClass1>("Name1");
            filters.ExcludedNameSpaces.Add("Namespace1");

            Assert.False(filters.Filter(method11));
            Assert.True(filters.Filter(method21));
        }

        [Fact]
        public static void MultipleFiltersAreAnAndOperation()
        {
            var filters = new XunitFilters();
            var method11 = Mocks.TestCase<Namespace1.ClassInNamespace1.InnerClass1>("Name1");
            var method21 = Mocks.TestCase<Namespace2.ClassInNamespace2.InnerClass1>("Name1");
            var method31 = Mocks.TestCase<Namespace3.ClassInNamespace3.InnerClass1>("Name1");
            filters.ExcludedNameSpaces.Add("Namespace1");
            filters.ExcludedNameSpaces.Add("Namespace2");

            Assert.False(filters.Filter(method11));
            Assert.False(filters.Filter(method21));
            Assert.True(filters.Filter(method31));
        }
    }
}

namespace Namespace1
{
    public class ClassInNamespace1
    {
        public class InnerClass1
        {
            [Fact]
            public static void Name1() { }

            [Fact]
            public static void Name2() { }

            [Fact]
            public static void Name3() { }
        }

        public class InnerClass2
        {
            [Fact]
            public static void Name3() { }
        }
    }
}

namespace Namespace2
{
    public class ClassInNamespace2
    {
        public class InnerClass1
        {
            [Fact]
            public static void Name1() { }
        }
    }
}

namespace Namespace3
{
    public class ClassInNamespace3
    {
        public class InnerClass1
        {
            [Fact]
            public static void Name1() { }
        }
    }
}
