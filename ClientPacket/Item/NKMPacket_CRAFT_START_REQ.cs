using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E91 RID: 3729
	[PacketId(ClientPacketId.kNKMPacket_CRAFT_START_REQ)]
	public sealed class NKMPacket_CRAFT_START_REQ : ISerializable
	{
		// Token: 0x0600980E RID: 38926 RVA: 0x0032DE3F File Offset: 0x0032C03F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.index);
			stream.PutOrGet(ref this.moldID);
			stream.PutOrGet(ref this.count);
		}

		// Token: 0x04008A60 RID: 35424
		public byte index;

		// Token: 0x04008A61 RID: 35425
		public int moldID;

		// Token: 0x04008A62 RID: 35426
		public int count;
	}
}
