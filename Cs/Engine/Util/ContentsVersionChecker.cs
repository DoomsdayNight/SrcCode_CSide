using System;
using System.Collections;
using System.Diagnostics;
using ClientPacket.Account;
using Cs.Core.Core;
using Cs.Engine.Network;
using Cs.Logging;
using Cs.Protocol;
using NKC;
using NKM;
using UnityEngine;

namespace Cs.Engine.Util
{
	// Token: 0x020010A6 RID: 4262
	internal static class ContentsVersionChecker
	{
		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x06009C0E RID: 39950 RVA: 0x003343DA File Offset: 0x003325DA
		public static bool VersionAckReceived
		{
			get
			{
				return ContentsVersionChecker.versionAckReceived;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06009C0F RID: 39951 RVA: 0x003343E1 File Offset: 0x003325E1
		public static float RetryInterval
		{
			get
			{
				return 10f;
			}
		}

		// Token: 0x06009C10 RID: 39952 RVA: 0x003343E8 File Offset: 0x003325E8
		static ContentsVersionChecker()
		{
			PacketController.Instance.Initialize();
		}

		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06009C11 RID: 39953 RVA: 0x0033441E File Offset: 0x0033261E
		// (set) Token: 0x06009C12 RID: 39954 RVA: 0x00334425 File Offset: 0x00332625
		public static NKMPacket_CONTENTS_VERSION_ACK Ack { get; private set; }

		// Token: 0x06009C13 RID: 39955 RVA: 0x0033442D File Offset: 0x0033262D
		public static IEnumerator GetVersion(string serverAddress, int serverPort = -1, bool bUseLocalSaveLastServerInfoToGetTags = true)
		{
			if (serverPort == -1)
			{
				serverPort = NKCConnectionInfo.ServicePort;
			}
			if (bUseLocalSaveLastServerInfoToGetTags)
			{
				string @string = PlayerPrefs.GetString("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_IP");
				int @int = PlayerPrefs.GetInt("LOCAL_SAVE_CONTENTS_TAG_LAST_SERVER_PORT");
				Log.Info(string.Format("LoadTagInfo IP[{0}] PORT[{1}]", @string, @int), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Util/ContentsVersionChecker.cs", 44);
				if (@string.Length > 1)
				{
					serverAddress = @string;
				}
				if (@int > 0)
				{
					serverPort = @int;
				}
				if (!NKCDefineManager.DEFINE_SERVICE())
				{
					NKCConnectionInfo.SetLoginServerInfo(NKCConnectionInfo.CurrentLoginServerType, serverAddress, serverPort, null);
				}
			}
			ContentsVersionChecker.onProcess.On();
			ContentsVersionChecker.Ack = null;
			ContentsVersionChecker.versionAckReceived = false;
			Log.Info("VersionChecker, serverAddress : " + serverAddress + ", port : " + serverPort.ToString(), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Util/ContentsVersionChecker.cs", 62);
			Stopwatch stopwatch = Stopwatch.StartNew();
			Connection connection = Connection.Create(serverAddress, serverPort, "versionChecker", new Action<Connection>(ContentsVersionChecker.OnConnected), ContentsVersionChecker.timeout);
			connection.RegisterHandler(typeof(ContentsVersionChecker));
			connection.OnDisconnected += ContentsVersionChecker.OnDisconnected;
			while (ContentsVersionChecker.onProcess.IsOn && stopwatch.Elapsed < ContentsVersionChecker.timeout)
			{
				yield return null;
				connection.ProcessResponses();
			}
			connection.Dispose();
			if (ContentsVersionChecker.Ack == null)
			{
				Log.ErrorAndExit(string.Format("[ContentsVersionChecker] get version failed. elapsed:{0}msec", stopwatch.ElapsedMilliseconds), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Util/ContentsVersionChecker.cs", 85);
				yield break;
			}
			Log.Info(string.Format("[ContentsVersionChecker] get version success. elapsed:{0}msec", stopwatch.ElapsedMilliseconds), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Util/ContentsVersionChecker.cs", 89);
			yield break;
		}

		// Token: 0x06009C14 RID: 39956 RVA: 0x0033444A File Offset: 0x0033264A
		private static void OnConnected(Connection connection)
		{
			ContentsVersionChecker.versionReqCount++;
			connection.Send(new NKMPacket_CONTENTS_VERSION_REQ());
		}

		// Token: 0x06009C15 RID: 39957 RVA: 0x00334464 File Offset: 0x00332664
		private static void OnDisconnected(Connection connection)
		{
			ContentsVersionChecker.onProcess.Off();
		}

		// Token: 0x06009C16 RID: 39958 RVA: 0x00334474 File Offset: 0x00332674
		public static void OnRecv(NKMPacket_CONTENTS_VERSION_ACK ack)
		{
			if (ack.errorCode != NKM_ERROR_CODE.NEC_OK)
			{
				Log.ErrorAndExit(string.Format("[ContentsVersionChecker] errorCode:{0}", ack.errorCode), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Util/ContentsVersionChecker.cs", 107);
				return;
			}
			NKMContentsVersionManager.Drop();
			Log.Info("[ContentsVersion] version:" + ack.contentsVersion + " tag:" + string.Join(", ", ack.contentsTag), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Util/ContentsVersionChecker.cs", 114);
			foreach (string tag in ack.contentsTag)
			{
				NKMContentsVersionManager.AddTag(tag);
			}
			NKCContentsVersionManager.SaveTagToLocal();
			ContentsVersionChecker.Ack = ack;
			NKCSynchronizedTime.OnRecv(ack.utcTime, ack.utcOffset);
			ContentsVersionChecker.versionAckReceived = true;
			ContentsVersionChecker.onProcess.Off();
		}

		// Token: 0x0400903D RID: 36925
		private static readonly TimeSpan timeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400903E RID: 36926
		private static AtomicFlag onProcess = new AtomicFlag(false);

		// Token: 0x0400903F RID: 36927
		private static int versionReqCount = 0;

		// Token: 0x04009040 RID: 36928
		private static bool versionAckReceived = false;
	}
}
