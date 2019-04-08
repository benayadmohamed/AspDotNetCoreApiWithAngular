using System;
using BackEnd.Data;
using BackEnd.Models;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using (var dbContext = new MyDbContext())
            {
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new AppUser {Username = "u1", Password = "0000"});
                dbContext.Users.Add(new AppUser {Username = "u2", Password = "0000"});
                dbContext.SaveChanges();
            }
        }
    }
}