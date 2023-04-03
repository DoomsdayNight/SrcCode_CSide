using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE3 RID: 4067
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK)]
	public sealed class NKMPacket_FRIEND_PROFILE_MODIFY_MAIN_CHAR_ACK : ISerializable
	{
		// Token: 0x06009A96 RID: 39574 RVA: 0x003319F0 File Offset: 0x0032FBF0
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.mainCharId);
			stream.PutOrGet(ref this.mainCharSkinId);
		}

		// Token: 0x04008DF0 RID: 36336
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DF1 RID: 36337
		public int mainCharId;

		// Token: 0x04008DF2 RID: 36338
		public int mainCharSkinId;
	}
}
