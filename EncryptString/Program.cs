using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncryptString
{
    class Program
    {
        private static string msg = "引数は3つ指定する必要があります。\r\n第一引数： /e 又は /d\r\n第二引数：対象の文字列（暗号化の場合は平文、複合の場合は暗号化済みテキスト）\r\n第三引数：パスワード（暗号化と複合は同じパスワードである必要があります。）";

        static int Main(string[] args)
        {
            try
            {
                if (args.Length != 3)
                {
                    throw new Exception(msg);
                }

                // モード
                var arg1_mode = args[0];
                // 対象の文字列
                var arg2_Text = args[1];
                // パスワード
                var arg3_PW = args[2];

                if (arg1_mode != "/e" && arg1_mode != "/d") {
                    throw new Exception("第一引数が不正です。/ e 又は / d を指定してください。");
                }

                if ( string.IsNullOrEmpty(arg2_Text) == true )
                {
                    throw new Exception("暗号化もしくは複合する対象の文字列が指定されていません。");
                }

                if (string.IsNullOrEmpty(arg3_PW) == true)
                {
                    throw new Exception("パスワードが指定されていません。");
                }

                var stdOut = "";

                if (arg1_mode == "/e")
                {
                    // 暗号化
                    stdOut = EncryptString.Encrypt(arg2_Text, arg3_PW);
                }
                else
                {
                    // 復号
                    stdOut = EncryptString.Decrypt(arg2_Text, arg3_PW);
                }

                Console.WriteLine(stdOut);

                return 0;
            }
            catch(Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return -1;
            }

            //var encrypted =  EncryptString.Encrypt("これは平文", "A");
            //var decrypted = EncryptString.Decrypt(encrypted, "B");

            //if ("これは平文" != decrypted) { throw new Exception("暗号化、複合失敗"); }
            //Console.WriteLine("成功");
            //Console.ReadKey();
        }
    }
}
