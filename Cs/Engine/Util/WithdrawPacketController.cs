using System;
using System.Collections;
using ClientPacket.Account;
using Cs.Core.Core;
using Cs.Engine.Network;
using Cs.Logging;
using Cs.Protocol;
using NKC;
using NKC.PacketHandler;
using NKC.Publisher;

namespace Cs.Engine.Util
{
	// Token: 0x020010A9 RID: 4265
	internal class WithdrawPacketController : IDisposable
	{
		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06009C21 RID: 39969 RVA: 0x00334A5D File Offset: 0x00332C5D
		public bool AckReceived
		{
			get
			{
				return this.ackReceived;
			}
		}

		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06009C22 RID: 39970 RVA: 0x00334A65 File Offset: 0x00332C65
		// (set) Token: 0x06009C23 RID: 39971 RVA: 0x00334A6D File Offset: 0x00332C6D
		public ISerializable Ack { get; private set; }

		// Token: 0x06009C24 RID: 39972 RVA: 0x00334A76 File Offset: 0x00332C76
		public IEnumerator WithdrawPacketProcess(string serverAddress, int serverPort)
		{
			this.onProcess.On();
			this.Ack = null;
			this.ackReceived = false;
			WithdrawPacketController.m_instance = this;
			this.reqPacket = NKCPublisherModule.Auth.MakeWithdrawReqPacket();
			Connection connection = Connection.Create(serverAddress, serverPort, "WithdrawPacketController", new Action<Connection>(this.OnConnected), WithdrawPacketController.timeout);
			connection.RegisterHandler(typeof(WithdrawPacketController));
			connection.OnDisconnected += this.OnDisconnected;
			while (this.onProcess.IsOn)
			{
				yield return null;
				connection.ProcessResponses();
			}
			connection.Dispose();
			if (this.Ack == null)
			{
				Log.ErrorAndExit("[WithdrawPacketController] Failed to notice withdrawal.", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Util/WithdrawPacketController.cs", 55);
				yield break;
			}
			Log.Info(string.Format("[WithdrawPacketController] Success to request withdrawal. ip: {0}, port: {1}", serverAddress, serverPort), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Cs.Engine/Util/WithdrawPacketController.cs", 59);
			yield break;
		}

		// Token: 0x06009C25 RID: 39973 RVA: 0x00334A93 File Offset: 0x00332C93
		private void OnConnected(Connection connection)
		{
			if (connection == null || this.reqPacket == null)
			{
				return;
			}
			connection.Send(this.reqPacket);
		}

		// Token: 0x06009C26 RID: 39974 RVA: 0x00334AAE File Offset: 0x00332CAE
		private void OnDisconnected(Connection connection)
		{
			NKMPopUpBox.OpenWaitBox(NKC_OPEN_WAIT_BOX_TYPE.NOWBT_NORMAL, 0f, "", null);
			this.onProcess.Off();
		}

		// Token: 0x06009C27 RID: 39975 RVA: 0x00334AD0 File Offset: 0x00332CD0
		public static void OnRecv(NKMPacket_GAMEBASE_LEAVE_ACK sPacket)
		{
			if (!NKCPacketHandlers.Check_NKM_ERROR_CODE(sPacket.errorCode, true, null, -2147483648))
			{
				WithdrawPacketController.m_instance.Ack = null;
			}
			else
			{
				WithdrawPacketController.m_instance.Ack = sPacket;
			}
			WithdrawPacketController.m_instance.ackReceived = true;
			WithdrawPacketController.m_instance.onProcess.Off();
		}

		// Token: 0x06009C28 RID: 39976 RVA: 0x00334B24 File Offset: 0x00332D24
		public void Dispose()
		{
			WithdrawPacketController.m_instance = null;
			this.Ack = null;
			this.reqPacket = null;
		}

		// Token: 0x04009042 RID: 36930
		private static readonly TimeSpan timeout = TimeSpan.FromSeconds(10.0);

		// Token: 0x04009043 RID: 36931
		private AtomicFlag onProcess = new AtomicFlag(false);

		// Token: 0x04009044 RID: 36932
		private bool ackReceived;

		// Token: 0x04009045 RID: 36933
		private ISerializable reqPacket;

		// Token: 0x04009047 RID: 36935
		private static WithdrawPacketController m_instance = null;
	}
}
