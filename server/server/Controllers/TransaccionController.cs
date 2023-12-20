using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;

namespace server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TransaccionController : Controller
  {
    private DBContext _db;
    //constructor inica lo primero del sistema
    public TransaccionController(DBContext db)
    {
      _db = db;
    }

    [HttpPost("ComprarTicket"), Authorize]
    public IActionResult Post([FromBody] CarritoDTO req)
    {
      if (req.Items.Count == 0)
      {
        return BadRequest(new { Message = "Debe seleccionar al menos un ticket", Data = ' ', Status = 400 });
      }
      var idEmpresa = User.FindFirst("CompanyId").Value;
      var dataEmpresa = _db.Empresas.Find(idEmpresa);
      var cuentaTicket = _db.Cuenta.Where(c => c.Idempresa == Guid.Parse(idEmpresa) && c.Nombre == "Venta entradas").First();
      var cajaTicket = _db.Cajas.Where(c => c.Idempresa == Guid.Parse(idEmpresa) && c.Nombre == "Caja virtual").First();
      var suma = 0.0;
      var cantidad = 0;
      for (int i = 0; i < req.Items.Count; i++)
      {
        var entrada = _db.TipoEntrada.Find(req.Items[i].IdEntrada);
        cantidad += req.Items[i].Cantidad;
        suma += req.Items[i].Cantidad * entrada.Costo;
        if (entrada.Stock - req.Items[i].Cantidad < 0)
        {
          return BadRequest(new { Message = "A la entrada " + entrada.Nombre + "solo le quedan " + entrada.Stock + " entradas", Data = ' ', Status = 400 });
        }
        entrada.Stock -= req.Items[i].Cantidad;
      }
      var transaccion = new Transaccion
      {
        Idcuenta = cuentaTicket.Id,
        Idusuario = req.IdUsuario,
        Idcaja = cajaTicket.Id,
        Montototal = suma,
        Cantidad = cantidad,
        Tipoentrega = "En línea",
        Fecha = DateTime.Now.ToString("yyyy-MM-dd")
      };

      _db.Transaccions.Add(transaccion);

      _db.SaveChanges();

      List<DetalleTransaccione> detalles = new List<DetalleTransaccione>();
      for (int i = 0; i < req.Items.Count; i++){
        var entrada = _db.TipoEntrada.Find(req.Items[i].IdEntrada);
        for (int j = 0; j < req.Items[i].Cantidad; j++){
          var detalle = new DetalleTransaccione{
            Idtransaccion = transaccion.Id,
            Detalle = entrada.IdtipoeventoNavigation.Nombre + " (" + entrada.Nombre +")",
            Preciounitario = entrada.Costo,
            Ci = req.Items[i].Ci[j]
          };
          detalles.Add(detalle);
        }
      }

      _db.DetalleTransacciones.AddRange(detalles);
      _db.SaveChanges();

      return Ok(new { Message = "Compra realizada", Data = transaccion, Status = 201 });
    }
  }
}


