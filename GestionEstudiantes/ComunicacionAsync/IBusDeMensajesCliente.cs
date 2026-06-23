using GestionEstudiantes.DTO;

namespace GestionEstudiantes.ComunicacionAsync
{
    public interface IBusDeMensajesCliente
    {
        void PublicarNuevoEstudiante(EstudianteProducerDTO estudiante);
    }
}
