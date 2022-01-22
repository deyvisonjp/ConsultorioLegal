using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelView;

namespace CL.Manager.Mappings
{
    public class AlteraClienteMappingProfile : Profile
    {
        public AlteraClienteMappingProfile()
        {
            CreateMap<AlteraCliente, Cliente>()
               .ForMember(dest => dest.UltimaAtualizacao, options => options.MapFrom(c => DateTime.Now))
               .ForMember(d => d.DataNascimento, o => o.MapFrom(x => x.DataNascimento.Date));
        }
    }
}
