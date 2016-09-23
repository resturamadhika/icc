Imports Newtonsoft.Json
Imports System.Xml
Imports System.Net
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Public Class _Default
    Inherits System.Web.UI.Page

    Dim Con As New SqlConnection(ConfigurationManager.ConnectionStrings("Restu").ConnectionString)
    Dim Com As SqlCommand
    Dim Dr As SqlDataReader
    Function highlightText(ByVal textNya As String)
        Dim aa, outputNya As String
        Dim s As String = textNya.ToString()
        Dim coutn As String() = s.ToString().Split(" ")
        Dim k As Integer = coutn.Length

      
        Dim selectTable As String
        For q As Integer = 0 To k - 1
            aa = Regex.Replace(coutn(q), "[^0-9a-zA-Z]+", "")
            'selectTable = "select Text_Keywords from mKeyword union select Text_SubKeywords from mSubkeyword where Text_Keywords like'%" & aa & "%'"
            selectTable = "select * from( " & _
                            "select Text_Keywords from mKeyword  " & _
                            "union " & _
                            "select Text_SubKeywords from mSubkeyword  " & _
                            ") as a " & _
                            "where a.Text_Keywords like'%" & aa & "%' "

            Com = New SqlCommand(selectTable, Con)
            Con.Open()
            Dr = Com.ExecuteReader()
            If Dr.Read() Then
                outputNya += "<span class='bl'>" & aa & "</span>"
            Else
                outputNya += aa & "&nbsp"
            End If
            Dr.Close()
            Con.Close()

            If aa = "" Then

            End If
        Next

        Return outputNya

    End Function



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.Write(highlightText("kur mikro ini gk di ambil permohonan POC"))

        'Dim url As String = "http://localhost:83/2016/bri/index.xml"
        'Dim myReq As WebRequest = WebRequest.Create(url)

        'Dim username As String = "username"
        'Dim password As String = "password"
        'Dim usernamePassword As String = Convert.ToString(username & Convert.ToString(":")) & password
        'Dim mycache As New CredentialCache()
        'mycache.Add(New Uri(url), "Basic", New NetworkCredential(username, password))
        'myReq.Credentials = mycache
        'myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(New ASCIIEncoding().GetBytes(usernamePassword)))

        'Dim wr As WebResponse = myReq.GetResponse()
        'Dim receiveStream As Stream = wr.GetResponseStream()
        'Dim reader As New StreamReader(receiveStream, Encoding.UTF8)
        'Dim content As String = reader.ReadToEnd()
        'TextBox1.Text = content
        'Response.Write(content)
        'Console.ReadLine()

        '=======================================================
        'Service provided by Telerik (www.telerik.com)
        'Conversion powered by NRefactory.
        'Twitter: @telerik
        'Facebook: facebook.com/telerik
        '=======================================================



        'Dim URLString As String = "http://localhost:83/2016/bri/index.xml"
        ' ''Dim reader As XmlTextReader = New XmlTextReader(URLString)
        ''Dim reader As XmlTextReader = New XmlTextReader(URLString)

        ''Dim doc As XmlDocument = New XmlDocument()
        ''doc.LoadXml(reader.ToString)

        ''Dim jsonText As String = JsonConvert.SerializeXmlNode(doc)

        ''Response.Write(jsonText)

        'Dim m_strFilePath = "http://localhost:83/2016/bri/index.xml"
        'Dim xmlStr As String
        ''Using wc = New WebClient()
        ''    xmlStr = wc.DownloadString(m_strFilePath)
        ''End Using
        'Dim xmlDoc = New XmlDocument()
        'xmlDoc.LoadXml(m_strFilePath)

        'Response.Write(JsonConvert.SerializeXmlNode(xmlDoc))
    End Sub

End Class