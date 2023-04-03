using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.User
{
	// Token: 0x02000CB0 RID: 3248
	[PacketId(ClientPacketId.kNKMPacket_DECK_UNLOCK_REQ)]
	public sealed class NKMPacket_DECK_UNLOCK_REQ : ISerializable
	{
		// Token: 0x0600945D RID: 37981 RVA: 0x003289A0 File Offset: 0x00326BA0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_DECK_TYPE>(ref this.deckType);
		}

		// Token: 0x040085BD RID: 34237
		public NKM_DECK_TYPE deckType;
	}
}
