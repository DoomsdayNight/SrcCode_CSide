using System;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x020009EF RID: 2543
	public interface INKCUIStageViewer
	{
		// Token: 0x06006DEC RID: 28140
		Vector2 SetData(bool bUseEpSlot, int EpisodeID, int ActID, EPISODE_DIFFICULTY Difficulty, IDungeonSlot.OnSelectedItemSlot onSelectedSlot, EPISODE_SCROLL_TYPE scrollType);

		// Token: 0x06006DED RID: 28141
		void ResetPosition(Transform parent);

		// Token: 0x06006DEE RID: 28142
		int GetActCount(EPISODE_DIFFICULTY difficulty);

		// Token: 0x06006DEF RID: 28143
		void SetActive(bool bValue);

		// Token: 0x06006DF0 RID: 28144
		void SetSelectNode(NKMStageTempletV2 stageTemplet);

		// Token: 0x06006DF1 RID: 28145
		void RefreshData();

		// Token: 0x06006DF2 RID: 28146
		int GetCurActID();

		// Token: 0x06006DF3 RID: 28147
		float GetTargetNormalizedPos(int slotIndex);
	}
}
