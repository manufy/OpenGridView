using System;
using System.ComponentModel;
using OpenGridWebDemo;
using OpenGridWebDemo.DataSetTestTableAdapters;

namespace OpenGridViewBLL.Facturas
{
    [DataObject]
    public class FacturasBll
    {
        private FacturasTableAdapter _tableAdapter;

        private FacturasTableAdapter Adapter
        {
            get
            {
                return _tableAdapter ??
                       (_tableAdapter = new FacturasTableAdapter());
            }
        }


        // Funciones para capas superiores

        // Funciones de acceso a datos a través de la DAL

        // CONSULTAS

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.FacturasDataTable Get()
        {
            return Adapter.GetData();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.FacturasDataTable GetDataByFacturaId(decimal facturaid)
        {
            return Adapter.GetDataByFacturaID(facturaid);
        }

        //   [DataObjectMethod(DataObjectMethodType.Select, true)]
        //   public DataSetTest.FacturasDataTable Get(int StartRow,int PageSize )
        //   {


        //       return Adapter.GetData();
        //   }


        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.FacturasDataTable Get(int? startRowIndex, int maximumRows)
        {
            return Adapter.GetDataPaged(startRowIndex, maximumRows);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.FacturasDataTable Get(string sortExpression, int? startRowIndex, int maximumRows)
        {
            // Parsea y mapea sortExpression al campo ID del datatable del que se trate

            string[] Tokens = sortExpression.Split(' ');
            string index = Tokens[0];
            string ordertype = "ASC";
            if (Tokens.Length > 1)
                ordertype = Tokens[1];
            if ((sortExpression == "") || (sortExpression == "id") || (index == "id"))
                sortExpression = "facturaid " + ordertype;

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
        public DataSetEtiquetasDeComposicion.BOFacturasDataTable GetBo()
        {
            return BOAdapter.GetData();
        }
        */


        // INSERCIONES

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public bool Insert(DateTime fecha)
        {
            var dataset = new DataSetTest.FacturasDataTable();
            DataSetTest.FacturasRow nuevoregistro =
                dataset.NewFacturasRow();
            nuevoregistro.fecha = fecha;
            dataset.AddFacturasRow(nuevoregistro);
            int rows_affected = Adapter.Update(dataset);
            return rows_affected == 1;
        }

        // ACTUALIZACIONES

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public bool Update(decimal id, DateTime fecha, decimal original_id, DateTime original_fecha)
        {
            DataSetTest.FacturasDataTable registros =
                Adapter.GetDataByFacturaID(original_id);
            if (registros.Count == 0)
                // No se ha encontrado la etiqueta de composicion, devuelve falso
                return false;
            DataSetTest.FacturasRow registro = registros[0];
            registro.fecha = original_fecha;
            registro.AcceptChanges();
            registro.fecha = fecha;
            int rows_affected = Adapter.Update(registro);
            return rows_affected == 1;
        }

        // BORRADOS

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(decimal original_id, DateTime original_fecha)
        {
            int rows_affected = Adapter.Delete(original_id, original_fecha);
            return rows_affected == 1;
        }
    }
}