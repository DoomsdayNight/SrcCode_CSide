using System;
using System.Collections;
using System.Collections.Generic;
using Cs.Logging;
using UnityEngine;

namespace NKC.Patcher
{
	// Token: 0x02000886 RID: 2182
	public class NKCPatchChecker : MonoBehaviour, IPatcher
	{
		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060056DF RID: 22239 RVA: 0x001A25F3 File Offset: 0x001A07F3
		public static NKCPatcherUI PatcherUI
		{
			get
			{
				if (NKCPatchChecker.m_instance == null)
				{
					return null;
				}
				return NKCPatchChecker.m_instance.m_patcherUI;
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060056E0 RID: 22240 RVA: 0x001A260E File Offset: 0x001A080E
		public static NKCPatcherBGVideoPlayer PatcherVideoPlayer
		{
			get
			{
				if (NKCPatchChecker.m_instance == null)
				{
					return null;
				}
				return NKCPatchChecker.m_instance.m_patcherVideoPlayer;
			}
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x001A262C File Offset: 0x001A082C
		private void Awake()
		{
			NKCPatchChecker.m_instance = this;
			if (NKCPatchChecker.PatcherVideoPlayer == null)
			{
				Log.Error("PatcherVideoPlayer is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchChecker/NKCPatchChecker.cs", 54);
				return;
			}
			if (NKCPatchChecker.PatcherUI == null)
			{
				Log.Error("PatcherUI is null", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchChecker/NKCPatchChecker.cs", 60);
				return;
			}
			NKCPatchChecker.PatcherVideoPlayer.SetAction(new Action<bool>(NKCPatchChecker.PatcherUI.OnInvalidPatcherVideoPlayer));
			this.RegisterPatchProcess();
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x001A26A0 File Offset: 0x001A08A0
		public void RegisterPatchProcess()
		{
			this._patchProcessModules.Clear();
			this._patchProcessModules.Add(new WaitForUnityEditorPatchSkip());
			this._patchProcessModules.Add(new WaitForInternetConnection());
			this._patchProcessModules.Add(new WaitForDownLoaderInitialization());
			this._patchProcessModules.Add(new WaitForAppVersionCheckStatus());
			this._patchProcessModules.Add(new WaitForAssetBundleVersionCheckStatus());
			this._patchProcessModules.Add(new WaitForDownloadStatus());
			this._patchProcessModules.Add(new WaitForTouch());
			this._patchProcessModules.Add(new WaitForAssetBundleInitialize());
			this._patchProcessModules.Add(new WaitForCheckVersion());
		}

		// Token: 0x060056E3 RID: 22243 RVA: 0x001A2748 File Offset: 0x001A0948
		private void OnApplicationFocus(bool focus)
		{
			if (NKCPatchChecker.PatcherVideoPlayer != null)
			{
				NKCPatcherBGVideoPlayer patcherVideoPlayer = NKCPatchChecker.PatcherVideoPlayer;
				if (patcherVideoPlayer == null)
				{
					return;
				}
				patcherVideoPlayer.OnFocus(focus);
			}
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x001A2767 File Offset: 0x001A0967
		public void SetActive(bool active)
		{
			NKCUtil.SetGameobjectActive(this, active);
			NKCPatchChecker.PatcherUI.SetActive(active);
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060056E5 RID: 22245 RVA: 0x001A277D File Offset: 0x001A097D
		public bool PatchSuccess
		{
			get
			{
				return this._patchProcessModules.Find((IPatchProcessStrategy x) => x.Status == IPatchProcessStrategy.ExecutionStatus.Fail) == null;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x060056E6 RID: 22246 RVA: 0x001A27AC File Offset: 0x001A09AC
		public string ReasonOfFailure
		{
			get
			{
				IPatchProcessStrategy patchProcessStrategy = this._patchProcessModules.Find((IPatchProcessStrategy x) => x.Status == IPatchProcessStrategy.ExecutionStatus.Fail);
				if (patchProcessStrategy == null)
				{
					return null;
				}
				return patchProcessStrategy.ReasonOfFailure;
			}
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x001A27E3 File Offset: 0x001A09E3
		public IEnumerator ProcessPatch()
		{
			foreach (IPatchProcessStrategy patchProcessModule in this._patchProcessModules)
			{
				Log.Debug(string.Format("[{0}][{1}] Start", "ProcessPatch", patchProcessModule.GetType()), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchChecker/NKCPatchChecker.cs", 123);
				yield return patchProcessModule.GetEnumerator();
				if (patchProcessModule.ErrorOccurred() || patchProcessModule.SkipNextProcess())
				{
					break;
				}
				patchProcessModule = null;
			}
			List<IPatchProcessStrategy>.Enumerator enumerator = default(List<IPatchProcessStrategy>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x001A27F4 File Offset: 0x001A09F4
		public static void StopBGM()
		{
			if (NKCPatchChecker.m_instance == null)
			{
				return;
			}
			if (NKCPatchChecker.m_instance.m_audioSource == null || NKCPatchChecker.m_instance.m_ambientBGM == null)
			{
				return;
			}
			NKCPatchChecker.m_instance.m_audioSource.Stop();
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x001A2844 File Offset: 0x001A0A44
		public void PlayBGM()
		{
			if (this.m_audioSource == null || this.m_ambientBGM == null)
			{
				return;
			}
			Log.Debug("[PatcherManager] Play BGM", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchChecker/NKCPatchChecker.cs", 159);
			this.m_audioSource.loop = true;
			this.m_audioSource.clip = this.m_ambientBGM;
			this.m_audioSource.volume = NKCPatchChecker.GetVolume();
			this.m_audioSource.enabled = true;
			this.m_audioSource.Play();
		}

		// Token: 0x060056EA RID: 22250 RVA: 0x001A28C8 File Offset: 0x001A0AC8
		public static float GetVolume()
		{
			bool flag = PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_MUTE", 0) == 1;
			float result = 1f;
			string @string = PlayerPrefs.GetString("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_VOLUMES", "");
			if (@string != "")
			{
				int num = 3;
				string[] array = @string.Split(new char[]
				{
					':'
				});
				int num2;
				if (array.Length > num && int.TryParse(array[num], out num2))
				{
					result = (float)num2 / 100f;
				}
			}
			if (!flag)
			{
				return result;
			}
			return 0f;
		}

		// Token: 0x040044EB RID: 17643
		public static NKCPatchChecker m_instance;

		// Token: 0x040044EC RID: 17644
		public AudioSource m_audioSource;

		// Token: 0x040044ED RID: 17645
		public AudioClip m_ambientBGM;

		// Token: 0x040044EE RID: 17646
		[SerializeField]
		public NKCPatcherBGVideoPlayer m_patcherVideoPlayer;

		// Token: 0x040044EF RID: 17647
		[SerializeField]
		public NKCPatcherUI m_patcherUI;

		// Token: 0x040044F0 RID: 17648
		private readonly List<IPatchProcessStrategy> _patchProcessModules = new List<IPatchProcessStrategy>();
	}
}
