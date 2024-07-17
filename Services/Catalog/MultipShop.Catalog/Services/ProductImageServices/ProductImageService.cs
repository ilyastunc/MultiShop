using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
    public class ProductImageService : IProductImageService
    {
        private readonly IMongoCollection<ProductImage> _productImageCollection;
        private readonly IMapper _mapper;

        public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName);
            _mapper = mapper;
        }

        public async Task CreateProductImageAsync(CreateProductImageDto productImageDto)
        {
            var productImage = _mapper.Map<ProductImage>(productImageDto);
            await _productImageCollection.InsertOneAsync(productImage);
        }

        public async Task DeleteProductImageAsync(string id)
        {
            await _productImageCollection.DeleteOneAsync(x => x.ProductImageId == id);
        }

        public async Task<List<ResultProductImageDto>> GetAllProductImagesAsync()
        {
            var productImages = await _productImageCollection.Find<ProductImage>(x => true).ToListAsync();
            return _mapper.Map<List<ResultProductImageDto>>(productImages);
        }

        public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
        {
            var productImage = await _productImageCollection.Find<ProductImage>(x => x.ProductImageId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(productImage);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto productImageDto)
        {
            var productImage = _mapper.Map<ProductImage>(productImageDto);
            await _productImageCollection.FindOneAndReplaceAsync(x => x.ProductImageId == productImage.ProductImageId, productImage);
        }
    }
}
