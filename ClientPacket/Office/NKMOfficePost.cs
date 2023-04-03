using System;
using ClientPacket.Common;
using Cs.Protocol;

namespace ClientPacket.Office
{
	// Token: 0x02000E06 RID: 3590
	public sealed class NKMOfficePost : ISerializable
	{
		// Token: 0x06009700 RID: 38656 RVA: 0x0032C62E File Offset: 0x0032A82E
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.postUid);
			stream.PutOrGet<NKMCommonProfile>(ref this.senderProfile);
			stream.PutOrGet<NKMGuildSimpleData>(ref this.senderGuildData);
			stream.PutOrGet(ref this.expirationDate);
		}

		// Token: 0x04008915 RID: 35093
		public long postUid;

		// Token: 0x04008916 RID: 35094
		public NKMCommonProfile senderProfile = new NKMCommonProfile();

		// Token: 0x04008917 RID: 35095
		public NKMGuildSimpleData senderGuildData = new NKMGuildSimpleData();

		// Token: 0x04008918 RID: 35096
		public DateTime expirationDate;
	}
}
