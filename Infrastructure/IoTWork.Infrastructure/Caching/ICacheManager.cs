using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Caching
{
    public interface ICacheManager
    {
        void Connect(String Name, TimeSpan? DefaultTimeout = null);

        void Set(String Key, object obj);

        void Set(String Key, object obj, TimeSpan Timeout);

        void Delete(String Key);

        object Get(String Key);

        object GetAndDelete(String Key);
    }
}
