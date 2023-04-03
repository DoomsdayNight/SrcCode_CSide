using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CD0 RID: 3280
	[PacketId(ClientPacketId.kNKMPacket_INVENTORY_EXPAND_REQ)]
	public sealed class NKMPacket_INVENTORY_EXPAND_REQ : ISerializable
	{
		// Token: 0x0600949D RID: 38045 RVA: 0x00328FD1 File Offset: 0x003271D1
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_INVENTORY_EXPAND_TYPE>(ref this.inventoryExpandType);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x0400861C RID: 34332
		public NKM_INVENTORY_EXPAND_TYPE inventoryExpandType;

		// Token: 0x0400861D RID: 34333
		public int count;
	}
}
