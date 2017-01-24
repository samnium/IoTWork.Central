using IoTWork.Infrastructure.Formatters;
using IoTWork.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Formatters
{
    public class BinaryFormatter<T> : IFormatter<T>
    {
        public byte[] Format(T data)
        {
            byte[] buffer = null;
            object o = (T)data;
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;
                formatter.Serialize(ms, o);
                buffer = ms.ToArray();
            }
            return buffer;
        }

        public T Unformat(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                ms.Seek(0, SeekOrigin.Begin);

                formatter.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple;

                formatter.Binder = new CustomAssemblyNameAndTypeNameBinder(); 

                object o = formatter.Deserialize(ms);
                return (T)o;
            }
        }

        // http://stackoverflow.com/questions/8183787/how-to-serialize-deserialize-an-object-loaded-from-another-assembly

        internal sealed class CustomAssemblyNameAndTypeNameBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                Type ttd = null;
                try
                {
                    string toassname = assemblyName.Split(',')[0];
                    Assembly[] asmblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (Assembly ass in asmblies)
                    {
                        if (ass.FullName.Split(',')[0] == toassname)
                        {
                            ttd = ass.GetType(typeName);
                            break;
                        }
                    }
                }
                catch (System.Exception)
                {
                }
                return ttd;
            }
        }
    }
}
