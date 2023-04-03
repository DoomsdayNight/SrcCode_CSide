using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE4 RID: 4068
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_REQ)]
	public sealed class NKMPacket_FRIEND_PROFILE_MODIFY_INTRO_REQ : ISerializable
	{
		// Token: 0x06009A98 RID: 39576 RVA: 0x00331A1E File Offset: 0x0032FC1E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.intro);
		}

		// Token: 0x04008DF3 RID: 36339
		public string intro;
	}
}
