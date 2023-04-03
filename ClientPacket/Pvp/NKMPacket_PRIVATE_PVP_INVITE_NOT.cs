using System;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D90 RID: 3472
	[PacketId(ClientPacketId.kNKMPacket_PRIVATE_PVP_INVITE_NOT)]
	public sealed class NKMPacket_PRIVATE_PVP_INVITE_NOT : ISerializable
	{
		// Token: 0x0600961B RID: 38427 RVA: 0x0032B3CC File Offset: 0x003295CC
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUserProfileData>(ref this.senderProfile);
			stream.PutOrGet(ref this.timeoutDurationSec);
			stream.PutOrGet<NKMPrivateGameConfig>(ref this.config);
		}

		// Token: 0x04008820 RID: 34848
		public NKMUserProfileData senderProfile = new NKMUserProfileData();

		// Token: 0x04008821 RID: 34849
		public int timeoutDurationSec;

		// Token: 0x04008822 RID: 34850
		public NKMPrivateGameConfig config = new NKMPrivateGameConfig();
	}
}
