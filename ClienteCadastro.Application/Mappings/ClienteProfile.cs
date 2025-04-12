using AutoMapper;
using ClienteCadastro.Application.DTOs;
using ClienteCadastro.Domain.Entities;

namespace ClienteCadastro.Application.Mappings;

public class ClienteProfile : Profile
{
    public ClienteProfile()
    {
        CreateMap<Cliente, ClienteDto>()
            .ForMember(dest => dest.Logradouro, opt => opt.MapFrom(src => src.Endereco.Logradouro))
            .ForMember(dest => dest.Bairro, opt => opt.MapFrom(src => src.Endereco.Bairro))
            .ForMember(dest => dest.Cidade, opt => opt.MapFrom(src => src.Endereco.Cidade))
            .ForMember(dest => dest.Uf, opt => opt.MapFrom(src => src.Endereco.Estado));

        CreateMap<CreateClienteDto, Cliente>();
        CreateMap<UpdateClienteDto, Cliente>();
    }
}
