using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FEA RID: 4074
	[PacketId(ClientPacketId.kNKMPacket_USER_PROFILE_INFO_REQ)]
	public sealed class NKMPacket_USER_PROFILE_INFO_REQ : ISerializable
	{
		// Token: 0x06009AA4 RID: 39588 RVA: 0x00331AEA File Offset: 0x0032FCEA
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUID);
			stream.PutOrGetEnum<NKM_DECK_TYPE>(ref this.deckType);
		}

		// Token: 0x04008DFF RID: 36351
		public long userUID;

		// Token: 0x04008E00 RID: 36352
		public NKM_DECK_TYPE deckType;
	}
}
