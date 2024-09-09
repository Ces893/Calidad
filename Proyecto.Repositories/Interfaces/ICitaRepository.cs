using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.Models;

namespace Proyecto.Repositories.Interfaces;

public interface ICitaRepository: IRepositoryBase<Cita>
{
    void Actualizar(Cita cita);

    IEnumerable<SelectListItem> ObtenerTodosDropdownLista(string obj);
}
