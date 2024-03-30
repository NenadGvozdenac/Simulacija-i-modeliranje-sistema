using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.OwnerRepositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.Owner;

public class OwnerService : IService<(OwnerInfo, User)>
{
    private OwnerInfoRepository _ownerInfoRepository;
    private UserRepository _userRepository;
    public OwnerService()
    {
        _ownerInfoRepository = OwnerInfoRepository.GetInstance();
        _userRepository = UserRepository.GetInstance();
    }

    public static OwnerService GetInstance()
    {
        return App.ServiceProvider.GetRequiredService<OwnerService>();
    }

    public (OwnerInfo, User) GetOwnerInfo(int ownerId)
    {
        return (_ownerInfoRepository.GetById(ownerId), _userRepository.GetById(ownerId));
    }

    public void UpdateOwnerInfo(OwnerInfo ownerInfo)
    {
        _ownerInfoRepository.Update(ownerInfo);
    }

    public void AddOwner(User user)
    {
        OwnerInfo ownerInfo = new(user.Id, false, 0, 0, 0);
        _ownerInfoRepository.Add(ownerInfo);
        _userRepository.Add(user);
    }

    public void DeleteOwner(int ownerId)
    {
        _ownerInfoRepository.Delete(ownerId);
        _userRepository.Delete(ownerId);
    }

    public void UpdateOwner(OwnerInfo ownerInfo, User user)
    {
        _ownerInfoRepository.Update(ownerInfo);
        _userRepository.Update(user);
    }

    public void Add((OwnerInfo, User) entity)
    {
        _ownerInfoRepository.Add(entity.Item1);
        _userRepository.Add(entity.Item2);
    }

    public void Delete((OwnerInfo, User) entity)
    {
        _ownerInfoRepository.Delete(entity.Item2.Id);
        _userRepository.Delete(entity.Item2.Id);
    }

    public List<(OwnerInfo, User)> GetAll()
    {
        List<(OwnerInfo, User)> owners = new();
        List<OwnerInfo> ownerInfos = _ownerInfoRepository.GetAll();
        ownerInfos.ForEach(ownerInfo => owners.Add((ownerInfo, _userRepository.GetById(ownerInfo.OwnerId))));
              
        return owners;
    }

    public (OwnerInfo, User) GetById(int entityId)
    {
        OwnerInfo ownerInfo = _ownerInfoRepository.GetById(entityId);
        User user = _userRepository.GetById(entityId);
        return (ownerInfo, user);
    }

    public void Update((OwnerInfo, User) entity)
    {
        _ownerInfoRepository.Update(entity.Item1);
        _userRepository.Update(entity.Item2);
    }
}
