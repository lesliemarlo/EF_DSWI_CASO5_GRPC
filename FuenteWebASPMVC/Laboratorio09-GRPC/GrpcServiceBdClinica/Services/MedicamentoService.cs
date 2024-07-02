using GrpcServiceBdClinica.Protos;
using Microsoft.Data.SqlClient;
using System.Data;
using Grpc.Core;
using System.Xml.Linq;
namespace GrpcServiceBdClinica.Services
{
    public class MedicamentoService : Medicamento.MedicamentoBase
    {

        //CADENA DE CONEXION
       string cadena = "server=DESKTOP-Q15P6V0;database=clinica;uid=sa;pwd=sql;TrustServerCertificate=False;Encrypt=False";

        //DEFINIR CADENA DE CONEXION
        private readonly ILogger<MedicamentoService> _logger;
        private List<medicamentoResponse> medicamentoResponse;

        public MedicamentoService(ILogger<MedicamentoService> logger)
        {
            _logger = logger;
            medicamentoResponse = listadoMedicamentos();
        }

        List<medicamentoResponse> listadoMedicamentos()
        {
            List<medicamentoResponse> aMedicamentos = new List<medicamentoResponse>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_LISTADOMEDICAMENTOS", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    aMedicamentos.Add(new Protos.medicamentoResponse()
                    {
                        IdeMed = Int32.Parse(dr[0].ToString()),
                        NomMed = dr[1].ToString(),
                        DesMed = dr[2].ToString(),
                        RecMed = dr[3].ToString(),
                        ViaMed = dr[4].ToString(),
                        CosMed = double.Parse(dr[5].ToString()),
                        NomPai = dr[6].ToString(),
                    });
                }
                dr.Close();
            }
            return aMedicamentos;
        }

        public override Task<medicamentoListResponse> listadoMedicamentos(Empty request, ServerCallContext context)
        {
            medicamentoListResponse response = new medicamentoListResponse();
            response.Items.AddRange(medicamentoResponse);
            return Task.FromResult(response);
        }
    }
}

