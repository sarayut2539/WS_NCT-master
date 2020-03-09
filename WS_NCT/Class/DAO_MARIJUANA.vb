Namespace DAO_MARIJUANA

    Public MustInherit Class MAINCONTEXT
        Public db As New LINQ_MAREJUANADataContext

        Public datas
        Public Interface MAIN
            Sub insert()
            Sub delete()
            Sub update()
        End Interface
    End Class
    Public Class TB_MARIJUANA
        Inherits MAINCONTEXT 'เรียก Class แม่มาใช้เพื่อให้รู้จักว่าเป็น Table ไหน

        Public fields As New MARIJUANA

        Public Sub insert()
            db.MARIJUANAs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub

        Public Sub delete()
            db.MARIJUANAs.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub

        Public Sub GetDataAll()

            datas = (From p In db.MARIJUANAs Select p Order By p.IDA)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As String)

            datas = (From p In db.MARIJUANAs Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub
        Public Sub CountStatusAPPROVED()

            datas = (From p In db.MARIJUANAs Where p.STATUS_ID = "8" Or p.STATUS_ID = "9" Select p).Count

        End Sub
    End Class
    Public Class TB_MARIJUANA_RENEW
        Inherits MAINCONTEXT 'เรียก Class แม่มาใช้เพื่อให้รู้จักว่าเป็น Table ไหน

        Public fields As New MARIJUANA_RENEW

        Public Sub insert()
            db.MARIJUANA_RENEWs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub

        Public Sub delete()
            db.MARIJUANA_RENEWs.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub

        Public Sub GetDataAll()

            datas = (From p In db.MARIJUANA_RENEWs Select p Order By p.IDA)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As String)

            datas = (From p In db.MARIJUANA_RENEWs Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_FK_IDA(ByVal IDA As String)

            datas = (From p In db.MARIJUANA_RENEWs Where p.FK_MARIJUANA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub
        Public Sub GetDataby_FK_IDA_and_YEAR(ByVal IDA As String, ByVal YEAR As String)

            datas = (From p In db.MARIJUANA_RENEWs Where p.FK_MARIJUANA = IDA And p.year = YEAR Select p)
            For Each Me.fields In datas
            Next
        End Sub
    End Class

    Public Class TB_MAS_TEMPLATE_PROCESS
        Inherits MAINCONTEXT 'เรียก Class แม่มาใช้เพื่อให้รู้จักว่าเป็น Table ไหน

        Public fields As New MAS_TEMPLATE_PROCESS

        Public Sub insert()
            db.MAS_TEMPLATE_PROCESSes.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub

        Public Sub delete()
            db.MAS_TEMPLATE_PROCESSes.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub

        Public Sub GetDataAll()
            datas = (From p In db.MAS_TEMPLATE_PROCESSes Select p)
            For Each Me.fields In datas
            Next
        End Sub

        Public Sub GetDataby_IDA(ByVal IDA As String)

            datas = (From p In db.MAS_TEMPLATE_PROCESSes Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub
        Public Sub GetDataby_ProcessId_STATUS_GROUPS_PREVIEW(ByVal PROCESS_ID As String, ByVal STATUS_ID As Integer, ByVal GROUPS As Integer, ByVal PREVIEW As Integer)

            datas = (From p In db.MAS_TEMPLATE_PROCESSes Where p.PROCESS_ID = PROCESS_ID And p.STATUS_ID = STATUS_ID And p.GROUPS = GROUPS And p.PREVIEW = PREVIEW Select p)
            For Each Me.fields In datas
            Next
        End Sub
    End Class


    Public Class TB_FILE_ATTACH
        Inherits MAINCONTEXT
        Public fields As New FILE_ATTACH
        Public Sub insert()
            db.FILE_ATTACHes.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub
        Public Sub delete()
            db.FILE_ATTACHes.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub GetDataAll()
            datas = (From p In db.FILE_ATTACHes Select p)
            For Each Me.fields In datas
            Next
        End Sub
        Public Sub GetDataby_IDA(ByVal IDA As String)
            datas = (From p In db.FILE_ATTACHes Where p.IDA = IDA Select p)
            For Each Me.fields In datas
            Next
        End Sub
        Public Sub GetDataby_TR_ID(ByVal TR_ID As String)
            datas = (From p In db.FILE_ATTACHes Where p.TR_ID = TR_ID Select p)
            For Each Me.fields In datas
            Next
        End Sub
        Public Sub GetDataby_TRID_AND_TYPE(ByVal TR_ID As String, TYPE As String)
            datas = (From p In db.FILE_ATTACHes Where p.TR_ID = TR_ID And p.TYPE = TYPE Select p)
            For Each Me.fields In datas
            Next
        End Sub
    End Class

    Public Class TB_LOG_ERROR
        Inherits MAINCONTEXT
        Public fields As New LOG_ERROR
        Public Sub insert()
            db.LOG_ERRORs.InsertOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub update()
            db.SubmitChanges()
        End Sub
        Public Sub delete()
            db.LOG_ERRORs.DeleteOnSubmit(fields)
            db.SubmitChanges()
        End Sub
        Public Sub GetDataAll()
            datas = (From p In db.LOG_ERRORs Select p)
            For Each Me.fields In datas
            Next
        End Sub

    End Class
End Namespace