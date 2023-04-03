using System;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI.Collection
{
	// Token: 0x02000C2A RID: 3114
	public class NKCUICollectionStorySlot : MonoBehaviour
	{
		// Token: 0x0600906A RID: 36970 RVA: 0x00312FAC File Offset: 0x003111AC
		public void Init(NKCUICollectionStory.OnPlayCutScene callback)
		{
			if (null != this.m_NKM_UI_COLLECTION_STORY_SLOT_BUTTON_PLAY1)
			{
				this.m_NKM_UI_COLLECTION_STORY_SLOT_BUTTON_PLAY1.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_COLLECTION_STORY_SLOT_BUTTON_PLAY1.PointerClick.AddListener(delegate()
				{
					this.CutScenePlay(1);
				});
			}
			if (null != this.m_NKM_UI_COLLECTION_STORY_SLOT_BUTTON_PLAY2)
			{
				this.m_NKM_UI_COLLECTION_STORY_SLOT_BUTTON_PLAY2.PointerClick.RemoveAllListeners();
				this.m_NKM_UI_COLLECTION_STORY_SLOT_BUTTON_PLAY2.PointerClick.AddListener(delegate()
				{
					this.CutScenePlay(2);
				});
			}
			base.gameObject.GetComponent<RectTransform>().localScale = Vector3.one;
			if (callback != null)
			{
				this.dOnPlayCutScene = callback;
			}
		}

		// Token: 0x0600906B RID: 36971 RVA: 0x0031304C File Offset: 0x0031124C
		public void SetData(int actID, int MissionID, int stageID, string name, bool bClear, string startStr, string endStr, bool bIsDive = false)
		{
			NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_STORY_SLOT_LIST_TEXT, !bIsDive);
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_SLOT_LIST_TEXT, actID.ToString() + "-" + MissionID.ToString());
			NKCUtil.SetLabelText(this.m_NKM_UI_COLLECTION_STORY_SLOT_LIST_TITLE_TEXT, name);
			bool bValue = !string.IsNullOrEmpty(startStr);
			bool bValue2 = !string.IsNullOrEmpty(endStr);
			if (bClear)
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_STORY_SLOT_TEXT_PART1, bValue);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_STORY_SLOT_TEXT_PART2, bValue2);
			}
			else
			{
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_STORY_SLOT_TEXT_PART1, bValue);
				NKCUtil.SetGameobjectActive(this.m_NKM_UI_COLLECTION_STORY_SLOT_TEXT_PART2, false);
			}
			this.m_StageID = stageID;
			this.m_beginStr = startStr;
			this.m_endStr = endStr;
		}

		// Token: 0x0600906C RID: 36972 RVA: 0x003130FC File Offset: 0x003112FC
		public void CutScenePlay(int part)
		{
			if (this.dOnPlayCutScene == null)
			{
				return;
			}
			if (part == 1)
			{
				this.dOnPlayCutScene(this.m_beginStr, this.m_StageID);
				return;
			}
			if (part == 2)
			{
				this.dOnPlayCutScene(this.m_endStr, this.m_StageID);
			}
		}

		// Token: 0x04007D8B RID: 32139
		public Text m_NKM_UI_COLLECTION_STORY_SLOT_LIST_TEXT;

		// Token: 0x04007D8C RID: 32140
		public Text m_NKM_UI_COLLECTION_STORY_SLOT_LIST_TITLE_TEXT;

		// Token: 0x04007D8D RID: 32141
		public NKCUIComStateButton m_NKM_UI_COLLECTION_STORY_SLOT_BUTTON_PLAY1;

		// Token: 0x04007D8E RID: 32142
		public NKCUIComStateButton m_NKM_UI_COLLECTION_STORY_SLOT_BUTTON_PLAY2;

		// Token: 0x04007D8F RID: 32143
		public GameObject m_NKM_UI_COLLECTION_STORY_SLOT_TEXT_PART1;

		// Token: 0x04007D90 RID: 32144
		public GameObject m_NKM_UI_COLLECTION_STORY_SLOT_TEXT_PART2;

		// Token: 0x04007D91 RID: 32145
		private int m_StageID;

		// Token: 0x04007D92 RID: 32146
		private string m_beginStr;

		// Token: 0x04007D93 RID: 32147
		private string m_endStr;

		// Token: 0x04007D94 RID: 32148
		private NKCUICollectionStory.OnPlayCutScene dOnPlayCutScene;
	}
}
