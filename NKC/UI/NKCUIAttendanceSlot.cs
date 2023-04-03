using System;
using System.Collections;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000970 RID: 2416
	public class NKCUIAttendanceSlot : MonoBehaviour
	{
		// Token: 0x06006179 RID: 24953 RVA: 0x001E8E03 File Offset: 0x001E7003
		private void Init()
		{
			NKCUISlot nkcuislot = this.m_NKCUISlot;
			if (nkcuislot != null)
			{
				nkcuislot.Init();
			}
			this.m_aniReceive.enabled = false;
			this.bInitComplete = true;
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x001E8E2C File Offset: 0x001E702C
		public void SetData(NKMAttendanceRewardTemplet rewardTemplet, bool bShowCheckMark, bool bShowCheckNext)
		{
			if (!this.bInitComplete)
			{
				this.Init();
			}
			NKCUtil.SetGameobjectActive(this.m_aniReceive.gameObject, true);
			this.m_aniReceive.enabled = false;
			this.m_NKCUISlot.SetData(NKCUISlot.SlotData.MakeRewardTypeData(rewardTemplet.RewardType, rewardTemplet.RewardID, rewardTemplet.RewardValue, 0), true, null);
			this.m_NKM_UI_EVENT_ICON_SLOT_DAY_TEXT.text = rewardTemplet.LoginDate.ToString();
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_EVENT_ICON_SLOT_CHECK, bShowCheckMark);
			NKCUtil.SetGameobjectActive(this.m_objNextMark, false);
			this.m_aniReceive.enabled = false;
			NKCUtil.SetGameobjectActive(this.m_objInduce, false);
		}

		// Token: 0x0600617B RID: 24955 RVA: 0x001E8ED2 File Offset: 0x001E70D2
		public IEnumerator ShowSlotCheckMarkAnimation(GameObject effectGo)
		{
			yield return null;
			NKCUtil.SetGameobjectActive(this.m_aniReceive.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_EVENT_ICON_SLOT_CHECK, true);
			this.m_aniReceive.enabled = true;
			this.m_aniReceive.Play("NKM_UI_ATTENDANCE_ICON_SLOT_ITEM_RECIEVED");
			if (effectGo != null)
			{
				effectGo.transform.position = base.transform.position;
			}
			NKCUtil.SetGameobjectActive(effectGo, true);
			NKCSoundManager.PlaySound("FX_UI_ATTENDANCE_CHECK", 1f, 0f, 0f, false, 0f, false, 0f);
			while (this.m_aniReceive.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
			{
				yield return null;
			}
			NKCUtil.SetGameobjectActive(effectGo, false);
			this.m_aniReceive.enabled = false;
			yield break;
		}

		// Token: 0x0600617C RID: 24956 RVA: 0x001E8EE8 File Offset: 0x001E70E8
		public void ShowInduceAnimation(bool bShow)
		{
			NKCUtil.SetGameobjectActive(this.m_objInduce, bShow);
		}

		// Token: 0x0600617D RID: 24957 RVA: 0x001E8EF6 File Offset: 0x001E70F6
		public IEnumerator ShowNextMark(bool bPlayAnimation)
		{
			yield return null;
			NKCUtil.SetGameobjectActive(this.m_objNextMark, true);
			if (bPlayAnimation)
			{
				this.m_aniReceive.enabled = true;
				this.m_aniReceive.Play("NKM_UI_ATTENDANCE_ICON_SLOT_ITEM_NEXT");
				if (this.m_objNextMark != null)
				{
					this.m_objNextMark.transform.position = base.transform.position;
				}
				NKCUtil.SetGameobjectActive(this.m_objNextMark, true);
				while (this.m_aniReceive.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
				{
					yield return null;
				}
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_objNextMark, true);
			}
			this.m_aniReceive.enabled = false;
			yield break;
		}

		// Token: 0x04004DAC RID: 19884
		public NKCUISlot m_NKCUISlot;

		// Token: 0x04004DAD RID: 19885
		public Animator m_aniReceive;

		// Token: 0x04004DAE RID: 19886
		public GameObject m_objInduce;

		// Token: 0x04004DAF RID: 19887
		public Text m_NKM_UI_EVENT_ICON_SLOT_DAY_TEXT;

		// Token: 0x04004DB0 RID: 19888
		public GameObject m_NKM_UI_EVENT_ICON_SLOT_CHECK;

		// Token: 0x04004DB1 RID: 19889
		public GameObject m_objNextMark;

		// Token: 0x04004DB2 RID: 19890
		private bool bInitComplete;
	}
}
