using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace Medical_CRM_Domain.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        // Repository'ler
        IPatientRepository Patients { get; }
        IAppointmentRepository Appointments { get; }
        IProcedureRepository Procedures { get; }
        IProductRepository Products { get; }
        IPaymentRepository Payments { get; }
        IStockTransactionRepository StockTransactions { get; }
        IUserRepository Users { get; }
        IOperatorRepository Operators { get; }
        IProcedureProductRepository ProcedureProducts { get; }

        void DetachEntity<T>(T entity) where T : class;
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<int> CommitAsync();
    }
}
