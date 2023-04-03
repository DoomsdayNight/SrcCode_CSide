using System;
using System.Collections.Generic;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A82 RID: 2690
	public class NKCPopupSelectionConfirmShip : MonoBehaviour
	{
		// Token: 0x0600770E RID: 30478 RVA: 0x00279944 File Offset: 0x00277B44
		public void SetData(int shipID)
		{
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(shipID);
			if (unitTempletBase == null)
			{
				return;
			}
			this.m_slot.SetData(unitTempletBase, 1, false, null);
			this.SetShipStat(shipID);
			NKMUnitTempletBase maxGradeShipTemplet = NKMShipManager.GetMaxGradeShipTemplet(unitTempletBase);
			int skillCount = maxGradeShipTemplet.GetSkillCount();
			for (int i = 0; i < this.m_lstSkillBtn.Count; i++)
			{
				if (i < skillCount)
				{
					NKMShipSkillTemplet shipSkillTemplet = NKMShipSkillManager.GetShipSkillTempletByIndex(maxGradeShipTemplet, i);
					if (shipSkillTemplet != null)
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkillBtn[i], true);
						NKCUtil.SetImageSprite(this.m_lstSkillBtn[i].transform.GetComponent<Image>(), NKCUtil.GetSkillIconSprite(shipSkillTemplet), false);
						this.m_lstSkillBtn[i].PointerDown.RemoveAllListeners();
						this.m_lstSkillBtn[i].PointerDown.AddListener(delegate(PointerEventData e)
						{
							this.OnPointDownSkill(shipSkillTemplet, e);
						});
					}
					else
					{
						NKCUtil.SetGameobjectActive(this.m_lstSkillBtn[i], false);
					}
				}
				else
				{
					NKCUtil.SetGameobjectActive(this.m_lstSkillBtn[i], false);
				}
			}
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x00279A64 File Offset: 0x00277C64
		private void SetShipStat(int unitID)
		{
			NKMUnitStatTemplet unitStatTemplet = NKMUnitManager.GetUnitStatTemplet(unitID);
			NKMUnitData unitData = new NKMUnitData(unitID, (long)unitID, false, false, false, false);
			NKMStatData nkmstatData = new NKMStatData();
			nkmstatData.Init();
			nkmstatData.MakeBaseStat(null, false, unitData, unitStatTemplet.m_StatData, false, 0, null);
			NKCUtil.SetLabelText(this.m_lbHP, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_HP)).ToString());
			NKCUtil.SetLabelText(this.m_lbATK, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_ATK)).ToString());
			NKCUtil.SetLabelText(this.m_lbDEF, ((int)nkmstatData.GetStatBase(NKM_STAT_TYPE.NST_DEF)).ToString());
		}

		// Token: 0x06007710 RID: 30480 RVA: 0x00279AF4 File Offset: 0x00277CF4
		private void OnPointDownSkill(NKMShipSkillTemplet shipSkillTemplet, PointerEventData eventData)
		{
			if (shipSkillTemplet != null)
			{
				NKCUITooltip.Instance.Open(shipSkillTemplet, new Vector2?(eventData.position));
			}
		}

		// Token: 0x0400638F RID: 25487
		public NKCUIShipSelectListSlot m_slot;

		// Token: 0x04006390 RID: 25488
		public Text m_lbHP;

		// Token: 0x04006391 RID: 25489
		public Text m_lbATK;

		// Token: 0x04006392 RID: 25490
		public Text m_lbDEF;

		// Token: 0x04006393 RID: 25491
		public List<NKCUIComStateButton> m_lstSkillBtn = new List<NKCUIComStateButton>(3);
	}
}
