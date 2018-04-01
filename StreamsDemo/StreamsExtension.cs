using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Linq;

namespace StreamsDemo
{
    // C# 6.0 in a Nutshell. Joseph Albahari, Ben Albahari. O'Reilly Media. 2015
    // Chapter 15: Streams and I/O
    // Chapter 6: Framework Fundamentals - Text Encodings and Unicode
    // https://msdn.microsoft.com/ru-ru/library/system.text.encoding(v=vs.110).aspx

    public static class StreamsExtension
    {

        #region Public members

        #region TODO: Implement by byte copy logic using class FileStream as a backing store stream .

        public static int ByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int flagWriteBite = 0;

            using (FileStream fileIn = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fileOut = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    int i = 0;

                    while ((i = fileIn.ReadByte()) != -1)
                    {
                        fileOut.WriteByte((byte)i);
                        flagWriteBite++;
                    }
                }
            }

            return flagWriteBite;
        }

        #endregion

        #region TODO: Implement by byte copy logic using class MemoryStream as a backing store stream.

        public static int InMemoryByByteCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            // TODO: step 1. Use StreamReader to read entire file in string

            var lineResult = String.Empty;

            using (FileStream fileIn = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader strReader = new StreamReader(fileIn, Encoding.Default))
                {
                    lineResult = strReader.ReadToEnd();
                }
            }

            // TODO: step 2. Create byte array on base string content - use  System.Text.Encoding class

            Encoding encodingStep2 = Encoding.Default;

            byte[] byteArrayStep2 = new byte[lineResult.Length];

            var countByteStep2 = encodingStep2.GetBytes(lineResult, 0, lineResult.Length, byteArrayStep2, 0);

            // TODO: step 3. Use MemoryStream instance to read from byte array (from step 2)

            var memStreamStep3 = new MemoryStream(byteArrayStep2);

            // TODO: step 4. Use MemoryStream instance (from step 3) to write it content in new byte array

            var byteArrayStep4 = new byte[memStreamStep3.Length];

            var countByteStep3 = memStreamStep3.Read(byteArrayStep4, 0, byteArrayStep4.Length);

            // TODO: step 5. Use Encoding class instance (from step 2) to create char array on byte array content

            var charArrayStep5 = encodingStep2.GetChars(byteArrayStep4);

            // TODO: step 6. Use StreamWriter here to write char array content in new file

            using (FileStream fileOut = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
            {
                using (StreamWriter strWriter = new StreamWriter(fileOut, Encoding.Default))
                {
                    strWriter.Write(charArrayStep5);
                }
            }

            return charArrayStep5.Length;
        }

        #endregion

        #region TODO: Implement by block copy logic using FileStream buffer.

        public static int ByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int flagCountByte = 0;

            using (FileStream fileIn = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fileOut = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                {
                    int byteBlock = 1000;

                    byte[] arrayByteBlock = new byte[byteBlock];

                    int countByteReadInBlock = 0, countIteration = 0;

                    while ((countByteReadInBlock = fileIn.Read(arrayByteBlock, 0, byteBlock)) != 0)
                    {
                        var rez = byteBlock * countByteReadInBlock;

                        fileOut.Write(arrayByteBlock, byteBlock * countIteration, countByteReadInBlock);

                        flagCountByte += countByteReadInBlock;
                    }
                }
            }

            return flagCountByte;
        }

        #endregion

        #region TODO: Implement by block copy logic using MemoryStream.

        public static int InMemoryByBlockCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            // TODO: Use InMemoryByByteCopy method's approach

            int flagCountByte = 0;
            using (FileStream fileIn = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (MemoryStream strMemory = new MemoryStream())
                {
                    using (FileStream fileOut = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                    {
                        fileIn.CopyTo(strMemory);

                        var arrayForWrite = strMemory.ToArray();

                        flagCountByte = arrayForWrite.Length;

                        fileOut.Write(arrayForWrite, 0, arrayForWrite.Length);
                    }
                }
            }

            return flagCountByte;
        }

        #endregion

        #region TODO: Implement by block copy logic using class-decorator BufferedStream.

        public static int BufferedCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int flagCountByte = 0;

            using (FileStream fileIn = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (BufferedStream strBuffered = new BufferedStream(fileIn))
                {
                    using (FileStream fileOut = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] arrayForResult = new byte[strBuffered.Length];
                        strBuffered.Position = 0;
                        strBuffered.Read(arrayForResult, 0, (int)strBuffered.Length);
                        fileOut.Write(arrayForResult, 0, arrayForResult.Length);
                        flagCountByte = arrayForResult.Length;
                    }
                }
            }

            return flagCountByte;
        }

        #endregion

        #region TODO: Implement by line copy logic using FileStream and classes text-adapters StreamReader/StreamWriter

        public static int ByLineCopy(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            int countLine = 0;

            using (FileStream fileIn = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader strReader = new StreamReader(fileIn, Encoding.Default))
                {
                    using (FileStream fileOut = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                    {
                        using (StreamWriter strWriter = new StreamWriter(fileOut, Encoding.Default))
                        {
                            string inputLine = String.Empty;

                            while ((inputLine = strReader.ReadLine()) != null)
                            {
                                if (countLine != 0)
                                {
                                    strWriter.WriteLine();
                                }

                                strWriter.Write(inputLine);

                                countLine++;
                            }
                        }
                    }
                }
            }

            return countLine;
        }

        #endregion

        #region TODO: Implement content comparison logic of two files 

        public static bool IsContentEquals(string sourcePath, string destinationPath)
        {
            InputValidation(sourcePath, destinationPath);

            using (FileStream fileInFirst = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (FileStream fileInSecond = new FileStream(destinationPath, FileMode.Open, FileAccess.Read))
                {
                    using (BufferedStream strBufferedFirst = new BufferedStream(fileInFirst))
                    {
                        using (BufferedStream strBufferedSecond = new BufferedStream(fileInSecond))
                        {
                            if (strBufferedFirst.Length != strBufferedSecond.Length)
                            {
                                return false;
                            }

                            while (strBufferedFirst.ReadByte() != -1 && strBufferedSecond.ReadByte() != -1)
                            {
                                if ((strBufferedFirst.ReadByte() ^ strBufferedSecond.ReadByte()) != 0)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }
        
        #endregion

        #endregion

        #region Private members

        #region TODO: Implement validation logic

        private static void InputValidation(string sourcePath, string destinationPath)
        {
            if (sourcePath == null)
            {
                throw new ArgumentNullException($"Input argument {nameof(sourcePath)} is null");
            }

            if (destinationPath == null)
            {
                throw new ArgumentNullException($"Input argument {nameof(destinationPath)} is null");
            }
        }

        #endregion

        #endregion

    }
}
