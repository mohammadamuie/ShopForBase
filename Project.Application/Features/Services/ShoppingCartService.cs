using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Domain.Entities.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Application.Features.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductRepository _productRepository;

        
    }


}
