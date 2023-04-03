using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x0200102C RID: 4140
	[PacketId(ClientPacketId.kNKMPacket_MENTORING_SEASON_ID_NOT)]
	public sealed class NKMPacket_MENTORING_SEASON_ID_NOT : ISerializable
	{
		// Token: 0x06009B28 RID: 39720 RVA: 0x00332462 File Offset: 0x00330662
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.seasonId);
		}

		// Token: 0x04008E79 RID: 36473
		public int seasonId;
	}
}
