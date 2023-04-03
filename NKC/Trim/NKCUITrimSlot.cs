using System;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Trim
{
	// Token: 0x02000AB2 RID: 2738
	public class NKCUITrimSlot : MonoBehaviour
	{
		// Token: 0x17001464 RID: 5220
		// (get) Token: 0x060079CE RID: 31182 RVA: 0x00288FDE File Offset: 0x002871DE
		public NKCUITrimSlot.SlotState TrimSlotState
		{
			get
			{
				return this.m_eSlotState;
			}
		}

		// Token: 0x060079CF RID: 31183 RVA: 0x00288FE6 File Offset: 0x002871E6
		public void Init(int slotIndex, NKCUITrimSlot.OnClick onClick)
		{
			this.m_iSlotIndex = slotIndex;
			this.m_dOnClick = onClick;
			NKCUtil.SetButtonClickDelegate(this.m_csbtnButton, new UnityAction(this.OnClickSlot));
			NKCUITrimUtility.InitBattleCondition(this.m_battleCondParent, false);
			this.m_animator.keepAnimatorControllerStateOnDisable = true;
		}

		// Token: 0x060079D0 RID: 31184 RVA: 0x00289028 File Offset: 0x00287228
		public void SetData(int trimId)
		{
			this.m_trimId = trimId;
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(trimId);
			NKMUserData userData = NKCScenManager.CurrentUserData();
			int trimLevel = 0;
			string msg;
			if (nkmtrimTemplet != null)
			{
				msg = NKCStringTable.GetString(nkmtrimTemplet.TirmGroupName, false);
				int clearedTrimLevel = NKCUITrimUtility.GetClearedTrimLevel(userData, trimId);
				trimLevel = Mathf.Min(nkmtrimTemplet.MaxTrimLevel, clearedTrimLevel + 1);
			}
			else
			{
				msg = " - ";
			}
			NKCUtil.SetGameobjectActive(this.m_objIntervalTime, nkmtrimTemplet != null && nkmtrimTemplet.ShowInterval);
			NKCUtil.SetLabelText(this.m_lbTrimName, msg);
			NKCUtil.SetLabelText(this.m_lbTrimLevel, trimLevel.ToString());
			NKCUITrimUtility.SetBattleCondition(this.m_battleCondParent, nkmtrimTemplet, trimLevel, false);
			NKCUtil.SetGameobjectActive(this.m_objEventDrop, NKCUITrimUtility.HaveEventDrop(trimId));
		}

		// Token: 0x060079D1 RID: 31185 RVA: 0x002890D4 File Offset: 0x002872D4
		public void SetSlotState(NKCUITrimSlot.SlotState slotState)
		{
			this.m_eSlotState = slotState;
			NKMTrimTemplet nkmtrimTemplet = NKMTrimTemplet.Find(this.m_trimId);
			NKMUserData cNKMUserData = NKCScenManager.CurrentUserData();
			if (nkmtrimTemplet == null || !NKMContentUnlockManager.IsContentUnlocked(cNKMUserData, nkmtrimTemplet.m_UnlockInfo, false))
			{
				this.m_eSlotState = NKCUITrimSlot.SlotState.Locked;
			}
			switch (this.m_eSlotState)
			{
			case NKCUITrimSlot.SlotState.Default:
			{
				Animator animator = this.m_animator;
				if (animator == null)
				{
					return;
				}
				animator.Play(this.m_baseAnimation);
				return;
			}
			case NKCUITrimSlot.SlotState.Selected:
			{
				Animator animator2 = this.m_animator;
				if (animator2 == null)
				{
					return;
				}
				animator2.Play(this.m_selectAnimation);
				return;
			}
			case NKCUITrimSlot.SlotState.Disable:
			{
				Animator animator3 = this.m_animator;
				if (animator3 == null)
				{
					return;
				}
				animator3.Play(this.m_disableAnimation);
				return;
			}
			case NKCUITrimSlot.SlotState.Locked:
			{
				Animator animator4 = this.m_animator;
				if (animator4 == null)
				{
					return;
				}
				animator4.Play(this.m_lockedAnimation);
				return;
			}
			default:
				return;
			}
		}

		// Token: 0x060079D2 RID: 31186 RVA: 0x0028918C File Offset: 0x0028738C
		public void SetActive(bool value)
		{
			base.gameObject.SetActive(value);
		}

		// Token: 0x060079D3 RID: 31187 RVA: 0x0028919A File Offset: 0x0028739A
		public bool IsActive()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x060079D4 RID: 31188 RVA: 0x002891A7 File Offset: 0x002873A7
		public void LetChangeClickState(int trimId)
		{
			if (this.m_trimId == trimId)
			{
				this.OnClickSlot();
			}
		}

		// Token: 0x060079D5 RID: 31189 RVA: 0x002891B8 File Offset: 0x002873B8
		private void OnClickSlot()
		{
			if (this.m_dOnClick != null)
			{
				this.m_dOnClick(this.m_iSlotIndex, this.m_trimId);
			}
		}

		// Token: 0x060079D6 RID: 31190 RVA: 0x002891D9 File Offset: 0x002873D9
		private void OnDestroy()
		{
			this.m_dOnClick = null;
		}

		// Token: 0x0400668F RID: 26255
		public NKCUIComStateButton m_csbtnButton;

		// Token: 0x04006690 RID: 26256
		public Animator m_animator;

		// Token: 0x04006691 RID: 26257
		public Text m_lbTrimName;

		// Token: 0x04006692 RID: 26258
		public Text m_lbTrimLevel;

		// Token: 0x04006693 RID: 26259
		[Header("��Ʋ �����")]
		public Transform m_battleCondParent;

		// Token: 0x04006694 RID: 26260
		[Header("���� ���� �ִϸ��̼� �̸�")]
		public string m_baseAnimation;

		// Token: 0x04006695 RID: 26261
		public string m_selectAnimation;

		// Token: 0x04006696 RID: 26262
		public string m_disableAnimation;

		// Token: 0x04006697 RID: 26263
		public string m_lockedAnimation;

		// Token: 0x04006698 RID: 26264
		[Space]
		public GameObject m_objIntervalTime;

		// Token: 0x04006699 RID: 26265
		public GameObject m_objEventDrop;

		// Token: 0x0400669A RID: 26266
		private NKCUITrimSlot.OnClick m_dOnClick;

		// Token: 0x0400669B RID: 26267
		private int m_iSlotIndex;

		// Token: 0x0400669C RID: 26268
		private int m_trimId;

		// Token: 0x0400669D RID: 26269
		private NKCUITrimSlot.SlotState m_eSlotState;

		// Token: 0x0200180C RID: 6156
		public enum SlotState
		{
			// Token: 0x0400A7F6 RID: 42998
			Default,
			// Token: 0x0400A7F7 RID: 42999
			Selected,
			// Token: 0x0400A7F8 RID: 43000
			Disable,
			// Token: 0x0400A7F9 RID: 43001
			Locked
		}

		// Token: 0x0200180D RID: 6157
		// (Invoke) Token: 0x0600B4FA RID: 46330
		public delegate void OnClick(int slotIndex, int trimId);
	}
}
