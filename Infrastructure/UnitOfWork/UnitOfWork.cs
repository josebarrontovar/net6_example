using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly TiendaContext _context;
        private IProductoRepository _productos;
        private IMarcaRepository _marcas;
        private ICategoriaRepository _categoria;
        public UnitOfWork(TiendaContext context)
        {
            _context = context;
        }
        public IProductoRepository Productos
        {
            get
            {
                if (_productos is null)
                {
                    _productos = new ProductRepository(_context);
                }
                return _productos;
            }
        }

        public IMarcaRepository Marca
        {
            get
            {
                if (_marcas is null)
                {
                    _marcas = new MarcaRepository(_context);
                }
                return _marcas;
            }
        }

        public ICategoriaRepository Categoria
        {
            get
            {
                if(_categoria is null)
                {
                    _categoria = new CategoriaRepository(_context);
                }
                return _categoria;
            }
        }

        public int Save()
        {
           return _context.SaveChanges();
        }
       
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
