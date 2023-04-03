using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FAE RID: 4014
	[PacketId(ClientPacketId.kNKMPacket_AD_INVENTORY_EXPAND_ACK)]
	public sealed class NKMPacket_AD_INVENTORY_EXPAND_ACK : ISerializable
	{
		// Token: 0x06009A30 RID: 39472 RVA: 0x00331098 File Offset: 0x0032F298
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGetEnum<NKM_INVENTORY_EXPAND_TYPE>(ref this.inventoryExpandType);
			stream.PutOrGet(ref this.expandedCount);
		}

		// Token: 0x04008D6A RID: 36202
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008D6B RID: 36203
		public NKM_INVENTORY_EXPAND_TYPE inventoryExpandType;

		// Token: 0x04008D6C RID: 36204
		public int expandedCount;
	}
}
