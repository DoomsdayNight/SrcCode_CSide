using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000DD4 RID: 3540
	[PacketId(ClientPacketId.kNKMPacket_PVP_ROOM_INVITE_NOT)]
	public sealed class NKMPacket_PVP_ROOM_INVITE_NOT : ISerializable
	{
		// Token: 0x0600969F RID: 38559 RVA: 0x0032BBC3 File Offset: 0x00329DC3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUserProfileData>(ref this.senderProfile);
			stream.PutOrGet(ref this.timeoutDurationSec);
			stream.PutOrGet<NKMPrivateGameConfig>(ref this.config);
			stream.PutOrGetEnum<PvpPlayerRole>(ref this.preferRole);
		}

		// Token: 0x04008888 RID: 34952
		public NKMUserProfileData senderProfile = new NKMUserProfileData();

		// Token: 0x04008889 RID: 34953
		public int timeoutDurationSec;

		// Token: 0x0400888A RID: 34954
		public NKMPrivateGameConfig config = new NKMPrivateGameConfig();

		// Token: 0x0400888B RID: 34955
		public PvpPlayerRole preferRole;
	}
}
