using System.Diagnostics;

namespace Allergic
{
	/// <summary>Debugger.Break helper.</summary>
	public static class Break
	{
		/// <summary>Equivalent of <code>Debugger.Break();</code> but launches
		/// the debugger if not attached yet.
		/// </summary>
		[DebuggerStepThrough]
		public static void Now() { If(true); }

		/// <summary>Equivalent of a conditional breakpoint but launches the
		/// debugger if not attached yet.</summary>
		/// <remarks>
		/// The processing of conditional breakpoints is extremely slow, this is quick.
		/// </remarks>
		[DebuggerStepThrough]
		public static void If(bool condition)
		{
			if (condition)
			{
				if (!Debugger.IsAttached)
				{
					Debugger.Launch();
				}
				else
				{
					Debugger.Break();
				}
			}
		}
	}
}
