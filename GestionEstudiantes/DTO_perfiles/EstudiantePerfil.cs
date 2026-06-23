using AutoMapper;
using GestionEstudiantes.DTO;
using GestionEstudiantes.Models;
namespace GestionEstudiantes.DTO_perfiles
{
    public class EstudiantePerfil : Profile
    {
        public EstudiantePerfil()
        {
            CreateMap<Estudiante, EstudianteReadDTO>();// ---> De Estudiante a EstudianteReadDTO
            CreateMap<EstudianteCreateDTO, Estudiante>();// ---> De EstudianteCreateDTO a Estudiante
            CreateMap<EstudianteUpdateDTO, Estudiante>();
            CreateMap<Estudiante,EstudianteUpdateDTO>();
            CreateMap<EstudianteReadDTO, EstudianteProducerDTO>();// ---> De EstudianteReadDTO a EstudianteProducerDTO
        }
    }
}
