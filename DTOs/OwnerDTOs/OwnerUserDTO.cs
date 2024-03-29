using BookingApp.Model.MutualModels;
using BookingApp.Model.OwnerModels;
using BookingApp.Repository.OwnerRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTOs.OwnerDTOs;

public class OwnerUserDTO
{
    private User _user;
    public User User
    {
        get => _user;
        set => _user = value;
    }

    private OwnerInfo _ownerInfo;
    public OwnerInfo OwnerInfo
    {
        get => _ownerInfo;
        set => _ownerInfo = value;
    }

    private string _title;
    public string Title
    {
        get => OwnerInfo.IsSuperOwner ? "SUPER OWNER" : "OWNER";
        set => _title = value;
    }

    public OwnerUserDTO(User user)
    {
        User = user;
        OwnerInfo = OwnerInfoRepository.GetInstance().GetByOwnerId(user.Id);
    }
}
