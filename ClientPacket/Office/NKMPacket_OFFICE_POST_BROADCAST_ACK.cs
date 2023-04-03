using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E0C RID: 3596
	[PacketId(ClientPacketId.kNKMPacket_OFFICE_POST_BROADCAST_ACK)]
	public sealed class NKMPacket_OFFICE_POST_BROADCAST_ACK : ISerializable
	{
		// Token: 0x0600970C RID: 38668 RVA: 0x0032C73D File Offset: 0x0032A93D
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
			stream.PutOrGet<NKMOfficePostState>(ref this.postState);
		}

		// Token: 0x04008922 RID: 35106
		public NKM_ERROR_CODE errorCode;

		// Token: 0x04008923 RID: 35107
		public NKMOfficePostState postState = new NKMOfficePostState();
	}
}
