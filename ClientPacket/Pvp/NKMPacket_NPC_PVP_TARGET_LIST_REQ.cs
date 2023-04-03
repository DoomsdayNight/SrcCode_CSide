using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DDF RID: 3551
	[PacketId(ClientPacketId.kNKMPacket_NPC_PVP_TARGET_LIST_REQ)]
	public sealed class NKMPacket_NPC_PVP_TARGET_LIST_REQ : ISerializable
	{
		// Token: 0x060096B5 RID: 38581 RVA: 0x0032BD4D File Offset: 0x00329F4D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.targetTier);
		}

		// Token: 0x0400889C RID: 34972
		public int targetTier;
	}
}
