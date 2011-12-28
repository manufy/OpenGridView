using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

namespace OpenControls
{
    public enum OpenGridViewStatus
    {
        Select,
        Edit,
        Cancel
    } ;
    
    /* public class FilterCommandEventArgs : EventArgs
    {
        private string filterExpression;

        public FilterCommandEventArgs()
        {
        }

        public FilterCommandEventArgs(string filterExpression)
        {
            this.filterExpression = filterExpression;
        }

        public string FilterExpression
        {
            get
            {
                return this.filterExpression;
            }
            set
            {
                this.filterExpression = value;
            }
        }
    } */


//    [DefaultProperty("Text")]
    [ToolboxData("<{0}:OpenGridView runat=\"server\" />")]
    [ToolboxBitmap(typeof (OpenGridView), "OpenGridView.testicon.gif")]
    public class OpenGridView : GridView
    {


        #region Delegates

        public delegate void FilterCommandEventHandler(object sender, FilterCommandEventArgs e);

        #endregion
     
        private static TextBox TextBoxFiltro;

        private static readonly object EventFilterCommand = new object();

        # region Resources

        public string GetResourceImageUrl(string image)
        {
            return Page.ClientScript.GetWebResourceUrl(typeof (OpenGridView),
                                                       "OpenGridView.Images." + image);
        }

        public string GetResourceCommandImageUrl(string command)
        {
            switch (command)
            {
                case "Edit":
                    return Page.ClientScript.GetWebResourceUrl(typeof (OpenGridView),
                                                               "OpenGridView.Images.lapizedit.png");
                case "Insert":
                    return Page.ClientScript.GetWebResourceUrl(typeof (OpenGridView),
                                                               "OpenGridView.Images.lapizadd.png");
                case "Cancel":
                    return Page.ClientScript.GetWebResourceUrl(typeof (OpenGridView),
                                                               "OpenGridView.Images.lapizcancel.png");
                case "Delete":
                    return Page.ClientScript.GetWebResourceUrl(typeof (OpenGridView),
                                                               "OpenGridView.Images.lapizdelete.png");
                case "Update":
                    return Page.ClientScript.GetWebResourceUrl(typeof (OpenGridView),
                                                               "OpenGridView.Images.lapizsave.png");
                case "Select":
                    return Page.ClientScript.GetWebResourceUrl(typeof (OpenGridView),
                                                               "OpenGridView.Images.lupa.gif");

                default:
                    return null;
            }
        }

        #endregion

        private string filterexpression;

        protected string filtertext
        {
            get { return (string) ViewState["FilterText"]; }
            set { ViewState["FilterText"] = filtertext; }
        }

        protected bool footerininsertmode
        {
            get
            {
                if (ViewState["FooterInInsertMode"] == null)
                    return false;
                return (bool)ViewState["FooterInInsertMode"];
            }
            set { ViewState["FooterInInsertMode"] = value; }


        }     

        /*    public event EventHandler FilterCommand
        {
            add
            {
                base.Events.AddHandler(EventFilterCommand, value);
            }
            remove
            {
                base.Events.RemoveHandler(EventFilterCommand, value);
            }
        } */

        #region Nested type: EventArgs

        public class EventArgs<T> : EventArgs
        {
            private readonly T m_value;

            public EventArgs(T value)
            {
                m_value = value;
            }

            public T Value
            {
                get { return m_value; }
            }
        }


        #endregion

   

        #region OpenGridView properties

        private bool _mostrandoprimerapagina;
        private bool _sendusertolastpageafterinsert;


        [Category("OpenGridView")]
        [DefaultValue("false")]
        public bool SendUserToLastPageAfterInsert { get; set; }

        [Category("OpenGridView")]
        [DefaultValue("false")]
        public bool ShowInsertButton { get; set; }

        [Category("OpenGridView")]
        [DefaultValue("false")]
        public bool ShowFilter { get; set; }

        [Category("OpenGridView")]
        [DefaultValue("")]
        public string OnInsertionValidationGroup { get; set; }

        [Category("OpenGridView")]
        [DefaultValue("")]
        public string NoDataMessage { get; set; }

        [Category("OpenGridView")]
        [DefaultValue("false")]
        public bool NoDataShowHeader { get; set; }

        #endregion

        #region Style properties

        private TableItemStyle _sortAscendingStyle;
        private TableItemStyle _sortDescendingStyle;


        [Description("The style applied to the header cell that is sorted in ascending order."), Themeable(true),
         Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle SortAscendingStyle
        {
            get
            {
                if (_sortAscendingStyle == null)
                {
                    _sortAscendingStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                        ((IStateManager) _sortAscendingStyle).TrackViewState();
                }

                return _sortAscendingStyle;
            }
        }

        [Description("The style applied to the header cell that is sorted in descending order."), Themeable(true),
         Category("Styles"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content),
         NotifyParentProperty(true), PersistenceMode(PersistenceMode.InnerProperty)]
        public TableItemStyle SortDescendingStyle
        {
            get
            {
                if (_sortDescendingStyle == null)
                {
                    _sortDescendingStyle = new TableItemStyle();
                    if (base.IsTrackingViewState)
                        ((IStateManager) _sortDescendingStyle).TrackViewState();
                }

                return _sortDescendingStyle;
            }
        }

        #endregion

        #region ArrowUpImageUrl

        [Description("The url of the image shown when a column is sorted in ascending order."), DefaultValue(""),
         Themeable(true), Category("Appearance"), Editor("System.Web.UI.Design.UrlEditor", typeof (UITypeEditor))]
        public string ArrowUpImageUrl
        {
            get
            {
                var str = ViewState["ArrowUpImageUrl"] as string;
                if (str == null)
                    return string.Empty;
                else
                    return str;
            }
            set { ViewState["ArrowUpImageUrl"] = value; }
        }

        protected virtual string ArrowUpImageUrlInternal
        {
            get
            {
                if (string.IsNullOrEmpty(ArrowUpImageUrl))
                    return Page.ClientScript.GetWebResourceUrl(GetType(), "OpenGridView.ArrowUp.gif");
                else
                    return ArrowUpImageUrl;
            }
        }

        #endregion

        #region ArrowDownImageUrl

        [Description("The url of the image shown when a column is sorted in descending order."), DefaultValue(""),
         Themeable(true), Category("Appearance"), Editor("System.Web.UI.Design.UrlEditor", typeof (UITypeEditor))]
        public string ArrowDownImageUrl
        {
            get
            {
                var str = ViewState["ArrowDownImageUrl"] as string;
                if (str == null)
                    return string.Empty;
                else
                    return str;
            }
            set { ViewState["ArrowDownImageUrl"] = value; }
        }

        protected virtual string ArrowDownImageUrlInternal
        {
            get
            {
                if (string.IsNullOrEmpty(ArrowDownImageUrl))
                    return Page.ClientScript.GetWebResourceUrl(GetType(), "OpenGridView.ArrowDown.gif");
                else
                    return ArrowDownImageUrl;
            }
        }

        #endregion

     

        #region Objetos internos

        protected GridViewRow _footerRow2;
        protected GridViewRow _headerRow2;
        protected GridViewRow _insertbutton;
        protected GridViewRow _nodataRow;

        protected Table InnerTable
        {
            get
            {
                if (HasControls())
                {
                    return (Table) Controls[0];
                }

                return null;
            }
        }

        #endregion

        #region Delegados, llamados desde el interface

        //public event EventHandler<EventArgs<GridViewRow>> OnInsertion;
        public event EventHandler FooterInsertButton;
      
        public event EventHandler<FilterCommandEventArgs> FilterCommand;

        #endregion

        #region Functions

        private int GetColumnIndex(string columnName)
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                var field = Columns[i] as DataControlField;
                if (field != null && field.SortExpression == columnName) // Datafield
                    return i;
            }
            return -1;
        }

        protected override void OnRowDataBound(GridViewRowEventArgs e)
        {
           
            base.OnRowDataBound(e);
            if (ShowFilter == false)
                return;
          
            if (e.Row.RowType != DataControlRowType.DataRow)
                return;
            if (String.IsNullOrEmpty(TextBoxFiltro.Text))
                return;
            if (TextBoxFiltro.Text == null)
                return;
            int colIndex = GetColumnIndex(SortExpression);
            if (colIndex == -1)
                return;
            TableCell cell = e.Row.Cells[colIndex];
            string cellText = cell.Text;

            // Busca las coincidencias, primero extraemos los tokens a resaltas

            string[] tokens = TextBoxFiltro.Text.Split(' ');
            int contador = 0;
            var resaltado = new StringBuilder();
            foreach (string token in tokens)
            {

                int leftIndex = cell.Text.IndexOf(token, StringComparison.OrdinalIgnoreCase);
                if (leftIndex != -1)
                {
                    int rightIndex = leftIndex + token.Length;
                    var builder = new StringBuilder();
                    builder.Append(cell.Text, 0, leftIndex);
                    builder.Append("<span class=\"highlight\">");
                    builder.Append(cell.Text, leftIndex, rightIndex - leftIndex);
                    builder.Append("</span>");
                    builder.Append(cell.Text, rightIndex, cell.Text.Length - rightIndex);
                    cell.Text = builder.ToString();
                }


                contador++;
            }
        }
        

        protected virtual void OnFilterCommand(FilterCommandEventArgs e)
        {
            FilterCommand(this, e);
            var handler = (EventHandler) base.Events[EventFilterCommand];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void HandleFilterCommand(object sender, EventArgs e)
        {
            RequiresDataBinding = true; //this is required to make sure that unsetting of filter is also handled
            var filterArgs = new FilterCommandEventArgs();
            //   filterArgs.FilterExpression = GetFilterCommand();
            filterArgs.FilterExpression = GetFilterExpression(TextBoxFiltro.Text);
            filtertext = TextBoxFiltro.Text;
            filterexpression = filterArgs.FilterExpression;
            //    FilterCommand(this, filterArgs);
            //this.OnFilterCommand(filterArgs);

            if (FilterCommand != null)
            FilterCommand(this, filterArgs);
        }

        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            // Call base method and get number of rows
            int numRows = base.CreateChildControls(dataSource, dataBinding);
           
            if (numRows == 0)
            {
                //no data rows created, create empty table
                //create table
                var table = new Table();
                table.ID = ID;

                //convert the exisiting columns into an array and initialize
                var fields = new DataControlField[Columns.Count];
                Columns.CopyTo(fields, 0);

                if (ShowHeader)
                {
                    if (NoDataShowHeader == true)
                    {

                        _headerRow2 = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);

                        InitializeRow(_headerRow2, fields);
                        _headerRow2.EnableTheming = true;
                        table.Rows.Add(_headerRow2);
                    }

                    // Crea un Row para mostrar en caso de que no haya datos en el datasource
                    // Solo se muestra si NoDataMessage contiene texto

                    if (string.IsNullOrEmpty(NoDataMessage) == false)
                    {

                        _nodataRow = base.CreateRow(-2, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);

                        InitializeRow(_nodataRow, fields);
                        _nodataRow.Cells[0].Text = NoDataMessage;
                        _nodataRow.EnableTheming = true;
                        table.Rows.Add(_nodataRow);
                    }
                }

                //     if (this.ShowFooter)
                {
                    //create footer row
                    _footerRow2 = base.CreateRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);
                    if (ShowFooter)
                        InitializeRow(_footerRow2, fields);
                    _footerRow2.EnableTheming = true;
                    table.Rows.Add(_footerRow2);
                    if ((ShowInsertButton) && (footerininsertmode==false))
                    {
                        //  if (this.ShowFooter)
                        {
                            var cell = new TableCell();
                            _footerRow2.Cells.Add(cell);
                            var buttonexpandinsertionfooter = new ImageButton();
                            //      insertimage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OpenGridView.testicon.gif");
                            buttonexpandinsertionfooter.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(),
                                                                                       "OpenGridView.Images.lapizadd.png");


                            buttonexpandinsertionfooter.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "OpenGridView.Images.lapizadd.png");
                            buttonexpandinsertionfooter.ID = "ButtonExpandInsertFooter";
                            buttonexpandinsertionfooter.CommandName = "ExpandInsertFooter";
                            buttonexpandinsertionfooter.CausesValidation = true;
                            buttonexpandinsertionfooter.ValidationGroup = OnInsertionValidationGroup;
                            buttonexpandinsertionfooter.Click += HandleInsertButton;

                            _footerRow2.Cells[0].Controls.Add(buttonexpandinsertionfooter);
                        }

                      
                       

                        // table.Rows.Add(row
                    }
                }

                Controls.Clear();
                Controls.Add(table);
            }

            return numRows;
        }


        private void ShowNoResultFound()
        {
            //  source.Rows.Add(source.NewRow()); // create a new blank row to the DataTable
            // Bind the DataTable which contain a blank row to the GridView
            //  DataSource = source;
            // gv.DataBind();
            // Get the total number of columns in the GridView to know what the Column Span should be
            var row = new GridViewRow(Rows.Count, Rows.Count, DataControlRowType.Footer,
                                      DataControlRowState.Normal);
            var left = new TableCell();
            left.ColumnSpan = 3;
            row.Cells.Add(left);
            //  row.Cells[0].Controls.Add(insertbutton);


            int columnsCount = Columns.Count;
            row.Cells.Clear(); // clear all the cells in the row
            row.Cells.Add(new TableCell()); //add a new blank cell
            row.Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

            //You can set the styles here
            row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[0].ForeColor = Color.Red;
            row.Cells[0].Font.Bold = true;
            //set No Results found to the new added cell
            //    Rows[0].Cells[0].Text = "NO RESULT FOUND!";
            //   InnerTable.Rows.Add(row);
        }

        protected virtual void CreateSortArrows(TableCell sortedCell)
        {
            // Add the appropriate arrow image and apply the appropriate state, depending
            // on whether we're sorting the results in ascending or descending order
            TableItemStyle sortStyle = null;
            string imgUrl = null;

            if (SortDirection == SortDirection.Ascending)
            {
                imgUrl = GetResourceImageUrl("ordenacionflechaarriba.gif");
                sortStyle = _sortAscendingStyle;
            }
            else
            {
                imgUrl = GetResourceImageUrl("ordenacionflechaabajo.gif");
                sortStyle = _sortDescendingStyle;
            }

            var arrow = new Image();
            arrow.ImageUrl = imgUrl;
            arrow.BorderStyle = BorderStyle.None;
            sortedCell.Controls.Add(arrow);

            if (sortStyle != null)
                sortedCell.MergeStyle(sortStyle);
        }

        protected override void PrepareControlHierarchy()
        {
            base.PrepareControlHierarchy();
            
            if (HasControls())
            {
                if (!string.IsNullOrEmpty(SortExpression) && ShowHeader)
                {
                    var table = Controls[0] as Table;


                    if ((table != null) && (table.Rows.Count > 0))
                    {
                        // Need to check first TWO rows because the first row may be a
                        // pager row... Thanks for Barbaros Saglamtimur for this catch!

                        // Si esta en modo mostrar filtro, se usa la segunda fila para buscar los campos

                        GridViewRow headerRow = CreateHeaderFilter(table);
                        CreateHeaderSortArrows(table, headerRow);
                    }
                }
            }
        }

        private GridViewRow CreateHeaderFilter(Table table)
        {
            GridViewRow headerRow = null;
            if (ShowFilter)
            {
                headerRow = table.Rows[1] as GridViewRow;
            }
            else
            {
                headerRow = table.Rows[0] as GridViewRow;
            }
            return headerRow;
        }

        private void CreateHeaderSortArrows(Table table, GridViewRow headerRow)
        {
            if ((headerRow.RowType != DataControlRowType.Header) && (table.Rows.Count > 1))
                headerRow = table.Rows[1] as GridViewRow;

            if (headerRow.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in headerRow.Cells)
                {
                    var gridViewCell = cell as DataControlFieldCell;
                    if (gridViewCell != null)
                    {
                        DataControlField cellsField = gridViewCell.ContainingField;
                        if (cellsField != null && cellsField.SortExpression == SortExpression)
                        {
                            // Add the sort arrows for this cell
                            CreateSortArrows(cell);

                            // We're done!
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region State Management Methods

        protected override object SaveViewState()
        {
            // We need to save any programmatic changes to the SortAscendingStyle or SortDescendingStyle
            // properties to view state...
            var state = new object[3];
            state[0] = base.SaveViewState();
            if (_sortAscendingStyle != null)
                state[1] = ((IStateManager) _sortAscendingStyle).SaveViewState();
            if (_sortDescendingStyle != null)
                state[2] = ((IStateManager) _sortDescendingStyle).SaveViewState();
            // if (TextBoxFiltro != null)
            //      state[3] = ((IStateManager) TextBoxFiltro).SaveViewState();
            return state;
        }

        protected override void LoadViewState(object savedState)
        {
            var state = (object[]) savedState;

            base.LoadViewState(state[0]);

            if (state[1] != null)
                ((IStateManager) SortAscendingStyle).LoadViewState(state[1]);
            if (state[2] != null)
                ((IStateManager) SortDescendingStyle).LoadViewState(state[2]);

      //      TextBoxFiltro.Text = filtertext;
            //    if (state[2] != null)
            //      ((IStateManager) TextBoxFiltro).LoadViewState(state[3]);
        }

        protected override void TrackViewState()
        {
            base.TrackViewState();

            if (_sortAscendingStyle != null)
                ((IStateManager) _sortAscendingStyle).TrackViewState();
            if (_sortDescendingStyle != null)
                ((IStateManager) _sortDescendingStyle).TrackViewState();
            //      if (TextBoxFiltro != null)
            //           ((IStateManager) TextBoxFiltro).TrackViewState();
        }

        #endregion

        # region Header and Footer

        private void CreateInsertButton()
        {
            //   var row = new GridViewRow(Rows.Count + 3, Rows.Count + 3, DataControlRowType.Footer,
            //                             DataControlRowState.Normal);

         

            var row = new GridViewRow(-1, -1, DataControlRowType.Footer,
                                      DataControlRowState.Normal);


            var left = new TableCell();
            left.ColumnSpan = 3;
            var buttonexpandinsertionfooter = new ImageButton();
            buttonexpandinsertionfooter.ImageUrl = Page.ClientScript.GetWebResourceUrl(GetType(), "OpenGridView.Images.lapizadd.png");
            buttonexpandinsertionfooter.ID = "ButtonExpandInsertFooter";
            
            buttonexpandinsertionfooter.CommandName = "ExpandInsertFooter";
            buttonexpandinsertionfooter.CausesValidation = true;
            buttonexpandinsertionfooter.ValidationGroup = OnInsertionValidationGroup;
            buttonexpandinsertionfooter.Click += HandleInsertButton;

            row.Cells.Add(left);
            row.Cells[0].Controls.Add(buttonexpandinsertionfooter);


            InnerTable.Rows.Add(row);
        }

        private void HandleInsertButton(object sender, EventArgs e)
        {
            // Lanza el evento OnClick y el Command "Insert" deprecated
            //    if (OnInsertion != null)
            //            OnInsertion(this, null);
   //         ShowFooter = !ShowFooter;
     //       if (ShowFooter)
       //         ShowInsertButton = true;

            footerininsertmode = true;// !footerininsertmode;
          
        }

        private void filterbutton_Click(object sender, EventArgs e)
        {
            // Encontrar la referencia al textbox del filtro y pasar este como parámetro

            //  var tb = this.FindControl("TextBoxFiltro");
            //  GridViewRow r = this.Rows[this.SelectedIndex];
            //  GridViewRow r2 = this.SelectedRow;
            var b = (Button) sender;
            GetFilterExpression(TextBoxFiltro.Text);
        }

        private string GetFilterExpression(string filterText)
        {
            string filterExpression = "";
            if (!String.IsNullOrEmpty(filterText) && (!String.IsNullOrEmpty(SortExpression)))
            {
                string[] tokens = filterText.Split(' ');
                bool primeravez = true;
                foreach (string token in tokens)
                {
                    if (!primeravez)
                        filterExpression += " AND ";
                    if (!string.IsNullOrEmpty(token))
                    {
                        primeravez = false;
                        filterExpression += string.Format("[{0}] LIKE '{1}'", SortExpression, "%" + token + "%");
                        ;
                    }
                }

                //  filterExpression = "[detalle] LIKE '%50%' AND [detalle] LIKE '%MERI%' " ;
                //  " AND [detalle] LIKE '%ACRI%'";// expresionlike; //  string.Format("[{0}] LIKE '{1}'", HWGridView1.SortExpression, expresionlike); // FilterText.Text);

                //   ObjectDataSourceEtiquetasDeComposicion.FilterExpression = filterExpression;
                //      FilterText.Text = "Filtro [" + HWGridView1.SortExpression + "]";
            }
            else
            {
                //     FilterText.Text = "Filtro";
                //       ObjectDataSourceEtiquetasDeComposicion.FilterExpression = "";
            }
            filterText = "";

            /*   FilterCommandEventArgs e = new FilterCommandEventArgs();
            e.FilterExpression = filterExpression;
            

            EventHandler handler = (EventHandler)base.Events[EventFilterCommand];
            if (handler != null)
            {
                handler(this, e);
            } */

            return filterExpression;
        }


        private void CreateFilterHeader()
        {
            var row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            TableCell left = new TableHeaderCell();
            left.ColumnSpan = 3;
            row.Cells.Add(left);

            //    var t = new TextBox();
            TextBoxFiltro = new TextBox();
            TextBoxFiltro.Columns = 10;
            TextBoxFiltro.MaxLength = 10;
            TextBoxFiltro.Text = filtertext;
            TextBoxFiltro.EnableViewState = true;
            TextBoxFiltro.ID = "TextBoxFiltro";

            var b = new Button();
            b.Text = "Filtro";
            b.ID = "ButtonFiltro";
            b.Click += HandleFilterCommand; //

            row.Cells[0].Controls.Add(TextBoxFiltro);
            row.Cells[0].Controls.Add(b);

            InnerTable.Rows.AddAt(0, row);
        }


        private void DoInsertion()
        {
            if (FooterInsertButton != null)
                FooterInsertButton(this, null);
            //OnInsertion(this, new EventArgs<GridViewRow>(r));

            if (SendUserToLastPageAfterInsert)
                if (_mostrandoprimerapagina)
                {
                    _sendusertolastpageafterinsert = true;
                    _mostrandoprimerapagina = false;
                }
        }

        #endregion

        #region Override Events

        /*      protected override int CreateChildControls(System.Collections.IEnumerable dataSource, bool dataBinding)
        {
            
            int numRows = base.CreateChildControls(dataSource, dataBinding);
            //If no data rows created so we will work to create our own header and footer

            if (numRows == 0)
            {

                //create table

                Table table = new Table(); table.ID = this.ID;
                //create a new header row based on the header that is usually created

                GridViewRow row = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
                //convert the exisiting columns into an array, initialize and then add the row to the previously created table

                DataControlField[] fields = new DataControlField[this.Columns.Count];
                this.Columns.CopyTo(fields, 0);

                this.InitializeRow(row, fields);
                table.Rows.Add(row);

                //create footer row that will follow the design and behavior of the footer created on the GridView Design

                row = new GridViewRow(-2, -1, DataControlRowType.Footer, DataControlRowState.Normal);
                //initialize the row based on the fields added 

                this.InitializeRow(row, fields);
                //adding the row to the previously created table

                table.Rows.Add(row);

                //adding the table to the control.

                this.Controls.Add(table);
            }

            return numRows;
        }*/


        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);
            switch (e.Row.RowType)
            {
                case DataControlRowType.Header:
                    {
                        if (ShowFilter)
                            CreateFilterHeader();
                        break;
                    }
                case DataControlRowType.Footer:
                    {
                        OpenGridView v = (OpenGridView)e.Row.Parent;

                        if ((ShowInsertButton) && (footerininsertmode == false))
                            CreateInsertButton();
                        break;
                    }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Para mostrar la primera página.

            footerininsertmode = false;
            _mostrandoprimerapagina = true;
        }

        protected override void OnRowCommand(GridViewCommandEventArgs e)
        {
            base.OnRowCommand(e);
            switch (e.CommandName)
            {
                case "ExpandInsertFooter":
                    footerininsertmode = true; //!footerininsertmode;
                  ShowFooter = !ShowFooter;
                    break;
                case "Cancel":
                    if (footerininsertmode)
                    {
                        footerininsertmode = false;
                        ShowFooter = false;
                    }
                    break;
                case "Select":


                    break;
                case "Insert":
                    {
                        ShowFooter = false;
                        footerininsertmode = false;

               //         if (InsertCommand != null)
                 //            InsertCommand (this,null);
                        break;
                    }
                default:
                    {
                        _mostrandoprimerapagina = false;
                        _sendusertolastpageafterinsert = false;
                        break;
                    }
            }
        }


        protected override void OnDataBound(EventArgs e)
        {
            base.OnDataBound(e);

            //  ////         if (this.Rows.Count == 0)
            //      {
//
            //              ShowNoResultFound();
            //     }
            // Send user to last page of data, if needed
            if (_sendusertolastpageafterinsert && !_mostrandoprimerapagina)
            {
                PageIndex = PageCount - 1;
            }
        }

        #endregion

        #region Nested type: FilterCommandEventArgs

        public class FilterCommandEventArgs : EventArgs
        {
            public string FilterExpression { get; set; }
        }

        #endregion
    }
}