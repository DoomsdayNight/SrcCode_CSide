using System;
using NKC.Loading;
using NKC.UI;
using NKM;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006FD RID: 1789
	public abstract class NKC_SCEN_BASIC
	{
		// Token: 0x06004622 RID: 17954 RVA: 0x00155180 File Offset: 0x00153380
		public NKM_SCEN_ID Get_NKM_SCEN_ID()
		{
			return this.m_NKM_SCEN_ID;
		}

		// Token: 0x06004623 RID: 17955 RVA: 0x00155188 File Offset: 0x00153388
		public NKC_SCEN_STATE Get_NKC_SCEN_STATE()
		{
			return this.m_NKC_SCEN_STATE;
		}

		// Token: 0x06004624 RID: 17956 RVA: 0x00155190 File Offset: 0x00153390
		public void Set_NKC_SCEN_STATE(NKC_SCEN_STATE eNKC_SCEN_STATE)
		{
			this.m_NKC_SCEN_STATE = eNKC_SCEN_STATE;
		}

		// Token: 0x06004625 RID: 17957 RVA: 0x00155199 File Offset: 0x00153399
		public NKC_SCEN_BASIC()
		{
		}

		// Token: 0x06004626 RID: 17958 RVA: 0x001551AC File Offset: 0x001533AC
		public virtual void ScenChangeStart()
		{
			Debug.LogFormat("{0}.ScenChangeStart", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_CHANGE_START;
			this.m_fLoadingProgress = 0f;
			NKCLoadingScreenManager.SetLoadingProgress(this.m_fLoadingProgress);
			this.ScenDataReq();
		}

		// Token: 0x06004627 RID: 17959 RVA: 0x00155200 File Offset: 0x00153400
		public virtual void ScenDataReq()
		{
			Debug.LogFormat("{0}.ScenDataReq", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_DATA_REQ_WAIT;
		}

		// Token: 0x06004628 RID: 17960 RVA: 0x0015522D File Offset: 0x0015342D
		public virtual void ScenDataReqWaitUpdate()
		{
			Debug.LogFormat("{0}.ScenDataReqWaitUpdate", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.ScenLoadUIStart();
		}

		// Token: 0x06004629 RID: 17961 RVA: 0x0015525C File Offset: 0x0015345C
		public virtual void ScenLoadUIStart()
		{
			Debug.LogFormat("{0}.ScenLoadUIStart", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_LOADING_UI;
			this.m_fLoadingProgress = 0.1f;
			NKCLoadingScreenManager.SetLoadingProgress(this.m_fLoadingProgress);
		}

		// Token: 0x0600462A RID: 17962 RVA: 0x001552AC File Offset: 0x001534AC
		public virtual void ScenLoadUIUpdate()
		{
			if (NKCAssetResourceManager.IsLoadEnd())
			{
				this.m_LoadUICompleteWaitCount = 0;
				this.ScenLoadUICompleteWait();
			}
			this.m_fLoadingProgress = 0.1f;
			this.m_fLoadingProgress += NKCAssetResourceManager.GetLoadProgress() * 0.1f;
			NKCLoadingScreenManager.SetLoadingProgress(this.m_fLoadingProgress);
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x001552FB File Offset: 0x001534FB
		public virtual void ScenLoadUICompleteWait()
		{
			if (this.m_LoadUICompleteWaitCount > 0)
			{
				this.ScenLoadUIComplete();
				return;
			}
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_LOADING_UI_COMPLETE_WAIT;
			this.m_LoadUICompleteWaitCount++;
			this.m_fLoadingProgress = 0.4f;
			NKCLoadingScreenManager.SetLoadingProgress(this.m_fLoadingProgress);
		}

		// Token: 0x0600462C RID: 17964 RVA: 0x00155338 File Offset: 0x00153538
		public virtual void ScenLoadUIComplete()
		{
			Debug.LogFormat("{0}.ScenLoadUIComplete", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_LOADING;
		}

		// Token: 0x0600462D RID: 17965 RVA: 0x00155365 File Offset: 0x00153565
		public virtual void ScenLoadUpdate()
		{
			if (NKCAssetResourceManager.IsLoadEnd())
			{
				this.ScenLoadLastStart();
			}
			this.m_fLoadingProgress = 0.4f;
			this.m_fLoadingProgress += NKCAssetResourceManager.GetLoadProgress() * 0.1f;
			NKCLoadingScreenManager.SetLoadingProgress(this.m_fLoadingProgress);
		}

		// Token: 0x0600462E RID: 17966 RVA: 0x001553A2 File Offset: 0x001535A2
		public virtual void ScenLoadLastStart()
		{
			Debug.LogFormat("{0}.ScenLoadLastStart", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_LOADING_LAST;
		}

		// Token: 0x0600462F RID: 17967 RVA: 0x001553D0 File Offset: 0x001535D0
		public virtual void ScenLoadLastUpdate()
		{
			if (NKCScenManager.GetScenManager().GetObjectPool().IsLoadComplete())
			{
				this.m_LoadCompleteWaitCount = 0;
				this.ScenLoadCompleteWait();
			}
			this.m_fLoadingProgress = 0.7f;
			this.m_fLoadingProgress += NKCScenManager.GetScenManager().GetObjectPool().GetLoadProgress() * 0.2f;
			NKCLoadingScreenManager.SetLoadingProgress(this.m_fLoadingProgress);
		}

		// Token: 0x06004630 RID: 17968 RVA: 0x00155433 File Offset: 0x00153633
		public virtual void ScenLoadCompleteWait()
		{
			if (this.m_LoadCompleteWaitCount > 0)
			{
				this.ScenLoadComplete();
				return;
			}
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_LOADING_COMPLETE_WAIT;
			this.m_LoadCompleteWaitCount++;
			this.m_fLoadingProgress = 1f;
			NKCLoadingScreenManager.SetLoadingProgress(this.m_fLoadingProgress);
		}

		// Token: 0x06004631 RID: 17969 RVA: 0x00155470 File Offset: 0x00153670
		public virtual void ScenLoadComplete()
		{
			Debug.LogFormat("{0}.ScenLoadComplete", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_LOADING_COMPLETE;
			NKCUIVoiceManager.CleanUp();
			NKCResourceUtility.SwapResource();
			this.m_bLoadedUI = true;
		}

		// Token: 0x06004632 RID: 17970 RVA: 0x001554B0 File Offset: 0x001536B0
		public virtual void ScenStart()
		{
			Debug.LogFormat("{0}.ScenStart", new object[]
			{
				this.m_NKM_SCEN_ID.ToString()
			});
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_START;
			this.PlayScenMusic();
			NKCUIManager.OnSceneOpenComplete();
			if (this.m_NKM_SCEN_ID != NKM_SCEN_ID.NSI_HOME)
			{
				NKCUIFadeInOut.FadeIn(0.1f, null, false);
				return;
			}
			NKCUIFadeInOut.FadeIn(0.01f, null, false);
		}

		// Token: 0x06004633 RID: 17971 RVA: 0x00155516 File Offset: 0x00153716
		public virtual void PlayScenMusic()
		{
			NKCSoundManager.PlayScenMusic(this.m_NKM_SCEN_ID, false);
		}

		// Token: 0x06004634 RID: 17972 RVA: 0x00155524 File Offset: 0x00153724
		public virtual void ScenEnd()
		{
			this.m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_END;
		}

		// Token: 0x06004635 RID: 17973 RVA: 0x0015552E File Offset: 0x0015372E
		public virtual void ScenUpdate()
		{
		}

		// Token: 0x06004636 RID: 17974 RVA: 0x00155530 File Offset: 0x00153730
		public virtual bool ScenMsgProc(NKCMessageData cNKCMessageData)
		{
			return false;
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x00155533 File Offset: 0x00153733
		public virtual bool GoBack()
		{
			return false;
		}

		// Token: 0x06004638 RID: 17976 RVA: 0x00155536 File Offset: 0x00153736
		public virtual void UnloadUI()
		{
			this.m_bLoadedUI = false;
		}

		// Token: 0x04003763 RID: 14179
		protected NKM_SCEN_ID m_NKM_SCEN_ID;

		// Token: 0x04003764 RID: 14180
		protected NKC_SCEN_STATE m_NKC_SCEN_STATE = NKC_SCEN_STATE.NSS_END;

		// Token: 0x04003765 RID: 14181
		protected int m_LoadUICompleteWaitCount;

		// Token: 0x04003766 RID: 14182
		protected int m_LoadCompleteWaitCount;

		// Token: 0x04003767 RID: 14183
		protected bool m_bLoadedUI;

		// Token: 0x04003768 RID: 14184
		protected float m_fLoadingProgress;
	}
}
