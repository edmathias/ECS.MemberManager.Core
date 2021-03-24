using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.DataAccess.Dal
{
    public interface IImageDal : IDisposable
    {
        Task<Image> Fetch(int id);
        Task<List<Image>> Fetch();
        Task<Image> Insert(Image imageToInsert);
        Task<Image> Update(Image imageToUpdate);
        Task Delete(int id);
    }
}