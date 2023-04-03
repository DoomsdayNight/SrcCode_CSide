using System;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x0200043F RID: 1087
	public class NKMMissionData : ISerializable
	{
		// Token: 0x06001DAA RID: 7594 RVA: 0x0008D5B4 File Offset: 0x0008B7B4
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.tabId);
			stream.PutOrGet(ref this.mission_id);
			stream.PutOrGet(ref this.group_id);
			stream.PutOrGet(ref this.times);
			stream.PutOrGet(ref this.last_update_date);
			stream.PutOrGet(ref this.isComplete);
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x0008D609 File Offset: 0x0008B809
		public DateTime LastUpdateDate
		{
			get
			{
				return new DateTime(this.last_update_date);
			}
		}

		// Token: 0x04001DEB RID: 7659
		public int tabId;

		// Token: 0x04001DEC RID: 7660
		public int mission_id;

		// Token: 0x04001DED RID: 7661
		public int group_id;

		// Token: 0x04001DEE RID: 7662
		public long times;

		// Token: 0x04001DEF RID: 7663
		public long last_update_date;

		// Token: 0x04001DF0 RID: 7664
		public bool isComplete;

		// Token: 0x04001DF1 RID: 7665
		public bool isEnable = true;

		// Token: 0x04001DF2 RID: 7666
		public DateTime endDate;
	}
}
