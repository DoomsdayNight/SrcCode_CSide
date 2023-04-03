using System;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Cs.Engine.Network.Buffer;
using Cs.Logging;
using Cs.Protocol;
using Protocol;

namespace Cs.Engine.Network
{
	// Token: 0x020010AA RID: 4266
	public sealed class Connection : IDisposable
	{
		// Token: 0x06009C2B RID: 39979 RVA: 0x00334B6C File Offset: 0x00332D6C
		private Connection(string ip, int port, string serverType)
		{
			this.socket_.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
			this.ServerType = serverType;
			this.host = ip;
			this.port = port;
			this.receiveArgs_.SetBuffer(this.receiveBuffer_, 0, this.receiveBuffer_.Length);
			this.receiveArgs_.Completed += this.OnReceiveCompleted;
			this.sendController_ = new SendController(this.socket_);
		}

		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06009C2C RID: 39980 RVA: 0x00334C26 File Offset: 0x00332E26
		public string ServerType { get; }

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06009C2D RID: 39981 RVA: 0x00334C2E File Offset: 0x00332E2E
		public bool IsConnected
		{
			get
			{
				return this.socket_.Connected;
			}
		}

		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06009C2E RID: 39982 RVA: 0x00334C3B File Offset: 0x00332E3B
		public long SendSequence
		{
			get
			{
				return this.sendSequence;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x06009C2F RID: 39983 RVA: 0x00334C44 File Offset: 0x00332E44
		// (remove) Token: 0x06009C30 RID: 39984 RVA: 0x00334C7C File Offset: 0x00332E7C
		public event Connection.ConnectionEventHandler OnDisconnected;

		// Token: 0x06009C31 RID: 39985 RVA: 0x00334CB1 File Offset: 0x00332EB1
		public static Connection Create(string ip, int port, string serverType, Action<Connection> onConnected, TimeSpan timeout)
		{
			Connection connection = new Connection(ip, port, serverType);
			connection.TryConnect(onConnected, timeout);
			return connection;
		}

		// Token: 0x06009C32 RID: 39986 RVA: 0x00334CC4 File Offset: 0x00332EC4
		public void Dispose()
		{
			this.socket_.Dispose();
			this.receiveArgs_.Dispose();
			this.receivePipe_.Dispose();
			this.sendController_.Dispose();
		}

		// Token: 0x06009C33 RID: 39987 RVA: 0x00334CF2 File Offset: 0x00332EF2
		public void RegisterHandler(Type containerType)
		{
			this.recvController_.RegisterHandler(containerType);
		}

		// Token: 0x06009C34 RID: 39988 RVA: 0x00334D00 File Offset: 0x00332F00
		public void ProcessResponses()
		{
			this.recvController_.ProcessResponses(this);
		}

		// Token: 0x06009C35 RID: 39989 RVA: 0x00334D10 File Offset: 0x00332F10
		public override string ToString()
		{
			return string.Format("endpoint:{0}:{1} connected:{2} send_queue:{3}", new object[]
			{
				this.host,
				this.port,
				this.socket_.Connected,
				this.sendController_.MessageCount
			});
		}

		// Token: 0x06009C36 RID: 39990 RVA: 0x00334D6C File Offset: 0x00332F6C
		public void CloseConnection()
		{
			if (this.finalized_)
			{
				return;
			}
			this.finalized_ = true;
			if (this.socket_.Connected)
			{
				try
				{
					this.socket_.Shutdown(SocketShutdown.Both);
				}
				catch (SocketException ex)
				{
					Log.Warn(string.Format("Shutdown failed. code:[{0}]{1} msg:{2}", ex.ErrorCode, ex.SocketErrorCode.ToString(), ex.Message), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 87);
				}
				finally
				{
					this.socket_.Close();
					this.socket_.Dispose();
					this.receivePipe_.Dispose();
					Connection.ConnectionEventHandler onDisconnected = this.OnDisconnected;
					if (onDisconnected != null)
					{
						onDisconnected(this);
					}
				}
			}
		}

		// Token: 0x06009C37 RID: 39991 RVA: 0x00334E38 File Offset: 0x00333038
		public bool Send(ISerializable msg)
		{
			if (msg == null)
			{
				return false;
			}
			if (!this.socket_.Connected)
			{
				Log.Warn("socket connected == false", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 109);
				this.CloseConnection();
				return false;
			}
			long sequence = Interlocked.Increment(ref this.sendSequence);
			Packet? packet = Packet.Pack(msg, sequence);
			if (packet == null)
			{
				Log.Error(string.Format("data serializing failed. packetid:{0}", PacketController.Instance.GetId(msg)), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 120);
				return false;
			}
			ClientPacketId id = (ClientPacketId)PacketController.Instance.GetId(msg);
			if (id != ClientPacketId.kNKMPacket_HEART_BIT_REQ)
			{
				Log.Info("<color=#FFFF00FF>" + id.ToString() + "</color>", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 126);
			}
			this.sendController_.Push(packet.Value);
			return true;
		}

		// Token: 0x06009C38 RID: 39992 RVA: 0x00334F04 File Offset: 0x00333104
		private void BeginReceive()
		{
			try
			{
				if (!this.socket_.ReceiveAsync(this.receiveArgs_))
				{
					this.OnReceiveCompleted(null, this.receiveArgs_);
				}
			}
			catch (Exception ex)
			{
				Log.Info(this.ServerType + " ReceiveAsync " + ex.Message, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 147);
				this.CloseConnection();
			}
		}

		// Token: 0x06009C39 RID: 39993 RVA: 0x00334F74 File Offset: 0x00333174
		private void OnReceiveCompleted(object sender, SocketAsyncEventArgs arg)
		{
			if (arg.BytesTransferred <= 0)
			{
				Log.Info("OnRecvCallback transferred zero", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 157);
				this.CloseConnection();
				return;
			}
			if (arg.SocketError != SocketError.Success)
			{
				if (arg.SocketError != SocketError.ConnectionReset)
				{
					Log.Warn(string.Format("[Connection] OnReceiveCompleted ErrorCode:{0} serverType:{1}", arg.SocketError, this.ServerType), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 166);
				}
				Log.Warn("socket error != success", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 169);
				this.CloseConnection();
				return;
			}
			this.receivePipe_.Write(this.receiveBuffer_, 0, arg.BytesTransferred);
			if (!Packet.ProcessRecv(this.receivePipe_, this.recvController_))
			{
				Log.Warn("packet process Recv fail", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 177);
				this.CloseConnection();
				return;
			}
			this.BeginReceive();
		}

		// Token: 0x06009C3A RID: 39994 RVA: 0x0033504B File Offset: 0x0033324B
		private void TryConnect(Action<Connection> callback, TimeSpan timeout)
		{
			Task.Run(delegate()
			{
				try
				{
					Log.Info(string.Format("{0} socket.ConnectAsync {1}:{2}", this.ServerType, this.host, this.port), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 192);
					IAsyncResult asyncResult = this.socket_.BeginConnect(this.host, this.port, null, null);
					asyncResult.AsyncWaitHandle.WaitOne((int)timeout.TotalMilliseconds, false);
					if (this.socket_.Connected)
					{
						this.socket_.EndConnect(asyncResult);
						this.BeginReceive();
						callback(this);
					}
					else
					{
						this.CloseConnection();
						callback(null);
					}
				}
				catch (Exception ex)
				{
					Log.Error(this.ServerType + " connection failed:" + ex.Message, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Network/Connection.cs", 212);
					this.CloseConnection();
					callback(null);
				}
			});
		}

		// Token: 0x04009048 RID: 36936
		private readonly Socket socket_ = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);

		// Token: 0x04009049 RID: 36937
		private readonly string host;

		// Token: 0x0400904A RID: 36938
		private readonly int port;

		// Token: 0x0400904B RID: 36939
		private readonly byte[] receiveBuffer_ = new byte[4096];

		// Token: 0x0400904C RID: 36940
		private readonly SocketAsyncEventArgs receiveArgs_ = new SocketAsyncEventArgs();

		// Token: 0x0400904D RID: 36941
		private readonly SendController sendController_;

		// Token: 0x0400904E RID: 36942
		private readonly RecvController recvController_ = new RecvController();

		// Token: 0x0400904F RID: 36943
		private readonly MemoryPipe receivePipe_ = new MemoryPipe();

		// Token: 0x04009050 RID: 36944
		private bool finalized_;

		// Token: 0x04009051 RID: 36945
		private long sendSequence;

		// Token: 0x02001A32 RID: 6706
		// (Invoke) Token: 0x0600BB57 RID: 47959
		public delegate void ConnectionEventHandler(Connection connection);
	}
}
