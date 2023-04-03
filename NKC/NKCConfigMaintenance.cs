using System;
using System.Collections.Generic;
using Cs.Logging;
using SimpleJSON;

namespace NKC
{
	// Token: 0x02000737 RID: 1847
	public class NKCConfigMaintenance
	{
		// Token: 0x060049CD RID: 18893 RVA: 0x00162684 File Offset: 0x00160884
		public bool UseMaintenance(NKCConnectionInfo.LOGIN_SERVER_TYPE type)
		{
			NKCConfigMaintenanceData nkcconfigMaintenanceData;
			if (!this._configMaintenanceData.TryGetValue(type, out nkcconfigMaintenanceData))
			{
				Log.Warn(string.Format("[ConfigMaintenance][UseMaintenance] Invalid serverType _ type : {0}", type), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/Maintenance/NKCConfigMaintenance.cs", 15);
				return false;
			}
			return nkcconfigMaintenanceData.UseMaintenance;
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x001626C8 File Offset: 0x001608C8
		public string GetDescription(NKCConnectionInfo.LOGIN_SERVER_TYPE type)
		{
			NKCConfigMaintenanceData nkcconfigMaintenanceData;
			if (!this._configMaintenanceData.TryGetValue(type, out nkcconfigMaintenanceData))
			{
				Log.Warn(string.Format("[ConfigMaintenance][GetDescription] Invalid serverType _ type : {0}", type), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/Maintenance/NKCConfigMaintenance.cs", 26);
				return string.Empty;
			}
			return nkcconfigMaintenanceData.GetDescription();
		}

		// Token: 0x060049CF RID: 18895 RVA: 0x00162710 File Offset: 0x00160910
		public void SetDescription(NKCConnectionInfo.LOGIN_SERVER_TYPE type, JSONArray defaultTagSetArray, JSONNode maintenanceNode)
		{
			NKCConfigMaintenanceData nkcconfigMaintenanceData;
			if (!this._configMaintenanceData.TryGetValue(type, out nkcconfigMaintenanceData))
			{
				nkcconfigMaintenanceData = new NKCConfigMaintenanceData();
				this._configMaintenanceData.Add(type, nkcconfigMaintenanceData);
			}
			nkcconfigMaintenanceData.SetDescription(defaultTagSetArray, maintenanceNode);
		}

		// Token: 0x040038B0 RID: 14512
		private readonly Dictionary<NKCConnectionInfo.LOGIN_SERVER_TYPE, NKCConfigMaintenanceData> _configMaintenanceData = new Dictionary<NKCConnectionInfo.LOGIN_SERVER_TYPE, NKCConfigMaintenanceData>();
	}
}
