using System;
using Cs.Protocol;
using NKM;

namespace ClientPacket.Game
{
	// Token: 0x02000F2B RID: 3883
	public sealed class NKMFierceResultData : ISerializable
	{
		// Token: 0x06009936 RID: 39222 RVA: 0x0032FA38 File Offset: 0x0032DC38
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.hpPercent);
			stream.PutOrGet(ref this.restTime);
			stream.PutOrGet(ref this.accquirePoint);
			stream.PutOrGet(ref this.bestPoint);
			stream.PutOrGet<NKMEventDeckData>(ref this.bestDeck);
		}

		// Token: 0x04008C14 RID: 35860
		public int hpPercent;

		// Token: 0x04008C15 RID: 35861
		public float restTime;

		// Token: 0x04008C16 RID: 35862
		public int accquirePoint;

		// Token: 0x04008C17 RID: 35863
		public int bestPoint;

		// Token: 0x04008C18 RID: 35864
		public NKMEventDeckData bestDeck;
	}
}
