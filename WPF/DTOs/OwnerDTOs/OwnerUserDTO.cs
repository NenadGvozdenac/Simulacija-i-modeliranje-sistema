using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.DTOs.OwnerDTOs;

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
        OwnerInfo = OwnerService.GetInstance().GetById(user.Id).Item1;
    }
}
