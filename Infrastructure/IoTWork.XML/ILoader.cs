using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.XML
{
    public interface ILoader
    {
        void SetUri(Uri Uri);

        String Load();
    }
}
