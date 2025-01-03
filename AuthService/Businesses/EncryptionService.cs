using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using AuthService.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AuthService.Bussenes;

public class EncryptionService:IEncryptionService
{
    private readonly string _key="";
    public EncryptionService(IConfiguration config)
    {
        _key = config["AppSettings:EncryptionKey"];
    }
     public string DencryptionValue(string value)
     {

         byte[] data = Convert.FromBase64String(value);
         using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
         {
             byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_key));
             using (TripleDESCryptoServiceProvider tripDesc = new TripleDESCryptoServiceProvider() { Key=keys,Mode=CipherMode.ECB,Padding=PaddingMode.PKCS7})
             {
                 ICryptoTransform transform = tripDesc.CreateDecryptor();
                 byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                 return UTF8Encoding.UTF8.GetString(result);
             }
         }

     }

     public string EncryptionValue(string value)
     {

         byte[] data = UTF8Encoding.UTF8.GetBytes(value);
         using (MD5CryptoServiceProvider md5 =new MD5CryptoServiceProvider())
         {
             byte[] keys=md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(_key));
             using (TripleDESCryptoServiceProvider tripDesc = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
             {
                 ICryptoTransform transform = tripDesc.CreateEncryptor();
                 byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
                 return Convert.ToBase64String(result,0,result.Length);
             }
         }
             
     }
}