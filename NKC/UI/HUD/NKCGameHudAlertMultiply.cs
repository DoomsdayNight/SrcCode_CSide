using System;
using NKM;
using NKM.Templet;
using UnityEngine;

namespace NKC.UI.HUD
{
	// Token: 0x02000C3E RID: 3134
	public class NKCGameHudAlertMultiply : MonoBehaviour, IGameHudAlert
	{
		// Token: 0x0600922C RID: 37420 RVA: 0x0031E19D File Offset: 0x0031C39D
		public bool IsFinished()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600922D RID: 37421 RVA: 0x0031E1A4 File Offset: 0x0031C3A4
		public void OnCleanup()
		{
			if (this.m_NKCAssetInstanceDataMultiplyReward != null)
			{
				NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataMultiplyReward);
			}
			this.m_NKCAssetInstanceDataMultiplyReward = null;
			this.m_NKCUIGameHUDMultiplyReward = null;
		}

		// Token: 0x0600922E RID: 37422 RVA: 0x0031E1C7 File Offset: 0x0031C3C7
		public void OnStart()
		{
			NKCUtil.SetGameobjectActive(this.m_NKCUIGameHUDMultiplyReward, true);
		}

		// Token: 0x0600922F RID: 37423 RVA: 0x0031E1D5 File Offset: 0x0031C3D5
		public void OnUpdate()
		{
		}

		// Token: 0x06009230 RID: 37424 RVA: 0x0031E1D7 File Offset: 0x0031C3D7
		public void SetActive(bool value)
		{
			NKCUtil.SetGameobjectActive(base.gameObject, value);
		}

		// Token: 0x06009231 RID: 37425 RVA: 0x0031E1E5 File Offset: 0x0031C3E5
		public void SetMultiply(int count)
		{
			if (this.m_NKCUIGameHUDMultiplyReward != null)
			{
				this.m_NKCUIGameHUDMultiplyReward.SetMultiply(count);
			}
		}

		// Token: 0x06009232 RID: 37426 RVA: 0x0031E204 File Offset: 0x0031C404
		public void LoadMultiplyReward(NKMGameData cNKMGameData)
		{
			if (cNKMGameData == null)
			{
				return;
			}
			if (!NKCContentManager.IsContentsUnlocked(ContentsType.OPERATION_MULTIPLY, 0, 0))
			{
				return;
			}
			if (cNKMGameData.GetGameType() == NKM_GAME_TYPE.NGT_PRACTICE)
			{
				return;
			}
			if (this.m_NKCUIGameHUDMultiplyReward == null)
			{
				if (this.m_NKCAssetInstanceDataMultiplyReward != null)
				{
					NKCAssetResourceManager.CloseInstance(this.m_NKCAssetInstanceDataMultiplyReward);
				}
				this.m_NKCAssetInstanceDataMultiplyReward = NKCAssetResourceManager.OpenInstance<GameObject>("AB_UI_NKM_UI_HUD_RENEWAL", "AB_UI_GAME_HUD_MULTIPLY_REWARD", false, null);
				this.m_NKCUIGameHUDMultiplyReward = this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.GetComponent<NKCUIGameHUDMultiplyReward>();
				this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.transform.SetParent(base.transform, false);
				this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.transform.localPosition = new Vector3(this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.transform.localPosition.x, this.m_NKCAssetInstanceDataMultiplyReward.m_Instant.transform.localPosition.y, 0f);
				NKCUtil.SetGameobjectActive(this.m_NKCUIGameHUDMultiplyReward, false);
			}
		}

		// Token: 0x04007F1B RID: 32539
		private NKCUIGameHUDMultiplyReward m_NKCUIGameHUDMultiplyReward;

		// Token: 0x04007F1C RID: 32540
		private NKCAssetInstanceData m_NKCAssetInstanceDataMultiplyReward;
	}
}
