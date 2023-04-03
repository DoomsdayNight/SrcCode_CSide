using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C9C RID: 3228
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_TURN_FINISH_ACK)]
	public sealed class NKMPacket_WARFARE_GAME_TURN_FINISH_ACK : ISerializable
	{
		// Token: 0x06009435 RID: 37941 RVA: 0x003285CB File Offset: 0x003267CB
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<WarfareSyncData>(ref this.warfareSyncData);
		}

		// Token: 0x04008586 RID: 34182
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008587 RID: 34183
		public WarfareSyncData warfareSyncData = new WarfareSyncData();
	}
}
