using System;
using System.Collections.Generic;
using System.IO;

namespace Tool_CsvToBinary
{
    class Program
    {
        static void Main(string[] args)
        {
            List<byte> binaryList = new List<byte>();


            // 1バイトずつ読み出し。
            using (BinaryReader reader = new BinaryReader(File.OpenRead(@"convert.csv")))
            {
                try
                {
                    bool isMinus = false;
                    List<int> numberList = new List<int>();

                    var baseStream = reader.BaseStream;
                    while (baseStream.Position != baseStream.Length)
                    {

                        // メモ
                        // intsの中に、"1,0,1,-1,1"のような文字列が入っている

                        // メモ
                        // -1は255に変換したい


                        char c = reader.ReadChar();
                        //Console.Write(c);

                        // 文字が数字かどうか
                        if ( char.IsNumber(c))
                        {
                            //Console.Write("数字");
                            numberList.Add((int)Char.GetNumericValue(c));
                            //Console.Write(c);
                        }
                        else if (c == '-')
                        {
                            isMinus = true;
                            //Console.Write("-");
                        }
                        else if (c == ',' || c == '\n')
                        {
                            // リセットの前に、値を作る
                            double result = 0;

                            int digit = numberList.Count;  // 桁
                            for (int i = 0; i < numberList.Count; i++ )
                            {
                                result += numberList[i] * Math.Pow(10, digit-1-i);




                               // binaryList
                            }

                            // マイナスの時
                            if (isMinus)
                            {
                                   result = 256 - result;
                            }


                            if (numberList.Count > 0)
                            {
                                // 数値があるときだけ出力
                                Console.WriteLine(result);

                                binaryList.Add((byte)result);
                            }
                            

                            // リセット
                            isMinus = false;
                            digit = 0;
                            numberList.Clear();
                        }

                    }

                    for (int i = 0; i < numberList.Count; i++)
                    {
                        //Console.Write(numberList[i]);
                    }

                    //Console.WriteLine("");
                }
                catch (EndOfStreamException)
                {
                   
                }
            }

 
  



            // ファイル書き込み
            using (Stream stream = File.OpenWrite("binary.dat"))
            {
                // streamに書き込むためのBinaryWriterを作成
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    for (int i = 0; i < binaryList.Count; i++)
                    {
                        writer.Write((byte)binaryList[i]);
                    }
                }
            }



            System.Threading.Thread.Sleep(100000);
        }


    }
}