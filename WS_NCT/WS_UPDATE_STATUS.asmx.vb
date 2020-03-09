Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.ComponentModel

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")> _
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<ToolboxItem(False)> _
Public Class WS_UPDATE_STATUS_PAY
    Inherits System.Web.Services.WebService

    <System.Web.Services.Protocols.SoapDocumentMethod(OneWay:=True)>
    <WebMethod(Description:="ปรับสถานะและส่งxmlระบบใบอนุญาตวัตถุเสพติด")>
    Public Sub update_status_pay_NCT(ByVal ref01 As String, ByVal ref02 As String)
        Dim txt As String = String.Empty
        Dim PROCESS_ID As Integer = 0
        Dim resMesg As String = ""
        Dim dvcd As Integer = 2

        Try
                Dim dao_fees As New DAO.TB_FEE
                dao_fees.GetDataby_ref1_ref2(ref01, ref02)
                Dim dao_dets As New DAO.TB_FEEDTL

                dao_dets.Getdata_by_fee_id(dao_fees.fields.IDA)

                For Each dao_dets.fields In dao_dets.datas
                    Try
                        PROCESS_ID = dao_dets.fields.process_id
                    Catch ex As Exception

                    End Try
                If PROCESS_ID = 940001 Or PROCESS_ID = 940002 Or PROCESS_ID = 940003 Or PROCESS_ID = 940004 Then
                    Dim ws_cer As New WS_UPDATE_PAYMENT_CER.WS_UPDATE_PAYMENT_CER
                    ws_cer.CER_NCT_SERVICE(PROCESS_ID, dao_dets.fields.fk_id)


                ElseIf PROCESS_ID = 291508001 Or PROCESS_ID = 291608001 Or PROCESS_ID = 292608001 Or PROCESS_ID = 293608001 Or PROCESS_ID = 293608002 Or
                    PROCESS_ID = 294508001 Or PROCESS_ID = 294508002 Then

                    RENEW_UPDATE_STATUS(dao_dets.fields.fk_id)

                Else
                    Try
                        Dim bao As New BAO_FEE.LGT_NCT2_128
                        Dim bao_MARIJUANA As New BAO_FEE.FDA_MARIJUANA
                        Try
                            bao.SP_FEE_UPDATE_STATUS_PAY_COMPLETE(ref01, ref02, dao_dets.fields.process_id, dvcd)
                        Catch ex As Exception
                            bao_MARIJUANA.SP_FEE_UPDATE_STATUS_PAY_COMPLETE(ref01, ref02, dao_dets.fields.process_id, dvcd)
                        End Try




                        Dim _IDA As Integer = 0
                        Dim bao22 As New BAO_FEE.LGT_NCT2_128
                        Try
                            _IDA = dao_dets.fields.fk_id 'bao22.SP_GET_IDA_NCT_FROM_FEE(ref1, ref2)
                        Catch ex As Exception

                        End Try

                        '----------------------------ส่ง xml ให้ห้องยา-------------------------------------

                        Dim ws_insert As New WS_NCT_INSERT.WS_NCT_INSERT
                        Dim ws_gen_xml As New WS_NCT_INSERT_XML.NCT_INSERT_XML

                        Dim IDA_NEW As String = String.Empty
                        Dim string_xml As String = String.Empty
                        Dim CC As String = String.Empty
                        Dim email As String = String.Empty
                        Dim title As String = String.Empty
                        Dim content As String = String.Empty
                        Dim filename As String = String.Empty


                        If PROCESS_ID <> 29100001 And PROCESS_ID <> 29100002 And PROCESS_ID <> 29200001 And PROCESS_ID <> 29200002 _
                                    And PROCESS_ID <> 29200003 And PROCESS_ID <> 29200005 And PROCESS_ID <> 29200004 _
                                    And PROCESS_ID <> 29300001 And PROCESS_ID <> 29300002 And PROCESS_ID <> 29300003 And PROCESS_ID <> 29300005 _
                                    And PROCESS_ID <> 29300004 And PROCESS_ID <> 29300011 And PROCESS_ID <> 29300012 And PROCESS_ID <> 29300013 _
                                    And PROCESS_ID <> 29300014 And PROCESS_ID <> 29300015 And PROCESS_ID <> 29100003 And PROCESS_ID <> 29100004 _
                                    Then
                            IDA_NEW = ws_insert.NCT_INSERT(_IDA)
                        Else
                            IDA_NEW = ws_insert.NCT_INSERT_YORSOR_4(_IDA)
                        End If

                        string_xml = ws_gen_xml.NCT_INSERT(IDA_NEW, filename)

                        ' ------------------ส่ง xml ที่ server------------------
                        Try
                            Dim ws_send_xml As New WS_NCT_INSERT_XML_LCN.NCT_INSERT_XML_LCN

                            'txt = ws_send_xml.NCT_INSERT_LCN_FOLDER(string_xml, filename, CONVERT_THAI_YEAR(CDec(Date.Now.Year)))
                            ws_send_xml.NCT_INSERT_LCN_FOLDER_NO_RETURN(string_xml, filename, CONVERT_THAI_YEAR(CDec(Date.Now.Year)))
                        Catch ex As Exception

                        End Try

                        ' ------------------ส่ง xml ที่ email------------------
                        email = "saree@systemsthai.com"
                        CC = "watchara@fusionsol.com"
                        title = "XML_" & filename
                        content = ""
                        SendMail_CC_ATTACH(content, email, title, CC, string_xml, filename)
                        '-------------------------------------------------
                    Catch ex As Exception

                    End Try




                End If


                Next
            Catch ex As Exception
                resMesg = "ส่วนของ UPDATE : System error = " & ex.Message
                Insert_log_error(ref01, ref02, "", resMesg, "", 0)
            End Try



    End Sub

    Private Sub SendMail_CC_ATTACH(ByVal Content As String, ByVal email As String, ByVal title As String, ByVal CC As String, ByVal string_xml As String, ByVal filename As String)
        Dim mm As New WS_FDA_MAIL.FDA_MAIL
        Dim mcontent As New WS_FDA_MAIL.Fields_Mail

        mcontent.EMAIL_CONTENT = Content
        mcontent.EMAIL_FROM = "fda_info@fda.moph.go.th"
        mcontent.EMAIL_PASS = "deeku181"
        mcontent.EMAIL_TILE = title
        mcontent.EMAIL_TO = email


        mm.SendMail_CC_ATTACHAsync(mcontent, CC, string_xml, filename)

    End Sub

    ''' <summary>
    ''' เช็ค ค.ศ. เปลี่ยนเป็น พ.ศ. ตามที่ใส่
    ''' </summary>
    ''' <param name="YEAR"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function CONVERT_THAI_YEAR(ByVal YEAR As Integer) As String

        If YEAR <= 2500 Then
            YEAR += 543
        End If
        Return YEAR.ToString()
    End Function

    Private Sub Insert_log_error(ByVal ref01 As String, ByVal ref02 As String, ByVal xmlname As String, ByVal error_str As String, _
                      ByVal account_id As String, ByVal status_id As Integer)
        Dim dao_logs As New DAO.TB_FEE_LOGS
        dao_logs.fields.CREATEDATE = Date.Now
        dao_logs.fields.ref1 = ref01
        dao_logs.fields.ref2 = ref02
        dao_logs.fields.STEP = 0
        dao_logs.fields.RESULT = error_str
        dao_logs.fields.XML_PATH = xmlname
        dao_logs.fields.ACCOUNT_ID = account_id
        dao_logs.fields.STATUS_ID = status_id
        dao_logs.insert()
    End Sub


    Private Sub RENEW_UPDATE_STATUS(renew_ida As String)
        Try

            Dim MARIJUANA_RENEW As New DAO_MARIJUANA.TB_MARIJUANA_RENEW
            MARIJUANA_RENEW.GetDataby_IDA(renew_ida)
            Dim MARIJUANA As New DAO_MARIJUANA.TB_MARIJUANA
            MARIJUANA.GetDataby_IDA(MARIJUANA_RENEW.fields.FK_MARIJUANA)
            Dim STATUS_ID As String = ""
            Dim CONSIDER_DATE As String = CDate("1/01/" & MARIJUANA_RENEW.fields.year)
            Dim CONSIDER_DATE_DISPLAY As String = CDate("1/01/" & MARIJUANA_RENEW.fields.year).ToLongDateString




            STATUS_ID = "8"
            MARIJUANA.fields.appvdate = Date.Now
            MARIJUANA.fields.APPROVE_BY = "สำนักงานคณะกรรมการอาหารและยา"
            MARIJUANA.fields.CONSIDER_DATE = CONSIDER_DATE
            MARIJUANA.fields.CONSIDER_DATE_DISPLAY = CONSIDER_DATE_DISPLAY
            MARIJUANA.fields.STATUS_ID = STATUS_ID
            '   MARIJUANA.fields.UPDATE_BY = _CLS.CITIZEN_ID
            ' MARIJUANA.fields.UPDATE_DATE = Date.Now
            MARIJUANA.fields.EXP_DATE = MARIJUANA_RENEW.fields.EXP_DATE
            MARIJUANA.fields.EXP_DATE_DISPLAY = MARIJUANA_RENEW.fields.EXP_DATE_DISPLAY
            MARIJUANA.fields.year = MARIJUANA_RENEW.fields.year
            MARIJUANA.fields.write_at = MARIJUANA_RENEW.fields.write_at
            MARIJUANA.fields.write_date = CDate(MARIJUANA_RENEW.fields.write_date).ToString("d MMM yyyy")
            MARIJUANA.update()


            MARIJUANA_RENEW.fields.appvdate = Date.Now
            MARIJUANA_RENEW.fields.APPROVE_BY = "สำนักงานคณะกรรมการอาหารและยา"
            MARIJUANA_RENEW.fields.CONSIDER_DATE = CONSIDER_DATE
            MARIJUANA_RENEW.fields.CONSIDER_DATE_DISPLAY = CONSIDER_DATE_DISPLAY
            MARIJUANA_RENEW.fields.STATUS_ID = STATUS_ID
            '  MARIJUANA_RENEW.fields.UPDATE_BY = _CLS.CITIZEN_ID
            '  MARIJUANA_RENEW.fields.UPDATE_DATE = Date.Now
            MARIJUANA_RENEW.update()

            'Dim FILE_ATTACHes As New DAO_NCT.TB_FILE_ATTACH
            'FILE_ATTACHes.GetDataby_TR_ID(MARIJUANA_RENEW.fields.TR_ID)
            'If FILE_ATTACHes.fields.IDA <> 0 Then
            '    If Checkfile(_PATH_FILE + "ATTACH_FILE/" + MARIJUANA.fields.TR_ID.ToString + "_1013.jpg") = True Then
            '        My.Computer.FileSystem.DeleteFile(_PATH_FILE + "ATTACH_FILE/" + MARIJUANA.fields.TR_ID.ToString + "_1013.jpg")
            '    End If

            '    My.Computer.FileSystem.CopyFile(_PATH_FILE + FILE_ATTACHes.fields.PATH, _PATH_FILE + "ATTACH_FILE/" + MARIJUANA.fields.TR_ID.ToString + "_1013.jpg")
            'End If
            'Dim DAO_MAS_TEMP_PROCESS As New DAO_NCT.TB_MAS_TEMPLATE_PROCESS
            'DAO_MAS_TEMP_PROCESS.GetDataby_ProcessId_STATUS_GROUPS_PREVIEW(MARIJUANA.fields.process_id, MARIJUANA.fields.STATUS_ID, 0, 0)
            'Dim FileName As String = "MARIJUANA-" + MARIJUANA.fields.process_id + "-" + MARIJUANA.fields.year + "-" + MARIJUANA.fields.TR_ID.ToString
            'Dim PATH_PDF_OUTPUT As String = _PATH_FILE + DAO_MAS_TEMP_PROCESS.fields.PDF_OUTPUT + "\" + FileName + ".pdf"
            'Dim PATH_XML As String = _PATH_FILE + DAO_MAS_TEMP_PROCESS.fields.XML_PATH + "\" + FileName + ".xml"
            'Dim PATH_PDF_TEMP As String = _PATH_FILE + "PDF_TEMPLATE\" + DAO_MAS_TEMP_PROCESS.fields.PDF_TEMPLATE
            'Dim PDF_XML_MARIJUANA As New PDF_XML.BIND_PDF_MARIJUANA
            'PDF_XML_MARIJUANA.SHOW_PDF(MARIJUANA.fields.IDA, PATH_PDF_OUTPUT, PATH_PDF_TEMP, PATH_XML, FileName, 0)

            'Dim FILE_ATTACHes As New DAO_MARIJUANA.TB_FILE_ATTACH
            'FILE_ATTACHes.GetDataby_TR_ID(MARIJUANA_RENEW.fields.TR_ID)
            'If FILE_ATTACHes.fields.IDA <> 0 Then
            '    If Checkfile(_PATH_FILE + "ATTACH_FILE/" + MARIJUANA.fields.TR_ID.ToString + "_1013.jpg") = True Then
            '        My.Computer.FileSystem.DeleteFile(_PATH_FILE + "ATTACH_FILE/" + MARIJUANA.fields.TR_ID.ToString + "_1013.jpg")
            '    End If

            '    My.Computer.FileSystem.CopyFile(_PATH_FILE + FILE_ATTACHes.fields.PATH, _PATH_FILE + "ATTACH_FILE/" + MARIJUANA.fields.TR_ID.ToString + "_1013.jpg")
            'End If
            'Dim DAO_MAS_TEMP_PROCESS As New DAO_MARIJUANA.TB_MAS_TEMPLATE_PROCESS
            'DAO_MAS_TEMP_PROCESS.GetDataby_ProcessId_STATUS_GROUPS_PREVIEW(MARIJUANA.fields.process_id, MARIJUANA.fields.STATUS_ID, 0, 0)
            'Dim FileName As String = "MARIJUANA-" + MARIJUANA.fields.process_id + "-" + MARIJUANA.fields.year + "-" + MARIJUANA.fields.TR_ID.ToString
            'Dim PATH_PDF_OUTPUT As String = _PATH_FILE + DAO_MAS_TEMP_PROCESS.fields.PDF_OUTPUT + "\" + FileName + ".pdf"
            'Dim PATH_XML As String = _PATH_FILE + DAO_MAS_TEMP_PROCESS.fields.XML_PATH + "\" + FileName + ".xml"
            'Dim PATH_PDF_TEMP As String = _PATH_FILE + "PDF_TEMPLATE\" + DAO_MAS_TEMP_PROCESS.fields.PDF_TEMPLATE
            'Dim PDF_XML_MARIJUANA As New PDF_XML.BIND_PDF_MARIJUANA
            'PDF_XML_MARIJUANA.SHOW_PDF(MARIJUANA.fields.IDA, PATH_PDF_OUTPUT, PATH_PDF_TEMP, PATH_XML, FileName, 0)

        Catch ex As Exception
            Dim LOG_ERROR As New DAO_MARIJUANA.TB_LOG_ERROR
            LOG_ERROR.fields.EX = ex.Message & "และ" & ex.StackTrace
            LOG_ERROR.fields.DATE = Date.Now
            LOG_ERROR.fields.FUNCTION_ERROR_NAME = "RENEW_UPDATE_STATUS"
            LOG_ERROR.insert()

        End Try



    End Sub


    Public Function Checkfile(ByVal Path As String) As Boolean
        Dim check As Boolean = System.IO.File.Exists(Path)
        Return check
    End Function

End Class