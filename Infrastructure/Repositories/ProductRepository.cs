﻿using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Producto>, IProductoRepository
    {
        //protected readonly TiendaContext _context;
        public ProductRepository(TiendaContext context):base(context)
        {
           // _context = context;

        }
        public async Task<IEnumerable<Producto>> GetProductosMasCaros(int cantidad)=>
            await _context.Productos.OrderByDescending(_ => _.Precio).Take(cantidad).ToListAsync();
           
        
    }
}