using System;
using System.Collections;
using AssetBundles;
using Cs.Logging;
using UnityEngine;
using UnityEngine.Video;

namespace NKC.Patcher
{
	// Token: 0x02000887 RID: 2183
	public class NKCPatcherBGVideoPlayer : MonoBehaviour
	{
		// Token: 0x060056EC RID: 22252 RVA: 0x001A2957 File Offset: 0x001A0B57
		public void SetAction(Action<bool> onAction)
		{
			this._onAction = onAction;
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x001A2960 File Offset: 0x001A0B60
		public void PlayVideo()
		{
			if (this._coroutine != null)
			{
				base.StopCoroutine(this._coroutine);
			}
			this._coroutine = base.StartCoroutine(this.Play());
		}

		// Token: 0x060056EE RID: 22254 RVA: 0x001A2988 File Offset: 0x001A0B88
		private string GetRawPatchMoviePath(string movieAssetPath)
		{
			string text = AssetBundleManager.GetLocalDownloadPath() ?? "";
			if (NKCDefineManager.DEFINE_UNITY_EDITOR())
			{
				text += "/";
			}
			string text2 = text + "ASSET_RAW/";
			Debug.Log("[PatchVideoPlayer] localRawPath : " + text2);
			foreach (string str in AssetBundleManager.GetMergedVariantString(movieAssetPath))
			{
				string text3 = text2 + str;
				if (NKCPatchUtility.IsFileExists(text3))
				{
					string name = text3.Replace(text, "");
					NKCPatchInfo defaultDownloadHistoryPatchInfo = PatchManifestManager.BasePatchInfoController.GetDefaultDownloadHistoryPatchInfo();
					if (defaultDownloadHistoryPatchInfo != null && defaultDownloadHistoryPatchInfo.PatchInfoExists(name))
					{
						Debug.Log("[PatchVideoPlayer] Exist in downloadedHistoryPatchInfo : " + text2);
						return text3;
					}
					NKCPatchInfo curPatchInfo = PatchManifestManager.BasePatchInfoController.GetCurPatchInfo();
					if (curPatchInfo != null && curPatchInfo.PatchInfoExists(name))
					{
						Debug.Log("[PatchVideoPlayer] Exist in currentPatchInfo : " + text2);
						return text3;
					}
				}
			}
			return string.Empty;
		}

		// Token: 0x060056EF RID: 22255 RVA: 0x001A2AA0 File Offset: 0x001A0CA0
		private IEnumerator Play()
		{
			Log.Debug("[BG Video Play]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchChecker/NKCPatcherBGVideoPlayer.cs", 78);
			if (this.m_videoPlayer == null)
			{
				Action<bool> onAction = this._onAction;
				if (onAction != null)
				{
					onAction(true);
				}
				yield break;
			}
			string text = AssetBundleManager.GetRawFilePath("Movie/PatchMovieHiRes.mp4");
			bool flag = NKCPatchDownloader.Instance.IsFileWillDownloaded("ASSET_RAW/Movie/PatchMovieHiRes.mp4");
			Debug.Log(string.Format("Hi-res movie path[{0}] moviePatch[{1}]", text, flag));
			string rawPatchMoviePath = this.GetRawPatchMoviePath("Movie/PatchMovieHiRes.mp4");
			if (!string.IsNullOrEmpty(rawPatchMoviePath))
			{
				text = rawPatchMoviePath;
			}
			if (string.IsNullOrEmpty(text) || flag)
			{
				if (string.IsNullOrEmpty(text))
				{
					Debug.Log("Hi-res movie not found. playing default movie");
				}
				else
				{
					Debug.Log("Hi-res movie found but will updated. playing default movie");
				}
				this.m_videoPlayer.clip = Resources.Load<VideoClip>("PatchMovie");
				if (this.m_videoPlayer.clip == null)
				{
					Action<bool> onAction2 = this._onAction;
					if (onAction2 != null)
					{
						onAction2(true);
					}
					yield break;
				}
			}
			else
			{
				Debug.Log("playing hi-res movie");
				this.m_videoPlayer.url = text;
			}
			bool flag2 = PlayerPrefs.GetInt("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_MUTE", 0) == 1;
			float num = 1f;
			string @string = PlayerPrefs.GetString("NKM_LOCAL_SAVE_GAME_OPTION_SOUND_VOLUMES", "");
			if (@string != "")
			{
				int num2 = 3;
				string[] array = @string.Split(new char[]
				{
					':'
				});
				int num3;
				if (array.Length > num2 && int.TryParse(array[num2], out num3))
				{
					num = (float)num3 / 100f;
				}
			}
			float volume = flag2 ? 0f : num;
			this.m_videoPlayer.renderMode = VideoRenderMode.CameraFarPlane;
			this.m_videoPlayer.enabled = true;
			this.m_videoPlayer.isLooping = true;
			this.m_videoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
			this.m_videoPlayer.EnableAudioTrack(0, true);
			this.m_videoPlayer.controlledAudioTrackCount = 1;
			this.m_videoPlayer.SetDirectAudioVolume(0, volume);
			this.m_videoPlayer.SetDirectAudioMute(0, !Application.isFocused);
			this.m_videoPlayer.playbackSpeed = 1f;
			this.m_videoPlayer.Prepare();
			while (!this.m_videoPlayer.isPrepared)
			{
				yield return null;
			}
			this.m_videoPlayer.Play();
			Debug.Log("[VideoPlayer] Play");
			Action<bool> onAction3 = this._onAction;
			if (onAction3 != null)
			{
				onAction3(false);
			}
			yield return null;
			yield break;
		}

		// Token: 0x060056F0 RID: 22256 RVA: 0x001A2AAF File Offset: 0x001A0CAF
		public void StopBG()
		{
			Log.Debug("[BG Video Stop]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/Patcher/PatchChecker/NKCPatcherBGVideoPlayer.cs", 162);
			Coroutine coroutine = this._coroutine;
			if (this.m_videoPlayer != null)
			{
				this.m_videoPlayer.Stop();
			}
		}

		// Token: 0x060056F1 RID: 22257 RVA: 0x001A2AE5 File Offset: 0x001A0CE5
		public void OnFocus(bool focus)
		{
			if (this.m_videoPlayer != null)
			{
				this.m_videoPlayer.SetDirectAudioMute(0, !focus);
			}
		}

		// Token: 0x040044F1 RID: 17649
		[Header("���� �÷��̾�")]
		[SerializeField]
		public VideoPlayer m_videoPlayer;

		// Token: 0x040044F2 RID: 17650
		private Coroutine _coroutine;

		// Token: 0x040044F3 RID: 17651
		private Action<bool> _onAction;
	}
}
