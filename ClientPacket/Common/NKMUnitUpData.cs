using System;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200104C RID: 4172
	public sealed class NKMUnitUpData : ISerializable
	{
		// Token: 0x06009B58 RID: 39768 RVA: 0x00332D0F File Offset: 0x00330F0F
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.unitId);
			stream.PutOrGet(ref this.upLevel);
		}

		// Token: 0x04008F18 RID: 36632
		public int unitId;

		// Token: 0x04008F19 RID: 36633
		public byte upLevel;
	}
}
