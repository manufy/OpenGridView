<%@ Page Title="" Language="C#" MasterPageFile="OpenGridWebDemo.Master" %>

<%@ Register Assembly="OpenGridView" Namespace="OpenGridView" TagPrefix="opengridview" %>
<asp:Content runat="server" ID="Head" ContentPlaceHolderID="Head"></asp:Content>
<asp:Content runat="server" ID="Main" ContentPlaceHolderID="Main">
    <opengridview:OpenGridView ID="OpenGridViewFacturas" runat="server" 
        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" 
        DataKeyNames="id,fecha" DataSourceID="ObjectDataSourceFacturas" 
        SendUserToLastPageAfterInsert="False" ShowInsertButton="True" 
        ononinsertion="OpenGridViewFacturas_OnInsertion">
        <Columns>
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" 
                ShowSelectButton="True" />
            <asp:BoundField DataField="id" HeaderText="id" ReadOnly="True" 
                SortExpression="id" />
            <asp:TemplateField HeaderText="fecha" SortExpression="fecha">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBoxEditFecha" runat="server" Text='<%# Bind("fecha") %>'></asp:TextBox>
                </EditItemTemplate>
                <FooterTemplate>
                    <asp:TextBox ID="TextBoxInsertFecha" runat="server"></asp:TextBox>
                </FooterTemplate>
                <ItemTemplate>
                    <asp:Label ID="LabeItemFecha" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </opengridview:OpenGridView>
    <asp:ObjectDataSource ID="ObjectDataSourceFacturas" runat="server" 
        ConflictDetection="CompareAllValues" DeleteMethod="Delete" 
        InsertMethod="Insert" OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get" TypeName="OpenGridViewBLL.Facturas.FacturasBll" 
        UpdateMethod="Update">
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
