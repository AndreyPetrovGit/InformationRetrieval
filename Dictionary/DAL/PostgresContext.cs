//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.EntityFrameworkCore;

//namespace Dictionary.DAL
//{
//    class PostgresContext: DbContext
//    {
//        public DbSet<Genre> genres { get; set; }
//        //dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=movies;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL
//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=password");

//        } 
//    }
//}
