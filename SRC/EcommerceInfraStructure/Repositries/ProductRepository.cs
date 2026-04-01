using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models.Product;
using Ecommerce.Core.Services;
using Ecommerce.Core.Sharing;
using EcommerceInfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure.Repositries
{
    public class ProductRepository : GenericRepositories<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IImageManagementService _imageManagementService;
        public ProductRepository(AppDbContext context, IMapper mapper, IImageManagementService imageManagementService) : base(context)
        {
            this._context = context;
            _mapper = mapper;
            _imageManagementService = imageManagementService;
        }

        public async Task<IEnumerable<ProductDTO>>GetAllAsync(ProductParameters productParameters)
        {
            var Query = _context.Products.Include(p=>p.category).Include(p => p.Photos).AsNoTracking();

            // Filtering  by word
            if (!string.IsNullOrEmpty(productParameters.Search))
            {
                var SearchWords=productParameters.Search.Split(' ');
                Query = Query.Where(p => SearchWords.All(word => p.Name.ToLower().Contains(word.ToLower())||p.Description.ToLower().Contains(word.ToLower())));
            }

            // filtering by categoryID
            if (productParameters.categoryID.HasValue)
            {
                Query=Query.Where(p=>p.CategoryID== productParameters.categoryID.Value);
            }
            // sort by 
            if (!string.IsNullOrEmpty(productParameters.sort))
            {
                Query = productParameters.sort switch
                {
                    "PriceAce" => Query.OrderBy(p => p.NewPrice),
                    "PriceDce" => Query.OrderByDescending(p => p.NewPrice),
                    _ => Query.OrderBy(p => p.Name),
                };
            }

            Query = Query.Skip((productParameters.PageSize) * (productParameters.PageNumber - 1)).Take(productParameters.PageSize);
            var result = _mapper.Map<List<ProductDTO>>(Query);
            return result;
        }
        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO is null) return false;
            var product = _mapper.Map<Product>(productDTO);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            var ImagePaths =await _imageManagementService.AddImageAsync(productDTO.Photos,productDTO.Name);

            var photo = ImagePaths.Select(path => new Photo
            {
                ImageName=path,
                ProductID=product.Id,
            }).ToList();
            await _context.Photos.AddRangeAsync(photo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task DeleteAsync(Product product)
        {
           var Photos=await _context.Photos.Where(ph=>ph.ProductID==product.Id).ToListAsync();
            foreach(var item in Photos)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateproductDTO)
        {
           if(updateproductDTO is null)
            {
                return false;
            }
            var product = await _context.Products.Include(p => p.category).Include(p => p.Photos)
                 .FirstOrDefaultAsync(p => p.Id == updateproductDTO.ProductID);
            if(product is null)
            {
                return false;
            }
            _mapper.Map<UpdateProductDTO>(product);
            var FindPhotos = await _context.Photos.Where(ph => ph.ProductID == updateproductDTO.ProductID).ToListAsync();

            foreach(var item in FindPhotos)
            {
                _imageManagementService.DeleteImageAsync(item.ImageName);
            }
            _context.Photos.RemoveRange(FindPhotos);
            var imagePaths = await _imageManagementService.AddImageAsync(updateproductDTO.Photos, updateproductDTO.Name);
            var photos = imagePaths.Select(path => new Photo
            {
                ImageName=path,
                ProductID=updateproductDTO.ProductID,

            }).ToList();

            await _context.Photos.AddRangeAsync(photos);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
