Namespace DAO
    Public MustInherit Class MAINCONTEXT
        Public db As New LINQ_NCTDataContext
        Public db_fee As New LINQ_FEEDataContext
        Public datas
        Public Interface MAIN
            Sub insert()
            Sub delete()
            Sub update()
        End Interface
    End Class

#Region "NCT"
    Public Class TB_LICENSE_LOCATION
        Inherits MAINCONTEXT
        Public fields As New LICENSE_LOCATION
        Public Sub insert()
            db.LICENSE_LOCATIONs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub
        Public Sub delete()
            db.LICENSE_LOCATIONs.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub GetDataby_FK_IDA(ByVal FK_IDA As Integer)
            datas = (From p In db.LICENSE_LOCATIONs Where p.FK_IDA = FK_IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As Integer)
            datas = (From p In db.LICENSE_LOCATIONs Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

    End Class

    Public Class TB_YORSOR4
        Inherits MAINCONTEXT
        Public fields As New YORSOR4
        Public Sub insert()
            db.YORSOR4s.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub
        Public Sub delete()
            db.YORSOR4s.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub GetDataby_FK_IDA(ByVal FK_IDA As Integer)
            datas = (From p In db.YORSOR4s Where p.FK_IDA = FK_IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As Integer)
            datas = (From p In db.YORSOR4s Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

    End Class

    Public Class TB_OVERUNIT
        Inherits MAINCONTEXT
        Public fields As New OVERUNIT
        Public Sub insert()
            db.OVERUNITs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub
        Public Sub delete()
            db.OVERUNITs.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub GetDataby_FK_IDA(ByVal FK_IDA As Integer)
            datas = (From p In db.OVERUNITs Where p.FK_IDA = FK_IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As Integer)
            datas = (From p In db.OVERUNITs Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

    End Class

    Public Class TB_SUBSTITUTE_REQUEST
        Inherits MAINCONTEXT
        Public fields As New SUBSTITUTE_REQUEST
        Public Sub insert()
            db.SUBSTITUTE_REQUESTs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub
        Public Sub delete()
            db.SUBSTITUTE_REQUESTs.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub GetDataby_FK_IDA(ByVal FK_IDA As Integer)
            datas = (From p In db.SUBSTITUTE_REQUESTs Where p.FK_IDA = FK_IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As Integer)
            datas = (From p In db.SUBSTITUTE_REQUESTs Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

    End Class

    Public Class TB_RENEW_REQUEST
        Inherits MAINCONTEXT
        Public fields As New RENEW_REQUEST
        Public Sub insert()
            db.RENEW_REQUESTs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub
        Public Sub delete()
            db.RENEW_REQUESTs.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub GetDataby_FK_IDA(ByVal FK_IDA As Integer)
            datas = (From p In db.RENEW_REQUESTs Where p.FK_IDA = FK_IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As Integer)
            datas = (From p In db.RENEW_REQUESTs Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

    End Class

    Public Class TB_LOG_XML_EXPORT
        Inherits MAINCONTEXT
        Public fields As New LOG_XML_EXPORT
        Public Sub insert()
            db.LOG_XML_EXPORTs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub
        Public Sub delete()
            db.LOG_XML_EXPORTs.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As Integer)
            datas = (From p In db.RENEW_REQUESTs Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

    End Class

#End Region


#Region "FEE"

    Public Class TB_FEE_LOGS
        Inherits MainContext

        Public fields As New fee_log

        Public Sub insert()
            db_fee.fee_logs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub

        Public Function Count_Confirm_by_ref1_ref2(ByVal ref1 As String, ByVal ref2 As String) As Integer
            Dim i As Integer = 0
            datas = (From p In db_fee.fee_logs Where p.ref1 = ref1 And p.ref2 = ref2 And p.RESULT.Contains("confirm") Select p)
            For Each Me.fields In datas
                i += 1
            Next
            Return i
        End Function
        Public Function Count_Verify_by_ref1_ref2(ByVal ref1 As String, ByVal ref2 As String) As Integer
            Dim i As Integer = 0
            datas = (From p In db_fee.fee_logs Where p.ref1 = ref1 And p.ref2 = ref2 And p.RESULT.Contains("verify") Select p)
            For Each Me.fields In datas
                i += 1
            Next
            Return i
        End Function
    End Class

    Public Class TB_FEE
        Inherits MainContext
        Public fields As New fee

        Private _Details As New List(Of fee)
        Public Property Details() As List(Of fee)
            Get
                Return _Details
            End Get
            Set(ByVal value As List(Of fee))
                _Details = value
            End Set
        End Property
        Public Sub Getdata_by_feeno_dvcd_feeabbr_and_pvncd(ByVal feeno As String, ByVal dvcd As Integer, ByVal feeabbr As String, ByVal pvncd As Integer)
            datas = From p In db_fee.fees Where p.feeno = feeno And p.dvcd = dvcd And p.feeabbr = feeabbr And p.pvncd = pvncd Select p
            For Each Me.fields In datas

            Next

        End Sub
        Public Sub Getdata_by_feeno_and_dvcd(ByVal feeno As String, ByVal dvcd As Integer)
            datas = From p In db_fee.fees Where p.feeno = feeno And p.dvcd = dvcd Select p
            For Each Me.fields In datas

            Next

        End Sub
        Public Sub GetDataby_ref1_ref2(ByVal ref1 As String, ByVal ref2 As String)
            datas = (From p In db_fee.fees Where p.ref01 = ref1 And p.ref02 = ref2 Select p)
            For Each Me.fields In datas

            Next
        End Sub
        Public Sub GetDataby_ref1(ByVal ref1 As String)
            datas = (From p In db_fee.fees Where p.ref01 = ref1 Select p)
            For Each Me.fields In datas

            Next
        End Sub
        Public Sub GetDataby_ref2(ByVal ref2 As String)
            datas = (From p In db_fee.fees Where p.ref02 = ref2 Select p)
            For Each Me.fields In datas

            Next
        End Sub
        Public Function Countby_ref1_ref2(ByVal ref1 As String, ByVal ref2 As String) As Integer
            Dim i As Integer = 0
            datas = (From p In db_fee.fees Where p.ref01 = ref1 And p.ref02 = ref2 Select p)
            For Each Me.fields In datas
                i += 1
            Next
            Return i
        End Function
        Public Function Countby_ref1(ByVal ref1 As String) As Integer
            Dim i As Integer = 0
            datas = (From p In db_fee.fees Where p.ref01 = ref1 Select p)
            For Each Me.fields In datas
                i += 1
            Next
            Return i
        End Function
        Public Sub insert()
            db_fee.fees.InsertOnSubmit(fields)
            db_fee.SubmitChanges()
        End Sub
        Public Sub delete()
            db_fee.fees.DeleteOnSubmit(fields)
            db_fee.SubmitChanges()
        End Sub
        Public Sub update()
            db_fee.SubmitChanges()
        End Sub


    End Class

    Public Class TB_FEEDTL
        Inherits MainContext
        Public fields As New feedtl

        Private _Details As New List(Of feedtl)
        Public Property Details() As List(Of feedtl)
            Get
                Return _Details
            End Get
            Set(ByVal value As List(Of feedtl))
                _Details = value
            End Set
        End Property
        Public Sub Getdata_by_fee_id(ByVal IDA As Integer)
            datas = From p In db_fee.feedtls Where p.fk_fee = IDA Select p
            For Each Me.fields In datas

            Next
        End Sub

        Public Function GetDataby_fk_fee(ByVal fk_fee As Integer) As Double
            Dim ant As Double = 0
            datas = (From p In db_fee.feedtls Where p.fk_fee = fk_fee Select p)
            For Each Me.fields In datas
                ant += Me.fields.amt
            Next
            Return ant
        End Function

        Public Sub update()
            db_fee.SubmitChanges()
        End Sub
        Public Sub insert()
            db_fee.feedtls.InsertOnSubmit(fields)
            db_fee.SubmitChanges()
        End Sub
        Public Sub delete()
            db_fee.feedtls.DeleteOnSubmit(fields)
            db_fee.SubmitChanges()
        End Sub
    End Class
#End Region
End Namespace
