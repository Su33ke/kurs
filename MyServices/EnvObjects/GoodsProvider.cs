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
    public class GoodsProvider : Saveable
    {
        public Guid GUID { get; set; }
        private String firstName = "None";
        private String lastName = "None";
        public Guid[] ProvideGoodsIds { get; set; } = null;

        public String FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (value.Length > FormatContants.PROVIDER_FIRSTNAME_LENGHT)
                {
                    throw new Exception("Не больше " +
                                        FormatContants.PROVIDER_FIRSTNAME_LENGHT.ToString() +
                                        " символов.");
                }
                firstName = Regex.IsMatch(value, "^[A-za-zа-яА-ЯёЁ0-9]+$") ? value : throw new Exception("Неверный формат имени.");
            }
        }

        public String LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (value.Length > FormatContants.PROVIDER_LASTNAME_LENGHT)
                {
                    throw new Exception("Не больше " +
                                        FormatContants.PROVIDER_LASTNAME_LENGHT.ToString() +
                                        " символов.");
                }
                lastName = Regex.IsMatch(value, "^[A-za-zа-яА-ЯёЁ0-9]+$") ? value : throw new Exception("Неверный формат фамилии.");
            }
        }

        public override bool Equals(object other)
        {
            GoodsProvider obj = other as GoodsProvider;

            if (obj.FirstName == FirstName &&
                obj.LastName == LastName &&
                obj.ProvideGoodsIds.SequenceEqual(ProvideGoodsIds) &&
                obj.GUID.Equals(GUID))
            {
                return true;
            }

            return false;


        }
    }
}
