using AutoMapper;
using MavericksBank.DTOs;
using MavericksBank.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MavericksBank.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Account, AccountDto>().ReverseMap();
            CreateMap<Beneficiary, BeneficiaryDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Loan, LoanDto>().ReverseMap();
            CreateMap<LoanApplication, LoanApplicationDto>().ReverseMap();
        }
    }
}