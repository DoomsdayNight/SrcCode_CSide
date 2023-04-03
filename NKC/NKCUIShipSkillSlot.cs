using System;
using NKC.UI.Tooltip;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009E7 RID: 2535
	public class NKCUIShipSkillSlot : MonoBehaviour
	{
		// Token: 0x17001292 RID: 4754
		// (get) Token: 0x06006D14 RID: 27924 RVA: 0x0023C167 File Offset: 0x0023A367
		public int CurrentSkillID
		{
			get
			{
				if (this.m_CurrentSkillTemplet == null)
				{
					return 0;
				}
				return this.m_CurrentSkillTemplet.m_ShipSkillID;
			}
		}

		// Token: 0x06006D15 RID: 27925 RVA: 0x0023C180 File Offset: 0x0023A380
		public void Init(UnityAction dOnClick, bool bCallToolTip = false)
		{
			if (this.m_cbtnSlot != null)
			{
				this.m_cbtnSlot.PointerClick.RemoveAllListeners();
				this.m_cbtnSlot.PointerClick.AddListener(new UnityAction(this.OnClick));
			}
			if (this.m_cbtnSlot != null)
			{
				this.m_cbtnSlot.PointerDown.RemoveAllListeners();
				this.m_cbtnSlot.PointerDown.AddListener(new UnityAction<PointerEventData>(this.OnPointerDown));
			}
			this.dOnClickSkillSlot = dOnClick;
			NKCUtil.SetGameobjectActive(this.m_objHighlighted, false);
			this.m_bToolTip = bCallToolTip;
		}

		// Token: 0x06006D16 RID: 27926 RVA: 0x0023C21B File Offset: 0x0023A41B
		public void Cleanup()
		{
			this.SetData(null, NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE);
		}

		// Token: 0x06006D17 RID: 27927 RVA: 0x0023C228 File Offset: 0x0023A428
		public void SetData(NKMShipSkillTemplet unitSkillTemplet, NKCUIShipSkillSlot.eShipSkillSlotStatus status = NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE)
		{
			this.m_CurrentSkillTemplet = unitSkillTemplet;
			if (unitSkillTemplet != null)
			{
				this.m_imgSkillIcon.sprite = NKCUtil.GetSkillIconSprite(unitSkillTemplet);
				NKCUtil.SetLabelText(this.m_lbSkillType, NKCUtilString.GetSkillTypeName(unitSkillTemplet.m_NKM_SKILL_TYPE));
			}
			else
			{
				this.m_imgSkillIcon.sprite = NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_SHIP_SKILL_ICON", "SS_NO_SKILL_ICON", false);
			}
			this.SetStatus(status);
		}

		// Token: 0x06006D18 RID: 27928 RVA: 0x0023C28C File Offset: 0x0023A48C
		public void SetStatus(NKCUIShipSkillSlot.eShipSkillSlotStatus status = NKCUIShipSkillSlot.eShipSkillSlotStatus.NONE)
		{
			NKCUtil.SetGameobjectActive(this.SKILL_ICON_ENHANCE, status == NKCUIShipSkillSlot.eShipSkillSlotStatus.ENHANCE);
			NKCUtil.SetGameobjectActive(this.SKILL_ICON_LOCK, status == NKCUIShipSkillSlot.eShipSkillSlotStatus.LOCK);
			NKCUtil.SetGameobjectActive(this.SKILL_ICON_UNLOCK, status == NKCUIShipSkillSlot.eShipSkillSlotStatus.UNLOCK);
			NKCUtil.SetGameobjectActive(this.SKILL_ICON_NEW, status == NKCUIShipSkillSlot.eShipSkillSlotStatus.NEW);
			NKCUtil.SetGameobjectActive(this.SKILL_ICON_NEW_GET, status == NKCUIShipSkillSlot.eShipSkillSlotStatus.NEW_GET_POPUP);
		}

		// Token: 0x06006D19 RID: 27929 RVA: 0x0023C2E4 File Offset: 0x0023A4E4
		public void SetText(string text)
		{
			NKCUtil.SetLabelText(this.SKILL_ICON_NEW_GET_TEXT, text);
		}

		// Token: 0x06006D1A RID: 27930 RVA: 0x0023C2F2 File Offset: 0x0023A4F2
		public void SetHighlight(bool value)
		{
			NKCUtil.SetGameobjectActive(this.m_objHighlighted, value);
		}

		// Token: 0x06006D1B RID: 27931 RVA: 0x0023C300 File Offset: 0x0023A500
		public void OnClick()
		{
			if (this.dOnClickSkillSlot != null)
			{
				this.dOnClickSkillSlot();
			}
		}

		// Token: 0x06006D1C RID: 27932 RVA: 0x0023C315 File Offset: 0x0023A515
		public void OnPointerDown(PointerEventData eventData)
		{
			if (this.m_bToolTip && this.m_CurrentSkillTemplet != null)
			{
				NKCUITooltip.Instance.Open(this.m_CurrentSkillTemplet, new Vector2?(eventData.position));
			}
		}

		// Token: 0x040058ED RID: 22765
		public NKCUIComButton m_cbtnSlot;

		// Token: 0x040058EE RID: 22766
		public GameObject m_objHighlighted;

		// Token: 0x040058EF RID: 22767
		public Image m_imgSkillIcon;

		// Token: 0x040058F0 RID: 22768
		public GameObject SKILL_ICON_LOCK;

		// Token: 0x040058F1 RID: 22769
		public GameObject SKILL_ICON_UNLOCK;

		// Token: 0x040058F2 RID: 22770
		public GameObject SKILL_ICON_NEW;

		// Token: 0x040058F3 RID: 22771
		public GameObject SKILL_ICON_ENHANCE;

		// Token: 0x040058F4 RID: 22772
		public GameObject SKILL_ICON_NEW_GET;

		// Token: 0x040058F5 RID: 22773
		public Text SKILL_ICON_NEW_GET_TEXT;

		// Token: 0x040058F6 RID: 22774
		public Text m_lbSkillType;

		// Token: 0x040058F7 RID: 22775
		private UnityAction dOnClickSkillSlot;

		// Token: 0x040058F8 RID: 22776
		private NKMShipSkillTemplet m_CurrentSkillTemplet;

		// Token: 0x040058F9 RID: 22777
		private bool m_bToolTip;

		// Token: 0x020016F3 RID: 5875
		public enum eShipSkillSlotStatus
		{
			// Token: 0x0400A59A RID: 42394
			NONE,
			// Token: 0x0400A59B RID: 42395
			LOCK,
			// Token: 0x0400A59C RID: 42396
			UNLOCK,
			// Token: 0x0400A59D RID: 42397
			NEW,
			// Token: 0x0400A59E RID: 42398
			ENHANCE,
			// Token: 0x0400A59F RID: 42399
			NEW_GET_POPUP
		}
	}
}
