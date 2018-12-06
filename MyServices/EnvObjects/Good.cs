using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDataBase.Interfaces;
using System.Text.RegularExpressions;
using LogicService.Services;

namespace LogicService.EnvObjects
{
    public class Good : Saveable
    {
        public Guid GUID { get; set; }
        private String name = "None";
        private String brandName = "None";
        public float Price { get; set; }
        public float Count { get; set; }
        public Guid[] CategoryesIds { get; set; } = null;

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                if (value.Length > FormatContants.GOOD_NAME_LENGTH) {
                    throw new Exception("Не больше " +
                                        FormatContants.GOOD_NAME_LENGTH.ToString() +
                                        " символов.");
                }
                name = Regex.IsMatch(value, "^[A-za-zа-яА-ЯёЁ 0-9]+$") ? value : throw new Exception("Неверный формат имени.");
            }
        }

        public String BrandName
        {
            get
            {
                return brandName;
            }
            set
            {
                if (value.Length > FormatContants.GOOD_BRANDNAME_LENGHT)
                {
                    throw new Exception("Не больше " +
                                        FormatContants.GOOD_BRANDNAME_LENGHT.ToString() +
                                        " символов.");
                }
                brandName = Regex.IsMatch(value, "^[A-za-zа-яА-ЯёЁ 0-9]+$") ?  value : throw new Exception("Неверный формат имени.");
            }
        }

        public void SetPrice(String price)
        {
            if (price.Length > FormatContants.GOOD_PRICE_LENGTH)
            {
                throw new Exception("Не больше " +
                                    FormatContants.GOOD_PRICE_LENGTH.ToString() +
                                    " символов.");
            }
            try
            {
                Price = (float)Convert.ToDouble(price);
            }
            catch (Exception e)
            {
                throw new Exception("Неверный формат цены.");
            }
        }

        public void SetCount(String count)
        {
            if (count.Length > FormatContants.GOOD_COUNT_LENGTH)
            {
                throw new Exception("Не больше " + FormatContants.GOOD_COUNT_LENGTH.ToString() +
                                    " символов.");
            }
            try
            {
                Count = (float)Convert.ToDouble(count);
            }
            catch (Exception e)
            {
                throw new Exception("Неверный формат количества.");
            }
        }


        public override bool Equals(object other)
        {
            Good obj = other as Good;

            if (obj.BrandName == BrandName &&
                obj.Name == Name &&
                obj.Price == Price &&
                obj.Count == Count &&
                obj.CategoryesIds.SequenceEqual(CategoryesIds) &&
                obj.GUID.Equals(GUID))
            {
                return true;
            }

            return false;


        }

        public void RemoveCategory(Guid CategoryId)
        {
            List<Guid> Guids = CategoryesIds.ToList();
            foreach (Guid Guid in new List<Guid>(Guids))
            {
                if (Guid.Equals(CategoryId))
                {
                    Guids.Remove(Guid);
                }
            }
            CategoryesIds = Guids.ToArray();
        }

    }
}
