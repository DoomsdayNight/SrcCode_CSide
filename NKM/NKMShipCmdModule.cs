using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000497 RID: 1175
	public class NKMShipCmdModule : ISerializable
	{
		// Token: 0x060020EB RID: 8427 RVA: 0x000A7BDD File Offset: 0x000A5DDD
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMShipCmdSlot>(ref this.slots);
		}

		// Token: 0x0400216F RID: 8559
		public NKMShipCmdSlot[] slots = new NKMShipCmdSlot[2];
	}
}
