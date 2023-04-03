using System;
using System.Collections.Generic;
using ClientPacket.Account;
using Cs.Logging;
using NKM;

namespace NKC.InfraTool
{
	// Token: 0x02000895 RID: 2197
	public sealed class TempLoginConnector : NKCConnectBase
	{
		// Token: 0x060057A3 RID: 22435 RVA: 0x001A4CCF File Offset: 0x001A2ECF
		public TempLoginConnector(Type handlerType) : base(handlerType)
		{
			this.Protocol = 845;
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x001A4CE3 File Offset: 0x001A2EE3
		public void Connect(string ip, int port, Action failedMainThread, Action connectedMainThread, Action disconnectedMainThread)
		{
			base.SetRemoteAddress(ip, port);
			base.Connect();
			this.FailedMainThread = failedMainThread;
			this.ConnectedMainThread = connectedMainThread;
			this.DisconnectedMainThread = disconnectedMainThread;
		}

		// Token: 0x060057A5 RID: 22437 RVA: 0x001A4D0A File Offset: 0x001A2F0A
		protected override void OnConnectFailedMainThread()
		{
			base.OnConnectFailedMainThread();
			Action failedMainThread = this.FailedMainThread;
			if (failedMainThread != null)
			{
				failedMainThread();
			}
			Log.Error(TempLoginConnector._logKey + " ConnectFailed", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempConnector.cs", 85);
		}

		// Token: 0x060057A6 RID: 22438 RVA: 0x001A4D3E File Offset: 0x001A2F3E
		protected override void OnConnectedMainThread()
		{
			base.OnConnectedMainThread();
			Action connectedMainThread = this.ConnectedMainThread;
			if (connectedMainThread != null)
			{
				connectedMainThread();
			}
			Log.Debug(TempLoginConnector._logKey + " Connected", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempConnector.cs", 94);
		}

		// Token: 0x060057A7 RID: 22439 RVA: 0x001A4D72 File Offset: 0x001A2F72
		protected override void OnDisconnectedMainThread()
		{
			base.OnDisconnectedMainThread();
			Action disconnectedMainThread = this.DisconnectedMainThread;
			if (disconnectedMainThread != null)
			{
				disconnectedMainThread();
			}
			Log.Debug(TempLoginConnector._logKey + " Disconnected", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempConnector.cs", 103);
		}

		// Token: 0x060057A8 RID: 22440 RVA: 0x001A4DA8 File Offset: 0x001A2FA8
		public void SendDevLogin()
		{
			NKMPacket_LOGIN_REQ nkmpacket_LOGIN_REQ = new NKMPacket_LOGIN_REQ();
			nkmpacket_LOGIN_REQ.protocolVersion = (long)this.Protocol;
			nkmpacket_LOGIN_REQ.accountID = "TestTest";
			nkmpacket_LOGIN_REQ.password = "1234432112344321";
			Log.Debug(string.Format("[Login] LoginReq ProtocolVersion[{0}] ID[{1}]", nkmpacket_LOGIN_REQ.protocolVersion, nkmpacket_LOGIN_REQ.accountID), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempConnector.cs", 114);
			NKM_USER_AUTH_LEVEL userAuthLevel = NKM_USER_AUTH_LEVEL.NORMAL_USER;
			nkmpacket_LOGIN_REQ.userAuthLevel = userAuthLevel;
			nkmpacket_LOGIN_REQ.deviceUid = "j3jerkwjeerhwejkrhjewk";
			base.RawSend(nkmpacket_LOGIN_REQ);
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x001A4E20 File Offset: 0x001A3020
		public void SetPacketReceiveAction(TempLoginConnector.TempLoginConnectReceiver.OnFailPacket onFail)
		{
			TempLoginConnector.TempLoginConnectReceiver.SetFailAction(onFail);
		}

		// Token: 0x04004547 RID: 17735
		private static readonly string _logKey = "[Patcher]";

		// Token: 0x04004548 RID: 17736
		private Action FailedMainThread;

		// Token: 0x04004549 RID: 17737
		private Action ConnectedMainThread;

		// Token: 0x0400454A RID: 17738
		private Action DisconnectedMainThread;

		// Token: 0x0400454B RID: 17739
		public int Protocol;

		// Token: 0x0200156B RID: 5483
		public static class TempLoginConnectReceiver
		{
			// Token: 0x170018CE RID: 6350
			// (get) Token: 0x0600AD10 RID: 44304 RVA: 0x00357907 File Offset: 0x00355B07
			// (set) Token: 0x0600AD0F RID: 44303 RVA: 0x003578FF File Offset: 0x00355AFF
			public static List<string> ContentTagList { get; private set; }

			// Token: 0x170018CF RID: 6351
			// (get) Token: 0x0600AD12 RID: 44306 RVA: 0x00357916 File Offset: 0x00355B16
			// (set) Token: 0x0600AD11 RID: 44305 RVA: 0x0035790E File Offset: 0x00355B0E
			public static List<string> OpenTagList { get; private set; }

			// Token: 0x0600AD13 RID: 44307 RVA: 0x00357920 File Offset: 0x00355B20
			public static void OnRecv(NKMPacket_LOGIN_ACK lPacket)
			{
				string text = TempLoginConnector._logKey + " [NKMPacket_LOGIN_ACK]";
				if (lPacket != null)
				{
					Log.Debug(string.Format("{0} errorCode :{1}", text, lPacket.errorCode), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempConnector.cs", 31);
					Log.Debug(text + " ServerVersion : " + lPacket.contentsVersion, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempConnector.cs", 32);
					Log.Debug(string.Format("{0} contentTag Count : {1}", text, lPacket.contentsTag.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempConnector.cs", 33);
					Log.Debug(string.Format("{0} openTag Count : {1}", text, lPacket.openTag.Count), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/PatchTool/TempConnector.cs", 34);
					TempLoginConnector.TempLoginConnectReceiver.ContentTagList = new List<string>(lPacket.contentsTag);
					TempLoginConnector.TempLoginConnectReceiver.OpenTagList = new List<string>(lPacket.openTag);
					if (lPacket.errorCode != NKM_ERROR_CODE.NEC_OK)
					{
						TempLoginConnector.TempLoginConnectReceiver.OnFailPacket onFail = TempLoginConnector.TempLoginConnectReceiver.OnFail;
						if (onFail == null)
						{
							return;
						}
						onFail(string.Format("{0} Error : {1}", text, lPacket.errorCode));
					}
					return;
				}
				TempLoginConnector.TempLoginConnectReceiver.OnFailPacket onFail2 = TempLoginConnector.TempLoginConnectReceiver.OnFail;
				if (onFail2 == null)
				{
					return;
				}
				onFail2(TempLoginConnector._logKey + " Packet is null");
			}

			// Token: 0x0600AD14 RID: 44308 RVA: 0x00357A39 File Offset: 0x00355C39
			public static void CleanUpContainer()
			{
				TempLoginConnector.TempLoginConnectReceiver.ContentTagList = null;
				TempLoginConnector.TempLoginConnectReceiver.OpenTagList = null;
			}

			// Token: 0x0600AD15 RID: 44309 RVA: 0x00357A47 File Offset: 0x00355C47
			public static void SetFailAction(TempLoginConnector.TempLoginConnectReceiver.OnFailPacket onFailPacketAction)
			{
				TempLoginConnector.TempLoginConnectReceiver.OnFail = (TempLoginConnector.TempLoginConnectReceiver.OnFailPacket)Delegate.Remove(TempLoginConnector.TempLoginConnectReceiver.OnFail, onFailPacketAction);
				TempLoginConnector.TempLoginConnectReceiver.OnFail = (TempLoginConnector.TempLoginConnectReceiver.OnFailPacket)Delegate.Combine(TempLoginConnector.TempLoginConnectReceiver.OnFail, onFailPacketAction);
			}

			// Token: 0x0400A0F8 RID: 41208
			public static TempLoginConnector.TempLoginConnectReceiver.OnFailPacket OnFail;

			// Token: 0x02001A6B RID: 6763
			// (Invoke) Token: 0x0600BBDF RID: 48095
			public delegate void OnFailPacket(string str);
		}
	}
}
