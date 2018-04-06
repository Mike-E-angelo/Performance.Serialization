using BenchmarkDotNet.Running;
using System;

namespace Performance.Serialization
{
	class Program
	{
		static void Main()
		{
			BenchmarkRunner.Run<XmlDocumentTest>();
			Console.ReadLine();
		}
	}
}