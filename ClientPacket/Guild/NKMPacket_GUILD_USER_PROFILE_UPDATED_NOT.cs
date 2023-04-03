using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Guild
{
	// Token: 0x02000F08 RID: 3848
	[PacketId(ClientPacketId.kNKMPacket_GUILD_USER_PROFILE_UPDATED_NOT)]
	public sealed class NKMPacket_GUILD_USER_PROFILE_UPDATED_NOT : ISerializable
	{
		// Token: 0x060098F0 RID: 39152 RVA: 0x0032F381 File Offset: 0x0032D581
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMCommonProfile>(ref this.commonProfile);
			stream.PutOrGet(ref this.lastOnlineTime);
		}

		// Token: 0x04008BB1 RID: 35761
		public NKMCommonProfile commonProfile = new NKMCommonProfile();

		// Token: 0x04008BB2 RID: 35762
		public DateTime lastOnlineTime;
	}
}
