using System;
using System.Collections.Generic;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x02000458 RID: 1112
	public class NKMPostData : ISerializable
	{
		// Token: 0x06001E32 RID: 7730 RVA: 0x0008F95E File Offset: 0x0008DB5E
		public NKMPostData()
		{
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0008F971 File Offset: 0x0008DB71
		public NKMPostData(NKM_POST_TYPE type, string title, string contents, DateTime sendDate, DateTime expireDate, long postIndex)
		{
			this.postType = type;
			this.title = title;
			this.contents = contents;
			this.expirationDate = expireDate;
			this.sendDate = sendDate;
			this.postIndex = postIndex;
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0008F9B4 File Offset: 0x0008DBB4
		public NKMPostData(NKM_POST_TYPE type, string title, string contents, DateTime sendDate, TimeSpan lifeTime, long postIndex)
		{
			this.postType = type;
			this.title = title;
			this.contents = contents;
			this.sendDate = sendDate;
			this.expirationDate = sendDate + lifeTime;
			this.postIndex = postIndex;
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0008FA06 File Offset: 0x0008DC06
		public void InsertItem(NKMRewardInfo item)
		{
			this.items.Add(item);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0008FA14 File Offset: 0x0008DC14
		public void InsertItem(NKM_REWARD_TYPE itemType, int itemId, int count, NKM_ITEM_PAYMENT_TYPE paymentType)
		{
			this.items.Add(new NKMRewardInfo
			{
				rewardType = itemType,
				paymentType = paymentType,
				ID = itemId,
				Count = count
			});
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0008FA44 File Offset: 0x0008DC44
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGetEnum<NKM_POST_TYPE>(ref this.postType);
			stream.PutOrGet(ref this.postIndex);
			stream.PutOrGet(ref this.title);
			stream.PutOrGet(ref this.contents);
			stream.PutOrGet(ref this.sendDate);
			stream.PutOrGet<NKMRewardInfo>(ref this.items);
			stream.PutOrGet(ref this.expirationDate);
		}

		// Token: 0x04001EE3 RID: 7907
		public NKM_POST_TYPE postType;

		// Token: 0x04001EE4 RID: 7908
		public long postIndex;

		// Token: 0x04001EE5 RID: 7909
		public string title;

		// Token: 0x04001EE6 RID: 7910
		public string contents;

		// Token: 0x04001EE7 RID: 7911
		public DateTime sendDate;

		// Token: 0x04001EE8 RID: 7912
		public DateTime expirationDate;

		// Token: 0x04001EE9 RID: 7913
		public List<NKMRewardInfo> items = new List<NKMRewardInfo>();
	}
}
