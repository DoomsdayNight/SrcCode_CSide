using System;
using ClientPacket.Common;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Pvp
{
	// Token: 0x02000D97 RID: 3479
	public sealed class NKMPvpGameLobbyUserState : ISerializable
	{
		// Token: 0x06009627 RID: 38439 RVA: 0x0032B4D2 File Offset: 0x003296D2
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMUserProfileData>(ref this.profileData);
			stream.PutOrGet(ref this.isReady);
			stream.PutOrGet(ref this.isHost);
			stream.PutOrGet<NKMDeckIndex>(ref this.deckIndex);
			stream.PutOrGet<NKMDummyDeckData>(ref this.deckData);
		}

		// Token: 0x04008838 RID: 34872
		public NKMUserProfileData profileData = new NKMUserProfileData();

		// Token: 0x04008839 RID: 34873
		public bool isReady;

		// Token: 0x0400883A RID: 34874
		public bool isHost;

		// Token: 0x0400883B RID: 34875
		public NKMDeckIndex deckIndex;

		// Token: 0x0400883C RID: 34876
		public NKMDummyDeckData deckData;
	}
}
