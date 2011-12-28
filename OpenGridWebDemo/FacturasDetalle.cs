using System;
using System.ComponentModel;
using OpenGridWebDemo;
using OpenGridWebDemo.DataSetTestTableAdapters;

namespace OpenGridViewBLL.Facturas
{
    [DataObject]
    public class FacturasDetalleBll
    {
        private FacturasDetalleTableAdapter _tableAdapter;

        private FacturasDetalleTableAdapter Adapter
        {
            get
            {
                return _tableAdapter ??
                       (_tableAdapter = new FacturasDetalleTableAdapter());
            }
        }


        // Funciones para capas superiores

        // Funciones de acceso a datos a través de la DAL

        // CONSULTAS

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.FacturasDetalleDataTable Get()
        {
            return Adapter.GetData();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.FacturasDetalleDataTable GetDataByFacturaId(decimal id)
        {
            return Adapter.GetDataByFacturaID(id);
        }

        //   [DataObjectMethod(DataObjectMethodType.Select, true)]
        //   public DataSetTest.FacturasDetalleDataTable Get(int StartRow,int PageSize )
        //   {


        //       return Adapter.GetData();
        //   }


        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.FacturasDetalleDataTable Get(int? startRowIndex, int maximumRows)
        {
            return Adapter.GetDataPaged(startRowIndex, maximumRows);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.FacturasDetalleDataTable Get(string sortExpression, int? startRowIndex, int maximumRows)
        {
            // Parsea y mapea sortExpression al campo ID del datatable del que se trate

            string[] Tokens = sortExpression.Split(' ');
            string index = Tokens[0];
            string ordertype = "ASC";
            if (Tokens.Length > 1)
                ordertype = Tokens[1];
            if ((sortExpression == "") || (sortExpression == "id") || (index == "id"))
                sortExpression = "facturadetalleid " + ordertype;

            return Adapter.GetDataPagedSorted(startRowIndex, maximumRows, sortExpression);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public int GetRowCount()
        {
            int? numero = Adapter.GetRowCount();
            int resultado = int.Parse(numero.ToString());
            return resultado;
        }

        /* / CONSULTAS BO

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetEtiquetasDeComposicion.BOFacturasDetalleDataTable GetBo()
        {
            return BOAdapter.GetData();
        }
        */


        // INSERCIONES

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public bool Insert(decimal facturaid, decimal articuloid, decimal cantidad)
        {
            var dataset = new DataSetTest.FacturasDetalleDataTable();
            DataSetTest.FacturasDetalleRow nuevoregistro =
                dataset.NewFacturasDetalleRow();
            nuevoregistro.articuloid = articuloid;
            nuevoregistro.facturaid = facturaid;
            nuevoregistro.cantidad = cantidad;
            dataset.AddFacturasDetalleRow(nuevoregistro);
            int rows_affected = Adapter.Update(dataset);
            return rows_affected == 1;
        }

        // ACTUALIZACIONES

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public bool Update(decimal id, DateTime fecha, decimal original_id, DateTime original_fecha)
        {
            DataSetTest.FacturasDetalleDataTable registros =
                Adapter.GetDataByFacturaID(original_id);
            if (registros.Count == 0)
                // No se ha encontrado la etiqueta de composicion, devuelve falso
                return false;
            DataSetTest.FacturasDetalleRow registro = registros[0];
        //    registro.fecha = original_fecha;
            registro.AcceptChanges();
    //        registro.fecha = fecha;
            int rows_affected = Adapter.Update(registro);
            return rows_affected == 1;
        }

        // BORRADOS

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(decimal original_id, DateTime original_fecha)
        {
       //     int rows_affected = Adapter.Delete(original_id, original_fecha);
            return true;// rows_affected == 1;
        }
    }
}