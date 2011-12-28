using System.ComponentModel;
using OpenGridWebDemo.DataSetTestTableAdapters;

namespace OpenGridWebDemo
{
    [DataObject]
    public class TiposDeArticuloBll
    {
        private TiposDeArticuloTableAdapter _tableAdapter;

        private TiposDeArticuloTableAdapter Adapter
        {
            get
            {
                return _tableAdapter ??
                       (_tableAdapter = new TiposDeArticuloTableAdapter());
            }
        }


        // Funciones para capas superiores

        // Funciones de acceso a datos a través de la DAL

        // CONSULTAS

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.TiposDeArticuloDataTable Get()
        {
            return Adapter.GetData();
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.TiposDeArticuloDataTable GetDataByTipoDeArticuloID(decimal articuloid)
        {
            return Adapter.GetDataByTipoArticuloID(articuloid);
        }

        // Paginacion en el servidor

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.TiposDeArticuloDataTable Get(int startRowIndex, int maximumRows)
        {
            return Adapter.GetDataPaged(startRowIndex, maximumRows);
        }

        // Paginacion y ordenación en el servidor

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataSetTest.TiposDeArticuloDataTable Get(string sortExpression, int startRowIndex, int maximumRows)
        {
            // Parsea y mapea sortExpression al campo ID del datatable del que se trate

            string[] Tokens = sortExpression.Split(' ');
            string index = Tokens[0];
            string ordertype = "ASC";
            if (Tokens.Length > 1)
                ordertype = Tokens[1];
            if ((sortExpression == "") || (sortExpression == "id") || (index == "id"))
                sortExpression = "tipoarticuloid " + ordertype;

            return Adapter.GetDataPagedSorted(startRowIndex, maximumRows, sortExpression);
        }

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public int GetRowCount()
        {
            int numero = (int)Adapter.GetRowCount();
            int resultado = int.Parse(numero.ToString());
            return resultado;
        }

        // INSERCIONES

        [DataObjectMethod(DataObjectMethodType.Insert, true)]
        public bool Insert(string descripcion)
        {
            var dataset = new DataSetTest.TiposDeArticuloDataTable();
            DataSetTest.TiposDeArticuloRow nuevoregistro =
                dataset.NewTiposDeArticuloRow();
            nuevoregistro.descripcion = descripcion;
            dataset.AddTiposDeArticuloRow(nuevoregistro);
            int rows_affected = Adapter.Update(dataset);
            return rows_affected == 1;
        }

        // ACTUALIZACIONES

        [DataObjectMethod(DataObjectMethodType.Update, true)]
        public bool Update(decimal id, string descripcion, decimal original_id ,string original_descripcion)
        {
            DataSetTest.TiposDeArticuloDataTable registros =
                Adapter.GetDataByTipoArticuloID(original_id);
            if (registros.Count != 1)
                // No se ha encontrado la etiqueta de composicion, devuelve falso
                return false;
            DataSetTest.TiposDeArticuloRow registro = registros[0];
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