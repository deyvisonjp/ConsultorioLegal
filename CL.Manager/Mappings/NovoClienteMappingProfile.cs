using AutoMapper;
using CL.Core.Domain;
using CL.Core.Shared.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CL.Manager.Mappings
{
    public class NovoClienteMappingProfile: Profile
    {
        public NovoClienteMappingProfile()
        {
            //Faz as transposição dos dados
            CreateMap<NovoCliente, Cliente>()
                .ForMember(dest => dest.Criacao, options => options.MapFrom(c => DateTime.Now))
                .ForMember(d => d.DataNascimento, o => o.MapFrom(x => x.DataNascimento.Date));
        }
    }
}
