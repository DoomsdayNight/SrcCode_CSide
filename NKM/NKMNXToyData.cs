using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000391 RID: 913
	public sealed class NKMNXToyData : ISerializable
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x0005EE67 File Offset: 0x0005D067
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_Npsn);
			stream.PutOrGet(ref this.m_NpToken);
			stream.PutOrGet(ref this.m_NpaCode);
			stream.PutOrGet(ref this.m_NexonSn);
		}

		// Token: 0x04000FE2 RID: 4066
		public long m_Npsn;

		// Token: 0x04000FE3 RID: 4067
		public string m_NpToken;

		// Token: 0x04000FE4 RID: 4068
		public string m_NpaCode;

		// Token: 0x04000FE5 RID: 4069
		public long m_NexonSn;
	}
}
