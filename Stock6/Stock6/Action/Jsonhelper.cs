using Newtonsoft.Json.Linq;
using Stock6.apiHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock6.action
{
   public class Jsonhelper
    {
        public static string[] JsonToString(string content)
        {

            string[] results = null;
            try
            {
                InvokeHelper.Login();
                string result = InvokeHelper.ExecuteBillQuery(content);
                //JArray array = new JArray(result);
                if (result == "[]" || result == "err") return results;
                result = result.Substring(0, result.Length - 1);
                result = result.Substring(1, result.Length - 1);
                result = result.Replace("\"", "");
                results = result.Split(new string[] { "]," }, StringSplitOptions.None);
                return results;
            }
            catch (Exception ex){ return results; }

        }
    }
}
