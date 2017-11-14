using System.Data;

namespace PersonalReferenceProject.Adapter
{
    public interface IDbCmdDef
    {
        string DbCommandText { get; set; }
        CommandType DbCommandType { get; set; }
        IDbDataParameter[] DbParameters { get; set; }
    }
}