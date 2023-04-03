using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C9A RID: 3226
	[PacketId(ClientPacketId.kNKMPacket_WARFARE_GAME_MOVE_ACK)]
	public sealed class NKMPacket_WARFARE_GAME_MOVE_ACK : ISerializable
	{
		// Token: 0x06009431 RID: 37937 RVA: 0x00328594 File Offset: 0x00326794
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<WarfareSyncData>(ref this.warfareSyncData);
		}

		// Token: 0x04008584 RID: 34180
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008585 RID: 34181
		public WarfareSyncData warfareSyncData = new WarfareSyncData();
	}
}
