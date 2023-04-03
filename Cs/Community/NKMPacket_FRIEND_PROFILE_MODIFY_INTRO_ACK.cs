using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02000FE5 RID: 4069
	[PacketId(ClientPacketId.kNKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK)]
	public sealed class NKMPacket_FRIEND_PROFILE_MODIFY_INTRO_ACK : ISerializable
	{
		// Token: 0x06009A9A RID: 39578 RVA: 0x00331A34 File Offset: 0x0032FC34
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.intro);
		}

		// Token: 0x04008DF4 RID: 36340
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008DF5 RID: 36341
		public string intro;
	}
}
