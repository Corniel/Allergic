using System.Diagnostics;

namespace Allergic
{
	/// <summary><see cref="Debugger"/>.Break helper.</summary>
	public static class Break
	{
		/// <summary>If the <see cref="Debugger"/> is not attached yet, it will be launched.</summary>
		[DebuggerStepThrough]
		public static bool Attach()
		{
			if (!Debugger.IsAttached)
			{
				Debugger.Launch();
				return true;
			}
			return false;
		}

		/// <summary>Equivalent of <see cref="Debugger.Break()"/> but launches
		/// the debugger if not attached yet.
		/// </summary>
		[DebuggerStepThrough]
		public static void Now() => If(true);

		/// <summary>Equivalent of a conditional breakpoint but launches the
		/// debugger if not attached yet.</summary>
		/// <remarks>
		/// The processing of conditional breakpoints is extremely slow, this is quicker.
		/// </remarks>
		[DebuggerStepThrough]
		public static void If(bool condition)
		{
			if (condition &&!Attach())
			{
				Debugger.Break();
			}
		}
	}
}
