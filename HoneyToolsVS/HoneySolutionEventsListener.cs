﻿using System;
using Microsoft.VisualStudio.Shell;

namespace HoneyToolsVS
{
	internal class HoneySolutionEventsListener : SolutionEventsListener
	{
		public HoneySolutionEventsListener(IServiceProvider serviceProvider)
			: base(serviceProvider)
		{
			ThreadHelper.ThrowIfNotOnUIThread();
			Init();
		}

		public string SolutionDirectory
		{
			get
			{
				ThreadHelper.ThrowIfNotOnUIThread();
				Solution.GetSolutionInfo(out string solutionDirectory, out string solutionFile, out string userOptsFile);
				_ = solutionFile;
				_ = userOptsFile;
				return solutionDirectory;
			}
		}
	}
}
