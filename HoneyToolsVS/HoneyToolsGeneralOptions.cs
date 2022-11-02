using System.ComponentModel;
using Microsoft.VisualStudio.Shell;

namespace HoneyToolsVS
{
	public class HoneyToolsGeneralOptions : DialogPage
	{

		[Category("Debugging")]
		[DisplayName("Connection Port")]
		[Description("The port that Honeynut is expected to use for the debugger. This value should match the Debugger Port value in the Honeynut project settings")]
		public int ConnectionPort { get; set; } = 2550;

		[Category("Debugging")]
		[DisplayName("Maximum Connection Attempts")]
		[Description("Determines how many connection attempts Honey Tools can make if it fails to attach to Honeynut (0 means inifite attempts. Default: 1)")]
		public int MaxConnectionAttempts { get; set; } = 1;

	}
}
