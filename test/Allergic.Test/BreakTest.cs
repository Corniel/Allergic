using NUnit.Framework;
using System;

namespace Allergic.UnitTests
{
	[TestFixture]
	public class BreakTest
	{
		[Test, Ignore("Only useful to test while debugging.")]
		public void Now_None_Breaks()
		{
			Break.Now();
		}

		[Test, Ignore("Only useful to test while debugging.")]
		public void If_Sqrt9Is3_Breaks()
		{
			Break.If(3 == (int)Math.Sqrt(9));
		}
	}
}
