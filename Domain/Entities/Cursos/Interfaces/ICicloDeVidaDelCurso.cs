namespace PlataformaDeGestionDeCursosOnline.Domain.Entities.Cursos;

public interface ICicloDeVidaDelCurso
{
    public void IniciarCurso(Curso curso);
    
    public void FinalizarCurso(Curso curso);
}