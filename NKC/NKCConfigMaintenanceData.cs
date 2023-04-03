using System;
using System.Collections.Generic;
using System.Linq;
using Cs.Logging;
using NKC.Localization;
using SimpleJSON;
using UnityEngine;

namespace NKC
{
	// Token: 0x02000738 RID: 1848
	public class NKCConfigMaintenanceData
	{
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x060049D2 RID: 18898 RVA: 0x00162764 File Offset: 0x00160964
		// (set) Token: 0x060049D1 RID: 18897 RVA: 0x0016275B File Offset: 0x0016095B
		public bool UseMaintenance { get; private set; }

		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x060049D4 RID: 18900 RVA: 0x00162775 File Offset: 0x00160975
		// (set) Token: 0x060049D3 RID: 18899 RVA: 0x0016276C File Offset: 0x0016096C
		private string DefaultDescription { get; set; } = "Maintenance";

		// Token: 0x060049D5 RID: 18901 RVA: 0x0016277D File Offset: 0x0016097D
		private void Add(string key, string value)
		{
			Debug.Log("[ConfigMaintenance] key: " + key + " / value : " + value);
			if (this._descriptions.ContainsKey(key))
			{
				return;
			}
			this._descriptions[key] = value;
		}

		// Token: 0x060049D6 RID: 18902 RVA: 0x001627B4 File Offset: 0x001609B4
		public string GetDescription()
		{
			NKM_NATIONAL_CODE languageCode = NKCGameOptionData.LoadLanguageCode(NKM_NATIONAL_CODE.NNC_KOREA);
			Debug.Log("[GetServerMaintenanceString] : " + languageCode.ToString());
			string key2 = NKCLocalization.s_dicLanguageTag.FirstOrDefault((KeyValuePair<string, NKM_NATIONAL_CODE> key) => key.Value == languageCode).Key;
			if (string.IsNullOrEmpty(key2))
			{
				Debug.Log(string.Format("[ConfigMaintenance] Not found description in NKCLocalization languageTag Dic _ languageCode : {0}", languageCode));
				return this.DefaultDescription;
			}
			string result;
			if (!this._descriptions.TryGetValue(key2, out result))
			{
				Debug.Log(string.Format("[ConfigMaintenance] Not found description in config descriptions _ languageCode : {0} _ languageCodeKey : {1}", languageCode, key2));
				return this.DefaultDescription;
			}
			return result;
		}

		// Token: 0x060049D7 RID: 18903 RVA: 0x00162870 File Offset: 0x00160A70
		public void SetDescription(JSONArray defaultTagSetArray, JSONNode maintenanceNode)
		{
			if (maintenanceNode["Use"] == null)
			{
				this.UseMaintenance = false;
				return;
			}
			this.UseMaintenance = maintenanceNode["Use"].AsBool;
			if (!this.UseMaintenance)
			{
				return;
			}
			if (maintenanceNode["Description"] == null)
			{
				return;
			}
			JSONNode jsonnode = maintenanceNode["Description"]["LANGUAGE_DEF"];
			if (jsonnode == null)
			{
				Log.Warn("[ConfigMaintenance] Config default language is empty", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/Maintenance/NKCConfigMaintenanceData.cs", 76);
			}
			else
			{
				this.DefaultDescription = jsonnode.ToString().Trim(new char[]
				{
					'"'
				});
			}
			foreach (object obj in defaultTagSetArray)
			{
				if (obj != null)
				{
					string text = obj.ToString();
					if (!string.IsNullOrEmpty(text))
					{
						string aKey = text.Trim(new char[]
						{
							'"'
						});
						JSONNode jsonnode2 = maintenanceNode["Description"][aKey];
						if (!(jsonnode2 == null))
						{
							Debug.Log(string.Format("[ConfigMaintenance] Add _ Key : {0}, Value : {1}", text, jsonnode2));
							this.Add(text, jsonnode2.ToString().Trim(new char[]
							{
								'"'
							}));
						}
					}
				}
			}
		}

		// Token: 0x040038B1 RID: 14513
		private readonly Dictionary<string, string> _descriptions = new Dictionary<string, string>();

		// Token: 0x040038B4 RID: 14516
		private const string LANGUAGE_DEF = "LANGUAGE_DEF";

		// Token: 0x040038B5 RID: 14517
		private const string Description = "Description";

		// Token: 0x040038B6 RID: 14518
		private const string Use = "Use";
	}
}
