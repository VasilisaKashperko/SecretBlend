using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretBlend
{
    public static class GlobalClass
    {
        public static bool isEncrypt = false;
        public static bool isDecrypt = false;
        public static bool isRadioButtonChanged = false;
        public static bool whatRadioButton = false;

        public static string WAVfile = "";
        public static string secretKey = "";
        public static string TXTFile = "";
        public static string TXTMessage = "";
        public static string EncryptedWAVFile = "";

        public static string ExtractResult = "";
        public static string ExtractFileResult = "";
    }
}
