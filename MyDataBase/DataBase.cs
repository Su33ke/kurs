using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using MyDataBase.Interfaces;
using MyDataBase.Exceptions;

namespace MyDataBase
{
    public class DataBase
    {
        private void InitializeProvider(out DataProvider provider, String path, Type otype)
        {
            Type t = otype.MakeArrayType();
            String format = path.Split(new char[] { '.' })[1];

            Type ct = null;
            switch (format)
            {
                case "xml":
                    ct = typeof(XmlProvider);
                    break;
                //case "json":
                //    ct = typeof(JsonProvider);
                //    break;
                //case "bin":
                //    ct = typeof(BinaryProvider);
                //    break;
            }

            if (ct != null)
            {
                DataProvider instance = (DataProvider)Activator.CreateInstance(ct, new object[] { path, t});
                provider = instance;
            }
            else
            {
                provider = null;
            }
        }

        public object[] GetItems(String path, Type type)
        {
            Type otype = type;
            DataProvider provider;
            InitializeProvider(out provider, path, type);

            return GetItems(provider);
        }

        private object[] GetItems(DataProvider Provider)
        {
            object[] loads;

            try
            {
                loads = (object[])Provider.Load();
            }
            catch (Exception e)
            {
                loads = new object[0];
            }

            return loads;
        }

        public object GetItem(String Path, Type Type, Guid Guid)
        {
            DataProvider provider;
            InitializeProvider(out provider, Path, Type);

            object[] loads = GetItems(provider);
            object[] finds = Array.FindAll(loads, val => ((Saveable)val).GUID.Equals(Guid));

            if (finds.Length > 1)
            {
                throw new MoreThanOneItemWithThisId();
            }

            if (finds.Length == 1)
            {
                return finds[0];
            } else
            {
                return null;
            }
        }

        public void UpdateItem(String Path, object obj)
        {
            if (((Saveable)obj).GUID.Equals(Guid.Empty))
            {
                throw new NullGuidNotExpectException();
            }

            Type otype = obj.GetType();
            var ct = obj.GetType().MakeArrayType();
            DataProvider provider;
            InitializeProvider(out provider, Path, otype);

            object[] loads = GetItems(provider);

            int index = 0;

            foreach (Saveable load in loads)
            {
                if (load.GUID.Equals(((Saveable)obj).GUID))
                {
                    loads[index] = obj;
                    provider.Save(loads);
                    return;
                }
                index++;
            }

            throw new ObjectWithThisGuidNotContain();

        }

        public void RemoveItem(String Path, Type Type, Guid Guid)
        {
            DataProvider provider;
            InitializeProvider(out provider, Path, Type);

            object[] loads = GetItems(provider);
            object[] finds = Array.FindAll(loads, val => !((Saveable)val).GUID.Equals(Guid));
            if ( finds.Length == loads.Length )
            {
                throw new ObjectWithThisGuidNotContain();
            }

            Array result = (Array)Activator.CreateInstance(Type.MakeArrayType(), new object[] { finds.Length });
            Array.Copy(finds, result, finds.Length);

            provider.Save(result);
        }

        public Guid AddItem(String path, object obj)
        {
            if (((Saveable)obj).GUID.Equals(Guid.Empty))
            {
                ((Saveable)obj).GUID = Guid.NewGuid();
            }

            Type otype = obj.GetType();
            var ct = obj.GetType().MakeArrayType();
            DataProvider provider;
            InitializeProvider(out provider, path, otype);

            object[] loads = GetItems(provider);

            object[] DupliatesByGuid = Array.FindAll(loads, val => ((Saveable)val).GUID.Equals(((Saveable)obj).GUID));
            if (DupliatesByGuid.Length > 0)
            {
                throw new GuidIsNowUsedException();
            }

            Array.Resize(ref loads, loads.Length + 1);
            loads[loads.Length - 1] = obj;

            Array result = (Array)Activator.CreateInstance(ct, new object[] { loads.Length });
            Array.Copy(loads, result, loads.Length);

            provider.Save(result);

            return ((Saveable)obj).GUID;

        }
    }
}
