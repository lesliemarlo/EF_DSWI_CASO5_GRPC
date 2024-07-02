//librerias
using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Proyecto.Presentacion.Models;
using GrpcServiceBdClinica;
using GrpcServiceBdClinica.Protos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Proyecto.Presentacion.Controllers
{
    public class MedicamentoController : Controller
    {
        private Medicamento.MedicamentoClient _cliente;
        private Pais.PaisClient _cliente1;

        // Este método obtiene la lista de medicamentos llamando al servicio gRPC
        public async Task<IActionResult> ListadoMedicamentos()
        {
            var canal = GrpcChannel.ForAddress("http://localhost:5169");
            _cliente = new Medicamento.MedicamentoClient(canal);

            var request = new Empty();
            var mensaje = await _cliente.listadoMedicamentosAsync(request);
            List<MedicamentoModel> aMedicamentos = new List<MedicamentoModel>();

            foreach (var c in mensaje.Items)
            {
                aMedicamentos.Add(new MedicamentoModel()
                {
                    ide_med = c.IdeMed,
                    nom_med = c.NomMed,
                    des_med = c.DesMed,
                    rec_med = c.RecMed,
                    via_med = c.ViaMed,
                    cos_med = c.CosMed,
                    nom_pai = c.NomPai
                });
            }

            var listaPaises = await ObtenerListaPaises();
            ViewBag.ListaPaises = listaPaises;

            return View(aMedicamentos);
        }

        //Este método obtiene la lista de medicamentos filtrada por un país específico
        //Filtra los medicamentos para incluir solo aquellos cuyo país coincida co
        public async Task<IActionResult> ListadoMedicamentosPorPais(string paisSeleccionado)
        {
            var canal = GrpcChannel.ForAddress("http://localhost:5169");
            _cliente = new Medicamento.MedicamentoClient(canal);

            var request = new Empty();
            var mensaje = await _cliente.listadoMedicamentosAsync(request);
            List<MedicamentoModel> aMedicamentos = new List<MedicamentoModel>();

            foreach (var c in mensaje.Items)
            {
                if (c.NomPai == paisSeleccionado)
                {
                    aMedicamentos.Add(new MedicamentoModel()
                    {
                        ide_med = c.IdeMed,
                        nom_med = c.NomMed,
                        des_med = c.DesMed,
                        rec_med = c.RecMed,
                        via_med = c.ViaMed,
                        cos_med = c.CosMed,
                        nom_pai = c.NomPai
                    });
                }
            }

            var listaPaises = await ObtenerListaPaises();
            ViewBag.ListaPaises = listaPaises;

            return View("ListadoMedicamentos", aMedicamentos);
        }
        //Este método obtiene la lista de países disponibles llamando al servicio gRPC 
        private async Task<List<PaisModel>> ObtenerListaPaises()
        {
            var canal = GrpcChannel.ForAddress("http://localhost:5169");
            _cliente1 = new Pais.PaisClient(canal);

            var request = new MyEmpty();
            var mensaje = await _cliente1.listadoPaisAsync(request);
            List<PaisModel> listaPaises = new List<PaisModel>();

            foreach (var c in mensaje.Items)
            {
                listaPaises.Add(new PaisModel()
                {
                    ide_pai = c.IdePai,
                    nom_pai = c.NomPai
                });
            }

            return listaPaises;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
