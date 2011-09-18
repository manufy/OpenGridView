<%@ Page Language="C#" MasterPageFile="OpenGridWebDemo.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OpenGridWebDemo.Default" %>


<%@ Register Assembly="OpenGridView" Namespace="OpenGridView" TagPrefix="opengridview" %>
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
        DataKeyNames="id,fecha" DataSourceID="ObjectDataSourceFacturas" 
        SendUserToLastPageAfterInsert="True" ShowInsertButton="True" 
        ononinsertion="OpenGridViewFacturas_OnInsertion" ShowFooter="True" 
        ArrowDownImageUrl="~/ArrowDown.gif" ArrowUpImageUrl="~/ArrowUp.gif" 
        oninsertionvalidationgroup="ValidacionInsercion">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:TemplateField HeaderText="id" SortExpression="id">
                <EditItemTemplate>
                    <asp:Label ID="LabelEditID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                </EditItemTemplate>
                <FooterTemplate>
               
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemID" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="fecha" SortExpression="fecha">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxEditFecha" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEditFecha" runat="server" ErrorMessage="Se requiere una fecha." ControlToValidate="TextBoxEditFecha" Text="*" ValidationGroup="ValidacionEdicion"></asp:RequiredFieldValidator>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TextBoxInsertFecha" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertFecha" runat="server" ErrorMessage="Se requiere una fecha." ControlToValidate="TextBoxInsertFecha" ValidationGroup="ValidacionInsercion" Text="*"></asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemFecha" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </opengridview:OpenGridView>
    <asp:ObjectDataSource ID="ObjectDataSourceFacturas" runat="server" 
        ConflictDetection="CompareAllValues" DeleteMethod="Delete" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get" TypeName="OpenGridViewBLL.Facturas.FacturasBll" 
        UpdateMethod="Update" EnablePaging="True" SelectCountMethod="GetRowCount" 
        ConvertNullToDBNull="True" 
        oninserting="ObjectDataSourceFacturas_Inserting" SortParameterName="sortExpression">
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
</asp:Content>
