using System;
using Cs.Protocol;

namespace ClientPacket.Game
{
	// Token: 0x02000F2A RID: 3882
	public sealed class NKMRaidBossResultData : ISerializable
	{
		// Token: 0x06009934 RID: 39220 RVA: 0x0032F9FE File Offset: 0x0032DBFE
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.initHp);
			stream.PutOrGet(ref this.curHP);
			stream.PutOrGet(ref this.maxHp);
			stream.PutOrGet(ref this.damage);
		}

		// Token: 0x04008C10 RID: 35856
		public float initHp;

		// Token: 0x04008C11 RID: 35857
		public float curHP;

		// Token: 0x04008C12 RID: 35858
		public float maxHp;

		// Token: 0x04008C13 RID: 35859
		public float damage;
	}
}
