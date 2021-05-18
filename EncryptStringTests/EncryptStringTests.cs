using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncryptString;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EncryptString.Tests
{
    [TestClass()]
    public class EncryptStringTests
    {
        [TestMethod()]
        public void EncryptTest_1()
        {
            var plainText = "元の文字列";
            var encryptedText = "";
            var decryptedText = "";
            var passWord = "abcd";
            
            encryptedText = EncryptString.Encrypt(plainText, passWord);
            decryptedText = EncryptString.Decrypt(encryptedText, passWord);

            Assert.AreEqual(plainText, decryptedText);
        }

        [TestMethod()]
        public void EncryptTest_2()
        {
            var plainText = "";
            var encryptedText = "";
            var decryptedText = "";
            var passWord = "";

            encryptedText = EncryptString.Encrypt(plainText, passWord);
            decryptedText = EncryptString.Decrypt(encryptedText, passWord);

            Assert.AreEqual(plainText, decryptedText);
        }


        [TestMethod()]
        public void EncryptTest_3()
        {
            var plainText = "元の文字列";
            var encryptedText = "";
            var decryptedText = "";

            var psi = new ProcessStartInfo
            {
                FileName = "EncryptString.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = $"/e {plainText} abcd"
            };

            
            using (var p1 = Process.Start(psi))
            {
                p1.WaitForExit();

                encryptedText = p1.StandardOutput.ReadToEnd();
            }


            var ps2 = new ProcessStartInfo
            {
                FileName = "EncryptString.exe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                Arguments = $"/d {encryptedText} abcd"
            };

            using (var p2 = Process.Start(ps2))
            {
                p2.WaitForExit();

                decryptedText = p2.StandardOutput.ReadToEnd().TrimEnd('\r', '\n');
            }

            Assert.AreEqual(plainText, decryptedText);
        }
    }
}