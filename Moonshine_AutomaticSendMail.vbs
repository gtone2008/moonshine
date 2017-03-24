REM 'declare the variables 
Option Explicit 

Dim emailTo
Dim emailCc
Dim emailSubject
Dim emailContent

Dim connSqlDB
Dim SQL
Dim connObj
Dim RsRpt
Dim NameSpace
Dim Email


'Define Sqlserver connection string
'connSqlDB="Provider=SQLOLEDB;Data Source=WUXSG01\SQLEXPRESS;Initial Catalog=SpareParts1;User Id=sa; Password=Jabil12345;"
connSqlDB="server=wuxsg01;database=moonshine;user id=root;password=Jabil12345;CharacterSet=gb2312;"
'create an instance of the ADO connection and recordset objects
set connObj = CreateObject("ADODB.Connection")
Set RsRpt   = CreateObject("ADODB.Recordset")

'open the connection to the database
connObj.open connSqlDB

'declare the SQL statement that will query the database
SQL = "exec AutomaticSendMail_VBS"

'Open the recordset object executing the SQL statement and return records 
RsRpt.Open SQL,connObj,0,1

'if there are records then loop through the fields 
Do While Not RsRpt.EOF
	emailContent = RsRpt("body")
	emailTo = RsRpt("to")
	emailSubject = RsRpt("subject")
	RsRpt.MoveNext
LOOP

'close the connection and recordset objects to free up resources
RsRpt.Close
connObj.Close
Set RsRpt = Nothing
Set connObj = Nothing

'define email parameters
NameSpace = "http://schemas.microsoft.com/cdo/configuration/"
Set Email = CreateObject("CDO.Message")
Email.From = "PerformanceSupport@jabil.com"    
Email.To = emailTo
Email.Cc = emailCc
Email.Subject = emailSubject
Email.BodyPart.Charset = "gb2312"
Email.Htmlbody = emailContent

'With Email.Configuration.Fields
'.Item(NameSpace&"sendusing") = 2
'.Item(NameSpace&"smtpserver") = "CORIMC04" 
'.Item(NameSpace&"smtpserverport") = 25
'.Item(NameSpace&"smtpauthenticate") = 1
'.update
'End With

'Send the email report
Email.Send