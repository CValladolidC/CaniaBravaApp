using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace CaniaBrava
{
    /// <summary>
    /// Clase que representa un campo de la tabla de origen.
    /// </summary>
    /// <remarks>
    /// Como la estructura de la tabla de destino tendrá la misma estructura que la sentencia
    /// de entrada, es importante obtener todos los datos posibles de dicha estructura.
    /// </remarks>
    internal class Campo
    {
        /// <summary>
        /// Tipos de datos que puede tener la tabla de origen. 
        /// </summary>
        /// <remarks>Como que la lectura de la base de datos de origen se hace mediante clases genéricas
        /// de acceso a datos, pueden surgir tipos no conocidos aquí</remarks>
        private enum TiposCampoEnum { Varchar, Int, Double, Bit, DateTime }

        private string nombre;
        private TiposCampoEnum tipo;
        private int tamano;

        /// <summary>
        /// Constructor. 
        /// </summary>
        public Campo(System.Data.DataColumn columna)
        {

            nombre = columna.ColumnName;
            tipo = TipoCampo(columna);
            tamano = columna.MaxLength;
            // Se omiten intencionadamente los campos Memo
            if (tamano > 254)
                tamano = 254;
        }

        /// <summary>
        /// Adaptación del tipo de campo de origen a un formato simple dbf
        /// </summary>
        /// <remarks>Es posible que si se usan otras bases de datos haya que modificar este
        /// procedimiento</remarks>
        private TiposCampoEnum TipoCampo(DataColumn columna)
        {
            TiposCampoEnum s = TiposCampoEnum.Varchar;

            switch (columna.DataType.FullName)
            {
                case "System.String":
                    s = TiposCampoEnum.Varchar;
                    break;

                case "System.Int":
                case "System.Int16":
                case "System.Int32":
                case "System.Int64":
                    s = TiposCampoEnum.Int;
                    break;

                case "System.Double":
                case "System.Decimal":
                    s = TiposCampoEnum.Double;
                    break;

                case "System.Boolean":
                    s = TiposCampoEnum.Bit;
                    break;

                case "System.DateTime":
                    s = TiposCampoEnum.DateTime;
                    break;

                default:
                    throw new ArgumentOutOfRangeException("El tipo " + columna.DataType.FullName +
                        " no está contemplado en el procedimiento Campo.TipoCampo");
            }
            return s;
        }

        /// <summary>
        /// Obtiene el campo en formato de parámetro
        /// </summary>
        /// <returns></returns>
        public string Parametro()
        {
            return "@" + nombre;
        }

        /// <summary>
        /// Obtiene el nombre del campo
        /// </summary>
        /// <returns></returns>
        public string Nombre()
        {
            return nombre;
        }

        /// <summary>
        /// Obtiene la estructura necesaria que se empleará en la creación de la tabla.
        /// </summary>
        /// <returns></returns>
        public string Tabla()
        {
            return "[" + nombre + "]" + " " + tipo + (tipo != TiposCampoEnum.Varchar ? "" : "(" + tamano + ")");
        }
    }
}