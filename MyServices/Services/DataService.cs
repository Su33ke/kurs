using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataBase;
using LogicService.EnvObjects;
using LogicService.Services;

namespace LogicService
{
    public class DataService
    {
        DataBase DataBase;

        public DataService()
        {
            DataBase = new DataBase();
        }

        public void RemoveItem(Type ObjectType, Guid ObjectId)
        {
            DataBase.RemoveItem(Formatter.GetPathForType(ObjectType), ObjectType, ObjectId);

            if (ObjectType == typeof(GoodCategory))
            {
                Good[] AllGoods = GetGoods();
                foreach( Good good in AllGoods)
                {
                    if (good.CategoryesIds.Any(item => item.Equals(ObjectId)))
                    {
                        good.RemoveCategory(ObjectId);
                        UpdateItem(good);
                    }
                }
            }
        }

        public object GetItem(Type ObjectType, Guid ObjectId)
        {
            return DataBase.GetItem(Formatter.GetPathForType(ObjectType), ObjectType, ObjectId);
        }


        public GoodsProvider GetProvider(Guid ObjectId)
        {
            return (GoodsProvider)DataBase.GetItem(Formatter.GetPathForType(typeof(GoodsProvider)), typeof(GoodsProvider), ObjectId);
        }

        public GoodsProvider[] GetProviders(String FilterKeyword = null)
        {
            object[] loads = DataBase.GetItems(Formatter.GetPathForType(typeof(GoodsProvider)), typeof(GoodsProvider));
            if (loads.Length > 0)
            {
                if (FilterKeyword != null)
                {
                    loads = Array.FindAll((GoodsProvider[])loads, (item) => {
                        if (item.FirstName == FilterKeyword || item.LastName == FilterKeyword)
                        {
                            return true;
                        }
                        return false;
                    });
                }
                return (GoodsProvider[])loads;
            }
            return new GoodsProvider[0];
        }

        public Good GetGood(Guid ObjectId)
        {
            return (Good)DataBase.GetItem(Formatter.GetPathForType(typeof(Good)), typeof(Good), ObjectId);
        }

        public void SetGoodCategories(Guid GoodId, Guid[] CategoriesIds)
        {
            Good obj = GetGood(GoodId);
            obj.CategoryesIds = CategoriesIds;
            UpdateItem(obj);
        }

        public void SetProvideGoods(Guid ProviderId, Guid[] GoodsIds)
        {
            GoodsProvider obj = GetProvider(ProviderId);
            obj.ProvideGoodsIds = GoodsIds;
            UpdateItem(obj);
        }

        public GoodCategory GetGoodsCategory(Guid ObjectId)
        {
            return (GoodCategory)DataBase.GetItem(Formatter.GetPathForType(typeof(GoodCategory)), typeof(GoodCategory), ObjectId);
        }

        public GoodCategory[] GetGoodsCategoryes()
        {
            object[] loads = DataBase.GetItems(Formatter.GetPathForType(typeof(GoodCategory)), typeof(GoodCategory));
            if (loads.Length > 0)
            {
                return (GoodCategory[])loads;
            }
            return new GoodCategory[0];
        }


        

        public Good[] GetGoods(String FilterKeyword = null)
        {
            object[] loads = DataBase.GetItems(Formatter.GetPathForType(typeof(Good)), typeof(Good));
            if (loads.Length > 0)
            {
                if (FilterKeyword != null)
                {
                    loads = Array.FindAll((Good[])loads, (item) => {
                        if (item.BrandName == FilterKeyword || item.Name == FilterKeyword)
                        {
                            return true;
                        }
                        return false;
                    });
                }
                return (Good[])loads;
            }

            return new Good[0];
        }

        public void UpdateItem(object obj)
        {
            Type otype = obj.GetType();
            DataBase.UpdateItem(Formatter.GetPathForType(otype), obj);
        }

        public Guid AddItem(object obj)
        {
            return DataBase.AddItem(Formatter.GetPathForType(obj.GetType()), obj);
        }

    }
}
