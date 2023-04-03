using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E97 RID: 3735
	[PacketId(ClientPacketId.kNKMPacket_EQUIP_TUNING_REFINE_REQ)]
	public sealed class NKMPacket_EQUIP_TUNING_REFINE_REQ : ISerializable
	{
		// Token: 0x0600981A RID: 38938 RVA: 0x0032DF3A File Offset: 0x0032C13A
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.equipUID);
			stream.PutOrGet(ref this.equipOptionID);
		}

		// Token: 0x04008A6F RID: 35439
		public long equipUID;

		// Token: 0x04008A70 RID: 35440
		public int equipOptionID = -1;
	}
}
