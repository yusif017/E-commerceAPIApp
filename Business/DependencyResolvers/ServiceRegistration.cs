﻿using AutoMapper;
using Business.Abstract;
using Business.AutoMapper;
using Business.Concrete;
using Core.Utilities.MailHelper;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers
{
    public static class ServiceRegistration
    {
        public static void Run(this IServiceCollection service)
        {
             service.AddScoped<AppDbContext>();

            service.AddScoped<ICategoryDAL, EFCategoryDAL>();
            service.AddScoped<ICategoryService, CategoryManager>();

            service.AddScoped<IProductDAL, EFProductDAL>();
            service.AddScoped<IProductService, ProductManager>();

            service.AddScoped<IOrderDAL, EFOrderDAL>();
            service.AddScoped<IOrderService, OrderManager>();

            service.AddScoped<IOrderDAL, EFOrderDAL>();
            service.AddScoped<IOrderService, OrderManager>();

            service.AddScoped<ISpecificationDAL, EFSpecificationDAL>();
            service.AddScoped<ISpecificationService, SpecificationManager>();

            service.AddScoped<IUserDAL, EFUserDAL>();
            service.AddScoped<IUserService, UserManager>();

            service.AddScoped<IWishListDAL, EFWishListDAL>();
            service.AddScoped<IWishListService, WishListManager>();

            service.AddScoped<IEmailHelper, EmailHelper>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            });

            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);
        }
    }
}
