using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.OperatorDTOs;
using Medical_CRM_Domain.Entities;
using Medical_CRM_Domain.Services;
using Medical_CRM_Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Medical_CRM_Application.Services
{
    public class OperatorService : IOperatorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OperatorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<OperatorGetDto>> GetAllOperatorsAsync()
        {
            try
            {
                var operators = await _unitOfWork.Operators.GetAllAsync();

                // Check if the data is retrieved correctly before mapping
                if (operators == null)
                {
                    throw new Exception("No operators were found in the database.");
                }

                return _mapper.Map<IEnumerable<OperatorGetDto>>(operators);
            }
            catch (DbUpdateException dbEx)
            {
                // Log database-specific errors here, such as connectivity issues
                throw new Exception("Database error occurred while retrieving operators.", dbEx);
            }
            catch (AutoMapperMappingException mapEx)
            {
                // Log AutoMapper-specific errors
                throw new Exception("Mapping error occurred while retrieving operators.", mapEx);
            }
            catch (Exception ex)
            {
                // Log general exceptions
                throw new Exception("An error occurred while retrieving operators.", ex);
            }
        }

        public async Task<OperatorGetDto> GetOperatorByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid operator ID.");
            }

            try
            {
                var operatorEntity = await _unitOfWork.Operators.GetByIdAsync(id);
                if (operatorEntity == null)
                {
                    throw new KeyNotFoundException("Operator not found.");
                }

                return _mapper.Map<OperatorGetDto>(operatorEntity);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new Exception("An error occurred while retrieving the operator.", ex);
            }
        }

        public async Task<OperatorGetDto> GetOperatorByCodeAsync(string operatorCode)
        {
            if (string.IsNullOrWhiteSpace(operatorCode))
            {
                throw new ArgumentException("Operator code cannot be null or empty.");
            }

            try
            {
                var operatorEntity = await _unitOfWork.Operators.GetOperatorByCodeAsync(operatorCode);
                if (operatorEntity == null)
                {
                    throw new KeyNotFoundException("Operator not found with the given code.");
                }

                return _mapper.Map<OperatorGetDto>(operatorEntity);
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new Exception("An error occurred while retrieving the operator by code.", ex);
            }
        }

        public async Task CreateOperatorAsync(OperatorCreateDto operatorCreateDto)
        {
            if (operatorCreateDto == null)
            {
                throw new ArgumentNullException(nameof(operatorCreateDto), "Operator data cannot be null.");
            }

            try
            {
                var operatorEntity = _mapper.Map<Operator>(operatorCreateDto);
                operatorEntity.Id = Guid.NewGuid();

                await _unitOfWork.Operators.AddAsync(operatorEntity);
                await _unitOfWork.CommitAsync();
            }
            catch (DbUpdateException dbEx)
            {
                // Log database-specific errors here, such as constraint violations
                throw new Exception("Database error occurred while creating the operator.", dbEx);
            }
            catch (AutoMapperMappingException mapEx)
            {
                // Log AutoMapper-specific errors
                throw new Exception("Mapping error occurred while creating the operator.", mapEx);
            }
            catch (Exception ex)
            {
                // Log general exceptions
                throw new Exception("An error occurred while creating the operator.", ex);
            }
        }

        public async Task UpdateOperatorAsync(Guid id, OperatorUpdateDto operatorUpdateDto)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid operator ID.");
            }

            if (operatorUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(operatorUpdateDto), "Operator data cannot be null.");
            }

            try
            {
                var existingOperator = await _unitOfWork.Operators.GetByIdAsync(id);
                if (existingOperator == null)
                {
                    throw new KeyNotFoundException("Operator not found.");
                }

                _mapper.Map(operatorUpdateDto, existingOperator); // Update the existing entity with new values

                await _unitOfWork.Operators.UpdateAsync(existingOperator);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new Exception("An error occurred while updating the operator.", ex);
            }
        }

        public async Task DeleteOperatorAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid operator ID.");
            }

            try
            {
                var operatorEntity = await _unitOfWork.Operators.GetByIdAsync(id);
                if (operatorEntity == null)
                {
                    throw new KeyNotFoundException("Operator not found.");
                }

                await _unitOfWork.Operators.DeleteAsync(operatorEntity);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Log the exception here
                throw new Exception("An error occurred while deleting the operator.", ex);
            }
        }
    }
}
