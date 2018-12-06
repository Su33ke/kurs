using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogicService.EnvObjects;
using System.IO;

namespace LogicService.Tests
{
    [TestClass()]
    public class DataServiceTests
    {
        private void ClearTestDocument(String path)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate);
            fs.SetLength(0);
            fs.Close();
        }

        [TestMethod()]
        public void AddItem_Good_ReturnAddedGuid()
        {
            DataService Service = new DataService();
            Good Good = new Good();
            Good.BrandName = "NameTest";
            Good.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Good.Count = 10;
            Good.Price = 100;
            Good.Name = "TestName";
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(Good)));

            Guid AddedGuid = Service.AddItem(Good);
            if (AddedGuid == null)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetItem_AddItemAndGetItemWithReturnedGuid_ReturnedSameGood()
        {
            DataService Service = new DataService();
            Good Good = new Good();
            Good.BrandName = "NameTest";
            Good.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Good.Count = 10;
            Good.Price = 100;
            Good.Name = "TestName";
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(Good)));

            Guid AddedGuid = Service.AddItem(Good);
            Good LoadedGood = (Good)Service.GetItem(typeof(Good), AddedGuid);

            Assert.AreEqual(Good, LoadedGood);
        }

        [TestMethod()]
        public void GetGood_AddGoodReturnedGuid_ReturnedSameGood()
        {
            DataService Service = new DataService();
            Good Good = new Good();
            Good.BrandName = "NameTest";
            Good.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Good.Count = 10;
            Good.Price = 100;
            Good.Name = "TestName";
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(Good)));

            Guid AddedGuid = Service.AddItem(Good);
            Good LoadedGood = Service.GetGood(AddedGuid);

            Assert.AreEqual(Good, LoadedGood);
        }

        [TestMethod()]
        public void GetGoods_AddTwoGoods_ReturnedAddedGoods()
        {
            DataService Service = new DataService();
            Good Good1 = new Good();
            Good1.BrandName = "NameTest";
            Good1.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Good1.Count = 10;
            Good1.Price = 100;
            Good1.Name = "TestName";

            Good Good2 = new Good();
            Good2.BrandName = "NameTest2";
            Good2.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Good2.Count = 103;
            Good2.Price = 1030;
            Good2.Name = "TestName2";

            ClearTestDocument(Services.Formatter.GetPathForType(typeof(Good)));

            Service.AddItem(Good1);
            Service.AddItem(Good2);
            Good[] LoadedGoods = Service.GetGoods();

            Assert.AreEqual(Good1, LoadedGoods[0]);
            Assert.AreEqual(Good2, LoadedGoods[1]);
        }

        [TestMethod()]
        public void GetGoods_ReadEmptyFile_ReturnEmptyArray()
        {
            //arrange
            DataService Service = new DataService();
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(Good)));

            //act
            Good[] LoadedGoods = Service.GetGoods();

            //assert
            if (!(LoadedGoods.Length == 0))
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetGoods_AddAndSearchGoods_ReturnedGoodsMeetsParameter()
        {
            //arrange
            DataService Service = new DataService();
            Good Good1 = new Good();
            Good1.BrandName = "Brand";
            Good1.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Good1.Count = 10;
            Good1.Price = 100;
            Good1.Name = "TestName";
            Good Good2 = new Good();
            Good2.BrandName = "Barnd";
            Good2.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Good2.Count = 103;
            Good2.Price = 1030;
            Good2.Name = "TestName2";
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(Good)));
            Guid TrueGoodId = Service.AddItem(Good1);
            Service.AddItem(Good2);

            //act
            Good[] LoadedGoods = Service.GetGoods("Brand");


            //assert
            if (LoadedGoods.Length == 1)
            {
                if (!LoadedGoods[0].GUID.Equals(TrueGoodId))
                {
                    Assert.Fail();
                }
            }
            else
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetGoodsCategory_AddGoodsCategoryReturnedGuid_ReturnedSameGoodsCategory()
        {
            DataService Service = new DataService();
            GoodCategory GoodCategory = new GoodCategory();
            GoodCategory.Description = "NameTest";
            GoodCategory.Name = "TestName";
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(GoodCategory)));

            Guid AddedGuid = Service.AddItem(GoodCategory);
            GoodCategory LoadedGoodCategory = Service.GetGoodsCategory(AddedGuid);

            Assert.AreEqual(GoodCategory, LoadedGoodCategory);
        }

        [TestMethod()]
        public void GetGoodsCategories_AddTwoGoodsCategories_ReturnedSameGoodsCategories()
        {
            DataService Service = new DataService();
            GoodCategory GoodCategory1 = new GoodCategory();
            GoodCategory1.Description = "NameTest2";
            GoodCategory1.Name = "TestName2";

            GoodCategory GoodCategory2 = new GoodCategory();
            GoodCategory2.Description = "NameTest2";
            GoodCategory2.Name = "TestName2";

            ClearTestDocument(Services.Formatter.GetPathForType(typeof(GoodCategory)));

            Service.AddItem(GoodCategory1);
            Service.AddItem(GoodCategory2);

            GoodCategory[] LoadedGoodCategories = Service.GetGoodsCategoryes();
            if (LoadedGoodCategories.Length != 2)
            {
                Assert.Fail();
            }

            Assert.AreEqual(LoadedGoodCategories[0], GoodCategory1);
            Assert.AreEqual(LoadedGoodCategories[1], GoodCategory2);
        }

        [TestMethod()]
        public void GetGoodsCategories_ReadEmptyFile_ReturnedSameGoodsCategories()
        {
            DataService Service = new DataService();
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(GoodCategory)));

            GoodCategory[] LoadedGoodCategories = Service.GetGoodsCategoryes();
            if (LoadedGoodCategories.Length != 0)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void GetGoodsProvider_AddGoodsProviderReturnedGuid_ReturnedSameGoodsProvider()
        {
            DataService Service = new DataService();
            GoodsProvider GoodsProvider = new GoodsProvider();
            GoodsProvider.FirstName = "NameTest";
            GoodsProvider.LastName = "TestName";
            GoodsProvider.ProvideGoodsIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(GoodsProvider)));

            Guid AddedGuid = Service.AddItem(GoodsProvider);
            GoodsProvider LoadedGoodsProvider = Service.GetProvider(AddedGuid);

            Assert.AreEqual(GoodsProvider, LoadedGoodsProvider);
        }

        [TestMethod()]
        public void RemoveItem_AddGood_GoodWillRemove()
        {
            //arrange
            DataService Service = new DataService();
            Good Good = new Good();
            Good.BrandName = "NameTest";
            Good.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Good.Count = 10;
            Good.Price = 100;
            Good.Name = "TestName";
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(Good)));
            Guid AddedGuid = Service.AddItem(Good);

            //act
            Service.RemoveItem(typeof(Good), AddedGuid);
            Good LoadedGood = Service.GetGood(AddedGuid);

            //assert
            if (LoadedGood != null)
            {
                Assert.Fail();
            }
        }

        [TestMethod()]
        public void RemoveItem_AddGoodsCategory_GoodsCategoryWillBeRemovedAndHisIdWillBeRemovedFromAllGoods()
        {
            //arrange
            DataService Service = new DataService();
            GoodCategory GoodsCategory = new GoodCategory();

            ClearTestDocument(Services.Formatter.GetPathForType(typeof(Good)));
            ClearTestDocument(Services.Formatter.GetPathForType(typeof(GoodCategory)));
            Guid AddedCategoryGuid = Service.AddItem(GoodsCategory);

            Good Good1 = new Good();
            Good1.CategoryesIds = new Guid[] { Guid.NewGuid(), AddedCategoryGuid };
            Guid Good1AddedGuid = Service.AddItem(Good1);

            Good Good2 = new Good();
            Good2.CategoryesIds = new Guid[] { Guid.NewGuid(), Guid.NewGuid() };
            Guid Good2AddedGuid = Service.AddItem(Good2);

            //act
            Service.RemoveItem(typeof(GoodCategory), AddedCategoryGuid);
            GoodCategory LoadedGoodsCategory = Service.GetGoodsCategory(AddedCategoryGuid);
            Good[] LoadedGoods = Service.GetGoods();


            //assert
            if (LoadedGoodsCategory != null)
            {
                Assert.Fail();
            }
            if (!LoadedGoods[0].GUID.Equals(Good1AddedGuid) ||
                !LoadedGoods[1].GUID.Equals(Good2AddedGuid) ||
                LoadedGoods[0].CategoryesIds.Any(item => item.Equals(AddedCategoryGuid)))
            {
                Assert.Fail();
            }
        }

    }
}
