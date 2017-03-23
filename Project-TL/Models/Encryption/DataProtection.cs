using System;
using System.Security.Cryptography;
using System.Text;
using org.jivesoftware.util;
namespace Project_TL.Models.Encryption
{
    public class DataProtection
    {

        //Save the keystring some place like your database and use it to decrypt and encrypt
        //any text string or text file etc. Make sure you dont lose it though.

        public string Encrypt(string inputString, string key)
        {
           
            Blowfish bf = new Blowfish(key);
            string encrypted = bf.encryptString(inputString);
            return encrypted;


        }

        public  string Decrypt(string encryptedText, string keyString)
        {
            Blowfish bf = new Blowfish(keyString);
            string decrypted = bf.decryptString(encryptedText);
            return decrypted;
        }

        
    }
}