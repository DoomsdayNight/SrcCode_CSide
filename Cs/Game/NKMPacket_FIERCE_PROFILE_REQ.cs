using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F5B RID: 3931
	[PacketId(ClientPacketId.kNKMPacket_FIERCE_PROFILE_REQ)]
	public sealed class NKMPacket_FIERCE_PROFILE_REQ : ISerializable
	{
		// Token: 0x06009996 RID: 39318 RVA: 0x00330357 File Offset: 0x0032E557
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.userUid);
			stream.PutOrGet(ref this.isForce);
		}

		// Token: 0x04008C9C RID: 35996
		public long userUid;

		// Token: 0x04008C9D RID: 35997
		public bool isForce;
	}
}
