using System;
using NKM;
using NKM.Templet.Base;

namespace NKC.Templet
{
	// Token: 0x02000852 RID: 2130
	public class NKCLobbyIconTemplet : INKMTemplet
	{
		// Token: 0x17001023 RID: 4131
		// (get) Token: 0x060054AB RID: 21675 RVA: 0x0019C9B8 File Offset: 0x0019ABB8
		public DateTime m_StartTimeUTC
		{
			get
			{
				return NKMTime.LocalToUTC(this.m_StartTime, 0);
			}
		}

		// Token: 0x17001024 RID: 4132
		// (get) Token: 0x060054AC RID: 21676 RVA: 0x0019C9C6 File Offset: 0x0019ABC6
		public DateTime m_EndTimeUTC
		{
			get
			{
				return NKMTime.LocalToUTC(this.m_EndTime, 0);
			}
		}

		// Token: 0x17001025 RID: 4133
		// (get) Token: 0x060054AD RID: 21677 RVA: 0x0019C9D4 File Offset: 0x0019ABD4
		public int Key
		{
			get
			{
				return this.IDX;
			}
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x0019C9DC File Offset: 0x0019ABDC
		public static NKCLobbyIconTemplet Find(int idx)
		{
			return NKMTempletContainer<NKCLobbyIconTemplet>.Find(idx);
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x0019C9E4 File Offset: 0x0019ABE4
		public static NKCLobbyIconTemplet LoadFromLUA(NKMLua lua)
		{
			NKCLobbyIconTemplet nkclobbyIconTemplet = new NKCLobbyIconTemplet();
			bool flag = true & lua.GetData("IDX", ref nkclobbyIconTemplet.IDX) & lua.GetData<NKM_SHORTCUT_TYPE>("m_ShortCutType", ref nkclobbyIconTemplet.m_ShortCutType);
			lua.GetData("m_ShortCut", ref nkclobbyIconTemplet.m_shortCutParam);
			bool flag2 = flag & lua.GetData("m_LobbyIconName", ref nkclobbyIconTemplet.m_IconName);
			lua.GetData("m_LobbyIconDesc", ref nkclobbyIconTemplet.m_Desc);
			bool flag3 = flag2 & lua.GetData("m_StartTime", ref nkclobbyIconTemplet.m_StartTime) & lua.GetData("m_EndTime", ref nkclobbyIconTemplet.m_EndTime);
			lua.GetData("m_OrderList", ref nkclobbyIconTemplet.m_OrderList);
			lua.GetData<STAGE_UNLOCK_REQ_TYPE>("m_UnlockReqType", ref nkclobbyIconTemplet.m_UnlockReqType);
			lua.GetData("m_UnlockReqValue", ref nkclobbyIconTemplet.m_UnlockReqValue);
			if (!flag3)
			{
				return null;
			}
			return nkclobbyIconTemplet;
		}

		// Token: 0x17001026 RID: 4134
		// (get) Token: 0x060054B0 RID: 21680 RVA: 0x0019CAB1 File Offset: 0x0019ACB1
		public bool HasDateLimit
		{
			get
			{
				return this.m_StartTime.Ticks > 0L && this.m_EndTime.Ticks > 0L;
			}
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x0019CAD3 File Offset: 0x0019ACD3
		public void Join()
		{
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x0019CAD5 File Offset: 0x0019ACD5
		public void Validate()
		{
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x0019CAD7 File Offset: 0x0019ACD7
		public string GetDesc()
		{
			if (string.IsNullOrEmpty(this.m_Desc))
			{
				return string.Empty;
			}
			return NKCStringTable.GetString(this.m_Desc, false);
		}

		// Token: 0x0400439E RID: 17310
		public int IDX;

		// Token: 0x0400439F RID: 17311
		public NKM_SHORTCUT_TYPE m_ShortCutType;

		// Token: 0x040043A0 RID: 17312
		public string m_shortCutParam = string.Empty;

		// Token: 0x040043A1 RID: 17313
		public string m_IconName = string.Empty;

		// Token: 0x040043A2 RID: 17314
		public string m_Desc = string.Empty;

		// Token: 0x040043A3 RID: 17315
		public DateTime m_StartTime;

		// Token: 0x040043A4 RID: 17316
		public DateTime m_EndTime;

		// Token: 0x040043A5 RID: 17317
		public int m_OrderList;

		// Token: 0x040043A6 RID: 17318
		public STAGE_UNLOCK_REQ_TYPE m_UnlockReqType;

		// Token: 0x040043A7 RID: 17319
		public int m_UnlockReqValue;
	}
}
