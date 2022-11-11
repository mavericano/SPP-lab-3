using System.Collections.Generic;
using AssemblyBrowser;
using NUnit.Framework;

namespace TestProject1
{
    [TestFixture]
    public class Tests
    {
        const string Path = "C:\\Users\\gavri\\RiderProjects\\WpfApplication1\\TestClass\\bin\\Debug\\TestClass.dll";
        private readonly List<AssemblyData> _typeList;
        
        public Tests()
        {
            _typeList = new ReaderImpl().GetAssemblyData(Path);
        }
    
        [Test]
        public void TestNamespacesCount()
        {
            int result = 0;
            foreach (var type in _typeList)
            {
                if (type.nameSpace != "")
                    result++;
            }
            Assert.AreEqual(result, 3);
        }

        [Test]
        public void TestMethodCountClassB()
        {
            Assert.AreEqual(_typeList[1].methods.Count, 2);
        }
        
        [Test]
        public void TestPropertiesCountClassC()
        {
            Assert.AreEqual(_typeList[2].properties.Count, 2);
        }
        
        [Test]
        public void TestFieldsCountClassA()
        {
            Assert.AreEqual(_typeList[0].fields.Count, 4);
        }
    }
}