using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BenchmarkDotNet.Attributes;
using ExtendedXmlSerializer.ExtensionModel.Xml;

namespace Performance.Serialization {
	public class XmlDocumentTest
	{
		readonly XmlSerializer      _classic;
		readonly IXmlReaderFactory  _readerFactory = new XmlReaderFactory();
		readonly ProjectItemElement _obj           = Instance.Default.Get();
		readonly byte[]             _xml;

		public XmlDocumentTest() : this(new XmlSerializer(typeof(ProjectItemElement))) {}

		public XmlDocumentTest(XmlSerializer classic)
		{
			_classic = classic;
			_xml     = Encoding.UTF8.GetBytes(Serialize());
		}

		string Serialize()
		{
			using (var stream = new MemoryStream())
			{
				using (var writer = XmlWriter.Create(stream))
				{
					_classic.Serialize(writer, _obj);
					writer.Flush();
					stream.Seek(0, SeekOrigin.Begin);
					var result = new StreamReader(stream).ReadToEnd();
					return result;
				}
			}
		}

		[Benchmark]
		public object Deserialize()
		{
			using (var stream = new MemoryStream(_xml))
			{
				using (var reader = _readerFactory.Get(stream))
				{
					var result = _classic.Deserialize(reader);
					return result;
				}
			}
		}

		[Benchmark]
		public object DeserializeDom()
		{
			var result = new ProjectItemElement();

			var document = Document();
			foreach (var child in document.DocumentElement.ChildNodes.OfType<XmlElement>())
			{
				switch (child.Name)
				{
					case nameof(ProjectItemElement.Condition):
						result.Condition = child.InnerText;
						break;
					case nameof(ProjectItemElement.Exclude):
						result.Exclude = child.InnerText;
						break;
					case nameof(ProjectItemElement.Include):
						result.Include = child.InnerText;
						break;
					case nameof(ProjectItemElement.ItemType):
						result.ItemType = child.InnerText;
						break;
					case nameof(ProjectItemElement.KeepMetadata):
						result.KeepMetadata = child.InnerText;
						break;
					case nameof(ProjectItemElement.Label):
						result.Label = child.InnerText;
						break;
					case nameof(ProjectItemElement.Update):
						result.Update = child.InnerText;
						break;
					case nameof(ProjectItemElement.KeepDuplicates):
						result.KeepDuplicates = child.InnerText;
						break;
					case nameof(ProjectItemElement.Remove):
						result.Remove = child.InnerText;
						break;
				}
			}

			return result;
		}

		XmlDocument Document()
		{
			using (var stream = new MemoryStream(_xml))
			{
				using (var reader = _readerFactory.Get(stream))
				{
					var result = new XmlDocument();
					result.Load(reader);
					return result;
				}
			}
		}
	}
}