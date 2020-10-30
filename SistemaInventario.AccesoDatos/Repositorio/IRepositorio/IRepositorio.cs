using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SistemaInventario.SistemaInventario.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        //este metodo devuleve una bodega por medio del Id
        T Obtener(int id);

        //este metodo devuelve una lista de bodegas
        //la primera expresion es para colocar un filtro
        //la segunda es una funcion para establecer el ordenamiento
        //por ultimo colocamos un nombre
        IEnumerable<T> ObtenerTodos(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null
            );

        //este metodo retorna el primer elemento encontrado
        T ObtenerPrimero(
            Expression<Func<T, bool>> filter = null,
            string incluirPropiedades = null
            );

        //metodo agregar 
        void Agregar(T entidad);

        void Remover(int id);

        //remover por toda la entidad
        void Remover(T entidad);

        void RemoverRango(IEnumerable<T> entidad);
    }
}
