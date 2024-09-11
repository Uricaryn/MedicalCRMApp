using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Medical_CRM_Domain.DTOs.AppointmentDTOs;
using Medical_CRM_Domain.DTOs.OperatorDTOs;
using Medical_CRM_Domain.DTOs.PatientDTOs;
using Medical_CRM_Domain.DTOs.PaymentDTOs;
using Medical_CRM_Domain.DTOs.ProcedureDTOs;
using Medical_CRM_Domain.DTOs.ProcedureProductDTOs;
using Medical_CRM_Domain.DTOs.ProductDTOs;
using Medical_CRM_Domain.DTOs.StockTransactionDTOs;
using Medical_CRM_Domain.DTOs.UserDTOs;
using Medical_CRM_Domain.Entities;

namespace Medical_CRM_Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Patient Mappings
            CreateMap<Patient, PatientGetDto>();
            CreateMap<PatientCreateDto, Patient>();
            CreateMap<PatientUpdateDto, Patient>();

            // Appointment Mappings
            CreateMap<Appointment, AppointmentGetDto>();
            CreateMap<AppointmentCreateDto, Appointment>();
            CreateMap<AppointmentUpdateDto, Appointment>();

            // Procedure Mappings
            CreateMap<Procedure, ProcedureGetDto>()
                .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.ProcedureProducts.Select(pp => new ProductGetDto
                {
                    Id = pp.ProductId,
                    Name = pp.Product.Name,
                })));

            // Mapping for creating Procedure with ProcedureProducts
            CreateMap<ProcedureCreateDto, Procedure>()
                .ForMember(dest => dest.ProcedureProducts, opt => opt.MapFrom(src => src.Products.Select(p => new ProcedureProduct
                {
                    ProductId = p.ProductId,
                    QuantityUsed = p.QuantityUsed
                })));

            // Mapping for updating Procedure with ProcedureProducts
            CreateMap<ProcedureUpdateDto, Procedure>()
                .ForMember(dest => dest.ProcedureProducts, opt => opt.MapFrom(src => src.Products.Select(p => new ProcedureProduct
                {
                    ProcedureProductId = p.ProcedureProductId, // Only map if necessary for updates
                    ProductId = p.ProductId,
                    QuantityUsed = p.QuantityUsed
                })));

            // ProcedureProduct Mapping
            CreateMap<ProcedureProductDto, ProcedureProduct>()
                .ForMember(dest => dest.ProcedureProductId, opt => opt.Ignore()) // Ignore ProcedureProductId during creation
                .ForMember(dest => dest.ProcedureId, opt => opt.Ignore()); // Ignore ProcedureId during creation

            CreateMap<ProcedureProductUpdateDto, ProcedureProduct>();

            // Product Mappings
            CreateMap<Product, ProductGetDto>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();

            // Payment Mappings
            CreateMap<Payment, PaymentGetDto>();
            CreateMap<PaymentCreateDto, Payment>();
            CreateMap<PaymentUpdateDto, Payment>();

            // StockTransaction Mappings
            CreateMap<StockTransaction, StockTransactionGetDto>();
            CreateMap<StockTransactionCreateDto, StockTransaction>();
            CreateMap<StockTransactionUpdateDto, StockTransaction>();

            // User Mappings
            CreateMap<User, UserGetDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            CreateMap<User, UserGetDto>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

            // Operator Mappings
            CreateMap<Operator, OperatorGetDto>();
            CreateMap<OperatorCreateDto, Operator>();
            CreateMap<OperatorUpdateDto, Operator>();
        }

    private static List<Payment> MapPayments(PaymentCreateDto paymentDto)
        {
           
            return paymentDto != null ? new List<Payment> { new Payment {
                Amount = paymentDto.Amount,
                PaymentDate = paymentDto.PaymentDate,
                PaymentMethod = paymentDto.PaymentMethod
            } } : new List<Payment>();
        }
    }
}
