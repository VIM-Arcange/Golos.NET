Imports System.Net.WebSockets
Imports System.Text
Imports System.Threading
Imports Newtonsoft.Json

Public Class CWebSocket
    Implements IDisposable

#Region "Variables"
    Private m_oURI As Uri
    Private m_oCWS As ClientWebSocket

#End Region

#Region "Constructors"
    Sub New(Optional ByVal strURI As String = "ws://golos.steem.ws")
        m_oURI = New Uri(strURI)
        m_oCWS = New ClientWebSocket()
    End Sub
#End Region

#Region "Public methods"
    Public Async Function SendRequest(ByVal strMethod As String, Optional ByVal strParams As ArrayList = Nothing) As Task(Of String)
        Dim nRetry As Integer = 0
        Dim strResult As String = String.Empty
        Dim arrRequest As New Hashtable
        Dim bytes(2048) As Byte
        Dim result As WebSocketReceiveResult

        arrRequest("jsonrpc") = "2.0"
        arrRequest("id") = 1
        arrRequest("method") = strMethod
        If Not strParams Is Nothing Then
            arrRequest("params") = strParams
        End If

        Dim strJson As String = JsonConvert.SerializeObject(arrRequest)

        For nRetry = 1 To 2
            Try
                If m_oCWS.State <> WebSocketState.Open Then
                    Await m_oCWS.ConnectAsync(m_oURI, CancellationToken.None)
                End If

                Dim bytesToSend As ArraySegment(Of Byte) = New ArraySegment(Of Byte)(Encoding.UTF8.GetBytes(strJson))
                Dim bytesReceived As ArraySegment(Of Byte) = New ArraySegment(Of Byte)(bytes)

                Await m_oCWS.SendAsync(bytesToSend, WebSocketMessageType.Text, True, CancellationToken.None)
                Do
                    result = Await m_oCWS.ReceiveAsync(bytesReceived, CancellationToken.None)
                    strResult += Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count)
                Loop Until result.EndOfMessage
                Exit For

            Catch ex As Exception
                ' Opps, might be a timeout
                If nRetry = 2 Then
                    ' second timeout - give up and re-throw exception
                    Throw ex
                End If
                ' wait 2 sec to give a seconde chance to a busy server
                Threading.Thread.Sleep(2000)
            End Try
        Next

        Return strResult
    End Function

#End Region

#Region "IDisposable Support"
    Public Sub Dispose() Implements IDisposable.Dispose
        If m_oCWS.State = WebSocketState.Open Then
            m_oCWS.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None)
        End If
        m_oCWS.Dispose()
    End Sub
#End Region

End Class
