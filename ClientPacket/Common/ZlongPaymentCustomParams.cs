using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace ClientPacket.Common
{
	// Token: 0x0200104E RID: 4174
	public sealed class ZlongPaymentCustomParams : ISerializable
	{
		// Token: 0x06009B5C RID: 39772 RVA: 0x00332D6B File Offset: 0x00330F6B
		void ISerializable.Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.selectIndices);
		}

		// Token: 0x04008F1E RID: 36638
		public List<int> selectIndices = new List<int>();
	}
}
