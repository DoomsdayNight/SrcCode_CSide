using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Event
{
	// Token: 0x02000FAD RID: 4013
	[PacketId(ClientPacketId.kNKMPacket_AD_INVENTORY_EXPAND_REQ)]
	public sealed class NKMPacket_AD_INVENTORY_EXPAND_REQ : ISerializable
	{
		// Token: 0x06009A2E RID: 39470 RVA: 0x00331082 File Offset: 0x0032F282
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_INVENTORY_EXPAND_TYPE>(ref this.inventoryExpandType);
		}

		// Token: 0x04008D69 RID: 36201
		public NKM_INVENTORY_EXPAND_TYPE inventoryExpandType;
	}
}
