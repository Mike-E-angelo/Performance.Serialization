namespace Performance.Serialization
{
	sealed class Instance
	{
		public static ProjectItemElement Default { get; } = new ProjectItemElement
		{
			Condition      = "My favorite. ;)",
			Exclude        = "Not to be confused with include.",
			Include        = "Not to be confused with exclude.",
			ItemType       = "Not to be confused with an item's type. ;)",
			KeepDuplicates = "Sounds like a boolean to me.",
			KeepMetadata   = "Another boolean?",
			Label          = "Some label",
			Update         = "Really?",
			Remove         = "Something to remove?  Or a boolean, can't tell."

		};
	}
}