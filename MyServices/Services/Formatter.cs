using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicService.EnvObjects;
using MyDataBase;
using LogicService.Services;

namespace LogicService.Services
{
    public static class Formatter
    {
        public static String GetPathForType(Type type)
        {
            String res = "";

            if (type == typeof(Good))
            {
                res = "Goods.xml";
            }
            else if (type == typeof(GoodCategory))
            {
                res = "GoodsCategoryes.xml";
            }
            else if (type == typeof(GoodsProvider))
            {
                res = "GoodsProviders.xml";
            }

            return res;
        }

        public static String GetStringToShow(object raw)
        {
            Type otype = raw.GetType();
            String res = "";

            if (otype == typeof(Good))
            {
                Good obj = (Good)raw;
                res = obj.Name.PadRight(FormatContants.GOOD_NAME_LENGTH) 
                    + obj.BrandName.PadRight(FormatContants.GOOD_BRANDNAME_LENGHT)
                    + Convert.ToString(obj.Price).PadRight(FormatContants.GOOD_PRICE_LENGTH)
                    + Convert.ToString(obj.Count).PadRight(FormatContants.GOOD_COUNT_LENGTH);

            }
            if (otype == typeof(GoodCategory))
            {
                GoodCategory obj = (GoodCategory)raw;
                res = obj.Name.PadRight(FormatContants.CATEGORY_NAME_LENGHT)
                    + obj.Description.PadRight(FormatContants.CATEGORY_DESCRIPT_LENGHT);

            }
            if (otype == typeof(GoodsProvider))
            {
                GoodsProvider obj = (GoodsProvider)raw;
                res = obj.FirstName.PadRight(FormatContants.CATEGORY_NAME_LENGHT)
                    + obj.LastName.PadRight(FormatContants.CATEGORY_DESCRIPT_LENGHT);

            }

            return res;
        }

        public static List<KeyValuePair<String, String>> GetKeyValuePairInfo( Type type, Guid guid )
        {
            List<KeyValuePair<String, String>> Result = new List<KeyValuePair<String, String>>();
            DataBase DB = new DataBase();

            object raw = DB.GetItem(GetPathForType(type), type, guid);
            
            if (type == typeof(Good))
            {
                Good obj = (Good)raw;

                Result.Add(new KeyValuePair<string, string>("Name", obj.Name));
                Result.Add(new KeyValuePair<string, string>("BrandName", obj.BrandName));
                Result.Add(new KeyValuePair<string, string>("Count", obj.Count.ToString()));
                Result.Add(new KeyValuePair<string, string>("Price", obj.Price.ToString()));

                String Categories = "";
                if (obj.CategoryesIds != null)
                {
                    foreach (Guid CatId in obj.CategoryesIds)
                    {
                        GoodCategory Category = (GoodCategory)DB.GetItem(GetPathForType(typeof(GoodCategory)), typeof(GoodCategory), CatId);
                        Categories += Category.Name + " ";
                    }
                }

                Result.Add(new KeyValuePair<string, string>("Categories", Categories));
                // categoryes
            }
            else if (type == typeof(GoodCategory))
            {
                GoodCategory obj = (GoodCategory)raw;

                Result.Add(new KeyValuePair<string, string>("Name", obj.Name));
                Result.Add(new KeyValuePair<string, string>("Description", obj.Description));
            }
            else if (type == typeof(GoodsProvider))
            {
                GoodsProvider obj = (GoodsProvider)raw;

                Result.Add(new KeyValuePair<string, string>("FirstName", obj.FirstName));
                Result.Add(new KeyValuePair<string, string>("LastName", obj.LastName));
                String Goods = "";
                if (obj.ProvideGoodsIds != null)
                {
                    foreach (Guid GoodId in obj.ProvideGoodsIds)
                    {
                        Good Good = (Good)DB.GetItem(GetPathForType(typeof(Good)), typeof(Good), GoodId);
                        Goods += Good.Name + " ";
                    }
                }

                Result.Add(new KeyValuePair<string, string>("Goods", Goods));
            }

            return Result;
        }
    }
}
