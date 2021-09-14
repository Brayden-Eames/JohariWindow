using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<ClientResponse> ClientResponse { get; set; }
        public DbSet<Friend> Friend { get; set; }
        public DbSet<FriendResponse> FriendResponse { get; set; }
        public DbSet<Adjective> Adjective { get; set; }
    }
}
