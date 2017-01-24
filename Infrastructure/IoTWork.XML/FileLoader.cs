using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTWork.XML
{
    public class FileLoader : ILoader
    {
        Uri Uri;

        public string Load()
        {
            String content = String.Empty;

            try
            {   using (StreamReader sr = new StreamReader(Uri.LocalPath))
                {
                    content = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                content = String.Empty;
            }

            return content;
        }

        public void SetUri(Uri Uri)
        {
            this.Uri = Uri;
        }
    }
}
