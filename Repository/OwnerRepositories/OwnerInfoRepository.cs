using BookingApp.Model.OwnerModels;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository.OwnerRepositories;

public class OwnerInfoRepository
{
    private const string FilePath = "../../../Resources/Data/owner_infos.csv";
    private readonly static Lazy<OwnerInfoRepository> instance = new Lazy<OwnerInfoRepository>(() => new OwnerInfoRepository());
    private readonly Serializer<OwnerInfo> _serializer;

    public List<OwnerInfo> OwnerInfos { get; set; }

    public OwnerInfoRepository()
    {
        _serializer = new Serializer<OwnerInfo>();
        OwnerInfos = _serializer.FromCSV(FilePath);
    }

    public static OwnerInfoRepository GetInstance()
    {
        return instance.Value;
    }

    public OwnerInfo GetByOwnerId(int id)
    {
        return OwnerInfos.FirstOrDefault(owner => owner.OwnerId == id);
    }

    public void Add(OwnerInfo ownerInfo)
    {
        OwnerInfos.Add(ownerInfo);
        _serializer.ToCSV(FilePath, OwnerInfos);
    }

    public void Update(OwnerInfo ownerInfo)
    {
        OwnerInfo oldOwnerInfo = OwnerInfos.FirstOrDefault(owner => owner.OwnerId == ownerInfo.OwnerId);

        if (oldOwnerInfo == null)
        {
            return;
        }

        oldOwnerInfo.OwnerId = ownerInfo.OwnerId;
        oldOwnerInfo.IsSuperOwner = ownerInfo.IsSuperOwner;
        oldOwnerInfo.Accommodations = ownerInfo.Accommodations;
        oldOwnerInfo.Reservations = ownerInfo.Reservations;
        oldOwnerInfo.NumberOfAccommodations = ownerInfo.NumberOfAccommodations;
        oldOwnerInfo.AverageReviewScore = ownerInfo.AverageReviewScore;
        oldOwnerInfo.NumberOfReviews = ownerInfo.NumberOfReviews;

        _serializer.ToCSV(FilePath, OwnerInfos);
    }

    public void Delete(int id)
    {
        OwnerInfo ownerInfo = OwnerInfos.FirstOrDefault(owner => owner.OwnerId == id);

        if (ownerInfo == null)
        {
            return;
        }

        OwnerInfos.Remove(ownerInfo);
        _serializer.ToCSV(FilePath, OwnerInfos);
    }

    public List<OwnerInfo> GetAll()
    {
        return OwnerInfos;
    }
}
