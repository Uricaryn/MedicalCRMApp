using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Medical_CRM_Domain.DTOs.OperatorDTOs;

namespace Medical_CRM_Domain.Services
{
    public interface IOperatorService
    {
        Task<IEnumerable<OperatorGetDto>> GetAllOperatorsAsync();
        Task<OperatorGetDto> GetOperatorByIdAsync(Guid id);
        Task<OperatorGetDto> GetOperatorByCodeAsync(string operatorCode);
        Task CreateOperatorAsync(OperatorCreateDto operatorCreateDto);
        Task UpdateOperatorAsync(Guid id, OperatorUpdateDto operatorUpdateDto);
        Task DeleteOperatorAsync(Guid id);
    }
}
