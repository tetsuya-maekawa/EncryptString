using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptString
{
    public class EncryptString
    {

        /// <summary>
        /// 文字列を暗号化する
        /// </summary>
        /// <param name="plainText">暗号化する文字列</param>
        /// <param name="passWord">暗号化に使用するパスワード</param>
        /// <returns>暗号化された文字列</returns>
        public static string Encrypt(string plainText,
                                     string passWord)
        {
            //パスワードから共有キーと初期化ベクタを作成
            byte[] key, iv;
            // 暗号化したバイト配列
            byte[] encBytes;

            //RijndaelManagedオブジェクトを作成
            using (var rijndael = new RijndaelManaged())
            {
                GenerateKeyFromPassword(passWord, rijndael.KeySize, out key, rijndael.BlockSize, out iv);

                rijndael.Key = key;
                rijndael.IV = iv;

                //文字列をバイト型配列に変換する
                var strBytes = Encoding.UTF8.GetBytes(plainText);

                //対称暗号化オブジェクトの作成
                using (ICryptoTransform encryptor = rijndael.CreateEncryptor())
                {
                    //バイト型配列を暗号化する
                    encBytes = encryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
                }

                //バイト型配列を文字列に変換して返す
                return System.Convert.ToBase64String(encBytes);
            }
        }

        /// <summary>
        /// 暗号化された文字列を復号化する
        /// </summary>
        /// <param name="encryptedText">暗号化された文字列</param>
        /// <param name="passWord">暗号化に使用したパスワード</param>
        /// <returns>復号化された文字列</returns>
        public static string Decrypt(string encryptedText,
                                     string passWord)
        {
            //パスワードから共有キーと初期化ベクタを作成
            byte[] key, iv;
            // 複合したバイト配列
            byte[] decBytes;

            //文字列をバイト型配列に戻す
            var strBytes = Convert.FromBase64String(encryptedText);

            //RijndaelManagedオブジェクトを作成
            using (var rijndael = new RijndaelManaged())
            {
                GenerateKeyFromPassword(passWord, rijndael.KeySize, out key, rijndael.BlockSize, out iv);
                rijndael.Key = key;
                rijndael.IV = iv;

                //対称暗号化オブジェクトの作成
                using (ICryptoTransform decryptor = rijndael.CreateDecryptor())
                {
                    //バイト型配列を復号化する
                    //復号化に失敗すると例外CryptographicExceptionが発生
                    decBytes = decryptor.TransformFinalBlock(strBytes, 0, strBytes.Length);
                }

                //バイト型配列を文字列に戻して返す
                return Encoding.UTF8.GetString(decBytes);
            }
        }

        /// <summary>
        /// パスワードから共有キーと初期化ベクタを生成する
        /// </summary>
        /// <param name="password">基になるパスワード</param>
        /// <param name="keySize">共有キーのサイズ（ビット）</param>
        /// <param name="key">作成された共有キー</param>
        /// <param name="blockSize">初期化ベクタのサイズ（ビット）</param>
        /// <param name="iv">作成された初期化ベクタ</param>
        private static void GenerateKeyFromPassword(string password,
                                                    int keySize,
                                                    out byte[] key,
                                                    int blockSize,
                                                    out byte[] iv)
        {
            //パスワードから共有キーと初期化ベクタを作成する
            //saltを決める
            byte[] salt = Encoding.UTF8.GetBytes("saltは必ず8バイト以上");

            //Rfc2898DeriveBytesオブジェクトを作成する
            var deriveBytes = new Rfc2898DeriveBytes(password, salt)
            {
                //反復処理回数を指定する デフォルトで1000回
                IterationCount = 1000
            };

            //共有キーと初期化ベクタを生成する
            key = deriveBytes.GetBytes(keySize / 8);
            iv = deriveBytes.GetBytes(blockSize / 8);
        }


    }
}
