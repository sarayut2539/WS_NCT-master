Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class GET_DOCTOR_MARIJUANA
    Inherits System.Web.Services.WebService

    <WebMethod()>
    Public Function GET_DOCTOR_BY_CITIZEN_ID(CITIZEN_ID As String) As DataTable
        Dim dt As DataTable
        Dim BAO_FEE_MARIJUANA As New BAO_FEE.FDA_MARIJUANA
        dt = BAO_FEE_MARIJUANA.SP_GET_DOCTOR(CITIZEN_ID)
        Return dt
    End Function

End Class