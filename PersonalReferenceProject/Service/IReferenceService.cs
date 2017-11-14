using System.Collections.Generic;
using PersonalReferenceProject.Models.Request;
using PersonalReferenceProject.Models.Response;

namespace PersonalReferenceProject.Service
{
    public interface IReferenceService
    {
        void Delete(int Id);
        IEnumerable<ReferenceRequest> GetAllReferenceByType(ReferenceRequestWithPage model);
        ReferenceResponse GetCurrentReference(int Id);
        int Insert(ReferenceRequest model);
        void Update(ReferenceUpdateRequest model);
    }
}