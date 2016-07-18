using NUnit.Framework;
using System.Data.SqlClient;

namespace Allergic.UnitTests
{
	[TestFixture]
	public class SqlExceptionsTest
	{
		[Test]
		public void ThrowSqlException()
		{
			Assert.Throws<SqlException>(() =>
			{
				SqlExceptions.ThrowLockTimeOut();
			});
		}
	}
}
