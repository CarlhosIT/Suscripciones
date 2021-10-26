using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAutores.DTOs;

namespace WebApiAutores.Controllers
{
    public class FacturasController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public FacturasController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<ActionResult> Pagar(PagarFacturaDTO pagarFacturaDTO)
        {
            var facturaDB = await context.Facturas
                                .Include(x => x.Usuario)
                                .FirstOrDefaultAsync(x => x.Id == pagarFacturaDTO.FacturaId);

            if (facturaDB == null)
            {
                return NotFound();
            }

            if (facturaDB.Pagada)
            {
                return BadRequest("La factura ya fue saldada");
            }

            // Lógica para pagar la factura
            // nosotros vamos a pretender que el pago fue exitoso

            facturaDB.Pagada = true;
            await context.SaveChangesAsync();

            var hayFacturasPendientesVencidas = await context.Facturas
                .AnyAsync(x => x.UsuarioId == facturaDB.UsuarioId &&
                !x.Pagada && x.FechaLimiteDePago < DateTime.Today);

            if (!hayFacturasPendientesVencidas)
            {
                facturaDB.Usuario.MalaPaga = false;
                await context.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
