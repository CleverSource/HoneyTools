using EnvDTE;
using Mono.Debugging.Soft;
using Mono.Debugging.VisualStudio;

namespace HoneyToolsVS.Debugging
{
    public enum HoneySessionType
    {
        PlayInEditor = 0,
        AttachHoneynutDebugger
    }

    internal class HoneyStartInfo : StartInfo
    {
        public readonly HoneySessionType SessionType;

        public HoneyStartInfo(SoftDebuggerStartArgs args, DebuggingOptions options, Project startupProject, HoneySessionType sessionType)
            : base(args, options, startupProject)
        {
            SessionType = sessionType;
        }
    }
}
