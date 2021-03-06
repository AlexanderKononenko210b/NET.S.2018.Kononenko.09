﻿using System;
using System.Configuration;
using System.IO;
using static StreamsDemo.StreamsExtension;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["fileSource"]);

            string destination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["fileOutput"]);

            Console.WriteLine($"ByteCopy() done. Total bytes: {ByByteCopy(source, destination)}");//ok

            Console.WriteLine(IsContentEquals(source, destination));

            Console.WriteLine($"InMemoryByteCopy() done. Total bytes: {InMemoryByByteCopy(source, destination)}");//ok

            Console.WriteLine(IsContentEquals(source, destination));

            Console.WriteLine($"ByBlockCopy() done. Total bytes: {ByBlockCopy(source, destination)}");// ok

            Console.WriteLine(IsContentEquals(source, destination));

            Console.WriteLine($"InMemoryByBlockCopy() done. Total bytes: {InMemoryByBlockCopy(source, destination)}");//ok

            Console.WriteLine(IsContentEquals(source, destination));

            Console.WriteLine($"BufferedCopy() done. Total bytes: {BufferedCopy(source, destination)}");//ok

            Console.WriteLine(IsContentEquals(source, destination));

            Console.WriteLine($"ByLineCopy() done. Total lines: {ByLineCopy(source, destination)}");//ok

            Console.WriteLine(IsContentEquals(source, destination));
        }
    }
}
