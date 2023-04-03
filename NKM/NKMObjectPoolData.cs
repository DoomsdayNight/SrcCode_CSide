using System;

namespace NKM
{
	// Token: 0x02000442 RID: 1090
	public abstract class NKMObjectPoolData : IComparable<NKMObjectPoolData>
	{
		// Token: 0x06001DB1 RID: 7601 RVA: 0x0008D884 File Offset: 0x0008BA84
		public virtual void Load(bool bAsync)
		{
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x0008D886 File Offset: 0x0008BA86
		public virtual bool LoadComplete()
		{
			return true;
		}

		// Token: 0x06001DB3 RID: 7603 RVA: 0x0008D889 File Offset: 0x0008BA89
		public virtual void Open()
		{
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x0008D88B File Offset: 0x0008BA8B
		public virtual void Close()
		{
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0008D88D File Offset: 0x0008BA8D
		public virtual void Unload()
		{
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0008D88F File Offset: 0x0008BA8F
		public int CompareTo(NKMObjectPoolData other)
		{
			if (this.m_NKM_OBJECT_POOL_TYPE > other.m_NKM_OBJECT_POOL_TYPE)
			{
				return -1;
			}
			if (other.m_NKM_OBJECT_POOL_TYPE > this.m_NKM_OBJECT_POOL_TYPE)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x04001E0A RID: 7690
		public long m_ObjUID = NKMObjectPool.GetObjUID();

		// Token: 0x04001E0B RID: 7691
		public NKM_OBJECT_POOL_TYPE m_NKM_OBJECT_POOL_TYPE;

		// Token: 0x04001E0C RID: 7692
		public string m_ObjectPoolBundleName = "";

		// Token: 0x04001E0D RID: 7693
		public string m_ObjectPoolName = "";

		// Token: 0x04001E0E RID: 7694
		public bool m_bIsLoaded;

		// Token: 0x04001E0F RID: 7695
		public bool m_bUnloadable;
	}
}
