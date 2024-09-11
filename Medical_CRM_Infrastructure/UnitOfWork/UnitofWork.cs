using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Interfaces;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.UnitOfWork;
using Medical_CRM_Infrastructure.Context;
using Medical_CRM_Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Medical_CRM_Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;

            // Repository'leri başlat
            Patients = new PatientRepository(_context);
            Appointments = new AppointmentRepository(_context);
            Procedures = new ProcedureRepository(_context);
            Products = new ProductRepository(_context);
            Payments = new PaymentRepository(_context);
            StockTransactions = new StockTransactionRepository(_context);
            Users = new UserRepository(_context);
            Operators = new OperatorRepository(_context);
            ProcedureProducts = new ProcedureProductRepository(_context);
            

        }

        public IPatientRepository Patients { get; }
        public IAppointmentRepository Appointments { get; }
        public IProcedureRepository Procedures { get; }
        public IProductRepository Products { get; }
        public IPaymentRepository Payments { get; }
        public IStockTransactionRepository StockTransactions { get; }
        public IUserRepository Users { get; }

        public IOperatorRepository Operators { get; }
        public IProcedureProductRepository ProcedureProducts { get; }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            if (_context.Database.CurrentTransaction != null)
            {
                await _context.Database.CurrentTransaction.RollbackAsync();
            }
        }

        public void DetachEntity<T>(T entity) where T : class
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
