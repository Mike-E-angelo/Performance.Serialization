using BenchmarkDotNet.Attributes;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Performance.Serialization
{
	public class XmlDocumentTest
	{
		readonly XmlSerializer      _classic;
		readonly XmlReaderSettings _settings;
		readonly XmlParserContext _context;
		readonly ProjectItemElement _obj = Instance.Default;
		readonly byte[]             _xml;

		public XmlDocumentTest() : this(new XmlSerializer(typeof(ProjectItemElement)), new XmlReaderSettings
		{
			IgnoreWhitespace             = true,
			IgnoreComments               = true,
			IgnoreProcessingInstructions = true
		}, new NameTable()) {}

		public XmlDocumentTest(XmlSerializer classic, XmlReaderSettings settings, XmlNameTable table)
			: this(classic, settings, new XmlParserContext(table, new XmlNamespaceManager(table), null, XmlSpace.Default)) {}

		public XmlDocumentTest(XmlSerializer classic, XmlReaderSettings settings, XmlParserContext context)
		{
			_classic = classic;
			_settings = settings;
			_context = context;
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
				using (var reader = XmlReader.Create(stream, _settings, _context))
				{
					var result = _classic.Deserialize(reader);
					return result;
				}
			}
		}

		[Benchmark]
		public object DeserializeDom()
		{
			using (var stream = new MemoryStream(_xml))
			{
				using (var reader = XmlReader.Create(stream, _settings, _context))
				{
					var result   = new ProjectItemElement();
					var document = new XmlDocument();
					document.Load(reader);
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
			}

		}
	}
}