using BookingApp.Application.Localization;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Models;

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
        get => OwnerInfo.IsSuperOwner ? TranslationSource.Instance["SuperOwnerUC"] : TranslationSource.Instance["OwnerUC"];
        set => _title = value;
    }

    public OwnerUserDTO(User user)
    {
        User = user;
        OwnerInfo = OwnerService.GetInstance().GetById(user.Id).Item1;
    }
}
