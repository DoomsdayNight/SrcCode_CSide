using System;
using System.Collections.Concurrent;
using Cs.Engine.Network;
using Cs.Logging;
using Cs.Protocol;
using NKC.UI;

namespace NKC
{
	// Token: 0x02000657 RID: 1623
	public abstract class NKCConnectBase
	{
		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x000FCF4D File Offset: 0x000FB14D
		public long SendSequence
		{
			get
			{
				Connection connection = this.connection_;
				if (connection == null)
				{
					return 0L;
				}
				return connection.SendSequence;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x000FCF61 File Offset: 0x000FB161
		protected string TypeName
		{
			get
			{
				return this.typeName_;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x000FCF69 File Offset: 0x000FB169
		public bool IsConnected
		{
			get
			{
				Connection connection = this.connection_;
				return connection != null && connection.IsConnected;
			}
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000FCF7C File Offset: 0x000FB17C
		public void SetRemoteAddress(string ip, int port)
		{
			this.remoteIp_ = ip;
			this.remotePort_ = port;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000FCF8C File Offset: 0x000FB18C
		public virtual void ResetConnection()
		{
			Log.Info(this.typeName_ + " ResetConnection", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectBase.cs", 34);
			if (this.connection_ == null)
			{
				return;
			}
			Connection connection = this.connection_;
			this.connection_ = null;
			connection.CloseConnection();
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000FCFC5 File Offset: 0x000FB1C5
		public void SimulateDisconnect()
		{
			Connection connection = this.connection_;
			if (connection == null)
			{
				return;
			}
			connection.CloseConnection();
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000FCFD7 File Offset: 0x000FB1D7
		public virtual void LoginComplete()
		{
			Log.Info(this.typeName_ + " LoginComplete", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectBase.cs", 54);
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000FCFF8 File Offset: 0x000FB1F8
		public bool Send(ISerializable packet, NKC_OPEN_WAIT_BOX_TYPE eNKC_OPEN_WAIT_BOX_TYPE, bool bSendFailBox = true)
		{
			bool flag = this.IsConnected;
			if (flag)
			{
				flag = this.connection_.Send(packet);
			}
			if (!flag)
			{
				if (bSendFailBox)
				{
					if (NKCScenManager.GetScenManager().IsReconnectScen())
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ERROR_RECONNECT, null, "");
						NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(4f, false);
					}
					else
					{
						NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_ERROR_DECONNECT, null, "");
					}
				}
			}
			else
			{
				NKMPopUpBox.OpenWaitBox(eNKC_OPEN_WAIT_BOX_TYPE, 0f, "", null);
			}
			return flag;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000FD07E File Offset: 0x000FB27E
		public void RawSend(ISerializable data)
		{
			Connection connection = this.connection_;
			if (connection == null)
			{
				return;
			}
			connection.Send(data);
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000FD094 File Offset: 0x000FB294
		public virtual void Update()
		{
			Connection connection = this.connection_;
			if (connection != null)
			{
				connection.ProcessResponses();
			}
			NKCConnectBase.NKC_CONNECT_MSG nkc_CONNECT_MSG;
			while (this.connectionEvents_.TryDequeue(out nkc_CONNECT_MSG))
			{
				switch (nkc_CONNECT_MSG)
				{
				case NKCConnectBase.NKC_CONNECT_MSG.NCM_ON_CONNECTED:
					this.OnConnectedMainThread();
					break;
				case NKCConnectBase.NKC_CONNECT_MSG.NCM_ON_CONNECTED_FAILED:
					this.OnConnectFailedMainThread();
					break;
				case NKCConnectBase.NKC_CONNECT_MSG.NCM_ON_DISCONNECTED:
					this.OnDisconnectedMainThread();
					break;
				}
			}
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000FD0EF File Offset: 0x000FB2EF
		protected NKCConnectBase(Type handlerType)
		{
			this.typeName_ = base.GetType().Name;
			this.handlerType_ = handlerType;
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000FD130 File Offset: 0x000FB330
		protected void Connect()
		{
			Log.Info(this.typeName_ + " Connect", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectBase.cs", 122);
			Connection.Create(this.remoteIp_, this.remotePort_, this.typeName_, new Action<Connection>(this.ConnectCompleted), this.ConnectTimeout);
			NKMPopUpBox.OpenWaitBox(0f, "");
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000FD194 File Offset: 0x000FB394
		protected void ConnectCompleted(Connection connection)
		{
			if (connection == null)
			{
				this.connectionEvents_.Enqueue(NKCConnectBase.NKC_CONNECT_MSG.NCM_ON_CONNECTED_FAILED);
				return;
			}
			this.connection_ = connection;
			this.connection_.RegisterHandler(this.handlerType_);
			this.connection_.OnDisconnected += this.OnDisconnected;
			this.connectionEvents_.Enqueue(NKCConnectBase.NKC_CONNECT_MSG.NCM_ON_CONNECTED);
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000FD1EC File Offset: 0x000FB3EC
		protected void OnDisconnected(Connection connection)
		{
			if (connection != this.connection_)
			{
				return;
			}
			this.connectionEvents_.Enqueue(NKCConnectBase.NKC_CONNECT_MSG.NCM_ON_DISCONNECTED);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000FD204 File Offset: 0x000FB404
		protected virtual void OnConnectedMainThread()
		{
			Log.Info(this.typeName_ + " OnConnectedMainThread", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectBase.cs", 154);
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000FD225 File Offset: 0x000FB425
		protected virtual void OnConnectFailedMainThread()
		{
			Log.Warn(this.typeName_ + " OnConnectFailedMainThread", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectBase.cs", 159);
			NKMPopUpBox.CloseWaitBox();
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000FD24B File Offset: 0x000FB44B
		protected virtual void OnDisconnectedMainThread()
		{
			Log.Warn(this.typeName_ + " OnDisconnectedMainThread", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectBase.cs", 165);
			NKMPopUpBox.CloseWaitBox();
		}

		// Token: 0x0400319A RID: 12698
		private readonly TimeSpan ConnectTimeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400319B RID: 12699
		private readonly string typeName_;

		// Token: 0x0400319C RID: 12700
		private readonly Type handlerType_;

		// Token: 0x0400319D RID: 12701
		private readonly ConcurrentQueue<NKCConnectBase.NKC_CONNECT_MSG> connectionEvents_ = new ConcurrentQueue<NKCConnectBase.NKC_CONNECT_MSG>();

		// Token: 0x0400319E RID: 12702
		private string remoteIp_;

		// Token: 0x0400319F RID: 12703
		private int remotePort_;

		// Token: 0x040031A0 RID: 12704
		private Connection connection_;

		// Token: 0x02001301 RID: 4865
		private enum NKC_CONNECT_MSG
		{
			// Token: 0x040097B4 RID: 38836
			NCM_INVALID,
			// Token: 0x040097B5 RID: 38837
			NCM_ON_CONNECTED,
			// Token: 0x040097B6 RID: 38838
			NCM_ON_CONNECTED_FAILED,
			// Token: 0x040097B7 RID: 38839
			NCM_ON_DISCONNECTED
		}
	}
}
