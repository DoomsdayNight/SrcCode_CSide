using System;
using Cs.Protocol;

namespace ClientPacket.Warfare
{
	// Token: 0x02000C91 RID: 3217
	public sealed class WarfareUnitSyncData : ISerializable
	{
		// Token: 0x0600941F RID: 37919 RVA: 0x00328109 File Offset: 0x00326309
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.warfareGameUnitUID);
			stream.PutOrGet(ref this.hp);
			stream.PutOrGet(ref this.isTurnEnd);
			stream.PutOrGet(ref this.supply);
		}

		// Token: 0x0400853D RID: 34109
		public int warfareGameUnitUID;

		// Token: 0x0400853E RID: 34110
		public float hp;

		// Token: 0x0400853F RID: 34111
		public bool isTurnEnd;

		// Token: 0x04008540 RID: 34112
		public byte supply;
	}
}
