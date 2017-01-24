using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.Infrastructure.Caching
{
    public interface ICacheDirectory
    {
        String[] KeysByPrefix(String Prefix);

        String[] KeysBySuffix(String Suffix);

        String[] KeysByValue(String Value);
    }
}
