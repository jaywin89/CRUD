using PersonalReferenceProject.Adapter;
using PersonalReferenceProject.Cryptography;
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
    public class UserNameService : BaseService, IUserNameService
    {

        private ICryptographyService _cryptographyService;
        private const int HASH_ITERATION_COUNT = 1;
        private const int RAND_LENGTH = 15;


        public UserNameService(ICryptographyService cryptographyService)
        {
            _cryptographyService = cryptographyService;
        }

        public int Insert(UserNameRequest model)
        {

            string salt;
            string passwordHash;

            salt = _cryptographyService.GenerateRandomString(RAND_LENGTH);
            passwordHash = _cryptographyService.Hash(model.Password, salt, HASH_ITERATION_COUNT);


            int id = 0;
            DbCmdDef cmdDef = new DbCmdDef
            {
                DbCommandText = "dbo.UserName_Insert",
                DbCommandType = System.Data.CommandType.StoredProcedure,
                DbParameters = new[]
                {

                SqlDbParameter.Instance.BuildParameter("@Email", model.Email, System.Data.SqlDbType.NVarChar, 256),
                SqlDbParameter.Instance.BuildParameter("@PasswordHash", passwordHash, System.Data.SqlDbType.NVarChar, 256),
                SqlDbParameter.Instance.BuildParameter("@Salt", salt, System.Data.SqlDbType.NVarChar, 256),
                SqlDbParameter.Instance.BuildParameter("@Id", id, SqlDbType.Int, paramDirection: ParameterDirection.Output)
                }
            };
            Adapter.ExecuteQuery(cmdDef, (collection =>
            {
                int.TryParse(collection[4].Value.ToString(), out id);
            }));
            return id;
        }

        public void Update(UserNameUpdateRequest model)
        {


            string salt;
            string passwordHash;

            salt = _cryptographyService.GenerateRandomString(RAND_LENGTH);
            passwordHash = _cryptographyService.Hash(model.Password, salt, HASH_ITERATION_COUNT);
            try
            {
                Adapter.ExecuteQuery(new DbCmdDef
                {
                    DbCommandText = "dbo.UserName_UpdatePassword",
                    DbCommandType = System.Data.CommandType.StoredProcedure,
                    DbParameters = new[]
                    {
                SqlDbParameter.Instance.BuildParameter("@Email", model.Email, System.Data.SqlDbType.NVarChar, 256),
                SqlDbParameter.Instance.BuildParameter("@PasswordHash", passwordHash, System.Data.SqlDbType.NVarChar, 256),
                SqlDbParameter.Instance.BuildParameter("@Salt", salt, System.Data.SqlDbType.NVarChar, 256)
                    }
                });
            }
            catch
            {
                throw;
            }
        }

        public LoginResponse Login(UserNameRequest model)
        {

            UserNameResponse oldModel = GetSaltByEmail(model.Email);
            string passwordHash = _cryptographyService.Hash(model.Password, oldModel.Salt, HASH_ITERATION_COUNT);

            UserNameResponse user = GetByEmailAndHash(model.Email, passwordHash);

            if (user == null)
            {
                return new LoginResponse { IsSuccessful= false };

            }
            else
            {
                return new LoginResponse { IsSuccessful= true };
            }


        }
        private UserNameResponse GetByEmailAndHash(string email, string passwordHash)
        {
            try
            {
                DbCmdDef cmdDef = new DbCmdDef
                {
                    DbCommandText = "dbo.UserName_GetByEmailHash",
                    DbCommandType = System.Data.CommandType.StoredProcedure,
                    DbParameters = new[]
                 {
                   SqlDbParameter.Instance.BuildParameter("@Email", email, System.Data.SqlDbType.NVarChar, 50),
                   SqlDbParameter.Instance.BuildParameter("@PasswordHash", passwordHash, System.Data.SqlDbType.NVarChar, 128)
                }
                };
                return Adapter.LoadObject<UserNameResponse>(cmdDef).FirstOrDefault();
            }
            catch
            {
                throw;
            }
        }

        private UserNameResponse GetSaltByEmail(string email)
        {
            try
            {
                DbCmdDef cmdDef = new DbCmdDef
                {
                    DbCommandText = "dbo.UserName_GetSaltByEmail",
                    DbCommandType = System.Data.CommandType.StoredProcedure,
                    DbParameters = new[]
                 {
                   SqlDbParameter.Instance.BuildParameter("@Email", email, System.Data.SqlDbType.NVarChar, 50)
                }
                };
                return Adapter.LoadObject<UserNameResponse>(cmdDef).FirstOrDefault();              
            }
            catch
            {
                throw;
            }
        }
       

    }
}