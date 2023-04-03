using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000927 RID: 2343
	public class NKCUIChangeLobbyFaceSlot : MonoBehaviour
	{
		// Token: 0x06005DDA RID: 24026 RVA: 0x001CF92C File Offset: 0x001CDB2C
		public void Init(NKCUIComToggle.ValueChangedWithData onToggle, NKCUIComToggleGroup tglgrp)
		{
			if (this.m_Toggle != null)
			{
				this.m_Toggle.OnValueChangedWithData = onToggle;
				this.m_Toggle.SetToggleGroup(tglgrp);
			}
		}

		// Token: 0x06005DDB RID: 24027 RVA: 0x001CF954 File Offset: 0x001CDB54
		public void SetData(NKMUnitData unitData, NKMLobbyFaceTemplet templet)
		{
			if (templet == null)
			{
				return;
			}
			this.SetName(NKCStringTable.GetString(templet.strFaceName, false));
			NKMUnitTempletBase unitTempletBase = NKMUnitManager.GetUnitTempletBase(unitData);
			if (unitTempletBase != null && unitTempletBase.HasUnitStyleType(NKM_UNIT_STYLE_TYPE.NUST_TRAINER))
			{
				this.SetNoLoyality();
			}
			else
			{
				this.SetLoyality(templet.reqLoyalty);
			}
			this.m_Toggle.m_DataInt = templet.Key;
			this.m_Toggle.SetLock(!templet.CanUseFace(unitData), false);
		}

		// Token: 0x06005DDC RID: 24028 RVA: 0x001CF9C5 File Offset: 0x001CDBC5
		public void SetData(NKMOperator operatorData, NKMLobbyFaceTemplet templet)
		{
			if (templet == null)
			{
				return;
			}
			this.SetName(NKCStringTable.GetString(templet.strFaceName, false));
			this.SetNoLoyality();
			this.m_Toggle.m_DataInt = templet.Key;
			this.m_Toggle.UnLock(false);
		}

		// Token: 0x06005DDD RID: 24029 RVA: 0x001CFA00 File Offset: 0x001CDC00
		public void SetSelected(bool value)
		{
			this.m_Toggle.Select(value, true, false);
		}

		// Token: 0x06005DDE RID: 24030 RVA: 0x001CFA14 File Offset: 0x001CDC14
		private void SetName(string value)
		{
			for (int i = 0; i < this.m_lbName.Length; i++)
			{
				NKCUtil.SetLabelText(this.m_lbName[i], value);
			}
		}

		// Token: 0x06005DDF RID: 24031 RVA: 0x001CFA44 File Offset: 0x001CDC44
		private void SetLoyality(int value)
		{
			string msg;
			if (value > 10000)
			{
				msg = NKCStringTable.GetString("SI_DP_STRING_VOICE_CATEGORY_LIFETIME", false);
			}
			else
			{
				msg = (value / 100).ToString();
			}
			for (int i = 0; i < this.m_lbLoyality.Length; i++)
			{
				NKCUtil.SetLabelText(this.m_lbLoyality[i], msg);
			}
		}

		// Token: 0x06005DE0 RID: 24032 RVA: 0x001CFA98 File Offset: 0x001CDC98
		private void SetNoLoyality()
		{
			for (int i = 0; i < this.m_lbLoyality.Length; i++)
			{
				NKCUtil.SetLabelText(this.m_lbLoyality[i], "-");
			}
		}

		// Token: 0x04004A1A RID: 18970
		public NKCUIComToggle m_Toggle;

		// Token: 0x04004A1B RID: 18971
		public Text[] m_lbLoyality;

		// Token: 0x04004A1C RID: 18972
		public Text[] m_lbName;
	}
}
