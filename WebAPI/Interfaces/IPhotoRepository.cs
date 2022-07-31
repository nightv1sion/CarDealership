namespace WebAPI.Interfaces
{
    public interface IPhotoRepository<T> where T : class 
    {
        void DeleteRange(List<T> photos);
        Task AddRangeAsync(List<T> photos);
        Task SaveAsync();
    }
}
