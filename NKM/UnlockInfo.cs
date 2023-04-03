using System;
using System.Collections.Generic;
using Cs.Logging;

namespace NKM
{
	// Token: 0x020003BA RID: 954
	public readonly struct UnlockInfo
	{
		// Token: 0x060018FC RID: 6396 RVA: 0x0006666C File Offset: 0x0006486C
		public UnlockInfo(STAGE_UNLOCK_REQ_TYPE reqType, int reqValue)
		{
			this.eReqType = reqType;
			this.reqValue = reqValue;
			this.reqValueStr = string.Empty;
			this.reqDateTime = DateTime.MinValue;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00066692 File Offset: 0x00064892
		public UnlockInfo(STAGE_UNLOCK_REQ_TYPE reqType, int reqValue, string reqValueStr)
		{
			this.eReqType = reqType;
			this.reqValue = reqValue;
			this.reqValueStr = reqValueStr;
			this.reqDateTime = DateTime.MinValue;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x000666B4 File Offset: 0x000648B4
		public UnlockInfo(STAGE_UNLOCK_REQ_TYPE reqType, int reqValue, DateTime reqDateTime)
		{
			this.eReqType = reqType;
			this.reqValue = reqValue;
			this.reqValueStr = string.Empty;
			this.reqDateTime = reqDateTime;
		}

		// Token: 0x060018FF RID: 6399 RVA: 0x000666D6 File Offset: 0x000648D6
		public UnlockInfo(STAGE_UNLOCK_REQ_TYPE reqType, int reqValue, string reqValueStr, DateTime reqDateTime)
		{
			this.eReqType = reqType;
			this.reqValue = reqValue;
			this.reqValueStr = reqValueStr;
			this.reqDateTime = reqDateTime;
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x000666F8 File Offset: 0x000648F8
		public static UnlockInfo LoadFromLua(NKMLua lua, bool nullable = true)
		{
			STAGE_UNLOCK_REQ_TYPE stage_UNLOCK_REQ_TYPE = STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED;
			if (!lua.GetData<STAGE_UNLOCK_REQ_TYPE>("m_UnlockReqType", ref stage_UNLOCK_REQ_TYPE))
			{
				if (!nullable)
				{
					Log.ErrorAndExit("invalid m_UnlockReqType", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKM/NKMContentUnlockManager.cs", 135);
				}
				return new UnlockInfo(stage_UNLOCK_REQ_TYPE, 0);
			}
			int num = 0;
			lua.GetData("m_UnlockReqValue", ref num);
			string s = "";
			if (UnlockInfo.IsDateTimeData(stage_UNLOCK_REQ_TYPE))
			{
				lua.GetData("m_UnlockReqValueStr", ref s);
				DateTime dateTime;
				DateTime.TryParse(s, out dateTime);
				return new UnlockInfo(stage_UNLOCK_REQ_TYPE, num, dateTime);
			}
			lua.GetData("m_UnlockReqValueStr", ref s);
			if (stage_UNLOCK_REQ_TYPE == STAGE_UNLOCK_REQ_TYPE.SURT_UNLOCK_STAGE)
			{
				lua.GetData("m_ContentsValue", ref num);
			}
			return new UnlockInfo(stage_UNLOCK_REQ_TYPE, num, s);
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x000667A0 File Offset: 0x000649A0
		public static List<UnlockInfo> LoadFromLua2(NKMLua lua)
		{
			List<UnlockInfo> list = new List<UnlockInfo>();
			int num = 1;
			for (;;)
			{
				STAGE_UNLOCK_REQ_TYPE stage_UNLOCK_REQ_TYPE = STAGE_UNLOCK_REQ_TYPE.SURT_ALWAYS_UNLOCKED;
				if (!lua.GetData<STAGE_UNLOCK_REQ_TYPE>(string.Format("m_UnlockReqType{0}", num), ref stage_UNLOCK_REQ_TYPE))
				{
					break;
				}
				int num2 = 0;
				lua.GetData(string.Format("m_UnlockReqValue{0}", num), ref num2);
				string s = "";
				if (UnlockInfo.IsDateTimeData(stage_UNLOCK_REQ_TYPE))
				{
					lua.GetData(string.Format("m_UnlockReqValueStr{0}", num), ref s);
					DateTime dateTime;
					DateTime.TryParse(s, out dateTime);
					list.Add(new UnlockInfo(stage_UNLOCK_REQ_TYPE, num2, dateTime));
				}
				else
				{
					lua.GetData("m_UnlockReqValueStr", ref s);
					list.Add(new UnlockInfo(stage_UNLOCK_REQ_TYPE, num2, s));
				}
				num++;
			}
			return list;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0006685A File Offset: 0x00064A5A
		public static bool IsDateTimeData(STAGE_UNLOCK_REQ_TYPE type)
		{
			return type - STAGE_UNLOCK_REQ_TYPE.SURT_START_DATETIME <= 2 || type == STAGE_UNLOCK_REQ_TYPE.SURT_REGISTER_DATE;
		}

		// Token: 0x04001188 RID: 4488
		public readonly STAGE_UNLOCK_REQ_TYPE eReqType;

		// Token: 0x04001189 RID: 4489
		public readonly int reqValue;

		// Token: 0x0400118A RID: 4490
		public readonly string reqValueStr;

		// Token: 0x0400118B RID: 4491
		public readonly DateTime reqDateTime;
	}
}
