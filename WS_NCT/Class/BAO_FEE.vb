Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Namespace BAO_FEE
    Public Class FEE
        Public Function Queryds(ByVal Commands As String) As DataTable
            Dim dt As New DataTable
            Dim MyConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FDA_FEEConnectionString").ConnectionString)
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
            Return dt
        End Function
        Public Sub SP_FEE_UPDATE_STATUS_PAY_COMPLETE(ByVal ref01 As String, ref02 As String, ByVal process As Integer, ByVal dvcd As Integer)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec [dbo].[SP_FEE_UPDATE_STATUS_PAY_COMPLETE] @ref01='" & ref01 & "', @ref02='" & ref02 & "' ,@dvcd=" & dvcd & " ,@process=" & process
            dt = Queryds(command)

        End Sub
        Public Function SP_get_receipt_by_feeabbr_and_feeno_group_sum2(ByVal feeno As String, ByVal dvcd As Integer) As DataTable
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec dbo.SP_get_receipt_by_feeabbr_and_feeno_group_sum2 @feeno='" & feeno & "' ,@dvcd=" & dvcd
            dt = Queryds(command)
            Return dt
        End Function
        Public Function SP_get_receipt_by_feeabbr_and_feeno_group_sum3(ByVal feeno As String, ByVal dvcd As Integer, ByVal feeabbr As String) As DataTable
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec dbo.SP_get_receipt_by_feeabbr_and_feeno_group_sum3 @feeno='" & feeno & "' ,@dvcd=" & dvcd & ",@feeabbr='" & feeabbr & "'"
            dt = Queryds(command)
            Return dt
        End Function
        '
        Public Function SP_GET_FEEDTL_BY_FK_FEE(ByVal fk_fee As Integer) As DataTable
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec [dbo].[SP_GET_FEEDTL_BY_FK_FEE] @fk_fee=" & fk_fee
            dt = Queryds(command)
            Return dt
        End Function
        Public Function get_lcn_name_type1(ByVal lcnsid As Integer, ByVal lcnscd As Integer) As String
            Dim dt As New DataTable
            Dim str_name As String = ""
            Dim command As String = " "
            command &= " select top(1) isnull(l.thanm,'') as fullname"
            command &= " from openquery(LGTCPN,'select thanm, prefixcd, suffixcd,lcnsid,thalnm,lcnscd,lctcd from syslcnsnm "
            command &= "  where lcnsid =" & lcnsid & " And lcnscd = " & lcnscd & " "
            command &= " ;') SLN"
            command &= " left join LGTCPN.[dbo].[sysprefix] pr on SLN.prefixcd = pr.prefixcd"
            command &= " left join LGTCPN.[dbo].[syssuffix] sf on SLN.suffixcd = sf.suffixcd"
            command &= " left join (select lcnsid,lctnmcd,thanm,lctcd"
            command &= " from openquery(LGTCPN,'select * from syslctnm where lcnsid = " & lcnsid & " "
            command &= " ;') ) l on SLN.lcnsid = l.lcnsid"
            command &= " left join (select * from openquery(LGTCPN,'select * from syslctaddr where lcnsid = " & lcnsid & " "
            command &= " ;')) a on a.lcnsid = sln.lcnsid "
            dt = Queryds(command)
            For Each dr As DataRow In dt.Rows
                Try
                    str_name = dr("fullname")
                Catch ex As Exception

                End Try

            Next
            Return str_name
        End Function
        Public Function get_lcn_name_type2(ByVal lcnsid As Integer, ByVal lcnscd As Integer) As String
            Dim dt As New DataTable
            Dim str_name As String = ""
            Dim command As String = " "
            command &= " Select top(1)"
            command &= " isnull(pr.thanm,'') + ' ' + isnull(SLN.thanm,'') + ' ' + "
            command &= " case when sln.thalnm is null then isnull(sf.thanm,'') else isnull(sln.thalnm,'') end as fullname"
            command &= " from openquery(LGTCPN,'select thanm, prefixcd, suffixcd,lcnsid,thalnm,lcnscd,lctcd from syslcnsnm"
            command &= "  where lcnsid =" & lcnsid & " And lcnscd = " & lcnscd & " "
            command &= " ;') SLN"
            command &= " left join LGTCPN.[dbo].[sysprefix] pr on SLN.prefixcd = pr.prefixcd"
            command &= " left join LGTCPN.[dbo].[syssuffix] sf on SLN.suffixcd = sf.suffixcd"
            command &= " left join (select lcnsid,lctnmcd,thanm,lctcd"
            command &= " from openquery(LGTCPN,'select * from syslctnm where lcnsid = " & lcnsid & " "
            command &= " ;') ) l on SLN.lcnsid = l.lcnsid"
            command &= " left join (select * from openquery(LGTCPN,'select * from syslctaddr where lcnsid = " & lcnsid & " "
            command &= " ;')) a on a.lcnsid = sln.lcnsid "
            dt = Queryds(command)
            For Each dr As DataRow In dt.Rows
                Try
                    str_name = dr("fullname")
                Catch ex As Exception

                End Try

            Next
            Return str_name
        End Function
        Public Sub SP_FEE_UPDATE_STATUS_PAY_CANCEL(ByVal ref01 As String, ref02 As String, ByVal process As Integer, ByVal dvcd As Integer)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec [dbo].[SP_FEE_UPDATE_STATUS_PAY_CANCEL] @ref01='" & ref01 & "', @ref02='" & ref02 & "' ,@dvcd=" & dvcd & " ,@process=" & process
            dt = Queryds(command)

        End Sub
        Public Sub SP_FEE_UPDATE_STATUS_PAY_COMPLETE_TXT_EXTEND_DATE(ByVal feeno As String)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec [dbo].[SP_FEE_UPDATE_STATUS_PAY_COMPLETE_TXT_EXTEND_DATE] @feeno='" & feeno & "'"
            dt = Queryds(command)

        End Sub
        Public Function SP_COUNT_FEE_OLD_BY_REF(ByVal ref01 As String, ref02 As String)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " select * from openquery(LGTCPN,'select f.*,b.ref02 ,b.feedate as feedate2,b.enddate as enddate2 "
            command &= " ,b.lcnprnst,b.lstfcd as lstfcd2,b.lmdfdate as lmdfdate2 "
            command &= " from fda.fee f "
            command &= " join fda.feebank b on f.ref01 = b.ref01 and f.dvcd = b.dvcd and f.pvncd = b.pvncd "
            command &= " where f.rcptst <> 2  and b.ref01=''" & ref01 & "'' and b.ref02= ''" & ref02 & "'' "
            command &= " ;')"
            'command &= " where cast(ref02 as int) = '" & ref02 & "'"
            Try
                dt = Queryds(command)
            Catch ex As Exception

            End Try

            'and b.ref02= ''" & ref02 & "'' "
            Return dt
        End Function
        Public Function update_fee_cancel(ByVal ref01 As String)
            '            UPDATE OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles WHERE id = 101')   
            'SET name = 'ADifferentName';


            Dim dt As New DataTable
            Dim command As String = " "
            command = " UPDATE OPENQUERY(LGTCPN,'select f.rcptst "
            command &= " from fda.fee f "
            command &= " where f.ref01=''" & ref01 & "'' "
            command &= " ;')"
            command &= " SET rcptst = 0 ;"
            'command &= " where cast(ref02 as int) = '" & ref02 & "'"
            Try
                dt = Queryds(command)
            Catch ex As Exception

            End Try

            'and b.ref02= ''" & ref02 & "'' "
            ''and b.ref02= ''" & ref02 & "'' "
            Return dt
        End Function

        Public Function SP_COUNT_FEE_OLD_BY_REF01(ByVal ref01 As String)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " select * from openquery(LGTCPN,'select f.*,b.ref02 ,b.feedate as feedate2,b.enddate as enddate2 "
            command &= " ,b.lcnprnst,b.lstfcd as lstfcd2,b.lmdfdate as lmdfdate2 "
            command &= " from fda.fee f "
            command &= " join fda.feebank b on f.ref01 = b.ref01 and f.dvcd = b.dvcd and f.pvncd = b.pvncd "
            command &= " where f.rcptst <> 2  and b.ref01=''" & ref01 & "'' "
            command &= " ;')"
            'command &= " where cast(ref02 as int) = '" & ref02 & "'"
            Try
                dt = Queryds(command)
            Catch ex As Exception

            End Try

            'and b.ref02= ''" & ref02 & "'' "
            ''and b.ref02= ''" & ref02 & "'' "
            Return dt
        End Function

        Public Function Q_feetype_by_feeabbr(ByVal feeabbr As String) As Boolean
            Dim dt As New DataTable
            Dim bool As Boolean = False
            Dim command As String = " "
            command = " select * from openquery(LGTCPN,'select * from feetype "
            command &= " where feeabbr =''" & feeabbr & "'' "
            command &= " ;')"
            'command &= " where cast(ref02 as int) = '" & ref02 & "'"
            Try
                dt = Queryds(command)
            Catch ex As Exception

            End Try
            For Each dr As DataRow In dt.Rows
                If CStr(dr("feetpnm")).Contains("ต่ออายุ") Then
                    bool = True
                End If
            Next
            Return bool
        End Function
        Public Function SP_FEEDTL(ByVal feeno As String, pvncd As String, ByVal feetpcd As String, ByVal dvcd As Integer)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " select * from openquery(LGTCPN,'select * from fda.feedtl d "
            command &= " where d.feeno=''" & feeno & "'' and d.pvncd= ''" & pvncd & "'' and d.feetpcd = ''" & feetpcd & "'' and d.dvcd =''" & dvcd & "''"
            command &= " ;')"
            Try
                dt = Queryds(command)
            Catch ex As Exception

            End Try
            Return dt
        End Function
    End Class

    Public Class LGT_NCT2_128
        Public Function Queryds(ByVal Commands As String) As DataTable
            Dim dt As New DataTable
            Dim MyConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("LGT_NCT2ConnectionString").ConnectionString)
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
            Return dt
        End Function
        Public Sub SP_FEE_UPDATE_STATUS_PAY_COMPLETE(ByVal ref01 As String, ref02 As String, ByVal process As Integer, ByVal dvcd As Integer)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec [dbo].[SP_FEE_UPDATE_STATUS_PAY_COMPLETE] @ref01='" & ref01 & "', @ref02='" & ref02 & "' ,@dvcd=" & dvcd & " ,@process=" & process
            dt = Queryds(command)

        End Sub
        '
        Public Function SP_GET_IDA_NCT_FROM_FEE(ByVal ref01 As String, ref02 As String) As Integer
            Dim dt As New DataTable
            Dim IDA As Integer = 0
            Dim command As String = " "
            command = " exec [dbo].[SP_GET_IDA_NCT_FROM_FEE] @ref01='" & ref01 & "', @ref02='" & ref02 & "' "
            dt = Queryds(command)
            For Each dr As DataRow In dt.Rows
                IDA = dr("IDA")
            Next

            Return IDA
        End Function
        Public Sub SP_FEE_UPDATE_STATUS_PAY_CANCEL(ByVal ref01 As String, ref02 As String, ByVal process As Integer, ByVal dvcd As Integer)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec [dbo].[SP_FEE_UPDATE_STATUS_PAY_CANCEL] @ref01='" & ref01 & "', @ref02='" & ref02 & "' ,@dvcd=" & dvcd & " ,@process=" & process
            dt = Queryds(command)

        End Sub

    End Class
    Public Class LGT_TXC_128
        Public Function Queryds(ByVal Commands As String) As DataTable
            Dim dt As New DataTable
            Dim MyConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("LGT_TXCConnectionString").ConnectionString)
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
            Return dt
        End Function

    End Class

    Public Class INFORMIX
        Public Function Queryds(ByVal Commands As String) As DataTable
            Dim dt As New DataTable
            Dim MyConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DTAMConnectionString").ConnectionString)
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
            Return dt
        End Function
        Public Function Query_Insert(ByVal Commands As String) As DataTable
            Dim dt As New DataTable
            Dim MyConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FDA_FEEConnectionString").ConnectionString)
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
            Return dt
        End Function
        Public Sub TestInsert()
            Dim strSQL As String = String.Empty
            'strSQL = " insert into LGTCPN.fda.feehis(lcnsid,lcnscd,txcqty,fexpdate) "
            strSQL = "INSERT into openquery(LGTCPN, 'select * from fda.feehis') values ('124','1','5',GETDATE())"
            Query_Insert(strSQL)
        End Sub

        Public Sub insert_fee(ByVal pvncd As Integer, ByVal dvcd As Integer, ByVal feetpcd As Integer, ByVal feeno As String, ByVal feeabbr As String, Optional feedate As Object = Nothing, Optional ref1 As String = "", _
                              Optional lcnsid As Integer = 0, Optional prnfeest As Integer = 0, Optional rcptst As Integer = 0, Optional rcptyear As Integer = 0, Optional rcptno As Integer = 0, Optional rcptdate As Object = Nothing, _
                              Optional feestfcd As Integer = 0, Optional remark As String = "", Optional expdate As Object = Nothing, Optional enddate As Object = Nothing, Optional cncstfcd As Integer = 0, Optional cncdate As Object = Nothing, _
                              Optional pvnbookno As Integer = 0, Optional pvnrcptno As Integer = 0, Optional lstfcd As Integer = 0, Optional lmdfdate As Object = Nothing, Optional lctnmcd As Integer = 0, Optional lcnscd As Integer = 0, Optional lctcd As Integer = 0, Optional feest As String = "")
            Dim strSQL As String = String.Empty


            'Try
            strSQL = "INSERT into openquery(LGTCPN, 'select * from fda.fee') values "
            strSQL &= " ('" & pvncd & "','" & dvcd & "','" & feetpcd & "','" & feeno & "','" & feeabbr & "'"

            If feedate = Nothing Then
                strSQL &= " ,null"
            Else
                strSQL &= ",'" & feedate & "'"
            End If
            'strSQL &= " , '" & lctnmcd & "','" & lcnscd & "','" & lctcd & "','" & prnfeest & "'"
            strSQL &= " ,'" & ref1 & "','" & lcnsid & "','" & lctnmcd & "','" & lcnscd & "','" & lctcd & "','" & prnfeest & "','" & rcptst & "','" & rcptyear & "','" & rcptno & "'"
            If rcptdate = Nothing Then
                strSQL &= " ,null"
            Else
                strSQL &= " ,'" & rcptdate & "'"
            End If

            strSQL &= " ,'" & feestfcd & "','" & remark & "'"
            If expdate = Nothing Then
                strSQL &= " ,null"
            Else
                strSQL &= " ,'" & expdate & "'"
            End If
            If enddate = Nothing Then
                strSQL &= " ,null"
            Else
                strSQL &= " ,'" & enddate & "'"
            End If

            strSQL &= " ,'" & cncstfcd & "'"   ','" & cncdate & "'"
            If cncdate = Nothing Then
                strSQL &= " ,null"
            Else
                strSQL &= " ,'" & cncdate & "'"
            End If

            strSQL &= " ,'" & pvnbookno & "','" & pvnrcptno & "','" & feest & "','" & lstfcd & "'" ','" & lmdfdate & "'"
            If lmdfdate = Nothing Then
                strSQL &= " ,null"
            Else
                strSQL &= " ,'" & lmdfdate & "'"
            End If
            strSQL &= " )"

            Query_Insert(strSQL)

        End Sub
        Public Sub insert_feedtl(ByVal pvncd As Integer, ByVal dvcd As Integer, ByVal feetpcd As Integer, ByVal feeno As String, ByVal rid As Integer, ByVal rcvabbr As Integer, _
                                 ByVal rcvcd As Integer, ByVal rcvno As String, ByVal apppvncd As Integer, ByVal appabbr As String, ByVal appvcd As String, _
                                 ByVal appvno As String, ByVal timeno As String, ByVal amt As Double, ByVal finevalue As String)

            Dim strSQL As String = String.Empty
            'Try
            strSQL = "INSERT into openquery(LGTCPN, 'select * from fda.feedtl') values "
            strSQL &= " ('" & pvncd & "','" & dvcd & "','" & feetpcd & "','" & feeno & "','" & rid & "','" & rcvabbr & "','" & rcvcd & "','" & rcvno & "','" & apppvncd & "','" & appabbr & "','" & appvcd & "','" & appvno & "'"
            strSQL &= " ,'" & timeno & "','" & amt & "','" & finevalue & "'"
            strSQL &= " )"

            Query_Insert(strSQL)
            'Catch ex As Exception

            'End Try
        End Sub
        Public Sub insert_feebank(ByVal pvncd As Integer, ByVal dvcd As Integer, ByVal ref1 As String, ByVal ref2 As String, Optional enddate As Object = Nothing, _
                                  Optional lcnprnst As Integer = 0, Optional lstfcd As Integer = 0, Optional lmdfdate As Object = Nothing)
            Dim strSQL As String = String.Empty
            'Try
            strSQL = "INSERT into openquery(LGTCPN, 'select * from fda.feedtl') values "
            strSQL &= " ('" & pvncd & "','" & dvcd & "','" & ref1 & "','" & ref2 & "'"

            If enddate = Nothing Then
                strSQL &= " ,null"
            Else
                strSQL &= " ,'" & enddate & "'"
            End If
            strSQL &= ",'" & lcnprnst & "','" & lstfcd & "'"
            If lmdfdate = Nothing Then
                strSQL &= " ,null"
            Else
                strSQL &= " ,'" & lmdfdate & "'"
            End If

            strSQL &= " )"

            Query_Insert(strSQL)
            'Catch ex As Exception

            'End Try
        End Sub
        Public Function SP_GET_MAX_RCPTNO_BY_YEAR(ByVal _year As Integer) As Integer
            Dim dt As New DataTable
            Dim no As Integer = 0
            Dim command As String = " "
            command = " exec [dbo].[SP_GET_MAX_RCPTNO_BY_YEAR] @year=" & _year
            dt = Queryds(command)

            Try
                no = dt(0)("max_no")
            Catch ex As Exception

            End Try
            Return no
        End Function
        Public Sub update_fee(ByVal lcnsid As String, ByVal feeabbr As String, ByVal dvcd As Integer, ByVal pvncd As Integer, ByVal ref1 As String)
            Dim strSQL As String = String.Empty
            Try
                strSQL = "update openquery(LGTCPN, 'select * from fda.fee') set rcptst = '1'"
                strSQL &= " where where lcnsid = ''" & lcnsid & "'' and feeabbr = ''" & feeabbr & "'' and dvcd = ''" & dvcd & "'' and pvncd = ''" & pvncd & "''"
                strSQL &= " and ref01 = ''" & ref1 & "''"
                strSQL &= " )"

                Query_Insert(strSQL)
            Catch ex As Exception

            End Try
        End Sub
        Public Function QUERY_GET_FEE_INFORMIX(ByVal feeno As String, ByVal feeabbr As String, ByVal dvcd As Integer) As DataTable
            Dim dt As New DataTable
            Dim command As String = " "
            command &= " SELECT * ,cast(rcptno as nvarchar(max)) + '/' + cast(rcptyear as nvarchar(max)) as receipt_number "
            command &= " , RIGHT(feeno , 5) as fee_no_5 , left(feeno , 2) as year_fee"
            command &= " ,FDA_BG.dbo.SC_LCNSID_NM(lcnsid) as fullname"
            command &= "  FROM OPENQUERY(LGTCPN,'SELECT f.lcnsid,ft.feetpnm , d.amt , ft.feeabbr,f.ref01 , case when f.rcptst = 1 then ''ชำระเงินแล้ว'' else ''ยังไม่ได้ชำระเงิน'' end as stat"
            command &= " , f.rcptno ,f.rcptyear,f.rcptst , f.feeno , f.pvncd,f.dvcd"
            command &= "  FROM fda.fee f "
            command &= "  join fda.feetype ft on f.feeabbr = ft.feeabbr"

            command &= "  join   (SELECT feeno,pvncd , dvcd,sum(amt) as amt "
            command &= " 		FROM fda.feedtl"
            command &= " 		where feeno = ''" & feeno & "'' "
            command &= " 		group by pvncd , dvcd , feeno ,feetpcd) d on f.pvncd = d.pvncd and f.dvcd = d.dvcd and f.feeno  =d.feeno"
            command &= " where f.feeabbr=''" & feeabbr & "'' and f.dvcd=''" & dvcd & "''"
            command &= " ;') "


            dt = Queryds(command)
            Return dt
        End Function
        Public Function QUERY_GET_DDL_LCNSNM(ByVal dvcd As Integer, ByVal lcnsid As Integer) As DataTable
            Dim dt As New DataTable
            Dim command As String = " "
            command &= " select * from openquery(LGTCPN,'select s.lcnsid , s.lcnscd ,lctnmcd"
            command &= " ,l.thanm ,l.lctcd "
            command &= " from fda.syslcnsnm s "
            command &= " join fda.syslctnm l on s.lcnsid = l.lcnsid and s.lctcd = l.lctcd "
            command &= " where s.lcnsid = ''" & lcnsid & "'' and s.lcnsst=1 and dvcd = ''" & dvcd & "''"
            command &= " group by s.lcnsid , s.lcnscd,l.thanm  ,l.lctcd ,lctnmcd"
            command &= " ;') "

            dt = Queryds(command)
            Return dt
        End Function
        Public Function QUERY_GET_DDL_LCNSNM_BY_LCNSID(ByVal lcnsid As Integer) As DataTable
            Dim dt As New DataTable
            Dim command As String = " "
            command &= " select * from openquery(LGTCPN,'select s.lcnsid , s.lcnscd ,lctnmcd"
            command &= " ,l.thanm ,l.lctcd "
            command &= " from fda.syslcnsnm s "
            command &= " join fda.syslctnm l on s.lcnsid = l.lcnsid and s.lctcd = l.lctcd "
            command &= " where s.lcnsid = ''" & lcnsid & "'' and s.lcnsst=1 "
            command &= " group by s.lcnsid , s.lcnscd,l.thanm  ,l.lctcd ,lctnmcd"
            command &= " ;') "

            dt = Queryds(command)
            Return dt
        End Function

        Public Function get_lcn_name_type(ByVal lcnsid As Integer, ByVal lcnscd As Integer) As DataTable
            Dim dt As New DataTable
            Dim str_name As String = ""
            Dim command As String = " "
            command &= " Select"
            command &= " isnull(pr.thanm,'') + ' ' + isnull(SLN.thanm,'') + ' ' + "
            command &= " case when sln.thalnm is null then isnull(sf.thanm,'') else isnull(sln.thalnm,'') end as thanm , l.lctnmcd"
            command &= " from openquery(LGTCPN,'select thanm, prefixcd, suffixcd,lcnsid,thalnm,lcnscd,lctcd from syslcnsnm"
            command &= "  where lcnsid =" & lcnsid & " And lcnscd = " & lcnscd & " "
            command &= " ;') SLN"
            command &= " left join LGTCPN.[dbo].[sysprefix] pr on SLN.prefixcd = pr.prefixcd"
            command &= " left join LGTCPN.[dbo].[syssuffix] sf on SLN.suffixcd = sf.suffixcd"
            command &= " left join (select lcnsid,lctnmcd,thanm,lctcd"
            command &= " from openquery(LGTCPN,'select * from syslctnm where lcnsid = " & lcnsid & " "
            command &= " ;') ) l on SLN.lcnsid = l.lcnsid"
            command &= " left join (select * from openquery(LGTCPN,'select * from syslctaddr where lcnsid = " & lcnsid & " "
            command &= " ;')) a on a.lcnsid = sln.lcnsid "
            dt = Queryds(command)
            'For Each dr As DataRow In dt.Rows
            '    Try
            '        str_name = dr("fullname")
            '    Catch ex As Exception

            '    End Try

            'Next
            Return dt
        End Function
        Public Function Count_Repeat_Old(ByVal feeno As String, ByVal dvcd As Integer) As Boolean
            Dim dt As New DataTable
            Dim bool As Boolean = False
            Dim command As String = " "
            command &= " select * from openquery(LGTCPN,'select * from fda.fee "
            command &= " where feeno = ''" & feeno & "'' and dvcd =''" & dvcd & "'' "
            command &= " ;') "
            dt = Queryds(command)
            Try
                If dt.Rows.Count > 0 Then
                    bool = True
                End If
            Catch ex As Exception

            End Try

            Return bool
        End Function
    End Class

    Public Class FDA_BG
        Public Function Queryds(ByVal Commands As String) As DataTable
            Dim dt As New DataTable
            Dim MyConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("DTAMConnectionString").ConnectionString)
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
            Return dt
        End Function
        Public Function get_max_receipt_normal(ByVal bgyear As Integer, ByVal r_type As Integer) As Integer
            Dim value As Integer = 0
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec [BUDGETS].[get_max_receipt_normal] @BUDGET_YEAR=" & bgyear & " ,@running_type=" & r_type
            dt = Queryds(command)
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt(0)("bill_max")) Then
                    value = 0
                Else
                    value = CInt(dt(0)("bill_max"))
                End If
            End If

            Return value
        End Function
    End Class
    Public Class FDA_DRUG
        Public Function Queryds(ByVal Commands As String) As DataTable
            Dim dt As New DataTable
            Dim MyConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("LGT_DRUGConnectionString").ConnectionString)
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
            Return dt
        End Function
        Public Function SP_GET_IDA_DA_FROM_FEE(ByVal ref01 As String, ref02 As String) As Integer
            Dim dt As New DataTable
            Dim IDA As Integer = 0
            Dim command As String = " "
            command = " exec [dbo].[SP_GET_IDA_DA_FROM_FEE] @ref01='" & ref01 & "', @ref02='" & ref02 & "' "
            dt = Queryds(command)
            For Each dr As DataRow In dt.Rows
                IDA = dr("IDA")
            Next

            Return IDA
        End Function
        Public Function SP_GET_IDA_DA_FROM_FEE_MULTI_ROW(ByVal ref01 As String, ref02 As String) As DataTable
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec dbo.SP_GET_IDA_DA_FROM_FEE_MULTI_ROW @ref01='" & ref01 & "', @ref02='" & ref02 & "' "
            dt = Queryds(command)
            Return dt
        End Function
    End Class

    Public Class FDA_MARIJUANA
        Public Function Queryds(ByVal Commands As String) As DataTable
            Dim dt As New DataTable
            Dim MyConnection As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("FDA_MARIJUANAConnectionString").ConnectionString)
            Dim mySqlDataAdapter As SqlDataAdapter = New SqlDataAdapter(Commands, MyConnection)
            mySqlDataAdapter.Fill(dt)
            MyConnection.Close()
            Return dt
        End Function

        Public Sub SP_FEE_UPDATE_STATUS_PAY_COMPLETE(ByVal ref01 As String, ref02 As String, ByVal process As Integer, ByVal dvcd As Integer)
            Dim dt As New DataTable
            Dim command As String = " "
            command = " exec [dbo].[SP_FEE_UPDATE_STATUS_PAY_COMPLETE] @ref01='" & ref01 & "', @ref02='" & ref02 & "' ,@dvcd=" & dvcd & " ,@process=" & process
            dt = Queryds(command)

        End Sub

    End Class

End Namespace
