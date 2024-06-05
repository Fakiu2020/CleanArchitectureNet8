using Domain.Interfaces;
using Domain.Entities;
namespace Application.Services
{
    public class AssetService : IAssetService
    {
        private readonly IAssetRepository _assetRepository;

        public AssetService(IAssetRepository AssetRepository)
        {
            _assetRepository = AssetRepository;
        }

        public async Task<Asset> GetAsync(int id)
        {
            return await _assetRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            return await _assetRepository.GetAllAsync();
        }

        public async Task AddAsync(Asset asset)
        {
            await _assetRepository.Add(asset);
            await _assetRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Asset asset)
        {
            _assetRepository.Update(asset);
            await _assetRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var asset = await _assetRepository.GetByIdAsync(id);
            if (asset != null)
            {
                _assetRepository.Delete(asset);
                await _assetRepository.SaveChangesAsync();
            }
        }

        public async Task<Asset?> GetAssetByNameAsync(string assetName)
        {
            var assets = await _assetRepository.FindAsync(a => a.Ticker.ToUpper() == assetName.ToUpper());
            return assets.FirstOrDefault();
        }
    }
}
