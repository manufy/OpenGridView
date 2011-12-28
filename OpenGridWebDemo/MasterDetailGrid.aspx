<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MasterDetailGrid.aspx.cs" Inherits="OpenGridWebDemo.MasterDetailGrid" %>

<%@ Register assembly="OpenGridView" namespace="OpenControls" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:OpenGridView ID="OpenGridViewFacturas" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="id" DataSourceID="ObjectDataSourceFacturas"  
            onrowcreated="OpenGridViewFacturas_RowCreated">
            <Columns>
                <asp:CommandField ShowSelectButton="True" />
                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                    ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="fecha" HeaderText="fecha" SortExpression="fecha" />
                <asp:BoundField DataField="ROW" HeaderText="ROW" SortExpression="ROW" />
                <asp:TemplateField HeaderText="Detalle">
                    <ItemTemplate>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                            DataKeyNames="id" DataSourceID="ObjectDataSourceDetalle">
                            <Columns>
                                <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                                    ReadOnly="True" SortExpression="id" />
                                <asp:BoundField DataField="facturaid" HeaderText="facturaid" 
                                    SortExpression="facturaid" />
                                <asp:BoundField DataField="articuloid" HeaderText="articuloid" 
                                    SortExpression="articuloid" />
                                <asp:BoundField DataField="cantidad" HeaderText="cantidad" 
                                    SortExpression="cantidad" />
                                <asp:BoundField DataField="row" HeaderText="row" SortExpression="row" />
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="ObjectDataSourceDetalle" runat="server" 
                            DeleteMethod="Delete" InsertMethod="Insert" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByFacturaId" 
                            TypeName="OpenGridViewBLL.Facturas.FacturasDetalleBll" UpdateMethod="Update">
                            <DeleteParameters>
                                <asp:Parameter Name="original_id" Type="Decimal" />
                                <asp:Parameter Name="original_fecha" Type="DateTime" />
                            </DeleteParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="id" Type="Decimal" />
                                <asp:Parameter Name="fecha" Type="DateTime" />
                                <asp:Parameter Name="original_id" Type="Decimal" />
                                <asp:Parameter Name="original_fecha" Type="DateTime" />
                            </UpdateParameters>
                            <SelectParameters>
                                <asp:SessionParameter Name="id" SessionField="id" Type="Decimal" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:Parameter Name="facturaid" Type="Decimal" />
                                <asp:Parameter Name="articuloid" Type="Decimal" />
                                <asp:Parameter Name="cantidad" Type="Decimal" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </cc1:OpenGridView>
        <cc1:OpenGridView ID="OpenGridView1" runat="server" />
        <asp:ObjectDataSource ID="ObjectDataSourceFacturas" runat="server" 
            DeleteMethod="Delete" InsertMethod="Insert" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
            TypeName="OpenGridViewBLL.Facturas.FacturasBll" UpdateMethod="Update">
            <DeleteParameters>
                <asp:Parameter Name="original_id" Type="Decimal" />
                <asp:Parameter Name="original_fecha" Type="DateTime" />
            </DeleteParameters>
            <UpdateParameters>
                <asp:Parameter Name="id" Type="Decimal" />
                <asp:Parameter Name="fecha" Type="DateTime" />
                <asp:Parameter Name="original_id" Type="Decimal" />
                <asp:Parameter Name="original_fecha" Type="DateTime" />
            </UpdateParameters>
            <InsertParameters>
                <asp:Parameter Name="fecha" Type="DateTime" />
            </InsertParameters>
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
