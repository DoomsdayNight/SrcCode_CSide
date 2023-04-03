using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E36 RID: 3638
	[PacketId(ClientPacketId.kNKMPacket_DIVE_MOVE_FORWARD_REQ)]
	public sealed class NKMPacket_DIVE_MOVE_FORWARD_REQ : ISerializable
	{
		// Token: 0x0600975C RID: 38748 RVA: 0x0032CE0C File Offset: 0x0032B00C
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.slotIndex);
		}

		// Token: 0x04008982 RID: 35202
		public int slotIndex;
	}
}
