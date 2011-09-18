<%@ Page Title="" Language="C#" MasterPageFile="~/OpenGridWebDemo.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="OpenGridWebDemo.Articulos" %>
<%@ Register assembly="OpenGridView" namespace="OpenGridView" tagprefix="opengridview" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="Main" runat="server">
    <opengridview:OpenGridView ID="OpenGridViewArticulos" runat="server" 
        DataSourceID="ObjectDataSourceArticulos" SendUserToLastPageAfterInsert="True" 
        ShowFilter="False" ShowInsertButton="True" AllowPaging="True" 
        oninsertionvalidationgroup="ValidacionInsercion" AllowSorting="True" 
        AutoGenerateColumns="False" DataKeyNames="id,descripcion" 
        >
    
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:BoundField DataField="descripcion" HeaderText="descripcion" 
                SortExpression="descripcion" />
        </Columns>
    </opengridview:OpenGridView>
    <asp:ObjectDataSource ID="ObjectDataSourceArticulos" runat="server" 
        TypeName="OpenGridWebDemo.ArticulosBll" DeleteMethod="Delete" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get" UpdateMethod="Update" 
        SortParameterName="sortExpression" EnablePaging="True" 
        SelectCountMethod="GetRowCount" ConflictDetection="CompareAllValues">
        <DeleteParameters>
            <asp:Parameter Name="original_id" Type="Decimal" />
            <asp:Parameter Name="original_descripcion" Type="String" />
        </DeleteParameters>
        <UpdateParameters>
            <asp:Parameter Name="id" Type="Decimal" />
            <asp:Parameter Name="descripcion" Type="String" />
            <asp:Parameter Name="original_id" Type="Decimal" />
            <asp:Parameter Name="original_descripcion" Type="String" />
        </UpdateParameters>
        <InsertParameters>
            <asp:Parameter Name="descripcion" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
