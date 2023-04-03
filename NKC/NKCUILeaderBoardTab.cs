using System;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x0200095E RID: 2398
	public class NKCUILeaderBoardTab : MonoBehaviour
	{
		// Token: 0x06005FB7 RID: 24503 RVA: 0x001DC968 File Offset: 0x001DAB68
		public void SetData(LeaderBoardType boardType, NKCUIComToggleGroup tglGroup, NKCUILeaderBoardTab.OnValueChange onValueChange)
		{
			this.m_tgl.SetToggleGroup(tglGroup);
			this.m_tgl.OnValueChanged.RemoveAllListeners();
			this.m_tgl.OnValueChanged.AddListener(new UnityAction<bool>(this.OnValueChanged));
			this.dOnValueChange = onValueChange;
			NKMLeaderBoardTemplet nkmleaderBoardTemplet = NKMLeaderBoardTemplet.Find(boardType, 0);
			if (nkmleaderBoardTemplet == null)
			{
				Debug.LogWarning(string.Format("NKMLeaderBoardTemplet is null - {0}", boardType));
				NKCUtil.SetGameobjectActive(base.gameObject, false);
				return;
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			this.m_BoardType = boardType;
			NKCUtil.SetLabelText(this.m_lbTitle, nkmleaderBoardTemplet.GetTabName());
			this.SetTitleColor(this.m_tgl.m_bSelect);
			NKCUtil.SetImageSprite(this.m_imgIcon, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", nkmleaderBoardTemplet.m_BoardTabIcon, false), false);
			NKCUtil.SetImageSprite(this.m_imgIconOff, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("AB_UI_NKM_UI_LEADER_BOARD_SPRITE", nkmleaderBoardTemplet.m_BoardTabIcon, false), false);
			this.CheckRedDot();
		}

		// Token: 0x06005FB8 RID: 24504 RVA: 0x001DCA56 File Offset: 0x001DAC56
		private void OnValueChanged(bool bValue)
		{
			if (bValue)
			{
				NKCUILeaderBoardTab.OnValueChange onValueChange = this.dOnValueChange;
				if (onValueChange == null)
				{
					return;
				}
				onValueChange(this.m_BoardType);
			}
		}

		// Token: 0x06005FB9 RID: 24505 RVA: 0x001DCA71 File Offset: 0x001DAC71
		public void SetTitleColor(bool bValue)
		{
			if (this.m_tgl.m_bSelect)
			{
				NKCUtil.SetLabelTextColor(this.m_lbTitle, this.TITLE_COLOR_TAB_ON);
				return;
			}
			NKCUtil.SetLabelTextColor(this.m_lbTitle, this.TITLE_COLOR_TAB_OFF);
		}

		// Token: 0x06005FBA RID: 24506 RVA: 0x001DCAA3 File Offset: 0x001DACA3
		public void CheckRedDot()
		{
			if (this.m_BoardType == LeaderBoardType.BT_FIERCE)
			{
				NKCUtil.SetGameobjectActive(this.m_objRedDot, NKCAlarmManager.CheckFierceDailyRewardNotify(NKCScenManager.CurrentUserData()));
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objRedDot, false);
		}

		// Token: 0x04004BD7 RID: 19415
		public NKCUIComToggle m_tgl;

		// Token: 0x04004BD8 RID: 19416
		public Image m_imgIcon;

		// Token: 0x04004BD9 RID: 19417
		public Image m_imgIconOff;

		// Token: 0x04004BDA RID: 19418
		public Text m_lbTitle;

		// Token: 0x04004BDB RID: 19419
		public GameObject m_objRedDot;

		// Token: 0x04004BDC RID: 19420
		private NKCUILeaderBoardTab.OnValueChange dOnValueChange;

		// Token: 0x04004BDD RID: 19421
		private Color TITLE_COLOR_TAB_ON = new Color(0.003921569f, 0.105882354f, 0.23137255f);

		// Token: 0x04004BDE RID: 19422
		private Color TITLE_COLOR_TAB_OFF = Color.white;

		// Token: 0x04004BDF RID: 19423
		private LeaderBoardType m_BoardType;

		// Token: 0x020015DE RID: 5598
		// (Invoke) Token: 0x0600AE7D RID: 44669
		public delegate void OnValueChange(LeaderBoardType boardType);
	}
}
