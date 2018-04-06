namespace Performance.Serialization
{
	public class ProjectItemElement : ProjectElementContainer
	{
		public string ItemType { get; set; }

		public string Include { get; set; }

		public string Exclude { get; set; }

		public string Remove { get; set; }

		public string Update { get; set; }

		public string KeepMetadata { get; set; }

		public string RemoveMetadata { get; set; }

		public string KeepDuplicates { get; set; }
	}
}