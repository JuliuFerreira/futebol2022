using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace futebol2022.Util
{
    public static class ConvertArquivoToBytes
    {
        public static byte[] ToByteArray(IFormFile file)
        {
            using BinaryReader reader = new(file.OpenReadStream());
            return reader.ReadBytes((int)file.Length);
        }
    }
}
