using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Community
{
	// Token: 0x02001012 RID: 4114
	[PacketId(ClientPacketId.kNKMPacket_USER_PROFILE_CHANGE_FRAME_ACK)]
	public sealed class NKMPacket_USER_PROFILE_CHANGE_FRAME_ACK : ISerializable
	{
		// Token: 0x06009AF4 RID: 39668 RVA: 0x0033210F File Offset: 0x0033030F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet(ref this.selfiFrameId);
		}

		// Token: 0x04008E50 RID: 36432
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008E51 RID: 36433
		public int selfiFrameId;
	}
}
