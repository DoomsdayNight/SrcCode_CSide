using System;
using ClientPacket.Service;
using Cs.Logging;
using NKC.PacketHandler;
using NKC.Publisher;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000658 RID: 1624
	public sealed class NKCConnectGame : NKCConnectBase
	{
		// Token: 0x060032E7 RID: 13031 RVA: 0x000FD271 File Offset: 0x000FB471
		public float GetPingTime()
		{
			return this.m_fPingTime;
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000FD279 File Offset: 0x000FB479
		public void SetReconnectKey(string key)
		{
			if (key == null)
			{
				this.m_ReconnectKey = "";
				return;
			}
			this.m_ReconnectKey = key;
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000FD291 File Offset: 0x000FB491
		public string GetReconnectKey()
		{
			return this.m_ReconnectKey;
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000FD29C File Offset: 0x000FB49C
		public NKCConnectGame() : base(typeof(NKCPacketHandlersLobby))
		{
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x000FD2EC File Offset: 0x000FB4EC
		public void SetEnable(bool bSet)
		{
			this.m_bEnable = bSet;
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x000FD2F5 File Offset: 0x000FB4F5
		public bool HasLoggedIn
		{
			get
			{
				return this.state_ == NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_LOGGED_IN;
			}
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x000FD300 File Offset: 0x000FB500
		public override void ResetConnection()
		{
			base.ResetConnection();
			this.heartBitTimeOutNow_ = 16f;
			this.heartBitIntervalNow_ = 10f;
			this.ChangeState(NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_INIT);
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x000FD325 File Offset: 0x000FB525
		public void SetAccessToken(string token)
		{
			this.accessToken = token;
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x000FD32E File Offset: 0x000FB52E
		public override void LoginComplete()
		{
			base.LoginComplete();
			this.ChangeState(NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_LOGGED_IN);
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x000FD33D File Offset: 0x000FB53D
		public void ConnectToLobbyServer()
		{
			this.ChangeState(NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_CONNECTING);
			base.Connect();
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x000FD34C File Offset: 0x000FB54C
		public void Send_JOIN_LOBBY_REQ()
		{
			if (!this.m_bEnable)
			{
				return;
			}
			if (!base.IsConnected)
			{
				return;
			}
			this.ChangeState(NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_CONNECTED);
			base.Send(NKCPublisherModule.Auth.MakeGameServerLoginReqPacket(this.accessToken), NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x000FD380 File Offset: 0x000FB580
		public override void Update()
		{
			base.Update();
			if (!base.IsConnected)
			{
				return;
			}
			NKCHeartbeatSupporter.Instance.Update();
			this.HeartBitProcess();
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x000FD3A4 File Offset: 0x000FB5A4
		public void Reconnect()
		{
			if (!this.m_bEnable)
			{
				return;
			}
			Log.Info("disconnect all connection. trying reconnect to login server.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectGame.cs", 105);
			if (NKCScenManager.GetScenManager().GetNowScenID() == NKM_SCEN_ID.NSI_GAME && NKCScenManager.GetScenManager().GetGameClient() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData() != null && NKCScenManager.GetScenManager().GetGameClient().GetGameData().GetGameType() == NKM_GAME_TYPE.NGT_DEV)
			{
				return;
			}
			NKCScenManager.GetScenManager().GetConnectLogin().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectGame().ResetConnection();
			NKCScenManager.GetScenManager().GetConnectLogin().AuthToLoginServer();
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x000FD43A File Offset: 0x000FB63A
		public void ResetHeartbitTimeout(float fTimeOutTime = 16f)
		{
			this.heartBitTimeOutNow_ = fTimeOutTime;
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x000FD443 File Offset: 0x000FB643
		protected override void OnConnectedMainThread()
		{
			base.OnConnectedMainThread();
			if (this.state_ == NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_CONNECTING)
			{
				this.Send_JOIN_LOBBY_REQ();
			}
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x000FD45A File Offset: 0x000FB65A
		protected override void OnConnectFailedMainThread()
		{
			base.OnConnectFailedMainThread();
			NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(1f, false);
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x000FD472 File Offset: 0x000FB672
		protected override void OnDisconnectedMainThread()
		{
			if (this.state_ == NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_CONNECTED || this.state_ == NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_LOGGED_IN)
			{
				base.OnDisconnectedMainThread();
				NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(1f, false);
			}
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x000FD49C File Offset: 0x000FB69C
		private void HeartBitProcess()
		{
			if (!base.IsConnected)
			{
				return;
			}
			if (this.state_ != NKCConnectGame.NKC_CONNECT_LOBBY_STATE.NCLS_LOGGED_IN)
			{
				return;
			}
			this.heartBitIntervalNow_ -= Time.deltaTime;
			if (this.heartBitIntervalNow_ <= 0f)
			{
				this.heartBitIntervalNow_ = 10f;
				this.heartbitReq_.time = DateTime.Now.Ticks;
				base.Send(this.heartbitReq_, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
			}
			this.heartBitTimeOutNow_ -= Time.deltaTime;
			if (this.heartBitTimeOutNow_ <= 0f)
			{
				Debug.Log("[HeartBitProcess] HeartBit timeout occurred!");
				NKCScenManager.GetScenManager().SetAppEnableConnectCheckTime(1f, false);
			}
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x000FD548 File Offset: 0x000FB748
		public void SetPingTime(long pingTime)
		{
			DateTime d = new DateTime(pingTime);
			this.m_fPingTime = (float)(DateTime.Now - d).TotalSeconds;
		}

		// Token: 0x060032FA RID: 13050 RVA: 0x000FD577 File Offset: 0x000FB777
		private void ChangeState(NKCConnectGame.NKC_CONNECT_LOBBY_STATE eNKC_CONNECT_LOBBY_STATE)
		{
			this.state_ = eNKC_CONNECT_LOBBY_STATE;
			Log.Info(string.Format("NKCConnectLobby:StateChange {0}", eNKC_CONNECT_LOBBY_STATE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectGame.cs", 208);
		}

		// Token: 0x040031A1 RID: 12705
		private const float HEARTBEAT_TIME_OUT = 16f;

		// Token: 0x040031A2 RID: 12706
		private const float HEARTBEAT_INTERVAL = 10f;

		// Token: 0x040031A3 RID: 12707
		private readonly NKMPacket_HEART_BIT_REQ heartbitReq_ = new NKMPacket_HEART_BIT_REQ();

		// Token: 0x040031A4 RID: 12708
		private NKCConnectGame.NKC_CONNECT_LOBBY_STATE state_;

		// Token: 0x040031A5 RID: 12709
		private float heartBitTimeOutNow_ = 16f;

		// Token: 0x040031A6 RID: 12710
		private float heartBitIntervalNow_ = 10f;

		// Token: 0x040031A7 RID: 12711
		private string accessToken;

		// Token: 0x040031A8 RID: 12712
		private float m_fPingTime;

		// Token: 0x040031A9 RID: 12713
		private string m_ReconnectKey = "";

		// Token: 0x040031AA RID: 12714
		private bool m_bEnable = true;

		// Token: 0x02001302 RID: 4866
		private enum NKC_CONNECT_LOBBY_STATE
		{
			// Token: 0x040097B9 RID: 38841
			NCLS_INIT,
			// Token: 0x040097BA RID: 38842
			NCLS_CONNECTING,
			// Token: 0x040097BB RID: 38843
			NCLS_CONNECTED,
			// Token: 0x040097BC RID: 38844
			NCLS_LOGGED_IN
		}
	}
}
