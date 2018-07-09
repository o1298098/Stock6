using System;
using System.Collections.Generic;
using System.Text;

namespace Stock6.Services
{
    public interface IToast
    {
        void LongAlert(string message);
        void ShortAlert(string message);
    }
}
