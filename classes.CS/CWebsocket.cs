using System;
using System.Collections;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GolosAPI.CS
{
	class CWebsocket : IDisposable
	{
		#region Variables
		private Uri m_oURI;
		private ClientWebSocket m_oCWS;
		#endregion

		#region Constructors
		public CWebsocket(string strURI = "ws://golos.steem.ws")
		{
			m_oURI = new Uri(strURI);
			m_oCWS = new ClientWebSocket();
		}
		#endregion

		#region Public methods
		public async Task<string> SendRequest(string strMethod, ArrayList strParams = null)
		{
			int nRetry = 0;
			string strResult = string.Empty;
			Hashtable arrRequest = new Hashtable();
			byte[] bytes = new byte[2048];
			WebSocketReceiveResult result;

			arrRequest["jsonrpc"] = "2.0";
			arrRequest["id"] = 1;
			arrRequest["method"] = strMethod;
			if (null != strParams)
			{
				arrRequest["params"] = strParams;
			}

			string strJson = JsonConvert.SerializeObject(arrRequest);
			for (nRetry = 1; nRetry < 2; nRetry++)
			{
				try
				{
					if (m_oCWS.State != WebSocketState.Open)
					{
						await m_oCWS.ConnectAsync(m_oURI, CancellationToken.None);
					}

					ArraySegment<byte> bytesToSend = new ArraySegment<byte>(Encoding.UTF8.GetBytes(strJson));
					ArraySegment<byte> bytesReceived = new ArraySegment<byte>(bytes);

					await m_oCWS.SendAsync(bytesToSend, WebSocketMessageType.Text, true, CancellationToken.None);
					do
					{
						result = await m_oCWS.ReceiveAsync(bytesReceived, CancellationToken.None);
						strResult += Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
					} while (!result.EndOfMessage);

					break;
				}
				catch (Exception ex)
				{
					// Opps, might be a timeout
					if (nRetry == 2)
					{
						// second timeout - give up and re-throw exception
						throw ex;
					}
				}
				// wait 1 sec to give a seconde chance to a busy server
				System.Threading.Thread.Sleep(1000);
			}
			return strResult;
		}

		#endregion

		#region IDisposable Support
		void IDisposable.Dispose()
		{
			if(WebSocketState.Open == m_oCWS.State )
			{
				m_oCWS.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
			}
			m_oCWS.Dispose();
			m_oCWS = null;
		}
		#endregion
	}
}
