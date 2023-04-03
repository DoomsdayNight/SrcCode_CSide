using System;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D4E RID: 3406
	[PacketId(ClientPacketId.kNKMPacket_SURVEY_COMPLETE_REQ)]
	public sealed class NKMPacket_SURVEY_COMPLETE_REQ : ISerializable
	{
		// Token: 0x06009599 RID: 38297 RVA: 0x0032A561 File Offset: 0x00328761
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.surveyId);
		}

		// Token: 0x04008744 RID: 34628
		public long surveyId;
	}
}
