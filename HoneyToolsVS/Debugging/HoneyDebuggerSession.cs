using System;
using System.ComponentModel.Design;
using System.IO;
using Mono.Debugging.Client;
using Mono.Debugging.Soft;
using Mono.Debugger.Soft;

namespace HoneyToolsVS.Debugging
{
	internal class HoneyDebuggerSession : SoftDebuggerSession
	{
		private bool m_IsAttached;
		private MenuCommand m_AttachToHoneynutMenuItem;

		/*internal HoneyDebuggerSession(MenuCommand attachToHoneynutMenuItem)
		{
			m_AttachToHoneynutMenuItem = attachToHoneynutMenuItem;
		}*/

		protected override void OnRun(DebuggerStartInfo startInfo)
		{
			var honeyStartInfo = startInfo as HoneyStartInfo;

			switch (honeyStartInfo.SessionType)
			{
			case HoneySessionType.PlayInEditor:
				break;
			case HoneySessionType.AttachHoneynutDebugger:
			{
				m_IsAttached = true;
				base.OnRun(honeyStartInfo);
				break;
			}
			default:
				throw new ArgumentOutOfRangeException(honeyStartInfo.SessionType.ToString());
			}
		}

		protected override void OnConnectionError(Exception ex)
		{
			// The session was manually terminated
			if (HasExited)
			{
				base.OnConnectionError(ex);
				return;
			}

			if (ex is VMDisconnectedException || ex is IOException)
			{
				HasExited = true;
				base.OnConnectionError(ex);
				return;
			}

			string message = "An error occured when trying to attach to Honeynut. Please make sure that Honeynut is running and that it's up-to-date.";
			message += Environment.NewLine;
			message += string.Format("Message: {0}", ex != null ? ex.Message : "No error message provided.");

			if (ex != null)
			{
				message += Environment.NewLine;
				message += string.Format("Source: {0}", ex.Source);
				message += Environment.NewLine;
				message += string.Format("Stack Trace: {0}", ex.StackTrace);

				if (ex.InnerException != null)
				{
					message += Environment.NewLine;
					message += string.Format("Inner Exception: {0}", ex.InnerException.ToString());
				}
			}
			
			_ = HoneyToolsPackage.Instance.ShowErrorMessageBoxAsync("Connection Error", message);
			base.OnConnectionError(ex);
		}

		protected override void OnExit()
		{
			if (m_IsAttached)
			{
				m_IsAttached = false;
				base.OnDetach();
			}
			else
			{
				base.OnExit();
			}
		}
	}
}
