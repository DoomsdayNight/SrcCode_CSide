using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE2 RID: 4066
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_REQ)]
	public sealed class NKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_REQ : ISerializable
	{
		// Token: 0x06009A94 RID: 39572 RVA: 0x003319CE File Offset: 0x0032FBCE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.mainCharId);
			stream.PutOrGet(ref this.mainCharSkinId);
		}

		// Token: 0x04008DEE RID: 36334
		public int mainCharId;

		// Token: 0x04008DEF RID: 36335
		public int mainCharSkinId;
	}
}
