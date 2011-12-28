<%@ Page Language="C#" MasterPageFile="OpenGridWebDemo.Master" AutoEventWireup="true" CodeBehind="DefaultOK.aspx.cs" Inherits="OpenGridWebDemo.Default" %>


<%@ Register Assembly="OpenGridView" Namespace="OpenControls" TagPrefix="opengridview" %>
<asp:Content runat="server" ID="Head" ContentPlaceHolderID="Head"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="Main">

    <asp:ValidationSummary ID="ValidationSummaryInserciones" runat="server" 
        ValidationGroup="ValidacionInsercion" />
    <asp:ValidationSummary ID="ValidationSummaryEdiciones" runat="server" 
        ValidationGroup="ValidacionEdicion" />
    <asp:ValidationSummary ID="ValidationSummaryBorrados" runat="server" 
        ValidationGroup="ValidacionBorrado" />

    <opengridview:OpenGridView ID="OpenGridViewFacturas" runat="server" ShowFilter="False"
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
        DataKeyNames="id" DataSourceID="ObjectDataSourceFacturas" 
        SendUserToLastPageAfterInsert="True" ShowInsertButton="True" 
        
        ArrowDownImageUrl="~/ArrowDown.gif" ArrowUpImageUrl="~/ArrowUp.gif" 
        oninsertionvalidationgroup="ValidacionInsercion" 
       
         
        BackColor="White" 
        BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Horizontal" NoDataShowHeader="False" 
        >
      
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BackColor="#F7F7F7" />
        <Columns>
               <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                   ReadOnly="True" SortExpression="id" />
               <asp:BoundField DataField="fecha" HeaderText="fecha" SortExpression="fecha" />
               <asp:BoundField DataField="ROW" HeaderText="ROW" SortExpression="ROW" />
        </Columns>
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
    </opengridview:OpenGridView>
    <asp:ObjectDataSource ID="ObjectDataSourceFacturas" runat="server" 
        ConflictDetection="CompareAllValues" DeleteMethod="Delete" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get" TypeName="OpenGridViewBLL.Facturas.FacturasBll" 
        UpdateMethod="Update" EnablePaging="True" SelectCountMethod="GetRowCount" 
        ConvertNullToDBNull="True" 
        SortParameterName="sortExpression">
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
   <asp:ObjectDataSource ID="ObjectDataSourceDetalle" runat="server" 
                      
                            DeleteMethod="Delete" InsertMethod="Insert" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByFacturaId" 
                            TypeName="OpenGridViewBLL.Facturas.FacturasDetalleBll" 
                            ConflictDetection="CompareAllValues" 
                            UpdateMethod="Update" onselecting="ObjectDataSourceDetalle_Selecting" 
                           
                           >
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
                                <asp:Parameter Name="id" Type="Decimal" />
                            </SelectParameters>
                            <InsertParameters>
                                <asp:Parameter Name="facturaid" Type="Decimal" />
                                <asp:Parameter Name="articuloid" Type="Decimal" />
                                <asp:Parameter Name="cantidad" Type="Decimal" />
                            </InsertParameters>
                        </asp:ObjectDataSource>
</asp:Content>
