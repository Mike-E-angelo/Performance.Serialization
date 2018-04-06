namespace Performance.Serialization {
	public abstract class ProjectElementContainer : ProjectElement
	{
		internal ProjectElementContainer()
			: base() {}

		public ProjectElement FirstChild { get; set; }

		public ProjectElement LastChild { get; set; }
	}
}