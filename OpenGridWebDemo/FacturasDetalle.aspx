<%@ Page Title="" Language="C#" MasterPageFile="~/OpenGridWebDemo.Master" AutoEventWireup="true" CodeBehind="FacturasDetalle.aspx.cs" Inherits="OpenGridWebDemo.FacturasDetalle" %>

<%@ Register Assembly="OpenGridView" Namespace="OpenControls" TagPrefix="opengridview" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="Main" runat="server">

    <asp:ValidationSummary ID="ValidationSummaryInserciones" runat="server" 
        ValidationGroup="ValidacionInsercion" />
    <asp:ValidationSummary ID="ValidationSummaryEdiciones" runat="server" 
        ValidationGroup="ValidacionEdicion" />
    <asp:ValidationSummary ID="ValidationSummaryBorrados" runat="server" 
        ValidationGroup="ValidacionBorrado" />

    <opengridview:OpenGridView ID="OpenGridViewFacturasDetalle" runat="server" 
        AutoGenerateColumns="False" DataKeyNames="id" 
        DataSourceID="ObjectDataSourceFacturasDetalle" 
        SendUserToLastPageAfterInsert="False" ShowFilter="False" 
        ShowInsertButton="True" style="margin-right: 0px" 
        oninsertcommand="OpenGridViewFacturasDetalle_InsertCommand">
        <Columns>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:TemplateField HeaderText="facturaid" SortExpression="facturaid">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxEditFacturaID" runat="server" Text='<%# Bind("facturaid") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ComboBox ID="ComboBox1" runat="server" 
                        DataSourceID="ObjectDataSourceFacturas" DataTextField="fecha" 
                        DataValueField="id" MaxLength="0" style="display: inline;">
                    </asp:ComboBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemFacturaID" runat="server" Text='<%# Bind("facturaid") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="articuloid" SortExpression="articuloid">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxEditArticuloID" runat="server" Text='<%# Bind("articuloid") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:ComboBox ID="ComboBox2" runat="server" 
                        DataSourceID="ObjectDataSourceArticulos" DataTextField="descripcion" 
                        DataValueField="id" MaxLength="0" style="display: inline;">
                    </asp:ComboBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemArticuloID" runat="server" Text='<%# Bind("articuloid") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="cantidad" SortExpression="cantidad">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxEditCantidad" runat="server" Text='<%# Bind("cantidad") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TextBoxInsertCantidad" runat="server" Text='<%# Bind("cantidad") %>'></asp:TextBox>
                    <asp:NumericUpDownExtender ID="TextBoxInsertCantidad_NumericUpDownExtender" 
                        runat="server" Enabled="True" Maximum="1.7976931348623157E+308" 
                        Minimum="-1.7976931348623157E+308" RefValues="" ServiceDownMethod="" 
                        ServiceDownPath="" ServiceUpMethod="" Tag="" TargetButtonDownID="" 
                        TargetButtonUpID="" TargetControlID="TextBoxInsertCantidad" Width="0">
                    </asp:NumericUpDownExtender>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemCantidad" runat="server" Text='<%# Bind("cantidad") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="row" HeaderText="row" 
                SortExpression="row" />
        </Columns>
    </opengridview:OpenGridView>

    <asp:ObjectDataSource ID="ObjectDataSourceFacturasDetalle" runat="server" 
        TypeName="OpenGridViewBLL.Facturas.FacturasDetalleBll" DeleteMethod="Delete" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get" UpdateMethod="Update"  ConflictDetection="CompareAllValues" EnablePaging="True" SelectCountMethod="GetRowCount" 
        ConvertNullToDBNull="False"  SortParameterName="sortExpression">
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
            <asp:Parameter Name="facturaid" Type="Decimal" />
            <asp:Parameter Name="articuloid" Type="Decimal" />
            <asp:Parameter Name="cantidad" Type="Decimal" />
        </InsertParameters>
    </asp:ObjectDataSource>


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
<asp:ObjectDataSource ID="ObjectDataSourceArticulos" runat="server" 
    DeleteMethod="Delete" InsertMethod="Insert" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
    TypeName="OpenGridWebDemo.ArticulosBll" UpdateMethod="Update">
    <DeleteParameters>
        <asp:Parameter Name="original_id" Type="Decimal" />
        <asp:Parameter Name="original_descripcion" Type="String" />
        <asp:Parameter Name="original_tipoarticuloid" Type="Decimal" />
        <asp:Parameter Name="original_campocalculadonovisible" Type="Int32" />
    </DeleteParameters>
    <UpdateParameters>
        <asp:Parameter Name="id" Type="Decimal" />
        <asp:Parameter Name="descripcion" Type="String" />
        <asp:Parameter Name="tipoarticuloid" Type="Decimal" />
        <asp:Parameter Name="campocalculadonovisible" Type="Int32" />
        <asp:Parameter Name="original_id" Type="Decimal" />
        <asp:Parameter Name="original_descripcion" Type="String" />
        <asp:Parameter Name="original_tipoarticuloid" Type="Decimal" />
        <asp:Parameter Name="original_campocalculadonovisible" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="descripcion" Type="String" />
    </InsertParameters>
</asp:ObjectDataSource>


</asp:Content>
