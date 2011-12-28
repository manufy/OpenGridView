<%@ Page Title="" Language="C#" MasterPageFile="~/OpenGridWebDemo.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="OpenGridWebDemo.Articulos" %>
<%@ Register assembly="OpenGridView" namespace="OpenGridView" tagprefix="opengridview" %>
<asp:Content ID="ContentHead" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="ContentMain" ContentPlaceHolderID="Main" runat="server">


    <asp:ValidationSummary ID="ValidationSummaryInserciones" runat="server" 
        ValidationGroup="ValidacionInsercion" />
    <asp:ValidationSummary ID="ValidationSummaryEdiciones" runat="server" 
        ValidationGroup="ValidacionEdicion" />
    <asp:ValidationSummary ID="ValidationSummaryBorrados" runat="server" 
        ValidationGroup="ValidacionBorrado" />

    <asp:Label ID="DeleteConflictMessage" runat="server" Visible="False" EnableViewState="False" CssClass="Warning"
        Text="The record you attempted to delete has been modified by another user since you last visited this page. Your delete was cancelled to allow you to review the other user's changes and determine if you want to continue deleting this record." /> 
    <asp:Label ID="UpdateConflictMessage" runat="server" Visible="False" EnableViewState="False" CssClass="Warning" 
        Text="The record you attempted to update has been modified by another user since you started the update process. Your changes have been replaced with the current values. Please review the existing values and make any needed changes." />
  

    <opengridview:OpenGridView ID="OpenGridViewArticulos" runat="server" 
        DataSourceID="ObjectDataSourceArticulos" SendUserToLastPageAfterInsert="True" 
        ShowFilter="False" ShowInsertButton="True" AllowPaging="True" 
        oninsertionvalidationgroup="ValidacionInsercion" AllowSorting="True" 
        AutoGenerateColumns="False" 
        oninsertcommand="OpenGridViewArticulos_InsertCommand" ShowFooter="True" 
        style="margin-right: 0px">
    
        <Columns>               
        
          <asp:TemplateField ShowHeader="False">
             <EditItemTemplate>
                     <asp:ImageButton ID="ImageButtonEditUpdate" runat="server" CausesValidation="true" 
                     CommandName = "Update" ImageUrl='<%# OpenGridViewArticulos.GetResourceCommandImageUrl("Update") %>' />
                     <asp:ImageButton ID="ImageButtonEditCancel" runat="server" CausesValidation="False" 
                     CommandName = "Cancel" ImageUrl='<%# OpenGridViewArticulos.GetResourceCommandImageUrl("Cancel") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
                <ItemTemplate>
                     <asp:ImageButton ID="ImageButtonItemEdit" runat="server" CausesValidation="true" 
                     CommandName = "Edit" ImageUrl='<%# OpenGridViewArticulos.GetResourceCommandImageUrl("Edit") %>' />
                     <asp:ImageButton ID="ImageButtonItemDelete" runat="server" CausesValidation="False" 
                     CommandName = "Delete" ImageUrl='<%# OpenGridViewArticulos.GetResourceCommandImageUrl("Delete") %>' /> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="id" InsertVisible="False" 
                ReadOnly="True" SortExpression="id" />
            <asp:TemplateField HeaderText="descripcion" SortExpression="descripcion">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxItemDescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TextBoxInsertDesccripcion" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertDescripcion" runat="server" ErrorMessage="Se necesita una descripción." ValidationGroup="ValidacionInsercion" ControlToValidate="TextBoxInsertDesccripcion" Text="*"></asp:RequiredFieldValidator>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemDescripcion" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="tipoarticuloid" SortExpression="tipoarticuloid">
                <ItemTemplate>
                    <asp:Label ID="LabelItemTipoArticuloID" runat="server" Text='<%# Bind("tipoarticuloid") %>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxItemTipoArticuloID" runat="server" Text='<%# Bind("tipoarticuloid") %>'></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="campocalculadonovisible" 
                SortExpression="campocalculadonovisible" Visible="False">
                <EditItemTemplate>
                    <asp:Label ID="LabelEditCampoCalculadoNoVisible" runat="server" Text='<%# Bind("campocalculadonovisible") %>'></asp:Label>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabelItemCampoCalculadoNoVisible" runat="server" Text='<%# Bind("campocalculadonovisible") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </opengridview:OpenGridView>
    <asp:ObjectDataSource ID="ObjectDataSourceArticulos" runat="server" 
        TypeName="OpenGridWebDemo.ArticulosBll" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get" UpdateMethod="Update" 
        SortParameterName="sortExpression" EnablePaging="True" 
        SelectCountMethod="GetRowCount" ConflictDetection="CompareAllValues" 
        DeleteMethod="Delete">
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
            <asp:Parameter Name="tipoarticuloid" Type="Decimal" />
            <asp:Parameter Name="campocalculadonovisible" Type="Int32" />    
        </InsertParameters>
    </asp:ObjectDataSource>
</asp:Content>
