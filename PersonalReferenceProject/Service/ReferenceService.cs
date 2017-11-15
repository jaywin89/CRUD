using PersonalReferenceProject.Adapter;
using PersonalReferenceProject.Models.Domain;
using PersonalReferenceProject.Models.Request;
using PersonalReferenceProject.Models.Response;
using PersonalReferenceProject.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PersonalReferenceProject.Service
{
    public class ReferenceService : BaseService, IReferenceService
    {
        public int Insert(ReferenceRequest model)
        {
            int id = 0;
            DbCmdDef cmdDef = new DbCmdDef
            {
                DbCommandText = "dbo.Reference_Insert",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new[]
                {

                SqlDbParameter.Instance.BuildParameter("@CategoryType", model.CategoryType, System.Data.SqlDbType.NVarChar, 50),
                SqlDbParameter.Instance.BuildParameter("@Keywords", model.Keywords, System.Data.SqlDbType.NVarChar, 256),
                SqlDbParameter.Instance.BuildParameter("@Example", model.Example, System.Data.SqlDbType.NVarChar, -1),
                SqlDbParameter.Instance.BuildParameter("@Id", id, SqlDbType.Int, paramDirection: ParameterDirection.Output)
                }
            };
            Adapter.ExecuteQuery(cmdDef, (collection) =>
            {
                id = collection.GetParmValue<int>("@Id");
                //int.TryParse(collection[3].Value.ToString(), out id);
            });
            return id;
        }

        public void Update(ReferenceUpdateRequest model)
        {
            try
            {
                Adapter.ExecuteQuery(new DbCmdDef
                {
                    DbCommandText = "dbo.Reference_Update",
                    DbCommandType = System.Data.CommandType.StoredProcedure,
                    DbParameters = new[]
                    {
                SqlDbParameter.Instance.BuildParameter("@Id", model.Id, System.Data.SqlDbType.Int),
                SqlDbParameter.Instance.BuildParameter("@Keywords", model.Keywords, System.Data.SqlDbType.NVarChar, 256),
                SqlDbParameter.Instance.BuildParameter("@Example", model.Example, System.Data.SqlDbType.NVarChar, -1)
                    }
                });
            }
            catch
            {
                throw;
            }
        }

        public void Delete(int Id)
        {
            try
            {
                Adapter.ExecuteQuery(new DbCmdDef
                {
                    DbCommandText = "dbo.Reference_Delete",
                    DbCommandType = System.Data.CommandType.StoredProcedure,
                    DbParameters = new[]
                    {
                SqlDbParameter.Instance.BuildParameter("@Id", Id, System.Data.SqlDbType.Int)
                    }
                });
            }
            catch
            {
                throw;
            }
        }
        public IEnumerable<ReferenceRequest> GetAllReferenceByType(ReferenceRequestWithPage model)
        {
            DbCmdDef cmdDef = new DbCmdDef
            {
                DbCommandText = "dbo.Reference_SelectAllByType",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new[]
                {
                          SqlDbParameter.Instance.BuildParameter("@CategoryType", model.CategoryType, System.Data.SqlDbType.NVarChar, 50),
                          SqlDbParameter.Instance.BuildParameter("@Keywords", model.Keywords, System.Data.SqlDbType.NVarChar, 256),
                          SqlDbParameter.Instance.BuildParameter("@PageNumber", model.PageNumber, System.Data.SqlDbType.Int),
                          SqlDbParameter.Instance.BuildParameter("@PageSize", model.PageSize, System.Data.SqlDbType.Int),
                }
            };
            return Adapter.LoadObject<ReferenceRequest>(cmdDef);
        }

        public ReferenceResponse GetCurrentReference(int Id)
        {
            DbCmdDef cmdDef = new DbCmdDef
            {
                DbCommandText = "dbo.Reference_SelectById",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new[]
             {
                          SqlDbParameter.Instance.BuildParameter("@Id", Id, System.Data.SqlDbType.NVarChar, 50),
                }
            };
            return Adapter.LoadObject<ReferenceResponse>(cmdDef).FirstOrDefault();
        }


    }
}