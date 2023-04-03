using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Item
{
	// Token: 0x02000E93 RID: 3731
	[PacketId(ClientPacketId.kNKMPacket_CRAFT_COMPLETE_REQ)]
	public sealed class NKMPacket_CRAFT_COMPLETE_REQ : ISerializable
	{
		// Token: 0x06009812 RID: 38930 RVA: 0x0032DEA6 File Offset: 0x0032C0A6
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.index);
		}

		// Token: 0x04008A66 RID: 35430
		public byte index;
	}
}
