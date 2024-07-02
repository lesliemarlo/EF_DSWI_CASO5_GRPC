using GrpcServiceBdClinica.Protos;
using Microsoft.Data.SqlClient;
using System.Data;
using Grpc.Core;
using System.Xml.Linq;
namespace GrpcServiceBdClinica.Services
{
    public class PaisService : Pais.PaisBase
    {
        //CADENA DE CONEXION
        string cadena = "server=DESKTOP-Q15P6V0;database=clinica;uid=sa;pwd=sql;TrustServerCertificate=False;Encrypt=False";

        //DEFINIR CADENA DE CONEXION
        private readonly ILogger<PaisService> _logger;
        private List<paisResponse> paisResponse;

        public PaisService(ILogger<PaisService> logger)
        {
            _logger = logger;
            paisResponse = listadoPaises();
        }

        List<paisResponse> listadoPaises()
        {
            List<paisResponse> aPaises = new List<paisResponse>();
            using (SqlConnection cn = new SqlConnection(cadena))
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand("SP_LISTADOPAISES", cn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    aPaises.Add(new Protos.paisResponse()
                    {
                        IdePai = Int32.Parse(dr[0].ToString()),
                        NomPai = dr[1].ToString()
                    });
                }
                dr.Close();
            }
            return aPaises;
        }

        public override Task<paisListResponse> listadoPais(MyEmpty request, ServerCallContext context)
        {
            paisListResponse response = new paisListResponse();
            response.Items.AddRange(paisResponse);
            return Task.FromResult(response);
        }
    }
}
