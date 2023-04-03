using System;
using System.Collections.Generic;
using NKC.UI.NPC;
using NKC.UI.Office;
using NKM;
using UnityEngine;

namespace NKC.Office
{
	// Token: 0x0200082C RID: 2092
	public class NKCOfficeCharacterNPCLobby : NKCOfficeCharacterNPC
	{
		// Token: 0x06005340 RID: 21312 RVA: 0x001960A5 File Offset: 0x001942A5
		private void SetBizcardEnable(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objBizcardEnable, value);
			NKCUtil.SetGameobjectActive(this.m_objBizcardDisable, !value);
		}

		// Token: 0x06005341 RID: 21313 RVA: 0x001960C2 File Offset: 0x001942C2
		public override void Init(NKCOfficeBuildingBase officeBuilding)
		{
			base.Init(officeBuilding);
			if (NKCUINPCBase.GetNPCTempletDic(this.m_eNPCType).Count == 0)
			{
				this.LoadFromLua();
			}
		}

		// Token: 0x06005342 RID: 21314 RVA: 0x001960E4 File Offset: 0x001942E4
		protected void LoadFromLua()
		{
			Dictionary<NPC_ACTION_TYPE, List<NKCNPCTemplet>> npctempletDic = NKCUINPCBase.GetNPCTempletDic(this.m_eNPCType);
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT_NPC", "LUA_NPC_MANAGER_KIMHANA_TEMPLET", true))
			{
				if (nkmlua.OpenTable("m_dicNPCTemplet"))
				{
					int num = 1;
					while (nkmlua.OpenTable(num))
					{
						NKCNPCTemplet nkcnpctemplet = new NKCNPCTemplet();
						if (nkcnpctemplet.LoadLUA(nkmlua))
						{
							if (!npctempletDic.ContainsKey(nkcnpctemplet.m_ActionType))
							{
								npctempletDic.Add(nkcnpctemplet.m_ActionType, new List<NKCNPCTemplet>());
							}
							npctempletDic[nkcnpctemplet.m_ActionType].Add(nkcnpctemplet);
						}
						num++;
						nkmlua.CloseTable();
					}
					nkmlua.CloseTable();
				}
				nkmlua.LuaClose();
			}
		}

		// Token: 0x06005343 RID: 21315 RVA: 0x00196188 File Offset: 0x00194388
		protected override bool OnTouchAction()
		{
			if (NKCScenManager.CurrentUserData().OfficeData.BizcardCount > 0)
			{
				NKCUIPopupOfficeInteract.Instance.Open();
				return true;
			}
			return false;
		}

		// Token: 0x06005344 RID: 21316 RVA: 0x001961AC File Offset: 0x001943AC
		protected override void Update()
		{
			base.Update();
			this.SetBizcardEnable(NKCScenManager.CurrentUserData().OfficeData.BizcardCount > 0);
			if (this.m_objBizcardRoot.transform.lossyScale.x < 0f)
			{
				this.m_objBizcardRoot.transform.localScale = new Vector3(-this.m_objBizcardRoot.transform.localScale.x, this.m_objBizcardRoot.transform.localScale.y, this.m_objBizcardRoot.transform.localScale.z);
			}
		}

		// Token: 0x040042C3 RID: 17091
		public GameObject m_objBizcardRoot;

		// Token: 0x040042C4 RID: 17092
		public GameObject m_objBizcardEnable;

		// Token: 0x040042C5 RID: 17093
		public GameObject m_objBizcardDisable;
	}
}
