

using Domain.Entities;

namespace Application.Services
{
    public interface IAssetService
    {
        Task<Asset> GetAsync(int id);
        Task<IEnumerable<Asset>> GetAllAsync();
        Task AddAsync(Asset asset);
        Task UpdateAsync(Asset asset);
        Task RemoveAsync(int id);

        Task<Asset?> GetAssetByNameAsync(string assetName);

    }
}
