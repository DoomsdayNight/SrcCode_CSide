using System;
using NKC.UI.Tooltip;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace NKC.UI
{
	// Token: 0x0200095F RID: 2399
	public class NKCComStatInfoToolTip : MonoBehaviour
	{
		// Token: 0x06005FBC RID: 24508 RVA: 0x001DCB00 File Offset: 0x001DAD00
		private void Init()
		{
			if (this.btn == null)
			{
				this.btn = base.gameObject.GetComponent<NKCUIComStateButton>();
				if (this.btn == null)
				{
					this.btn = base.gameObject.AddComponent<NKCUIComStateButton>();
				}
			}
			if (this.btn != null)
			{
				this.btn.PointerDown.RemoveAllListeners();
				this.btn.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnTouchStatInfo));
			}
			if (this.raycastTarget == null)
			{
				this.raycastTarget = base.gameObject.GetComponent<NKCUIComRaycastTarget>();
				if (this.raycastTarget == null)
				{
					this.raycastTarget = base.gameObject.AddComponent<NKCUIComRaycastTarget>();
				}
			}
		}

		// Token: 0x06005FBD RID: 24509 RVA: 0x001DCBC4 File Offset: 0x001DADC4
		public void SetType(NKM_STAT_TYPE type, bool bNegative = false)
		{
			if (!this.bInit)
			{
				this.Init();
				this.bInit = true;
			}
			this.m_bNegative = bNegative;
			foreach (NKCStatInfoTemplet nkcstatInfoTemplet in NKCStatInfoTemplet.Values)
			{
				if (nkcstatInfoTemplet != null && string.Equals(type.ToString(), nkcstatInfoTemplet.Stat_ID))
				{
					this.m_StatType = type;
					if (this.m_bNegative && !string.IsNullOrEmpty(nkcstatInfoTemplet.Stat_Negative_DESC))
					{
						this.statDesc = NKCStringTable.GetString(nkcstatInfoTemplet.Stat_Negative_DESC, false);
						break;
					}
					this.statDesc = NKCStringTable.GetString(nkcstatInfoTemplet.Stat_Desc, false);
					break;
				}
			}
		}

		// Token: 0x06005FBE RID: 24510 RVA: 0x001DCC88 File Offset: 0x001DAE88
		private void OnTouchStatInfo(PointerEventData e)
		{
			NKCUITooltip.Instance.Open(NKCUISlot.eSlotMode.Etc, NKCUtilString.GetStatShortName(this.m_StatType, this.m_bNegative), this.statDesc, new Vector2?(e.position));
		}

		// Token: 0x04004BE0 RID: 19424
		private NKCUIComStateButton btn;

		// Token: 0x04004BE1 RID: 19425
		private NKCUIComRaycastTarget raycastTarget;

		// Token: 0x04004BE2 RID: 19426
		private string statDesc;

		// Token: 0x04004BE3 RID: 19427
		private bool m_bNegative;

		// Token: 0x04004BE4 RID: 19428
		private bool bInit;

		// Token: 0x04004BE5 RID: 19429
		private NKM_STAT_TYPE m_StatType;
	}
}
