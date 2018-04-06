namespace Performance.Serialization {
	public abstract class ProjectElement
	{
		public string Condition { get; set; }

		public string Label { get; set; }

		public ProjectElementContainer Parent { get; set; }

		public ProjectElement PreviousSibling { get; set; }

		public ProjectElement NextSibling { get; set; }
	}
}