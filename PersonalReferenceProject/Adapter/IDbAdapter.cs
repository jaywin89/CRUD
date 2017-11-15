using System;
using System.Collections.Generic;
using System.Data;

namespace PersonalReferenceProject.Adapter
{
    public interface IDbAdapter
    {
        int CommandTimeout { get; set; }
        IDbCommand DbCommand { get; }
        IDbConnection DbConnection { get; }

        int ExecuteQuery(IDbCmdDef cmdDef, Action<IDbDataParameter[]> returnParameters = null);
        object ExecuteScalar(IDbCmdDef cmdDef);
        IEnumerable<T> LoadObject<T>(IDbCmdDef cmdDef) where T : class;
        IEnumerable<T> LoadObject<T>(IDbCmdDef cmdDef, Func<IDataReader, T> mapper) where T : class;
    }
}