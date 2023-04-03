using System;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x02000A12 RID: 2578
	public class NKCUIStageInfo : MonoBehaviour
	{
		// Token: 0x06007094 RID: 28820 RVA: 0x00255181 File Offset: 0x00253381
		public bool IsOpened()
		{
			return base.gameObject.activeSelf;
		}

		// Token: 0x06007095 RID: 28821 RVA: 0x0025518E File Offset: 0x0025338E
		public void InitUI(NKCUIStageInfo.OnButton onButton)
		{
			this.m_StageUI.InitUI(new NKCUIStageInfoSubBase.OnButton(this.OnOK));
			this.m_StoryUI.InitUI(new NKCUIStageInfoSubBase.OnButton(this.OnOK));
			this.dOnOKButton = onButton;
		}

		// Token: 0x06007096 RID: 28822 RVA: 0x002551C8 File Offset: 0x002533C8
		public void Open(NKMStageTempletV2 stageTemplet)
		{
			if (this.m_StageTemplet == stageTemplet)
			{
				return;
			}
			this.m_StageTemplet = stageTemplet;
			if (this.m_StageTemplet.DungeonTempletBase != null && this.m_StageTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
			{
				this.SetStoryInfo();
			}
			else
			{
				this.SetStageInfo(true);
			}
			NKCUtil.SetGameobjectActive(base.gameObject, true);
		}

		// Token: 0x06007097 RID: 28823 RVA: 0x00255221 File Offset: 0x00253421
		public void Close()
		{
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			this.m_StageTemplet = null;
			this.m_StageUI.Close();
			this.m_StoryUI.Close();
		}

		// Token: 0x06007098 RID: 28824 RVA: 0x0025524C File Offset: 0x0025344C
		public void RefreshUI()
		{
			if (this.m_StageTemplet.DungeonTempletBase != null && this.m_StageTemplet.DungeonTempletBase.m_DungeonType == NKM_DUNGEON_TYPE.NDT_CUTSCENE)
			{
				this.SetStoryInfo();
				return;
			}
			this.SetStageInfo(false);
		}

		// Token: 0x06007099 RID: 28825 RVA: 0x0025527C File Offset: 0x0025347C
		private void SetStageInfo(bool bFirstOpen = true)
		{
			NKCUtil.SetGameobjectActive(this.m_StageUI, true);
			NKCUtil.SetGameobjectActive(this.m_StoryUI, false);
			this.m_StageUI.SetData(this.m_StageTemplet, bFirstOpen);
		}

		// Token: 0x0600709A RID: 28826 RVA: 0x002552A8 File Offset: 0x002534A8
		private void SetStoryInfo()
		{
			NKCUtil.SetGameobjectActive(this.m_StageUI, false);
			NKCUtil.SetGameobjectActive(this.m_StoryUI, true);
			this.m_StoryUI.SetData(this.m_StageTemplet, true);
		}

		// Token: 0x0600709B RID: 28827 RVA: 0x002552D4 File Offset: 0x002534D4
		public void RefreshFavoriteState()
		{
			this.m_StageUI.RefreshFavoriteState();
		}

		// Token: 0x0600709C RID: 28828 RVA: 0x002552E1 File Offset: 0x002534E1
		public void OnOK(bool bSkip, int skipCount)
		{
			if (this.dOnOKButton != null)
			{
				this.dOnOKButton(this.m_StageTemplet, bSkip, skipCount);
			}
		}

		// Token: 0x04005C53 RID: 23635
		[Header("스테이지")]
		public NKCUIStageInfoSubStage m_StageUI;

		// Token: 0x04005C54 RID: 23636
		[Header("스토리")]
		public NKCUIStageInfoSubStory m_StoryUI;

		// Token: 0x04005C55 RID: 23637
		private NKCUIStageInfo.OnButton dOnOKButton;

		// Token: 0x04005C56 RID: 23638
		private NKMStageTempletV2 m_StageTemplet;

		// Token: 0x0200174F RID: 5967
		// (Invoke) Token: 0x0600B2EA RID: 45802
		public delegate void OnButton(NKMStageTempletV2 stageTemplet, bool bSkip, int skipCount);
	}
}
