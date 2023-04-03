using System;
using NKM.Templet;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A13 RID: 2579
	public class NKCUIStageInfoSubBase : MonoBehaviour
	{
		// Token: 0x0600709E RID: 28830 RVA: 0x00255306 File Offset: 0x00253506
		public virtual void InitUI(NKCUIStageInfoSubBase.OnButton onButton)
		{
			this.m_NKCUIComDungeonRewardList.InitUI();
			this.dOnOKButton = onButton;
		}

		// Token: 0x0600709F RID: 28831 RVA: 0x0025531C File Offset: 0x0025351C
		public virtual void SetData(NKMStageTempletV2 stageTemplet, bool bFirstOpen = true)
		{
			this.m_StageTemplet = stageTemplet;
			NKCUtil.SetLabelText(this.m_lbStageNum, NKCUtilString.GetEpisodeNumber(stageTemplet.EpisodeTemplet, stageTemplet));
			NKCUtil.SetLabelText(this.m_lbStageName, stageTemplet.GetDungeonName());
			NKCUtil.SetLabelText(this.m_lbDesc, stageTemplet.GetStageDesc());
			if (this.m_srDesc != null)
			{
				this.m_srDesc.normalizedPosition = new Vector2(0f, 1f);
			}
			if (this.m_NKCUIComDungeonRewardList.CreateRewardSlotDataList(NKCScenManager.CurrentUserData(), stageTemplet, stageTemplet.m_StageBattleStrID))
			{
				NKCUtil.SetGameobjectActive(this.m_objDropItem, true);
				return;
			}
			NKCUtil.SetGameobjectActive(this.m_objDropItem, false);
		}

		// Token: 0x060070A0 RID: 28832 RVA: 0x002553C3 File Offset: 0x002535C3
		public virtual void Update()
		{
			if (base.gameObject.activeInHierarchy)
			{
				this.m_NKCUIComDungeonRewardList.ShowRewardListUpdate();
			}
		}

		// Token: 0x060070A1 RID: 28833 RVA: 0x002553DD File Offset: 0x002535DD
		public void OnOK()
		{
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton(this.m_bOperationSkip, this.m_SkipCount);
			}
		}

		// Token: 0x04005C57 RID: 23639
		[Header("공용")]
		[Header("상단")]
		public TMP_Text m_lbStageNum;

		// Token: 0x04005C58 RID: 23640
		public TMP_Text m_lbStageName;

		// Token: 0x04005C59 RID: 23641
		[Header("중단")]
		public ScrollRect m_srDesc;

		// Token: 0x04005C5A RID: 23642
		public TMP_Text m_lbDesc;

		// Token: 0x04005C5B RID: 23643
		[Header("던전 보상 리스트 컴포넌트")]
		public GameObject m_objDropItem;

		// Token: 0x04005C5C RID: 23644
		public NKCUIComDungeonRewardList m_NKCUIComDungeonRewardList;

		// Token: 0x04005C5D RID: 23645
		[Header("치트")]
		public EventTrigger m_ETDungeonClearReward;

		// Token: 0x04005C5E RID: 23646
		private NKCUIStageInfoSubBase.OnButton dOnOKButton;

		// Token: 0x04005C5F RID: 23647
		protected NKMStageTempletV2 m_StageTemplet;

		// Token: 0x04005C60 RID: 23648
		protected int m_SkipCount = 1;

		// Token: 0x04005C61 RID: 23649
		protected bool m_bOperationSkip;

		// Token: 0x02001750 RID: 5968
		// (Invoke) Token: 0x0600B2EE RID: 45806
		public delegate void OnButton(bool bSkip, int skipCount);
	}
}
