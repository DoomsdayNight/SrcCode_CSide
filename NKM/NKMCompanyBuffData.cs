using System;
using Cs.Protocol;
using NKM.Templet;

namespace NKM
{
	// Token: 0x020003B5 RID: 949
	public class NKMCompanyBuffData : ISerializable
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060018EE RID: 6382 RVA: 0x00066402 File Offset: 0x00064602
		public int Id
		{
			get
			{
				return this.companyBuffId;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060018EF RID: 6383 RVA: 0x0006640A File Offset: 0x0006460A
		public long ExpireTicks
		{
			get
			{
				return this.expireTicks;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060018F0 RID: 6384 RVA: 0x00066412 File Offset: 0x00064612
		public DateTime ExpireDate
		{
			get
			{
				return new DateTime(this.expireTicks, DateTimeKind.Utc);
			}
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x00066420 File Offset: 0x00064620
		public NKMCompanyBuffData()
		{
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00066428 File Offset: 0x00064628
		public NKMCompanyBuffData(NKMCompanyBuffTemplet templet, DateTime current)
		{
			this.companyBuffId = templet.m_CompanyBuffID;
			this.expireTicks = current.AddMinutes((double)templet.m_CompanyBuffTime).Ticks;
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x00066463 File Offset: 0x00064663
		public NKMCompanyBuffData(int companyBuffId, long expireTicks)
		{
			this.companyBuffId = companyBuffId;
			this.expireTicks = expireTicks;
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x0006647C File Offset: 0x0006467C
		public void UpdateExpireTicksAsMinutes(int minutes)
		{
			DateTime dateTime = new DateTime(this.ExpireTicks, DateTimeKind.Utc);
			this.expireTicks = dateTime.AddMinutes((double)minutes).Ticks;
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x000664AE File Offset: 0x000646AE
		public void SetExpireTicks(long expireTicks)
		{
			this.expireTicks = expireTicks;
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x000664B7 File Offset: 0x000646B7
		public void Serialize(IPacketStream stream)
		{
			stream.PutOrGet(ref this.companyBuffId);
			stream.PutOrGet(ref this.expireTicks);
		}

		// Token: 0x060018F7 RID: 6391 RVA: 0x000664D1 File Offset: 0x000646D1
		public void SetExpireTime(DateTime expireTime)
		{
			this.expireTicks = expireTime.Ticks;
		}

		// Token: 0x04001150 RID: 4432
		private int companyBuffId;

		// Token: 0x04001151 RID: 4433
		private long expireTicks;
	}
}
