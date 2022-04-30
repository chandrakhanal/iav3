using IndianArmyWeb.Infrastructure.Constants;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace IndianArmyWeb
{
    public class cPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password)
        {
            using (SHA512 mySHA512 = SHA512Managed.Create())
            {
                byte[] hashed = mySHA512.ComputeHash(Encoding.UTF8.GetBytes(password.ToString()));
                return BitConverter.ToString(hashed).Replace("-", string.Empty);
            }
           
            //using (SHA256 mySHA256 = SHA256Managed.Create())
            //{
            //    byte[] hash = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(password.ToString()));

            //    StringBuilder hashSB = new StringBuilder();
            //    for (int i = 0; i < hash.Length; i++)
            //    {
            //        hashSB.Append(hash[i].ToString("x2"));
            //    }
            //    return hashSB.ToString();
            //}
        }
        public PasswordVerificationResult VerifyHashedPassword(
          string hashedPassword, string providedPassword)
        {
            //if (HashPassword(hashedPassword + salt) == providedPassword)
            //HashPassword(providedPassword)
            string sdd = csConst.cSalt;
            if (HashPassword(hashedPassword + sdd) == providedPassword)
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
        
    }
}