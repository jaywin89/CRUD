using PersonalReferenceProject.Models.Request;
using PersonalReferenceProject.Models.Response;

namespace PersonalReferenceProject.Service
{
    public interface IUserNameService
    {
        int Insert(UserNameRequest model);
        void Update(UserNameUpdateRequest model);
        LoginResponse Login(UserNameRequest model);
    }
}