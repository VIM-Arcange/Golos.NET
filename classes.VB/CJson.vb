Imports System.IO
Imports System.Text
Imports Newtonsoft.Json
Imports System.Net
Imports System.Security.Cryptography.X509Certificates

Public Class CJson

#Region "Constants"
    Private Const CRLF As String = "\r\n"
    Private Const HTTP_SERVER_OK As Integer = 200
#End Region

#Region "Enums"
    Public Enum EHTTPMethod
        [GET]
        POST
        PUT
    End Enum
#End Region

#Region "Structures"
    Public Structure SJsonRPC
        Dim Version As String
        Dim Port As Integer
        Dim Api As String
    End Structure
#End Region

#Region "Variables"
    Private m_nRequestID As Integer
    Private m_strHostname As String
    Private m_strURL As String
    Private m_oJsonRpc As SJsonRPC

#End Region

#Region "Constructors"
    Sub New(strHostname As String, nPort As UShort, strApi As String, Optional strVersion As String = "2.0")

        m_strHostname = strHostname
        With m_oJsonRpc
            .Port = nPort
            .Api = strApi
            .Version = strVersion
        End With

        m_strURL = String.Format("http://{0}:{1}{2}", m_strHostname, m_oJsonRpc.Port, m_oJsonRpc.Api)

        'Bypass SSL
        ServicePointManager.ServerCertificateValidationCallback = New Security.RemoteCertificateValidationCallback(AddressOf CertificateValidationCallBack)
        ServicePointManager.Expect100Continue = True
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3
    End Sub
#End Region

#Region "Private methods"
    Private Function getRequestID() As Integer
        m_nRequestID += 1
        Return m_nRequestID
    End Function

    Private Function GetHttpRequest(strData As String, eMethod As EHTTPMethod)
        Dim strBodyRequest As String = String.Empty
        Dim strResponse As String

        Dim oRequest As HttpWebRequest = DirectCast(WebRequest.Create(m_strURL), HttpWebRequest)

        Select Case eMethod
            Case EHTTPMethod.POST
                'm_oRequest.Headers.Add("POST:" & String.Format("{0} HTTP/1.1", m_oJsonRpc.Api))
                oRequest.Method = "POST"
                oRequest.Accept = "application/json-rpc"
                oRequest.ContentType = "application/json-rpc; charset=UTF-8"
                strBodyRequest = strData
            Case EHTTPMethod.GET
                oRequest.Method = "GET"
                oRequest.Headers.Add(String.Format("GET:{0} HTTP/1.1", strData))
                oRequest.Accept = "*/*"
                strBodyRequest = strData
            Case EHTTPMethod.PUT
                Dim strBoundary As String = Now.Ticks.ToString.Substring(0, 10)
                oRequest.Headers.Add(String.Format("POST:{0}{1} HTTP/1.1", m_oJsonRpc.Api, "upload/"))
                oRequest.Method = "POST"
                oRequest.Accept = "*/*"
                oRequest.ContentType = String.Format("multipart/form-data; boundary=%s", strBoundary)
                strBodyRequest = String.Format("--{0}{1}", strBoundary, CJson.CRLF)
                strBodyRequest += String.Format("Content-Disposition: form-data; name=""unknown""; filename=""newFile.bin""{0}", CJson.CRLF)
                strBodyRequest += String.Format("{0}", CJson.CRLF)
                strBodyRequest += String.Format("{0}{1}", strData, CJson.CRLF)
                strBodyRequest += String.Format("--{0}--{1}", strBoundary, CJson.CRLF)
            Case Else
                Throw New Exception("Method not found : supported : GET/POST/PUT")
        End Select

        If eMethod <> EHTTPMethod.GET Then
            'Setting DATA
            Dim aData() As Byte
            Dim oStringBuilder As New StringBuilder
            oStringBuilder.Append(strBodyRequest)
            aData = Encoding.UTF8.GetBytes(oStringBuilder.ToString())
            oRequest.Headers.Add("ContentLength: " & CStr(aData.Length))
            Using oStream As IO.Stream = oRequest.GetRequestStream()
                oStream.Write(aData, 0, aData.Length)
            End Using
        End If

        'Execute request
        Using oResponseWeb As HttpWebResponse = CType(oRequest.GetResponse(), HttpWebResponse)

            If Not oResponseWeb.StatusCode = HttpStatusCode.OK Then
                Throw New Exception(String.Format("Error request response : {0} - {1}", oResponseWeb.StatusCode, oResponseWeb.StatusDescription))
            End If

            'Read response
            Using oStream As New StreamReader(oResponseWeb.GetResponseStream())
                strResponse = oStream.ReadToEnd
                oStream.Close()
            End Using
        End Using

        Return strResponse
    End Function

    Private Function CertificateValidationCallBack(ByVal sender As Object, ByVal certificate As X509Certificate,
                                                      ByVal chain As X509Chain, ByVal sslPolicyErrors As Security.SslPolicyErrors) As Boolean
        Return True
    End Function
#End Region

#Region "Public methods"

    Public Function SendRequest(strMethod As String, Optional strParams As ArrayList = Nothing) As String
        Dim nRetry As Integer = 0
        Dim strResult As String = String.Empty
        Dim arrRequest As New Hashtable

        arrRequest("jsonrpc") = m_oJsonRpc.Version
        arrRequest("id") = Me.getRequestID
        arrRequest("method") = strMethod
        If Not strParams Is Nothing Then
            arrRequest("params") = strParams
        End If

        Dim strJson As String = JsonConvert.SerializeObject(arrRequest)
        For nRetry = 1 To 2
            Try
                strResult = GetHttpRequest(strJson, EHTTPMethod.POST)
                Exit For
            Catch ex As Exception
                ' Opps, might be a timeout
                If nRetry = 2 Then
                    ' second timeout - give up and re-throw exception
                    Throw ex
                End If
            End Try
            ' wait 1 sec to give a seconde chance to a busy server
            Threading.Thread.Sleep(1000)
        Next

        Return strResult
    End Function

#End Region

End Class
