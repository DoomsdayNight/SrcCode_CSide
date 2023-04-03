using System;
using System.Collections.Generic;
using ClientPacket.Common;
using Cs.Protocol;
using Protocol;

namespace ClientPacket.Service
{
	// Token: 0x02000D4D RID: 3405
	[PacketId(ClientPacketId.kNKMPacket_SURVEY_UPSERT_NOT)]
	public sealed class NKMPacket_SURVEY_UPSERT_NOT : ISerializable
	{
		// Token: 0x06009597 RID: 38295 RVA: 0x0032A540 File Offset: 0x00328740
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet<SurveyInfo>(ref this.surveyInfos);
		}

		// Token: 0x04008743 RID: 34627
		public List<SurveyInfo> surveyInfos = new List<SurveyInfo>();
	}
}
