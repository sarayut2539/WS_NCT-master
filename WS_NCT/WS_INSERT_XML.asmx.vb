Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WS_INSERT_XML
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function INSERT_XML(ByVal XML_DATA As String) As String
        Dim chk As String = String.Empty
        Try
            Dim dao As New DAO.TB_LOG_XML_EXPORT
            dao.fields.XML_DATA = XML_DATA
            dao.fields.CREATE_DATE = Date.Now
            dao.insert()
            chk = "บันทึกข้อมูลเรียบร้อยแล้ว"
        Catch ex As Exception
            chk = "มีข้อผิดพลาดในการบันทึกข้อมูล"
        End Try


        Return chk
    End Function

End Class