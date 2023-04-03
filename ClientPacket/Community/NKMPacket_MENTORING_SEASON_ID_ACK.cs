using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200102A RID: 4138
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_SEASON_ID_ACK)]
	public sealed class NKMPacket_MENTORING_SEASON_ID_ACK : ISerializable
	{
		// Token: 0x06009B24 RID: 39716 RVA: 0x00332442 File Offset: 0x00330642
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.seasonId);
		}

		// Token: 0x04008E78 RID: 36472
		public int seasonId;
	}
}
