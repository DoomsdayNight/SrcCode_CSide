using System;
using Cs.Logging;
using Cs.Protocol;
using NKC.PacketHandler;
using NKC.Publisher;
using NKC.UI;
using NKM;

namespace NKC
{
	// Token: 0x02000659 RID: 1625
	public sealed class NKCConnectLogin : NKCConnectBase
	{
		// Token: 0x060032FB RID: 13051 RVA: 0x000FD59F File Offset: 0x000FB79F
		public NKCConnectLogin() : base(typeof(NKCPacketHandlersLogin))
		{
		}

		// Token: 0x060032FC RID: 13052 RVA: 0x000FD5B8 File Offset: 0x000FB7B8
		public void SetEnable(bool bSet)
		{
			this.m_bEnable = bSet;
		}

		// Token: 0x060032FD RID: 13053 RVA: 0x000FD5C1 File Offset: 0x000FB7C1
		public override void ResetConnection()
		{
			base.ResetConnection();
			this.StateChange(NKCConnectLogin.NKC_CONNECT_LOGIN_STATE.NCLS_INIT);
		}

		// Token: 0x060032FE RID: 13054 RVA: 0x000FD5D0 File Offset: 0x000FB7D0
		public void AuthToLoginServer()
		{
			this.StateChange(NKCConnectLogin.NKC_CONNECT_LOGIN_STATE.NCLS_CONNECTING_FOR_LOGIN);
			base.Connect();
		}

		// Token: 0x060032FF RID: 13055 RVA: 0x000FD5E0 File Offset: 0x000FB7E0
		public void Send_LOGIN_REQ()
		{
			if (!this.m_bEnable)
			{
				return;
			}
			if (!base.IsConnected)
			{
				return;
			}
			ISerializable serializable = NKCPublisherModule.Auth.MakeLoginServerLoginReqPacket();
			if (serializable == null)
			{
				NKMPopUpBox.CloseWaitBox();
				this.ResetConnection();
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_TRY_AGAIN, null, "");
				return;
			}
			this.StateChange(NKCConnectLogin.NKC_CONNECT_LOGIN_STATE.NCLS_CONNECTED_AND_LOGIN);
			base.Send(serializable, NKC_OPEN_WAIT_BOX_TYPE.NOWBT_INVALID, true);
		}

		// Token: 0x06003300 RID: 13056 RVA: 0x000FD63F File Offset: 0x000FB83F
		public override void LoginComplete()
		{
			if (!base.IsConnected)
			{
				return;
			}
			base.LoginComplete();
			this.StateChange(NKCConnectLogin.NKC_CONNECT_LOGIN_STATE.NCLS_CONNECTED_AND_READY_TO_JOIN_LOBBY);
		}

		// Token: 0x06003301 RID: 13057 RVA: 0x000FD657 File Offset: 0x000FB857
		protected override void OnConnectedMainThread()
		{
			base.OnConnectedMainThread();
			if (this.state_ == NKCConnectLogin.NKC_CONNECT_LOGIN_STATE.NCLS_CONNECTING_FOR_LOGIN)
			{
				this.Send_LOGIN_REQ();
			}
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000FD670 File Offset: 0x000FB870
		protected override void OnConnectFailedMainThread()
		{
			base.OnConnectFailedMainThread();
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_LOGIN)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_FAIL_CONNECT, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
				}, "");
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_FAIL_CONNECT, null, "");
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000FD6DC File Offset: 0x000FB8DC
		protected override void OnDisconnectedMainThread()
		{
			base.OnDisconnectedMainThread();
			if (NKCScenManager.GetScenManager().GetNowScenID() != NKM_SCEN_ID.NSI_LOGIN)
			{
				NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_DECONNECT, delegate()
				{
					NKCScenManager.GetScenManager().ScenChangeFade(NKM_SCEN_ID.NSI_LOGIN, true);
				}, "");
				return;
			}
			NKCPopupOKCancel.OpenOKBox(NKCUtilString.GET_STRING_ERROR, NKCUtilString.GET_STRING_ERROR_DECONNECT, null, "");
		}

		// Token: 0x06003304 RID: 13060 RVA: 0x000FD745 File Offset: 0x000FB945
		private void StateChange(NKCConnectLogin.NKC_CONNECT_LOGIN_STATE eNKC_CONNECT_LOGIN_STATE)
		{
			Log.Info(string.Format("{0} StateChange {1} -> {2}", base.TypeName, this.state_, eNKC_CONNECT_LOGIN_STATE), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCConnectLogin.cs", 130);
			this.state_ = eNKC_CONNECT_LOGIN_STATE;
		}

		// Token: 0x040031AB RID: 12715
		private NKCConnectLogin.NKC_CONNECT_LOGIN_STATE state_;

		// Token: 0x040031AC RID: 12716
		private bool m_bEnable = true;

		// Token: 0x02001303 RID: 4867
		private enum NKC_CONNECT_LOGIN_STATE
		{
			// Token: 0x040097BE RID: 38846
			NCLS_INIT,
			// Token: 0x040097BF RID: 38847
			NCLS_CONNECTING_FOR_LOGIN,
			// Token: 0x040097C0 RID: 38848
			NCLS_CONNECTED_AND_LOGIN,
			// Token: 0x040097C1 RID: 38849
			NCLS_CONNECTED_AND_READY_TO_JOIN_LOBBY
		}
	}
}
