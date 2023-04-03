using System;
using Cs.Protocol;

namespace ClientPacket.Mode
{
	// Token: 0x02000E65 RID: 3685
	public sealed class NKMShortCutInfo : ISerializable
	{
		// Token: 0x060097BA RID: 38842 RVA: 0x0032D5B3 File Offset: 0x0032B7B3
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.gameType);
			stream.PutOrGet(ref this.stageId);
		}

		// Token: 0x040089E6 RID: 35302
		public int gameType;

		// Token: 0x040089E7 RID: 35303
		public int stageId;
	}
}
