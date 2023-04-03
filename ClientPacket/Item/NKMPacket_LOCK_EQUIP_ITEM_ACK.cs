using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E8A RID: 3722
	[PacketId(ClientPacketId.kNKMPacket_LOCK_EQUIP_ITEM_ACK)]
	public sealed class NKMPacket_LOCK_EQUIP_ITEM_ACK : ISerializable
	{
		// Token: 0x06009800 RID: 38912 RVA: 0x0032DD19 File Offset: 0x0032BF19
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.equipItemUID);
			stream.PutOrGet(ref this.isLock);
		}

		// Token: 0x04008A51 RID: 35409
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008A52 RID: 35410
		public long equipItemUID;

		// Token: 0x04008A53 RID: 35411
		public bool isLock;
	}
}
