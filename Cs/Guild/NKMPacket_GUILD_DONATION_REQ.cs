using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F0C RID: 3852
	[PacketId(ClientPacketId.kNKMPacket_GUILD_DONATION_REQ)]
	public sealed class NKMPacket_GUILD_DONATION_REQ : ISerializable
	{
		// Token: 0x060098F8 RID: 39160 RVA: 0x0032F407 File Offset: 0x0032D607
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.guildUid);
			stream.PutOrGet(ref this.donationId);
		}

		// Token: 0x04008BB7 RID: 35767
		public long guildUid;

		// Token: 0x04008BB8 RID: 35768
		public int donationId;
	}
}
