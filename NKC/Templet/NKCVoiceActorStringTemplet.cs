using System;
using System.Collections.Generic;
using NKM;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC.Templet
{
	// Token: 0x0200085E RID: 2142
	public class NKCVoiceActorStringTemplet
	{
		// Token: 0x0600550F RID: 21775 RVA: 0x0019DE58 File Offset: 0x0019C058
		public static void LoadFromLua()
		{
			string[] array = new string[]
			{
				"LUA_STRING_VOICE_ACTOR_NAME_KOR",
				"LUA_STRING_VOICE_ACTOR_NAME_JPN",
				"LUA_STRING_VOICE_ACTOR_NAME_CHN"
			};
			NKCVoiceActorStringTemplet.voiceActorStringList.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				IEnumerable<NKCVoiceActorStringTemplet> enumerable = NKMTempletLoader.LoadCommonPath<NKCVoiceActorStringTemplet>("AB_SCRIPT", array[i], "STRING_VOICE_ACTOR_NAME", new Func<NKMLua, NKCVoiceActorStringTemplet>(NKCVoiceActorStringTemplet.LoadFromLua));
				if (enumerable != null)
				{
					foreach (NKCVoiceActorStringTemplet nkcvoiceActorStringTemplet in enumerable)
					{
						if (nkcvoiceActorStringTemplet != null)
						{
							if (!NKCVoiceActorStringTemplet.voiceActorStringList.ContainsKey(nkcvoiceActorStringTemplet.strKey))
							{
								NKCVoiceActorStringTemplet.voiceActorStringList.Add(nkcvoiceActorStringTemplet.strKey, new Dictionary<NKM_NATIONAL_CODE, string>());
								foreach (object obj in Enum.GetValues(typeof(NKM_NATIONAL_CODE)))
								{
									NKM_NATIONAL_CODE key = (NKM_NATIONAL_CODE)obj;
									switch (key)
									{
									case NKM_NATIONAL_CODE.NNC_KOREA:
										NKCVoiceActorStringTemplet.voiceActorStringList[nkcvoiceActorStringTemplet.strKey].Add(key, nkcvoiceActorStringTemplet.strValueKOR);
										continue;
									case NKM_NATIONAL_CODE.NNC_JAPAN:
										NKCVoiceActorStringTemplet.voiceActorStringList[nkcvoiceActorStringTemplet.strKey].Add(key, nkcvoiceActorStringTemplet.strValueJPN);
										continue;
									case NKM_NATIONAL_CODE.NNC_CENSORED_CHINESE:
									case NKM_NATIONAL_CODE.NNC_SIMPLIFIED_CHINESE:
										NKCVoiceActorStringTemplet.voiceActorStringList[nkcvoiceActorStringTemplet.strKey].Add(key, nkcvoiceActorStringTemplet.strValueCHN);
										continue;
									}
									NKCVoiceActorStringTemplet.voiceActorStringList[nkcvoiceActorStringTemplet.strKey].Add(key, nkcvoiceActorStringTemplet.strValueENG);
								}
							}
							else
							{
								Debug.LogError("StringKey: " + nkcvoiceActorStringTemplet.strKey + " already exist in LUA_STRING_VOICE_ACTOR_NAME_TEMPLET");
							}
						}
					}
				}
			}
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x0019E064 File Offset: 0x0019C264
		private static NKCVoiceActorStringTemplet LoadFromLua(NKMLua cNKMLua)
		{
			if (!NKMContentsVersionManager.CheckContentsVersion(cNKMLua, "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Templet/NKCVoiceActorNameTemplet.cs", 259))
			{
				return null;
			}
			NKCVoiceActorStringTemplet nkcvoiceActorStringTemplet = new NKCVoiceActorStringTemplet();
			bool flag = true & cNKMLua.GetData("StringKey", ref nkcvoiceActorStringTemplet.strKey);
			cNKMLua.GetData("StringValue_KOR", ref nkcvoiceActorStringTemplet.strValueKOR);
			cNKMLua.GetData("StringValue_ENG", ref nkcvoiceActorStringTemplet.strValueENG);
			cNKMLua.GetData("StringValue_JPN", ref nkcvoiceActorStringTemplet.strValueJPN);
			cNKMLua.GetData("StringValue_CHN", ref nkcvoiceActorStringTemplet.strValueCHN);
			if (!flag)
			{
				return null;
			}
			return nkcvoiceActorStringTemplet;
		}

		// Token: 0x06005511 RID: 21777 RVA: 0x0019E0EC File Offset: 0x0019C2EC
		public static string FindVoiceActorName(string actorKey)
		{
			if (!NKCVoiceActorStringTemplet.voiceActorStringList.ContainsKey(actorKey))
			{
				return " - ";
			}
			NKM_NATIONAL_CODE key = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_KOREA);
			if (!NKCVoiceActorStringTemplet.voiceActorStringList[actorKey].ContainsKey(key))
			{
				return " - ";
			}
			if (string.IsNullOrEmpty(NKCVoiceActorStringTemplet.voiceActorStringList[actorKey][key]))
			{
				return " - ";
			}
			return NKCVoiceActorStringTemplet.voiceActorStringList[actorKey][key];
		}

		// Token: 0x040043EE RID: 17390
		private string strKey;

		// Token: 0x040043EF RID: 17391
		private string strValueKOR;

		// Token: 0x040043F0 RID: 17392
		private string strValueENG;

		// Token: 0x040043F1 RID: 17393
		private string strValueJPN;

		// Token: 0x040043F2 RID: 17394
		private string strValueCHN;

		// Token: 0x040043F3 RID: 17395
		private static Dictionary<string, Dictionary<NKM_NATIONAL_CODE, string>> voiceActorStringList = new Dictionary<string, Dictionary<NKM_NATIONAL_CODE, string>>();
	}
}
