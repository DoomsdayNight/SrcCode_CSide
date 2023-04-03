using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC
{
	// Token: 0x02000767 RID: 1895
	public class NKCUIComUnitFilterOptionList : MonoBehaviour
	{
		// Token: 0x06004BA1 RID: 19361 RVA: 0x0016A460 File Offset: 0x00168660
		public void Init(NKCUIComUnitFilterOptionList.OnFilterOptionChange onFilterOptionChange)
		{
			this.m_tglFilterOption.OnValueChanged.RemoveAllListeners();
			this.m_tglFilterOption.OnValueChanged.AddListener(new UnityAction<bool>(this.OnTglFilter));
			this.dOnFilterOptionChange = onFilterOptionChange;
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x0016A495 File Offset: 0x00168695
		private void OnTglFilter(bool bChecked)
		{
			this.SetOpenFilterOption(bChecked, true);
		}

		// Token: 0x06004BA3 RID: 19363 RVA: 0x0016A4A0 File Offset: 0x001686A0
		private void SetOpenFilterOption(bool bOpen, bool bAnimate = true)
		{
			this.m_tglFilterOption.Select(bOpen, true, false);
			this.m_rtFilterOption.DOKill(false);
			Vector3 one = Vector3.one;
			one.y = (float)(bOpen ? 1 : 0);
			if (bAnimate && this.m_rtFilterOption.gameObject.activeInHierarchy)
			{
				this.m_rtFilterOption.DOScale(one, this.MENU_ANIM_TIME).SetEase(Ease.OutCubic);
				return;
			}
			this.m_rtFilterOption.localScale = one;
		}

		// Token: 0x06004BA4 RID: 19364 RVA: 0x0016A51A File Offset: 0x0016871A
		public void SetFilterState(NKM_UNIT_STYLE_TYPE type, bool bAnimate)
		{
			this.SetOpenFilterOption(false, bAnimate);
			this.SetFilterUI(type);
		}

		// Token: 0x06004BA5 RID: 19365 RVA: 0x0016A52C File Offset: 0x0016872C
		public void SetFilterButtonList(NKM_UNIT_TYPE eType)
		{
			List<NKM_UNIT_STYLE_TYPE> list;
			if (eType == NKM_UNIT_TYPE.NUT_NORMAL || eType != NKM_UNIT_TYPE.NUT_SHIP)
			{
				list = this.lstUnitFilter;
			}
			else
			{
				list = this.lstShipFilter;
			}
			for (int i = 0; i < list.Count; i++)
			{
				NKM_UNIT_STYLE_TYPE targetFilterType = list[i];
				if (i < this.m_lstFilterButtons.Count)
				{
					NKCUtil.SetGameobjectActive(this.m_lstFilterButtons[i].m_StateButton, true);
					NKCUtil.SetLabelText(this.m_lstFilterButtons[i].m_Label, NKCUnitSortSystem.GetFilterName(targetFilterType));
					if (this.m_lstFilterButtons[i].m_StateButton != null)
					{
						this.m_lstFilterButtons[i].m_StateButton.PointerClick.RemoveAllListeners();
						this.m_lstFilterButtons[i].m_StateButton.PointerClick.AddListener(delegate()
						{
							this.OnSelectFilter(targetFilterType);
						});
					}
				}
			}
			if (list.Count < this.m_lstFilterButtons.Count)
			{
				for (int j = list.Count; j < this.m_lstFilterButtons.Count; j++)
				{
					NKCUtil.SetGameobjectActive(this.m_lstFilterButtons[j].m_StateButton, false);
				}
			}
			this.SetFilterUI(NKM_UNIT_STYLE_TYPE.NUST_INVALID);
			this.SetOpenFilterOption(false, true);
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0016A67C File Offset: 0x0016887C
		private void SetFilterUI(NKM_UNIT_STYLE_TYPE filterOption)
		{
			foreach (Text label in this.m_lstlbFilterOption)
			{
				NKCUtil.SetLabelText(label, NKCUnitSortSystem.GetFilterName(filterOption));
			}
		}

		// Token: 0x06004BA7 RID: 19367 RVA: 0x0016A6D4 File Offset: 0x001688D4
		private void OnSelectFilter(NKM_UNIT_STYLE_TYPE type)
		{
			this.SetOpenFilterOption(false, true);
			this.SetFilterUI(type);
			NKCUIComUnitFilterOptionList.OnFilterOptionChange onFilterOptionChange = this.dOnFilterOptionChange;
			if (onFilterOptionChange == null)
			{
				return;
			}
			onFilterOptionChange(type);
		}

		// Token: 0x04003A3C RID: 14908
		public List<NKCUIComUnitFilterOptionList.ButtonWithLabel> m_lstFilterButtons;

		// Token: 0x04003A3D RID: 14909
		public NKCUIComToggle m_tglFilterOption;

		// Token: 0x04003A3E RID: 14910
		public List<Text> m_lstlbFilterOption;

		// Token: 0x04003A3F RID: 14911
		public RectTransform m_rtFilterOption;

		// Token: 0x04003A40 RID: 14912
		public float MENU_ANIM_TIME = 0.3f;

		// Token: 0x04003A41 RID: 14913
		private NKCUIComUnitFilterOptionList.OnFilterOptionChange dOnFilterOptionChange;

		// Token: 0x04003A42 RID: 14914
		private readonly List<NKM_UNIT_STYLE_TYPE> lstUnitFilter = new List<NKM_UNIT_STYLE_TYPE>
		{
			NKM_UNIT_STYLE_TYPE.NUST_INVALID,
			NKM_UNIT_STYLE_TYPE.NUST_COUNTER,
			NKM_UNIT_STYLE_TYPE.NUST_SOLDIER,
			NKM_UNIT_STYLE_TYPE.NUST_MECHANIC
		};

		// Token: 0x04003A43 RID: 14915
		private readonly List<NKM_UNIT_STYLE_TYPE> lstShipFilter = new List<NKM_UNIT_STYLE_TYPE>
		{
			NKM_UNIT_STYLE_TYPE.NUST_INVALID,
			NKM_UNIT_STYLE_TYPE.NUST_SHIP_ASSAULT,
			NKM_UNIT_STYLE_TYPE.NUST_SHIP_CRUISER,
			NKM_UNIT_STYLE_TYPE.NUST_SHIP_HEAVY,
			NKM_UNIT_STYLE_TYPE.NUST_SHIP_SPECIAL,
			NKM_UNIT_STYLE_TYPE.NUST_SHIP_PATROL
		};

		// Token: 0x02001431 RID: 5169
		[Serializable]
		public class ButtonWithLabel
		{
			// Token: 0x04009DAB RID: 40363
			public NKCUIComStateButton m_StateButton;

			// Token: 0x04009DAC RID: 40364
			public Text m_Label;
		}

		// Token: 0x02001432 RID: 5170
		// (Invoke) Token: 0x0600A818 RID: 43032
		public delegate void OnFilterOptionChange(NKM_UNIT_STYLE_TYPE type);
	}
}
