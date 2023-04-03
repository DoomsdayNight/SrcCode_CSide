using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E32 RID: 3634
	[PacketId(ClientPacketId.kNKMPacket_COUNTERCASE_UNLOCK_REQ)]
	public sealed class NKMPacket_COUNTERCASE_UNLOCK_REQ : ISerializable
	{
		// Token: 0x06009754 RID: 38740 RVA: 0x0032CD3E File Offset: 0x0032AF3E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.dungeonID);
		}

		// Token: 0x04008976 RID: 35190
		public int dungeonID;
	}
}
