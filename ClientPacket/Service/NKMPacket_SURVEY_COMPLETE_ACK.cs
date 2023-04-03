using System;
using Cs.Protocol;
using NKM;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D4F RID: 3407
	[PacketId(ClientPacketId.kNKMPacket_SURVEY_COMPLETE_ACK)]
	public sealed class NKMPacket_SURVEY_COMPLETE_ACK : ISerializable
	{
		// Token: 0x0600959B RID: 38299 RVA: 0x0032A577 File Offset: 0x00328777
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_ERROR_CODE>(ref this.errorCode);
		}

		// Token: 0x04008745 RID: 34629
		public NKM_ERROR_CODE errorCode;
	}
}
