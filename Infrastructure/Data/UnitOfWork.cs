using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext; //dependency injection

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IGenericRepository<Client> _Client;
        private IGenericRepository<ClientResponse> _ClientResponse;
        private IGenericRepository<Friend> _Friend;
        private IGenericRepository<FriendResponse> _FriendResponse;
        private IGenericRepository<Adjective> _Adjective;
        //private IGenericRepository<ApplicationUser> _ApplicationUser;

        public IGenericRepository<Client> Client
        {
            get
            {
                if (_Client == null) _Client = new GenericRepository<Client>(_dbContext);
                return _Client;
            }
        }

        public IGenericRepository<ClientResponse> ClientResponse
        {
            get
            {
                if (_ClientResponse == null) _ClientResponse = new GenericRepository<ClientResponse>(_dbContext);
                return _ClientResponse;
            }
        }

        public IGenericRepository<Friend> Friend
        {
            get
            {
                if (_Friend == null) _Friend = new GenericRepository<Friend>(_dbContext);
                return _Friend;
            }
        }

        public IGenericRepository<FriendResponse> FriendResponse
        {
            get
            {
                if (_FriendResponse == null) _FriendResponse = new GenericRepository<FriendResponse>(_dbContext);
                return _FriendResponse;
            }
        }

        public IGenericRepository<Adjective> Adjective
        {
            get
            {
                if (_Adjective == null) _Adjective = new GenericRepository<Adjective>(_dbContext);
                return _Adjective;
            }
        }

        //public IGenericRepository<ApplicationUser> ApplicationUser
        //{
        //    get
        //    {
        //        if (_ApplicationUser == null) _ApplicationUser = new GenericRepository<ApplicationUser>(_dbContext);
        //        return _ApplicationUser;
        //    }
        //} //if someone is not logged in this makes the object to use during the session

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose() => _dbContext.Dispose();
    }
}
