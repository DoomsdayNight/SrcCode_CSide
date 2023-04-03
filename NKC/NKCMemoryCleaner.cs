using System;
using System.Collections;
using NKC.UI;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006A4 RID: 1700
	public class NKCMemoryCleaner : MonoBehaviour
	{
		// Token: 0x0600381B RID: 14363 RVA: 0x00122262 File Offset: 0x00120462
		public void Clean(NKCMemoryCleaner.OnComplete _OnComplete = null, NKCUIManager.eUIUnloadFlag eUIUnloadFlag = NKCUIManager.eUIUnloadFlag.DEFAULT, bool bDontCloseOpenedUI = true)
		{
			if (this.m_bWaitingMemCleaning)
			{
				Debug.LogWarning("Waiting NKC Memory Cleaning");
				return;
			}
			this.m_OnComplete = _OnComplete;
			this.m_eUIUnloadFlag = eUIUnloadFlag;
			this.m_bDontCloseOpenedUI = bDontCloseOpenedUI;
			base.StartCoroutine(this.Clean_());
		}

		// Token: 0x0600381C RID: 14364 RVA: 0x00122299 File Offset: 0x00120499
		public void UnloadObjectPool()
		{
			if (this.m_bWaitingMemCleaning)
			{
				Debug.LogWarning("Waiting NKC Memory Cleaning");
				return;
			}
			if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetObjectPool() != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().Unload();
			}
		}

		// Token: 0x0600381D RID: 14365 RVA: 0x001222D6 File Offset: 0x001204D6
		public void DoUnloadUnusedAssetsAndGC()
		{
			if (this.m_bWaitingMemCleaning)
			{
				return;
			}
			base.StartCoroutine(this.DoUnloadUnusedAssetsAndGC_());
		}

		// Token: 0x0600381E RID: 14366 RVA: 0x001222EE File Offset: 0x001204EE
		private IEnumerator DoUnloadUnusedAssetsAndGC_()
		{
			this.m_bWaitingMemCleaning = true;
			yield return new WaitForEndOfFrame();
			AsyncOperation async = Resources.UnloadUnusedAssets();
			while (!async.isDone)
			{
				yield return null;
			}
			GC.Collect();
			GC.WaitForPendingFinalizers();
			this.m_bWaitingMemCleaning = false;
			yield break;
		}

		// Token: 0x0600381F RID: 14367 RVA: 0x001222FD File Offset: 0x001204FD
		private IEnumerator Clean_()
		{
			this.m_bWaitingMemCleaning = true;
			Debug.Log("NKCMemoryCleaner Clean Start");
			NKCUIManager.UnloadAllUI(this.m_eUIUnloadFlag, this.m_bDontCloseOpenedUI);
			NKCSoundManager.Unload();
			if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetObjectPool() != null)
			{
				NKCScenManager.GetScenManager().GetObjectPool().Unload();
			}
			yield return new WaitForEndOfFrame();
			AsyncOperation async = Resources.UnloadUnusedAssets();
			while (!async.isDone)
			{
				yield return null;
			}
			GC.Collect();
			GC.WaitForPendingFinalizers();
			Debug.Log("NKCMemoryCleaner Clean Finish");
			this.m_bWaitingMemCleaning = false;
			if (this.m_OnComplete != null)
			{
				this.m_OnComplete();
				this.m_OnComplete = null;
			}
			yield break;
		}

		// Token: 0x04003499 RID: 13465
		public bool m_bWaitingMemCleaning;

		// Token: 0x0400349A RID: 13466
		private NKCUIManager.eUIUnloadFlag m_eUIUnloadFlag;

		// Token: 0x0400349B RID: 13467
		private bool m_bDontCloseOpenedUI = true;

		// Token: 0x0400349C RID: 13468
		private NKCMemoryCleaner.OnComplete m_OnComplete;

		// Token: 0x02001368 RID: 4968
		// (Invoke) Token: 0x0600A5DE RID: 42462
		public delegate void OnComplete();
	}
}
