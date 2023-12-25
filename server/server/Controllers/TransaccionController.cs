using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Dtos;
using server.Model;
using server.Responses;

//pdf
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.PdfSharp;
using System.Text;

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

    [HttpGet, Authorize]
    public IActionResult Get()
    {
      var rol = User.FindFirst("RoleName").Value;
      if (rol == "Super Administrador")
      {
        var transaccion = _db.Transaccions.Where(x => x.IdcuentaNavigation.Tipo == "Ingreso").Select(t => new TransaccionResponse
        {
          NombreEmpresa = t.IdcajaNavigation.IdempresaNavigation.Nombre,
          NombreUsuario = t.IdusuarioNavigation.NombreUsuario,
          Total = t.Montototal,
          Cantidad = t.Cantidad,
          Extra = t.Extra,
          TipoEntrega = t.Tipoentrega,
          Fecha = t.Fecha
        }).ToList();
        return Ok(new { Message = "Lista de ingresos", Data = transaccion, Status = 200 });
      }
      else
      {
        var idEmpresa = User.FindFirst("CompanyId").Value;
        var transaccion = _db.Transaccions.Where(x => x.IdcuentaNavigation.Tipo == "Ingreso" && x.IdcajaNavigation.Idempresa == Guid.Parse(idEmpresa) && x.IdcuentaNavigation.Idempresa == Guid.Parse(idEmpresa)).Select(t => new TransaccionResponse
        {
          NombreEmpresa = t.IdcajaNavigation.IdempresaNavigation.Nombre,
          NombreUsuario = t.IdusuarioNavigation.NombreUsuario,
          Total = t.Montototal,
          Cantidad = t.Cantidad,
          Extra = t.Extra,
          TipoEntrega = t.Tipoentrega,
          Fecha = t.Fecha
        }).ToList();
        return Ok(new { Message = "Lista de ingresos", Data = transaccion, Status = 200 });
      }
    }

    [HttpPost("ComprarTicket"), Authorize]
    public IActionResult Post([FromBody] CarritoDTO req)
    {
      if (req.Items.Count == 0)
      {
        return BadRequest(new { Message = "Debe seleccionar al menos un ticket", Data = ' ', Status = 400 });
      }
      var idEmpresa = User.FindFirst("CompanyId").Value;
      var dataEmpresa = _db.Empresas.Find(Guid.Parse(idEmpresa));
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
      for (int i = 0; i < req.Items.Count; i++)
      {
        var entrada = _db.TipoEntrada.Where(te => te.Id == req.Items[i].IdEntrada).Join(
          _db.TipoEventos,
          te => te.Idtipoevento,
          t => t.Id,
          (te, t) => new
          {
            Nombre = te.Nombre,
            NombreEvento = t.Nombre,
            Costo = te.Costo
          }
        ).First();
        for (int j = 0; j < req.Items[i].Cantidad; j++)
        {
          var detalle = new DetalleTransaccione
          {
            Idtransaccion = transaccion.Id,
            Detalle = entrada.NombreEvento + " (" + entrada.Nombre + ")",
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

    [Authorize, HttpGet("GeneratePdf/{transactionId}")]
    public IActionResult GeneratePdf(Guid transactionId)
    {
      var transaccionsInfo = _db.DetalleTransacciones.Where(v => v.Idtransaccion == transactionId);

      var transaccionInfo = _db.Transaccions.FirstOrDefault(v => v.Id == transactionId);

      if (transaccionInfo != null)
      {

        var company = _db.Empresas
          .FirstOrDefault(v => v.Cajas.First().Id == transaccionInfo.Idcaja);


        var document = new PdfDocument();

        StringBuilder HtmlContent = new StringBuilder();

        HtmlContent.Append("<body> ");
        HtmlContent.Append("<h1 style='text-align:center;'>" + company.Nombre + "</h1>");

        foreach (DetalleTransaccione val in transaccionsInfo)
        {
          HtmlContent.Append("<p style='text-align:center;'>" + val.Detalle + "</p>");
          HtmlContent.Append("<p>" + val.Id + "</p>");
        }

        HtmlContent.Append("</body>");

        PdfGenerator.AddPdfPages(document, HtmlContent.ToString(), PageSize.A4);
        byte[]? response = null;
        using (MemoryStream ms = new MemoryStream())
        {
          document.Save(ms);
          response = ms.ToArray();
        }
        string fileName = "transaccion" + transactionId + ".pdf";
        return File(response, "application/pdf", fileName);
      }

      return NotFound(new { Message = "No se encontraron datos", Data = " ", Status = 404 });
    }

  }
}


