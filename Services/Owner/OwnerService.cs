using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository;
using BookingApp.Repository.OwnerRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services.Owner;

public class OwnerService
{
    private OwnerInfoRepository _ownerInfoRepository;
    private UserRepository _userRepository;

    private static Lazy<OwnerService> instance = new Lazy<OwnerService>(() => new OwnerService());
    public OwnerService()
    {
        _ownerInfoRepository = OwnerInfoRepository.GetInstance();
        _userRepository = UserRepository.GetInstance();
    }

    public static OwnerService GetInstance()
    {
        return instance.Value;
    }

    public (OwnerInfo, User) GetOwnerInfo(int ownerId)
    {
        return (_ownerInfoRepository.GetByOwnerId(ownerId), _userRepository.GetById(ownerId));
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
}
