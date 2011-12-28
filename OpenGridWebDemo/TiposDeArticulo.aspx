<%@ Page Title="" Language="C#" MasterPageFile="~/OpenGridWebDemo.Master" AutoEventWireup="true" CodeBehind="TiposDeArticulo.aspx.cs" Inherits="OpenGridWebDemo.TiposDeArticulo" %>
<%@ Register assembly="OpenGridView" namespace="OpenControls" tagprefix="cc1" %>
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
  

    <cc1:OpenGridView ID="OpenGridViewTiposDeArticulo" runat="server" 
        AutoGenerateColumns="False" 
        DataSourceID="ObjectDataSourceTiposDeArticulo" 
        SendUserToLastPageAfterInsert="False" ShowFilter="True" 
        ShowInsertButton="True" ShowFooter="True" AllowPaging="True" 
        AllowSorting="True" 
        onfiltercommand="OpenGridViewTiposDeArticulo_FilterCommand" >
        <Columns>
           <asp:TemplateField ShowHeader="False">
             <EditItemTemplate>
                     <asp:ImageButton ID="ImageButtonEditUpdate" runat="server" CausesValidation="true" 
                     CommandName = "Update" ImageUrl='<%# OpenGridViewTiposDeArticulo.GetResourceCommandImageUrl("Update") %>' />
                     <asp:ImageButton ID="ImageButtonEditCancel" runat="server" CausesValidation="False" 
                     CommandName = "Cancel" ImageUrl='<%# OpenGridViewTiposDeArticulo.GetResourceCommandImageUrl("Cancel") %>' />
                </EditItemTemplate>
                <FooterTemplate>
                </FooterTemplate>
                <ItemTemplate>
                     <asp:ImageButton ID="ImageButtonItemEdit" runat="server" CausesValidation="true" 
                     CommandName = "Edit" ImageUrl='<%# OpenGridViewTiposDeArticulo.GetResourceCommandImageUrl("Edit") %>' />
                     <asp:ImageButton ID="ImageButtonItemDelete" runat="server" CausesValidation="False" 
                     CommandName = "Delete" ImageUrl='<%# OpenGridViewTiposDeArticulo.GetResourceCommandImageUrl("Delete") %>' /> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="id" HeaderText="id" 
                SortExpression="id" InsertVisible="False" ReadOnly="True" />
            <asp:TemplateField HeaderText="descripcion" SortExpression="descripcion">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("descripcion") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TextBoxInsertDescripcion" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorInsertDescripcion" runat="server" ErrorMessage="Se necesita una descripción." ValidationGroup="ValidacionInsercion" ControlToValidate="TextBoxInsertDescripcion" Text="*"></asp:RequiredFieldValidator>
       
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </cc1:OpenGridView>
    <asp:ObjectDataSource ID="ObjectDataSourceTiposDeArticulo" runat="server" 
        DeleteMethod="Delete" InsertMethod="Insert" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get" 
        TypeName="OpenGridWebDemo.TiposDeArticuloBll" UpdateMethod="Update"
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
