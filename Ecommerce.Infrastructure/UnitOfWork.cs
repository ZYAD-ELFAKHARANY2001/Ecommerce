using Ecommerce.Application.Contracts;
using Ecommerce.Context;
using Ecommerce.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Infrastructure
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly EcommerceContext _jumiaContext;
        private readonly ILogger _logger; // Assuming you have a logging mechanism
        private Hashtable _repositories;

        public UnitOfWork(EcommerceContext jumiaContext, ILoggerFactory loggerFactory)
        {
            _jumiaContext = jumiaContext;
            _logger = loggerFactory.CreateLogger("UnitOfWorkLogs");
            _repositories = new Hashtable();
        }
        public IInoviceDataRepository? InvoiceRepository => GetRepository<IInoviceDataRepository, InvoiceDetailRepository>();
        public ICashierRepository? CashierRepository => GetRepository<ICashierRepository, CashierRepository>();
        public IRepository<User,string> UserRepository { get; private set; }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _jumiaContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred when saving changes");
                // Depending on your error handling policy, you might want to rethrow, handle or log the exception
                throw; // Rethrow the exception if you want calling code to handle it
            }
        }

        public void Dispose()
        {
            _jumiaContext?.Dispose();
        }

        private TRepository GetRepository<TRepositoryInterface, TRepository>() where TRepository : TRepositoryInterface
        {
            var type = typeof(TRepository).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInstance = Activator.CreateInstance(typeof(TRepository), _jumiaContext);
                _repositories.Add(type, repositoryInstance);
            }

            return (TRepository)_repositories[type];
        }
    }
}
