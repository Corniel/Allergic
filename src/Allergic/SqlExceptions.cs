using System;
using System.Data.SqlClient;
using System.Reflection;

namespace Allergic
{
    /// <summary>Helper to throw specific <see cref=" SqlException"/>s, as this
    /// class is sealed, without public constructors.
    /// </summary>
    public static class SqlExceptions
    {
        /// <remarks>See http://msdn.microsoft.com/en-us/library/cc645603(v=sql.105).aspx for a list of all SqlException numbers</remarks>
        public enum Error
        {
            /// <summary>Timeout expired. The timeout period elapsed prior to completion of the operation or the server is not responding.: The wait operation timed out.</summary>
            TimeOut = -2,
            /// <summary>TODO: find out what this error number means.</summary>
            Number4 = 4,
            /// <summary>TODO: find out what this error number means.</summary>
            Number11 = 11,
            /// <summary>The client was unable to establish a connection because of an error during connection initialization process before login.</summary>
            ConnectionInitialization = 233,
            /// <summary>Transaction (Process ID %d) was deadlocked on {%Z} resources with another process and has been chosen as the deadlock victim. Rerun the transaction.</summary>
            Deadlock = 1205,
            /// <summary>Lock request time out period exceeded.</summary>
            LockTimeout = 1222,
            /// <summary>Cannot insert duplicate key row in object '%.*ls' with unique index '%.*ls'.</summary>
            DuplicateKey = 2601,
            /// <summary>Violation of PRIMARY KEY constraint Constraint Name. Cannot insert duplicate key in object Table Name. The statement has been terminated.</summary>
            PrimaryKeyViolation = 2627,
            /// <summary>The server failed to resume the transaction.</summary>
            TheServerFailedToResumeTheTransaction = 3971,
            /// <summary>The operation failed because the session is not single threaded.</summary>
            OperationFailedSessionNotSingleThreaded = 3983,
            /// <summary>Could not find prepared statement with handle.</summary>
            InvalidPreparedHandle = 8179,
            /// <summary>A transport-level error has occurred when sending the request to the server.</summary>
            TransportLevelSending = 10054,
        }

        /// <summary>Throws a lock time-out <see cref="SqlException"/>.</summary>
        public static void ThrowLockTimeOut()
        {
            Throw(Error.LockTimeout, "Some SQL lock time-out.", null, Guid.NewGuid());
        }

        /// <summary>Throws a dead lock <see cref="SqlException"/>.</summary>
        public static void ThrowDeadLock()
        {
            Throw(Error.Deadlock, "Some SQL dead lock.", null, Guid.NewGuid());
        }

        public static void Throw(Error error, string message, Exception inner, Guid conId)
        {
            var collection = GetCollection(error, message);
            throw Ctor.New<SqlException>(message, collection, inner, conId);
        }

        private static SqlErrorCollection GetCollection(Error error, string message)
        {
            var collection = Ctor.New<SqlErrorCollection>();
            AddError(collection, error, 0, 0, "localhost", message, "unknown procedure", 17);
            return collection;
        }
        private static void AddError(SqlErrorCollection collection, Error infoNumber, byte errorState, byte errorClass, string server, string errorMessage, string procedure, int lineNumber)
        {
            var error = Ctor.New<SqlError>((int)infoNumber, errorState, errorClass, server, errorMessage, procedure, lineNumber);
            var add = typeof(SqlErrorCollection).GetMethod("Add", BindingFlags.Instance | BindingFlags.NonPublic);
            add.Invoke(collection, new object[] { error });
        }
    }
}