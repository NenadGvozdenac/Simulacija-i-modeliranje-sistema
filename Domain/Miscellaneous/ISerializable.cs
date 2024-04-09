namespace BookingApp.Domain.Miscellaneous
{
    public interface ISerializable
    {
        string[] ToCSV();
        void FromCSV(string[] values);
    }
}
