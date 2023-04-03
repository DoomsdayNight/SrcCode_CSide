using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC.Templet
{
	// Token: 0x0200085D RID: 2141
	public class NKCVoiceActorNameTemplet
	{
		// Token: 0x06005502 RID: 21762 RVA: 0x0019DAC0 File Offset: 0x0019BCC0
		public static void LoadFromLua()
		{
			NKCVoiceActorNameTemplet.Load("LUA_VOICE_ACTOR_NAME_TEMPLET_V2", NKCVoiceActorNameTemplet.voiceActorList_v2);
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x0019DAD4 File Offset: 0x0019BCD4
		private static void Load(string fileName, Dictionary<string, Dictionary<NKC_VOICE_CODE, string>> voiceActorList)
		{
			IEnumerable<NKCVoiceActorNameTemplet> enumerable = NKMTempletLoader.LoadCommonPath<NKCVoiceActorNameTemplet>("AB_SCRIPT", fileName, "VOICE_ACTOR_NAME_TEMPLET", new Func<NKMLua, NKCVoiceActorNameTemplet>(NKCVoiceActorNameTemplet.LoadFromLua));
			if (enumerable == null)
			{
				return;
			}
			voiceActorList.Clear();
			foreach (NKCVoiceActorNameTemplet nkcvoiceActorNameTemplet in enumerable)
			{
				if (nkcvoiceActorNameTemplet != null)
				{
					if (!voiceActorList.ContainsKey(nkcvoiceActorNameTemplet.voiceActorNameStrID))
					{
						voiceActorList.Add(nkcvoiceActorNameTemplet.voiceActorNameStrID, new Dictionary<NKC_VOICE_CODE, string>());
						voiceActorList[nkcvoiceActorNameTemplet.voiceActorNameStrID].Add(NKC_VOICE_CODE.NVC_KOR, nkcvoiceActorNameTemplet.actorNameVKOR);
						voiceActorList[nkcvoiceActorNameTemplet.voiceActorNameStrID].Add(NKC_VOICE_CODE.NVC_JPN, nkcvoiceActorNameTemplet.actorNameVJPN);
						voiceActorList[nkcvoiceActorNameTemplet.voiceActorNameStrID].Add(NKC_VOICE_CODE.NVC_CHN, nkcvoiceActorNameTemplet.actorNameVCHN);
					}
					else
					{
						Debug.LogError("voiceActorNameStrID: " + nkcvoiceActorNameTemplet.voiceActorNameStrID + " already exist in LUA_VOICE_ACTOR_NAME_TEMPLET");
					}
				}
			}
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x0019DBD0 File Offset: 0x0019BDD0
		private static NKCVoiceActorNameTemplet LoadFromLua(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCVoiceActorNameTemplet.cs", 56))
			{
				return null;
			}
			NKCVoiceActorNameTemplet nkcvoiceActorNameTemplet = new NKCVoiceActorNameTemplet();
			bool flag = true & cNKMLua.GetData("VOICE_ACTOR_NAME_StrID", ref nkcvoiceActorNameTemplet.voiceActorNameStrID);
			cNKMLua.GetData("Actor_Name_VKOR", ref nkcvoiceActorNameTemplet.actorNameVKOR);
			cNKMLua.GetData("Actor_Name_VJPN", ref nkcvoiceActorNameTemplet.actorNameVJPN);
			cNKMLua.GetData("Actor_Name_VCHN", ref nkcvoiceActorNameTemplet.actorNameVCHN);
			if (!flag)
			{
				return null;
			}
			return nkcvoiceActorNameTemplet;
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x0019DC44 File Offset: 0x0019BE44
		public static string FindActorName(string unitStrId, int skinId)
		{
			string text = null;
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(skinId);
			if (skinTemplet != null)
			{
				text = NKCVoiceActorNameTemplet.FindVoiceActorKey(skinTemplet.m_SkinStrID);
			}
			if (!string.IsNullOrEmpty(text))
			{
				return NKCVoiceActorStringTemplet.FindVoiceActorName(text);
			}
			text = NKCVoiceActorNameTemplet.FindVoiceActorKey(unitStrId);
			if (string.IsNullOrEmpty(text))
			{
				text = NKCVoiceActorNameTemplet.FindVoiceActorKeyFromBaseUnit(NKMUnitManager.GetUnitTempletBase(unitStrId));
			}
			if (string.IsNullOrEmpty(text))
			{
				return " - ";
			}
			return NKCVoiceActorStringTemplet.FindVoiceActorName(text);
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x0019DCA8 File Offset: 0x0019BEA8
		public static string FindActorName(NKCCollectionUnitTemplet collectionUnitTemplet)
		{
			if (collectionUnitTemplet == null)
			{
				return " - ";
			}
			string text = NKCVoiceActorNameTemplet.FindVoiceActorKey(collectionUnitTemplet.m_UnitStrID);
			if (string.IsNullOrEmpty(text))
			{
				text = NKCVoiceActorNameTemplet.FindVoiceActorKeyFromBaseUnit(NKMUnitManager.GetUnitTempletBase(collectionUnitTemplet.m_UnitStrID));
			}
			if (string.IsNullOrEmpty(text))
			{
				return " - ";
			}
			return NKCVoiceActorStringTemplet.FindVoiceActorName(text);
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0019DCF8 File Offset: 0x0019BEF8
		public static string FindActorName(NKMUnitTempletBase unitTempletBase)
		{
			if (unitTempletBase == null)
			{
				return " - ";
			}
			string text = NKCVoiceActorNameTemplet.FindVoiceActorKey(unitTempletBase.m_UnitStrID);
			if (string.IsNullOrEmpty(text))
			{
				text = NKCVoiceActorNameTemplet.FindVoiceActorKeyFromBaseUnit(unitTempletBase);
			}
			if (string.IsNullOrEmpty(text))
			{
				return " - ";
			}
			return NKCVoiceActorStringTemplet.FindVoiceActorName(text);
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x0019DD40 File Offset: 0x0019BF40
		public static string FindActorName(NKMUnitData unitData)
		{
			if (unitData == null)
			{
				return " - ";
			}
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData.m_UnitID);
			if (unitTempletBase == null)
			{
				return " - ";
			}
			string text = null;
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitData.m_SkinID);
			if (skinTemplet != null)
			{
				text = NKCVoiceActorNameTemplet.FindVoiceActorKey(skinTemplet.m_SkinStrID);
			}
			if (!string.IsNullOrEmpty(text))
			{
				return NKCVoiceActorStringTemplet.FindVoiceActorName(text);
			}
			text = NKCVoiceActorNameTemplet.FindVoiceActorKey(unitTempletBase.m_UnitStrID);
			if (string.IsNullOrEmpty(text))
			{
				text = NKCVoiceActorNameTemplet.FindVoiceActorKeyFromBaseUnit(unitTempletBase);
			}
			if (string.IsNullOrEmpty(text))
			{
				return " - ";
			}
			return NKCVoiceActorStringTemplet.FindVoiceActorName(text);
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x0019DDC6 File Offset: 0x0019BFC6
		private static string FindVoiceActorKey(string unitStrID)
		{
			if (!NKCVoiceActorNameTemplet.voiceActorList_v2.ContainsKey(unitStrID))
			{
				return null;
			}
			if (!NKCVoiceActorNameTemplet.voiceActorList_v2[unitStrID].ContainsKey(NKCUIVoiceManager.CurrentVoiceCode))
			{
				return null;
			}
			return NKCVoiceActorNameTemplet.voiceActorList_v2[unitStrID][NKCUIVoiceManager.CurrentVoiceCode];
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x0019DE08 File Offset: 0x0019C008
		private static string FindVoiceActorKeyFromBaseUnit(NKMUnitTempletBase unitTempletBase)
		{
			if (unitTempletBase != null && unitTempletBase.m_BaseUnitID > 0)
			{
				NKMUnitTempletBase unitTempletBase2 = NKMUnitManager.GetUnitTempletBase(unitTempletBase.m_BaseUnitID);
				if (unitTempletBase2 != null)
				{
					return NKCVoiceActorNameTemplet.FindVoiceActorKey(unitTempletBase2.m_UnitStrID);
				}
			}
			return null;
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x0019DE3D File Offset: 0x0019C03D
		public void Join()
		{
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x0019DE3F File Offset: 0x0019C03F
		public void Validate()
		{
		}

		// Token: 0x040043E9 RID: 17385
		private string voiceActorNameStrID;

		// Token: 0x040043EA RID: 17386
		private string actorNameVKOR;

		// Token: 0x040043EB RID: 17387
		private string actorNameVJPN;

		// Token: 0x040043EC RID: 17388
		private string actorNameVCHN;

		// Token: 0x040043ED RID: 17389
		private static Dictionary<string, Dictionary<NKC_VOICE_CODE, string>> voiceActorList_v2 = new Dictionary<string, Dictionary<NKC_VOICE_CODE, string>>();
	}
}
