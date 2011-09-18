using System.ComponentModel;
using OpenGridWebDemo.DataSetTestTableAdapters;

namespace OpenGridWebDemo
{
    [DataObject]
    public class ArticulosBll
    {
        private ArticulosTableAdapter _tableAdapter;

        private ArticulosTableAdapter Adapter
        {
            get
            {
                return _tableAdapter ??
                       (_tableAdapter = new ArticulosTableAdapter());
            }
        }


        // Funciones para capas superiores

        // Funciones de acceso a datos a través de la DAL

        // CONSULTAS

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.ArticulosDataTable Get()
        {
            return Adapter.GetData();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.ArticulosDataTable GetDataByFacturaId(decimal articuloid)
        {
            return Adapter.GetDataByArticuloID(articuloid);
        }

        // Paginacion en el servidor

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.ArticulosDataTable Get(int? startRowIndex, int maximumRows)
        {
            return Adapter.GetDataPaged(startRowIndex, maximumRows);
        }

        // Paginacion y ordenación en el servidor

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.ArticulosDataTable Get(string sortExpression, int? startRowIndex, int maximumRows)
        {
            // Parsea y mapea sortExpression al campo ID del datatable del que se trate

            string[] Tokens = sortExpression.Split(' ');
            string index = Tokens[0];
            string ordertype = "ASC";
            if (Tokens.Length > 1)
                ordertype = Tokens[1];
            if ((sortExpression == "") || (sortExpression == "id") || (index == "id"))
                sortExpression = "articuloid " + ordertype;

            return Adapter.GetDataPagedSorted(startRowIndex, maximumRows, sortExpression);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public int GetRowCount()
        {
            int? numero = Adapter.GetRowCount();
            int resultado = int.Parse(numero.ToString());
            return resultado;
        }

        // INSERCIONES

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public bool Insert(string descripcion)
        {
            var dataset = new DataSetTest.ArticulosDataTable();
            DataSetTest.ArticulosRow nuevoregistro =
                dataset.NewArticulosRow();
            nuevoregistro.descripcion = descripcion;
            dataset.AddArticulosRow(nuevoregistro);
            int rows_affected = Adapter.Update(dataset);
            return rows_affected == 1;
        }

        // ACTUALIZACIONES

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public bool Update(decimal id, string descripcion, decimal original_id, string original_descripcion)
        {
            DataSetTest.ArticulosDataTable registros =
                Adapter.GetDataByArticuloID(original_id);
            if (registros.Count == 0)
                // No se ha encontrado la etiqueta de composicion, devuelve falso
                return false;
            DataSetTest.ArticulosRow registro = registros[0];
            registro.descripcion = original_descripcion;
            registro.AcceptChanges();
            registro.descripcion = descripcion;
            int rows_affected = Adapter.Update(registro);
            return rows_affected == 1;
        }

        // BORRADOS

        [DataObjectMethod(DataObjectMethodType.Delete, true)]
        public bool Delete(decimal original_id, string original_descripcion)
        {
            int rows_affected = Adapter.Delete(original_id, original_descripcion);
            return rows_affected == 1;
        }
    }
}