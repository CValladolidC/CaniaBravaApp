using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaniaBrava
{
    public class MaestroSAP
    {
        public string Codigo { get; set; }
        public string CodClaseMedidad { get; set; }
        public string ClaseMedida { get; set; }
        public string MotMedida { get; set; }
        public string MotivoMedida { get; set; }
        public string Sociedad { get; set; }
        public string DireccionSoc { get; set; }
        public string RUCcia { get; set; }
        public string Codcia { get; set; }
        public string DescripcionCia { get; set; }
        public string SubDivision { get; set; }
        public string SubdivisiónPersonal { get; set; }
        public string GrupoPers { get; set; }
        public string CodAreaPersonal { get; set; }
        public string AreaPersonal { get; set; }
        public string Posicion { get; set; }
        public string DescripcionPosicion { get; set; }
        public string PosCese { get; set; }
        public string UltPosicionCese { get; set; }
        public string CenCosto { get; set; }
        public string CentroCosto { get; set; }
        public string ANom { get; set; }
        public string AreaNomina { get; set; }
        public string CodFuncion { get; set; }
        public string Funcion { get; set; }
        public string CodOcup { get; set; }
        public string Ocupacion { get; set; }
        public string Categ { get; set; }
        public string CategoriaCorta { get; set; }
        public string CategoriaLarga { get; set; }
        public string NivTalento { get; set; }
        public string NivTalentoCorto { get; set; }
        public string NivTalentoLarga { get; set; }
        public string Promedio { get; set; }
        public string Descripcion1 { get; set; }
        public string Descripcion2 { get; set; }
        public string Descripcion3 { get; set; }
        public string Descripcion4 { get; set; }
        public string Descripcion5 { get; set; }
        public string Descripcion6 { get; set; }
        public string NivOrg { get; set; }
        public string NivOrganizCorta { get; set; }
        public string NivOrganizLarga { get; set; }
        public string TipoBono { get; set; }
        public string TipoBonoCorta { get; set; }
        public string TipoBonoLarga { get; set; }
        public string CatBono { get; set; }
        public string CatBonocorta { get; set; }
        public string CatBonolarga { get; set; }
        public string ViceALI { get; set; }
        public string ViceALIlarga { get; set; }
        public string ViceALIcorta { get; set; }
        public string DirGer { get; set; }
        public string DirGerCorta { get; set; }
        public string DirGerLarga { get; set; }
        public string GCentral { get; set; }
        public string GerenciaCentral { get; set; }
        public string UnidadOrg { get; set; }
        public string UnidadOrganizativa { get; set; }
        public string AdmDatosPersonal { get; set; }
        public string EncRegTiempos { get; set; }
        public string AdmNomina { get; set; }
        public string CodJefe { get; set; }
        public string NombreJefe { get; set; }
        public string RelLab { get; set; }
        public string RelacionLaboral { get; set; }
        public string MotivoRenuncia { get; set; }
        public string JOBCODE { get; set; }
        public string JOBCODEcorta { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreTrabajador { get; set; }
        public string NombrePila { get; set; }
        public string Trabajador { get; set; }
        public string PaisNac { get; set; }
        public string PaisNacimiento { get; set; }
        public string CodNac { get; set; }
        public string Nacionalidad { get; set; }
        public string CodSexo { get; set; }
        public string Sexo { get; set; }
        public string NroHijos { get; set; }
        public string FecNac { get; set; }
        public string EdadAnios { get; set; }
        public string EdadMeses { get; set; }
        public string EdadDias { get; set; }
        public string EstadoCivil { get; set; }
        public string DireccionTrab { get; set; }
        public string Poblacion { get; set; }
        public string Distrito1 { get; set; }
        public string CodPostal { get; set; }
        public string CiudadCO { get; set; }
        public string Ubigeo { get; set; }
        public string Departamento { get; set; }
        public string Provincia { get; set; }
        public string Distrito { get; set; }
        public string Telefono { get; set; }
        public string TipoZona { get; set; }
        public string Zona { get; set; }
        public string NroCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public string CodBanco { get; set; }
        public string Banco { get; set; }
        public string Moneda { get; set; }
        public string CodBancoCI { get; set; }
        public string BancoCI { get; set; }
        public string CuentaSUNAT { get; set; }
        public string NroCuentaCTS { get; set; }
        public string TipoCuentaCTS { get; set; }
        public string CodBancoCTS { get; set; }
        public string BancoCTS { get; set; }
        public string MonedaCTS { get; set; }
        public string CodClaseContrato { get; set; }
        public string ClaseContrato { get; set; }
        public string FecIniContrato { get; set; }
        public string DiaIniContrato { get; set; }
        public string MesIniContrato { get; set; }
        public string AnioIniContrato { get; set; }
        public string DescMesIniContrato { get; set; }
        public string FinCont { get; set; }
        public string MotMedidaCont { get; set; }
        public string MotivoMedidaCont { get; set; }
        public string CodClaseMedidaCont { get; set; }
        public string ClaseMedidaCont { get; set; }
        public string SubMotMedidaCont { get; set; }
        public string SubmotivoMedidaCont { get; set; }
        public string PeriodoPrueba { get; set; }
        public string AniosTranscContrato { get; set; }
        public string MesesTranscContrato { get; set; }
        public string DiasTranscContrato { get; set; }
        public string AuiosDuracContrato { get; set; }
        public string MesesDuracContrato { get; set; }
        public string DiasDuracContrato { get; set; }
        public string FIni1c { get; set; }
        public string FFin1c { get; set; }
        public string Anios1c { get; set; }
        public string Meses1c { get; set; }
        public string Dias1c { get; set; }
        public string EmpAnterior { get; set; }
        public string Pedido { get; set; }
        public string DescPedido { get; set; }
        public string PedidoDistrib { get; set; }
        public string CenCosteDist { get; set; }
        public string DescCentroCosteDistrib { get; set; }
        public string CenCosteDistrib { get; set; }
        public string Cero1 { get; set; }
        public string Cero2 { get; set; }
        public string GrupoSang { get; set; }
        public string FactorSang { get; set; }
        public string Camisa { get; set; }
        public string Calzado { get; set; }
        public string Pantalon { get; set; }
        public string Casaca { get; set; }
        public string NivelEducativo { get; set; }
        public string DescNivelEduc { get; set; }
        public string InstitucionEducativa { get; set; }
        public string DescInstEduc { get; set; }
        public string Carrera { get; set; }
        public string DescCarrera { get; set; }
        public string AnioEgreso { get; set; }
        public string Pais { get; set; }
        public string DescPais { get; set; }
        public string AniosTSerEmp { get; set; }
        public string MesesTSerEmp { get; set; }
        public string DiasTSerEmp { get; set; }
        public string AniosTSerGrp { get; set; }
        public string MesesTSerGrp { get; set; }
        public string DiasTSerGrupo { get; set; }
        public string FecAlta { get; set; }
        public string FIPlanilla { get; set; }
        public string FecBaja { get; set; }
        public string FIGrupo { get; set; }
        public string FIPlanillaTP { get; set; }
        public string FecBajaTP { get; set; }
        public string CiaProc { get; set; }
        public string FecIngProc { get; set; }
        public string CodProv { get; set; }
        public string HRADMSF { get; set; }
        public string email { get; set; }
        public string CodClientVentas { get; set; }
        public string Anexo { get; set; }
        public string Celular { get; set; }
        public string UsuarioSAP { get; set; }
        public string UsuarioNetweaver { get; set; }
        public string UsuarioRED { get; set; }
        public string CentralAnexo { get; set; }
        public string PrimerNroTelefono { get; set; }
        public string CentralAnexo2 { get; set; }
        public string AsignacionLapTop { get; set; }
        public string AsignacionDeskTop { get; set; }
        public string UsuarioVPN { get; set; }
        public string CodSSFF { get; set; }
        public string NumeroCLARO { get; set; }
        public string MinutosCLARO { get; set; }
        public string TipoCLARO { get; set; }
        public string NumeroMOVISTAR { get; set; }
        public string MinutosMOVISTAR { get; set; }
        public string TipoMOVISTAR { get; set; }
        public string NumeroNEXTEL { get; set; }
        public string MinutosNEXTEL { get; set; }
        public string TipoNEXTEL { get; set; }
        public string TipoUSB { get; set; }
        public string NivelInternet { get; set; }
        public string Placa { get; set; }
        public string ImporteCombustible { get; set; }
        public string MonedaComb { get; set; }
        public string CantidadCombustible { get; set; }
        public string Unidad { get; set; }
        public string Color { get; set; }
        public string Marca { get; set; }
        public string Estacionamiento { get; set; }
        public string CorreoPrivado { get; set; }
        public string CellPhone { get; set; }
        public string DNI { get; set; }
        public string LibMilitar { get; set; }
        public string LicConducir { get; set; }
        public string Pasaporte { get; set; }
        public string CarneExtranjeria { get; set; }
        public string CarneESSALUD { get; set; }
        public string CUILCodIdentLab { get; set; }
        public string Cedula { get; set; }
        public string FotocheckORUS { get; set; }
        public string CarneDicscamec { get; set; }
        public string TSDicscamec { get; set; }
        public string FEmiDic { get; set; }
        public string FVtoDic { get; set; }
        public string CodEdificio { get; set; }
        public string Edificio { get; set; }
        public string CodOficina { get; set; }
        public string Oficina { get; set; }
        public string Correlativo { get; set; }
        public string IndRetJudiciales { get; set; }
        public string TotalFijosRetJudiciales { get; set; }
        public string TotalRetJudiciales { get; set; }
        public string TotalFijosEmbJudiciales { get; set; }
        public string TotalEmbJudiciales { get; set; }
        public string CodSistPrevPens { get; set; }
        public string SistPrevPens { get; set; }
        public string FAfilSPP { get; set; }
        public string CodUnicoSPP { get; set; }
        public string IndJubSPP { get; set; }
        public string TipoPension { get; set; }

        public void UpdateSincronizacion(string query)
        {
            SqlConnection conexion = new SqlConnection();
            conexion.ConnectionString = ConfigurationManager.AppSettings.Get("CADENA_CONEXION");
            conexion.Open();

            try
            {
                SqlCommand myCommand = new SqlCommand(query, conexion);
                myCommand.CommandTimeout = 360;
                myCommand.ExecuteNonQuery();
                myCommand.Dispose();
                MessageBox.Show("Sincronización de datos correcto..!!", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            conexion.Close();
        }
    }
}
