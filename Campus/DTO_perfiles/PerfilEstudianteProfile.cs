using AutoMapper;
using Campus.DTO;
using Campus.Models;

namespace Campus.DTO_perfiles
{
    public class PerfilEstudianteProfile : Profile
    {
        public PerfilEstudianteProfile()
        {
            CreateMap<Estudiante, EstudianteReadDTO>();
            CreateMap<Perfil, PerfilReadDTO>();
            CreateMap<PerfilCreateDTO, Perfil>();
            CreateMap<EstudianteProducerDTO,Estudiante>()
                .ForMember(dest => dest.fId, opt => opt.MapFrom(f => f.Id));
        }
    }
}
