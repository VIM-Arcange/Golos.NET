Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Public Class CGolosAPI
    Implements IDisposable

#Region "Enums"

    Public Enum EType
        [RPC]
        [WS]
    End Enum

#End Region

#Region "Variables"
    Private m_eType As EType
    Private m_oJson As CJson
    Private m_oSocket As CWebSocket
#End Region

#Region "Constructors"
    Sub New(strHostname As String, nPort As UShort)
        m_oJson = New CJson(strHostname, nPort, "/rpc")
        m_eType = EType.RPC
    End Sub

    Sub New(strURI As String)
        m_oSocket = New CWebSocket(strURI)
        m_eType = EType.WS
    End Sub

#End Region

#Region "Private methods"
    Private Function SendRequest(strMethod As String, Optional strParams As ArrayList = Nothing) As String

        If m_eType = EType.RPC Then
            Return m_oJson.SendRequest(strMethod, strParams)
        Else
            Using t As Task(Of String) = m_oSocket.SendRequest(strMethod, strParams)
                t.Wait()
                Return t.Result
            End Using
        End If
    End Function

#End Region

#Region "Protected methods"
    Protected Function call_api(strMethod As String) As JObject
        Return JsonConvert.DeserializeObject(SendRequest(strMethod)).Item("result")
    End Function

    Protected Function call_api(strMethod As String, arrParams As ArrayList) As JObject
        Return JsonConvert.DeserializeObject(SendRequest(strMethod, arrParams)).Item("result")
    End Function

    Protected Function call_api_array(strMethod As String, arrParams As ArrayList) As JArray
        Return JsonConvert.DeserializeObject(SendRequest(strMethod, arrParams)).Item("result")
    End Function

    Protected Function call_api_array(strMethod As String) As JArray
        Return JsonConvert.DeserializeObject(SendRequest(strMethod)).Item("result")
    End Function

    Protected Function call_api_value(strMethod As String) As JValue
        Return JsonConvert.DeserializeObject(SendRequest(strMethod)).Item("result")
    End Function

    Protected Function call_api_value(strMethod As String, arrParams As ArrayList) As JValue
        Return JsonConvert.DeserializeObject(SendRequest(strMethod, arrParams)).Item("result")
    End Function

    Protected Function call_api_token(strMethod As String, arrParams As ArrayList) As JToken
        Return JsonConvert.DeserializeObject(SendRequest(strMethod, arrParams)).Item("result")
    End Function

    Protected Function call_api_token(strMethod As String) As JToken
        Return JsonConvert.DeserializeObject(SendRequest(strMethod)).Item("result")
    End Function

#End Region

#Region "IDisposable Support"
    Public Sub Dispose() Implements IDisposable.Dispose
        If Not m_oSocket Is Nothing Then
            m_oSocket.Dispose()
        End If
    End Sub
#End Region

End Class
