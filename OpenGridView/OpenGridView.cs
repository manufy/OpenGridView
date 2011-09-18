using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Web.UI;
using System.Web.UI.WebControls;
using Image = System.Web.UI.WebControls.Image;

namespace OpenGridView
{
//    [DefaultProperty("Text")]
    [ToolboxData("<{0}:OpenGridView runat=\"server\" />")]
    [System.Drawing.ToolboxBitmap(typeof(OpenGridView), "OpenGridView.testicon.gif")]
    public class OpenGridView : GridView
    {

         public string GetResourceImageUrl(string command)
        {
             switch (command)
             {
                 case "Edit":   return Page.ClientScript.GetWebResourceUrl(typeof(OpenGridView), "OpenGridView.Images.lapizedit.png");
                 case "Insert": return Page.ClientScript.GetWebResourceUrl(typeof(OpenGridView), "OpenGridView.Images.lapizinsert.png");
                 case "Cancel": return Page.ClientScript.GetWebResourceUrl(typeof(OpenGridView), "OpenGridView.Images.lapizcancel.png");
                 case "Delete": return Page.ClientScript.GetWebResourceUrl(typeof(OpenGridView), "OpenGridView.Images.lapizdelete.png");
                 case "Update": return Page.ClientScript.GetWebResourceUrl(typeof(OpenGridView), "OpenGridView.Images.lapizsave.png");

                 default:
                     return null;
             }
            
        }



        public class EventArgs<T> : EventArgs
        {
            public EventArgs(T value)
            {
                m_value = value;
            }

            private T m_value;

            public T Value
            {
                get { return m_value; }
            }
        }

        public class EventArgs<T, U> : EventArgs<T>
        {

            public EventArgs(T value, U value2)
                : base(value)
            {
                m_value2 = value2;
            }

            private U m_value2;

            public U Value2
            {
                get { return m_value2; }
            }
        }


        #region Properties

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
        [DefaultValue("false")]
        public string OnInsertionValidationGroup { get; set; }

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

        #endregion

        #region Objetos internos

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
        public event EventHandler OnInsertion;
     
        #endregion

        #region Functions

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
            row.Cells.Clear();// clear all the cells in the row
            row.Cells.Add(new TableCell()); //add a new blank cell
            row.Cells[0].ColumnSpan = columnsCount; //set the column span to the new added cell

            //You can set the styles here
            row.Cells[0].HorizontalAlign = HorizontalAlign.Center;
            row.Cells[0].ForeColor = System.Drawing.Color.Red;
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
                imgUrl = ArrowUpImageUrlInternal;
                sortStyle = _sortAscendingStyle;
            }
            else
            {
                imgUrl = ArrowDownImageUrlInternal;
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
        }

        protected override void TrackViewState()
        {
            base.TrackViewState();

            if (_sortAscendingStyle != null)
                ((IStateManager) _sortAscendingStyle).TrackViewState();
            if (_sortDescendingStyle != null)
                ((IStateManager) _sortDescendingStyle).TrackViewState();
        }

        #endregion

        # region Header and Footer

        private void CreateInsertButton()
        {
            var row = new GridViewRow(Rows.Count + 3, Rows.Count + 3, DataControlRowType.Footer,
                                      DataControlRowState.Normal);
            var left = new TableCell();
            left.ColumnSpan = 3;
            var insertbutton = new Button();
            insertbutton.Text = "Insertar";
            insertbutton.ID = "buttonInsertar";
            insertbutton.CommandName = "Insert";
            insertbutton.CausesValidation = true;
            insertbutton.ValidationGroup = OnInsertionValidationGroup;
            insertbutton.Click += insertbutton_Click;
            row.Cells.Add(left);
         //   row.Cells[0].Controls.Add(insertbutton);

            var insertimage = new ImageButton();
      //      insertimage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OpenGridView.testicon.gif");
            insertimage.ImageUrl = Page.ClientScript.GetWebResourceUrl(this.GetType(), "OpenGridView.Images.lapizadd.png");

            insertimage.ID = "buttonInsertar";
            insertimage.CommandName = "Insert";
            insertimage.CausesValidation = true;
            insertimage.ValidationGroup = OnInsertionValidationGroup;
            insertimage.Click += insertbutton_Click;

            row.Cells.Add(left);
            row.Cells[0].Controls.Add(insertimage);


            InnerTable.Rows.Add(row);
        }

        private void insertbutton_Click(object sender, EventArgs e)
        {
            // Lanza el evento OnClick y el Command "Insert" deprecated
            //    if (OnInsertion != null)
            //            OnInsertion(this, null);
        }

        private void CreateFilterHeader()
        {
            var row = new GridViewRow(0, -1, DataControlRowType.Header, DataControlRowState.Normal);

            TableCell left = new TableHeaderCell();
            left.ColumnSpan = 3;
            row.Cells.Add(left);

            var t = new TextBox();
            t.Columns = 10;
            t.MaxLength = 10;
            t.Text = "";
            t.ID = "textboxFiltro";

            var b = new Button();
            b.Text = "Filtro";
            b.ID = "buttonFiltro";

            row.Cells[0].Controls.Add(t);
            row.Cells[0].Controls.Add(b);

            InnerTable.Rows.AddAt(0, row);
        }


        private void DoInsertion()
        {

            if (OnInsertion != null)
                OnInsertion(this, null);
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
                        if (ShowInsertButton)
                            CreateInsertButton();
                        break;
                    }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            // Para mostrar la primera página.

            _mostrandoprimerapagina = true;
        }

        protected override void OnRowCommand(GridViewCommandEventArgs e)
        {
            base.OnRowCommand(e);
            switch (e.CommandName)
            {
                case "Insert":
                    {
                    //    int index = Convert.ToInt32(e.CommandArgument);
                    //    GridViewRow gvRow = Rows[index];

                        DoInsertion();
                        //   var t = (ObjectDataSource) this.DataSourceID;
                        //   t.Insert();
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

     //       if (this.Rows.Count == 0)
       //     {
               
         //       ShowNoResultFound();
           // }
            // Send user to last page of data, if needed
            if (_sendusertolastpageafterinsert && !_mostrandoprimerapagina)
            {
                PageIndex = PageCount - 1;
            }
        }

        #endregion
    }
}