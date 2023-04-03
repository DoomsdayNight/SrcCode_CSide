using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E95 RID: 3733
	[PacketId(ClientPacketId.kNKMPacket_CRAFT_INSTANT_COMPLETE_REQ)]
	public sealed class NKMPacket_CRAFT_INSTANT_COMPLETE_REQ : ISerializable
	{
		// Token: 0x06009816 RID: 38934 RVA: 0x0032DEEA File Offset: 0x0032C0EA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.index);
		}

		// Token: 0x04008A6A RID: 35434
		public byte index;
	}
}
