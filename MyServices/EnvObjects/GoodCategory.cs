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
    public class GoodCategory : Saveable
    {
        public Guid GUID { get; set; }
        private String name = "None";
        private String description = "None";

        public String Name {
            get
            {
                return name;
            }
            set
            {
                if (value.Length > FormatContants.CATEGORY_NAME_LENGHT)
                {
                    throw new Exception("Не больше " +
                                        FormatContants.CATEGORY_NAME_LENGHT.ToString() +
                                        " символов.");
                }
                name = Regex.IsMatch(value, "^[A-za-zа-яА-ЯёЁ0-9 ]+$") ? value : throw new Exception("Неверный формат имени.");
            }
        }

        public String Description
        {
            get
            {
                return description;
            }
            set
            {
                if (value.Length > FormatContants.CATEGORY_DESCRIPT_LENGHT)
                {
                    throw new Exception("Не больше " +
                                        FormatContants.CATEGORY_DESCRIPT_LENGHT.ToString() +
                                        " символов.");
                }
                description = Regex.IsMatch(value, "^[A-za-zа-яА-ЯёЁ 0-9]+$") ? value : throw new Exception("Неверный формат описания.");
            }
        }

        public override bool Equals(object other)
        {
            GoodCategory obj = other as GoodCategory;

            if (obj.Description == Description &&
                obj.Name == Name &&
                obj.GUID.Equals(GUID))
            {
                return true;
            }

            return false;


        }
    }
}
