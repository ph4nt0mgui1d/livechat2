﻿using System;
using Microsoft.EntityFrameworkCore;

namespace livechat2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){
        }
         
    }
}

