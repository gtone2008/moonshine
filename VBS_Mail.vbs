'*********************************************
' Scheduled Mail sending script tempalte
'*********************************************
'
'
'
'*********************************************

Option Explicit
Dim emailContent
Dim connSqlDB
Dim SQL
Dim connObj
Dim RsRpt
Dim NameSpace
Dim Email

'Define Sqlserver connection string
connSqlDB="Provider=SQLOLEDB;Data Source=wuxats01;Initial Catalog=ooJabilWX;User Id=opusone; Password=hcetenosupo28;"

'create an instance of the ADO connection and recordset objects
set connObj = CreateObject("ADODB.Connection")
Set RsRpt   = CreateObject("ADODB.Recordset")

'open the connection to the database
connObj.open connSqlDB

'declare the SQL statement that will query the database
SQL = "select x.cdate,"_
	&"max(case x.machineid when '071' then x.num else 0 end) as [071]," _
	&"max(case x.machineid when '072' then x.num else 0 end) as [072],"_
	&"max(case x.machineid when '085' then x.num else 0 end) as [085],"_
	&"max(case x.machineid when '086' then x.num else 0 end) as [086],"_
	&"sum(x.num) as total  "_
	&"from ( "_
	&"select machineid,CONVERT(varchar, att_datetime -case inorout when 0 then 9.0/24 else 0 end, 101) as cdate, "_
	&"count(*) as num from ats_originaldata "_
	&"where att_datetime >CONVERT(varchar(100), GETDATE()-10, 23) "_
	&"group by machineid,CONVERT(varchar, att_datetime -case inorout when 0 then 9.0/24 else 0 end, 101) "_
	&")x group by x.cdate"

'Open the recordset object executing the SQL statement and return records
RsRpt.Open SQL,connObj,0,1

'define email content
emailContent = "Attendance Count Daily Report " _
                    & "<TABLE border=1 cellPadding=5 cellSpacing=0>" _
                    & "<TR style='background-color:#005288; color:#fff; font-weight:bold'>" _
                    & "<TD>Date</TD>" _
                    & "<TD>071</TD>" _
                    & "<TD>072</TD>" _
					& "<TD>085</TD>" _
					& "<TD>086</TD>" _
					& "<TD>087</TD>" _
					& "<TD>088</TD>" _
                    & "</TR>"

'AttachedFile.WriteLine("serialnumber,empcode,c_name,e_name,department,costcenter,InTime")

'if there are records then loop through the fields
Do While Not RsRpt.EOF    
     emailContent = emailContent & "<TR>" 
     emailContent = emailContent & "<TD>"   & RsRpt("cdate")   
	 emailContent = emailContent & "</TD><TD>"   & RsRpt("071")	
	 emailContent = emailContent & "</TD><TD bgcolor='green' align=right>"   & RsRpt("072")	
	 emailContent = emailContent & "</TD><TD bgcolor='red' align=center>"   & RsRpt("085")	
	 emailContent = emailContent & "</TD><TD style='color:red'>"   & RsRpt("086")
	 emailContent = emailContent & "</TD><TD>"   & RsRpt("087")	 
     emailContent = emailContent & "</TD><TD>"   & RsRpt("088")
	 emailContent = emailContent & "</TD></TR>"
	 
     RsRpt.MoveNext         
Loop

emailContent = emailContent & "</TABLE>"

'housekeeping for database objects
RsRpt.Close
connObj.Close
Set RsRpt = Nothing
Set connObj = Nothing

'define email parameters
Set Email = CreateObject("CDO.Message")
Email.From = "PerformanceSupport@jabil.com"   
Email.To = "Jun_li5@jabil.com"  
Email.cc = "Jun_li5@jabil.com"
Email.bcc="Jun_li5@jabil.com"  
Email.Subject = "Attendance Count Daily Report" 
Email.Htmlbody = emailContent
'if need add attachment 
'Email.AddAttachment filePath&"Result.csv"
Email.Send

