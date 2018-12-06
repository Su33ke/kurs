using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataBase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace MyDataBase.Tests
{
    [TestClass()]
    public class DataBaseTests
    {
        public class TestClass : MyDataBase.Interfaces.Saveable
        {
            public int TestInt { get; set; }
            public String TestString { get; set; }
            public Guid GUID { get; set; }

            public override bool Equals(object obj)
            {
                var InnerObj = obj as TestClass;
                if (InnerObj != null && InnerObj.GUID.Equals(GUID) &&
                    InnerObj.TestInt == TestInt &&
                    InnerObj.TestString == TestString)
                {
                    return true;
                } else
                {
                    return false;
                }
            }
        }

        private void ClearTestDocument(String path)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            fs.SetLength(0);
            fs.Close();
        }

        [TestMethod()]
        public void AddItemandGetItem_TestClassXmlandTestStructureObject_GetItemWithSameGuidMustReturnThisTestStructure()
        {
            DataBase DataBase = new DataBase();
            TestClass TestStructure = new TestClass();
            TestStructure.TestInt = 5;
            TestStructure.TestString = "TestString";
            TestStructure.GUID = Guid.NewGuid();
            ClearTestDocument("TestClass.xml");

            DataBase.AddItem("TestClass.xml", TestStructure);

            TestClass LoadTestClass = (TestClass)DataBase.GetItem("TestClass.xml", typeof(TestClass), TestStructure.GUID);
            if (LoadTestClass == null)
            {
                Assert.Fail();
            }

            Assert.AreEqual(TestStructure, LoadTestClass);

        }

        [TestMethod()]
        public void GetItems_TestClassXmlandTestStructureObject_MustReturnThisTestStructureIn0indexArray()
        {
            DataBase DataBase = new DataBase();
            TestClass TestStructure = new TestClass();
            TestStructure.TestInt = 5;
            TestStructure.TestString = "TestString";
            TestStructure.GUID = Guid.NewGuid();
            ClearTestDocument("TestClass.xml");

            DataBase.AddItem("TestClass.xml", TestStructure);

            TestClass[] LoadTestClass = (TestClass[])DataBase.GetItems("TestClass.xml", typeof(TestClass));
            if (LoadTestClass.Length != 1)
            {
                Assert.Fail();
            }

            Assert.AreEqual(TestStructure, LoadTestClass[0]);

        }

        [TestMethod()]
        public void AddItem_TestClassXmlandTestStructureObjectWithEmptyGuid_GetItemsMustReturnThisTestStructureIn0indexOfArrayWithNotEmptyGuid()
        {
            DataBase DataBase = new DataBase();
            TestClass TestStructure = new TestClass();
            TestStructure.TestInt = 5;
            TestStructure.TestString = "TestString";
            TestStructure.GUID = Guid.Empty;
            ClearTestDocument("TestClass.xml");

            DataBase.AddItem("TestClass.xml", TestStructure);

            TestClass[] LoadTestClass = (TestClass[])DataBase.GetItems("TestClass.xml", typeof(TestClass));
            if (LoadTestClass.Length != 1)
            {
                Assert.Fail();
            }

            if (LoadTestClass[0].TestInt == TestStructure.TestInt &&
                LoadTestClass[0].TestString == TestStructure.TestString &&
                !LoadTestClass[0].GUID.Equals(Guid.Empty))
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.Fail();
            }

        }

        [ExpectedException(typeof(MyDataBase.Exceptions.GuidIsNowUsedException))]
        [TestMethod()]
        public void AddItem_AddTwoItemsWithSameGuid_MustThrowExpection()
        {
            DataBase DataBase = new DataBase();
            TestClass TestStructure = new TestClass();
            TestClass TestStructure2 = new TestClass();
            Guid GuidToAdd = Guid.NewGuid();

            TestStructure.TestInt = 5;
            TestStructure.TestString = "TestString";
            TestStructure.GUID = GuidToAdd;

            TestStructure2.TestInt = 15;
            TestStructure2.TestString = "TestString2";
            TestStructure2.GUID = GuidToAdd;

            ClearTestDocument("TestClass.xml");

            DataBase.AddItem("TestClass.xml", TestStructure);
            DataBase.AddItem("TestClass.xml", TestStructure2);

        }
    }
}
