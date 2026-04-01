using AutoMapper;
using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models;
using Ecommerce.Core.Services;
using EcommerceInfraStructure.Data;
using EcommerceInfraStructure.Repositries.Service;
using Microsoft.AspNetCore.Identity;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure.Repositries
{
    public class UnitOFwork : IUniOfwork
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;
        private readonly UserManager<AppUser> _userManager;
        private readonly IImageManagementService _imageManagementService;
        private readonly IEmailService _emailService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IGenerateToken token;

        public IPhotoRepository photoRepository {  get;}

        public ICategoryRepository categoryRepository {  get;}

        public IProductRepository productRepository {  get;}

        public ICustomerBasketRepository customerBasketRepository { get; }

        public IAutho Autho { get; }

        public UnitOFwork(AppDbContext context, IImageManagementService imageManagementService, IMapper mapper, IConnectionMultiplexer redis, UserManager<AppUser> userManager, IEmailService emailService, SignInManager<AppUser> signInManager, IGenerateToken token)
        {
            _context = context;
            _imageManagementService = imageManagementService;
            _mapper = mapper;
            _redis = redis;
            _userManager = userManager;
            _emailService = emailService;
            _signInManager = signInManager;
            this.token = token;
            photoRepository = new PhotoRepository(_context);
            categoryRepository = new CategoryRepository(_context);
            productRepository = new ProductRepository(_context, _mapper, _imageManagementService);
            customerBasketRepository = new CustomerBasketRepository(_redis);
            Autho = new AuthRepository(userManager, emailService, signInManager, token, context);
          
        }
    }
}
