using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x02001049 RID: 4169
	public sealed class SurveyInfo : ISerializable
	{
		// Token: 0x06009B52 RID: 39762 RVA: 0x00332C2F File Offset: 0x00330E2F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.surveyId);
			stream.PutOrGet(ref this.userLevel);
			stream.PutOrGet(ref this.startDate);
			stream.PutOrGet(ref this.endDate);
		}

		// Token: 0x04008F09 RID: 36617
		public long surveyId;

		// Token: 0x04008F0A RID: 36618
		public int userLevel;

		// Token: 0x04008F0B RID: 36619
		public DateTime startDate;

		// Token: 0x04008F0C RID: 36620
		public DateTime endDate;
	}
}
