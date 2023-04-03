using System;
using System.Runtime.Serialization;
using Cs.Protocol;

namespace NKM
{
	// Token: 0x020004F3 RID: 1267
	[DataContract]
	public class NKMItemMiscData : Cs.Protocol.ISerializable
	{
		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x000BA9FB File Offset: 0x000B8BFB
		// (set) Token: 0x060023C1 RID: 9153 RVA: 0x000BAA03 File Offset: 0x000B8C03
		[DataMember]
		public int ItemID
		{
			get
			{
				return this.m_ItemMiscID;
			}
			set
			{
				this.m_ItemMiscID = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000BAA0C File Offset: 0x000B8C0C
		// (set) Token: 0x060023C3 RID: 9155 RVA: 0x000BAA14 File Offset: 0x000B8C14
		[DataMember]
		public long CountFree
		{
			get
			{
				return this.m_CountFree;
			}
			set
			{
				this.m_CountFree = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060023C4 RID: 9156 RVA: 0x000BAA1D File Offset: 0x000B8C1D
		// (set) Token: 0x060023C5 RID: 9157 RVA: 0x000BAA25 File Offset: 0x000B8C25
		[DataMember]
		public long CountPaid
		{
			get
			{
				return this.m_CountPaid;
			}
			set
			{
				this.m_CountPaid = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000BAA2E File Offset: 0x000B8C2E
		[DataMember]
		public long TotalCount
		{
			get
			{
				return this.CountFree + this.CountPaid;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060023C7 RID: 9159 RVA: 0x000BAA3D File Offset: 0x000B8C3D
		public NKM_ITEM_PAYMENT_TYPE PaymentType
		{
			get
			{
				if (this.m_CountFree > 0L && this.m_CountPaid > 0L)
				{
					return NKM_ITEM_PAYMENT_TYPE.NIPT_BOTH;
				}
				if (this.m_CountPaid <= 0L)
				{
					return NKM_ITEM_PAYMENT_TYPE.NIPT_FREE;
				}
				return NKM_ITEM_PAYMENT_TYPE.NIPT_PAID;
			}
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000BAA62 File Offset: 0x000B8C62
		public NKMItemMiscData()
		{
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000BAA6A File Offset: 0x000B8C6A
		public NKMItemMiscData(int ItemID, long CountFree, long CountPaid = 0L, int bonusRatio = 0)
		{
			this.m_ItemMiscID = ItemID;
			this.m_CountFree = CountFree;
			this.m_CountPaid = CountPaid;
			this.BonusRatio = bonusRatio;
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000BAA8F File Offset: 0x000B8C8F
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.m_ItemMiscID);
			stream.PutOrGet(ref this.m_CountFree);
			stream.PutOrGet(ref this.m_CountPaid);
			stream.PutOrGet(ref this.BonusRatio);
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000BAAC1 File Offset: 0x000B8CC1
		public NKMItemMiscTemplet GetTemplet()
		{
			return NKMItemManager.GetItemMiscTempletByID(this.m_ItemMiscID);
		}

		// Token: 0x040025A9 RID: 9641
		public int BonusRatio;

		// Token: 0x040025AA RID: 9642
		private int m_ItemMiscID;

		// Token: 0x040025AB RID: 9643
		private long m_CountFree;

		// Token: 0x040025AC RID: 9644
		private long m_CountPaid;
	}
}
