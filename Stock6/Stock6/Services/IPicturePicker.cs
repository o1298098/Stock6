using Stock6.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Stock6.Services
{
    public interface IPicturePicker
    {
        Task<List<ImageModel>> GetImageStreamAsync();
    }
}
