using System;
using System.Collections.Generic;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x02000376 RID: 886
	public sealed class NKMDiveSlotSet : ISerializable
	{
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0005956D File Offset: 0x0005776D
		public List<NKMDiveSlot> Slots
		{
			get
			{
				return this.slots;
			}
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00059588 File Offset: 0x00057788
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet<NKMDiveSlot>(ref this.slots);
		}

		// Token: 0x04000EEB RID: 3819
		private List<NKMDiveSlot> slots = new List<NKMDiveSlot>();
	}
}
