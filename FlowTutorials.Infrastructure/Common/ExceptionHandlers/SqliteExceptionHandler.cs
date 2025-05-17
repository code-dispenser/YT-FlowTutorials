using Flow.Core.Areas.Returns;
using FlowTutorials.Application.Common.Seeds;
using Microsoft.Data.Sqlite;

namespace FlowTutorials.Infrastructure.Common.ExceptionHandlers;

public class SqliteExceptionHandler : IDbExceptionHandler
{
    public Flow<T> Handle<T>(Exception ex)
        
        => ex switch
        {
            SqliteException sqliteEx when sqliteEx.SqliteErrorCode == 19 => new Failure.ConstraintFailure("This action cannot be performed due to related items preventing it."),
            SqliteException                                              => new Failure.ConnectionFailure("Unable to connect to the sqlite database"),

            _ => Flow<T>.Failed(new Failure.UnknownFailure("A problem has occurred, please try again later", null, 0, true, ex))
        };
}
