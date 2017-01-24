using IoTWork.Infrastructure.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Formatters
{
    public class JSonFormatter<T> : IFormatter<T>
    {
        public byte[] Format(T data)
        {
            string json = JsonConvert.SerializeObject(data);
            var bytes = Encoding.UTF8.GetBytes(json);
            return bytes;
        }

        public T Unformat(byte[] data)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Binder = new CustomAssemblyNameAndTypeNameBinder()
            };
            string json = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(json, settings);
        }

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
                        if (ass.FullName.Split(',')[0].Replace(".mono","") == toassname)
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
