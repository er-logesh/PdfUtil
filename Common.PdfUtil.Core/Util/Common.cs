using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Common.PdfUtil.Core.Util
{
    public static class Common
    {
        public static string ConvertToString(this SecureString secureString)
        {
            IntPtr bstr = Marshal.SecureStringToBSTR(secureString);
            try
            {
                return Marshal.PtrToStringBSTR(bstr);
            }
            finally
            {
                Marshal.FreeBSTR(bstr);
            }
        }
        public static SecureString ConvertToSecureString(this string str)
        {
            SecureString sec_pass = new SecureString();
            Array.ForEach(str.ToCharArray(), sec_pass.AppendChar);
            sec_pass.MakeReadOnly();
            return sec_pass;
        }
    }
}