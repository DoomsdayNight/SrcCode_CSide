using System;
using System.Collections.Generic;
using NKM;

namespace NKC
{
	// Token: 0x020006F9 RID: 1785
	public static class NKCVoiceTimingManager
	{
		// Token: 0x060045E1 RID: 17889 RVA: 0x001534C4 File Offset: 0x001516C4
		public static void LoadFromLua(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) && nkmlua.OpenTable("UNIT_VOICE_TEMPLET"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKCVoiceTimingTemplet nkcvoiceTimingTemplet = new NKCVoiceTimingTemplet();
					nkcvoiceTimingTemplet.LoadLUA(nkmlua);
					if (!NKCVoiceTimingManager.unitVoiceTimingData.ContainsKey(nkcvoiceTimingTemplet.UnitId))
					{
						NKCVoiceTimingManager.unitVoiceTimingData.Add(nkcvoiceTimingTemplet.UnitId, new Dictionary<VOICE_TYPE, List<NKCVoiceTimingTemplet>>());
					}
					if (!NKCVoiceTimingManager.unitVoiceTimingData[nkcvoiceTimingTemplet.UnitId].ContainsKey(nkcvoiceTimingTemplet.VoiceType))
					{
						NKCVoiceTimingManager.unitVoiceTimingData[nkcvoiceTimingTemplet.UnitId].Add(nkcvoiceTimingTemplet.VoiceType, new List<NKCVoiceTimingTemplet>());
					}
					NKCVoiceTimingManager.unitVoiceTimingData[nkcvoiceTimingTemplet.UnitId][nkcvoiceTimingTemplet.VoiceType].Add(nkcvoiceTimingTemplet);
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
		}

		// Token: 0x060045E2 RID: 17890 RVA: 0x001535B8 File Offset: 0x001517B8
		public static float GetDelayTime(int unitId, int skinId, NKCVoiceTemplet voiceTemplet)
		{
			if (!NKCVoiceTimingManager.unitVoiceTimingData.ContainsKey(unitId))
			{
				return 0f;
			}
			if (!NKCVoiceTimingManager.unitVoiceTimingData[unitId].ContainsKey(voiceTemplet.Type))
			{
				return 0f;
			}
			NKCVoiceTimingTemplet nkcvoiceTimingTemplet = NKCVoiceTimingManager.unitVoiceTimingData[unitId][voiceTemplet.Type].Find((NKCVoiceTimingTemplet e) => e.FileName == voiceTemplet.FileName && e.SkinId == skinId);
			if (nkcvoiceTimingTemplet == null)
			{
				return 0f;
			}
			return nkcvoiceTimingTemplet.VoiceStartTime;
		}

		// Token: 0x04003747 RID: 14151
		private static Dictionary<int, Dictionary<VOICE_TYPE, List<NKCVoiceTimingTemplet>>> unitVoiceTimingData = new Dictionary<int, Dictionary<VOICE_TYPE, List<NKCVoiceTimingTemplet>>>();
	}
}
