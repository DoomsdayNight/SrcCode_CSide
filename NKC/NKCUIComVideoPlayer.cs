using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetBundles;
using NKC.UI;
using NKM;
using NKM.Templet.Base;
using UnityEngine;
using UnityEngine.Video;

namespace NKC
{
	// Token: 0x02000769 RID: 1897
	[RequireComponent(typeof(VideoPlayer))]
	public abstract class NKCUIComVideoPlayer : MonoBehaviour
	{
		// Token: 0x06004BAF RID: 19375 RVA: 0x0016A800 File Offset: 0x00168A00
		public static void OnUpdateVolume()
		{
			foreach (NKCUIComVideoPlayer nkcuicomVideoPlayer in NKCUIComVideoPlayer.s_setVideoPlayers)
			{
				if (nkcuicomVideoPlayer != null)
				{
					nkcuicomVideoPlayer.VolumeUpdated();
				}
			}
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0016A85C File Offset: 0x00168A5C
		private void OnDestroy()
		{
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x0016A85E File Offset: 0x00168A5E
		protected void Register()
		{
			NKCUIComVideoPlayer.s_setVideoPlayers.Add(this);
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x0016A86C File Offset: 0x00168A6C
		protected void Unregister()
		{
			if (NKCUIComVideoPlayer.s_setVideoPlayers != null)
			{
				int count = NKCUIComVideoPlayer.s_setVideoPlayers.Count;
			}
			NKCUIComVideoPlayer.s_setVideoPlayers.Remove(this);
		}

		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x0016A88C File Offset: 0x00168A8C
		protected VideoPlayer VideoPlayer
		{
			get
			{
				if (this.m_VideoPlayer == null)
				{
					this.m_VideoPlayer = base.GetComponent<VideoPlayer>();
					this.m_VideoPlayer.aspectRatio = VideoAspectRatio.FitOutside;
					this.m_VideoPlayer.enabled = false;
				}
				return this.m_VideoPlayer;
			}
		}

		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06004BB4 RID: 19380 RVA: 0x0016A8C8 File Offset: 0x00168AC8
		private bool IsPrepared
		{
			get
			{
				if (!this.m_bForcePlay)
				{
					NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
					if (gameOptionData != null && !gameOptionData.UseVideoTexture)
					{
						return true;
					}
				}
				return this.VideoPlayer != null && this.VideoPlayer.isPrepared;
			}
		}

		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06004BB5 RID: 19381 RVA: 0x0016A910 File Offset: 0x00168B10
		public bool IsPlaying
		{
			get
			{
				return this.VideoPlayer != null && this.VideoPlayer.isPlaying;
			}
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x0016A92D File Offset: 0x00168B2D
		public bool IsPlayingOrPreparing()
		{
			return !(this.VideoPlayer == null) && (this.VideoPlayer.isPlaying || this.m_VideoState == NKCUIComVideoPlayer.VideoState.PreparingPlay);
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x0016A95A File Offset: 0x00168B5A
		public bool IsPreparing()
		{
			return this.m_VideoState == NKCUIComVideoPlayer.VideoState.PreparingPlay;
		}

		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06004BB8 RID: 19384 RVA: 0x0016A965 File Offset: 0x00168B65
		public bool PlayOnAwake
		{
			get
			{
				return this.VideoPlayer != null && this.VideoPlayer.playOnAwake;
			}
		}

		// Token: 0x06004BB9 RID: 19385 RVA: 0x0016A982 File Offset: 0x00168B82
		public void SetCamera(Camera cam)
		{
			this.VideoPlayer.targetCamera = cam;
		}

		// Token: 0x06004BBA RID: 19386 RVA: 0x0016A990 File Offset: 0x00168B90
		public void SetAlpha(float fAlpha)
		{
			if (this.VideoPlayer != null)
			{
				this.VideoPlayer.targetCameraAlpha = fAlpha;
			}
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x0016A9AC File Offset: 0x00168BAC
		private void Start()
		{
			this.Register();
			if (this.PlayOnAwake && this.m_VideoState == NKCUIComVideoPlayer.VideoState.Stop)
			{
				this.Play(this.VideoPlayer.isLooping, this.m_bUseSound, null, false);
			}
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0016A9DD File Offset: 0x00168BDD
		public void Prepare(string rawVideoFileName)
		{
			this.m_sFilename = rawVideoFileName;
			this.Prepare();
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x0016A9EC File Offset: 0x00168BEC
		public virtual void Prepare()
		{
			if (!this.m_bForcePlay)
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData != null && !gameOptionData.UseVideoTexture)
				{
					return;
				}
			}
			if (this.VideoPlayer.isPlaying)
			{
				this.VideoPlayer.Stop();
			}
			string rawFilePath = AssetBundleManager.GetRawFilePath("Movie/" + this.m_sFilename);
			if (!this.VideoPlayer.isPrepared || this.VideoPlayer.url != rawFilePath)
			{
				this.VideoPlayer.enabled = true;
				this.VideoPlayer.url = rawFilePath;
				this.VideoPlayer.audioOutputMode = VideoAudioOutputMode.Direct;
				this.VideoPlayer.EnableAudioTrack(0, this.m_bUseSound);
				this.VideoPlayer.controlledAudioTrackCount = (this.m_bUseSound ? 1 : 0);
				this.VideoPlayer.SetDirectAudioVolume(0, NKCSoundManager.GetFinalVol(0f, 0f, 1f));
				this.VideoPlayer.SetDirectAudioMute(0, !Application.isFocused);
				this.VideoPlayer.Prepare();
			}
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0016AAF5 File Offset: 0x00168CF5
		protected virtual void OnStateChange(NKCUIComVideoPlayer.VideoState state)
		{
			this.m_VideoState = state;
		}

		// Token: 0x06004BBF RID: 19391 RVA: 0x0016AAFE File Offset: 0x00168CFE
		public virtual void CleanUp()
		{
			this.Unregister();
			this.SetAlpha(1f);
			if (this.VideoPlayer != null)
			{
				this.VideoPlayer.Stop();
				this.VideoPlayer.enabled = false;
			}
		}

		// Token: 0x06004BC0 RID: 19392 RVA: 0x0016AB36 File Offset: 0x00168D36
		protected virtual bool CanPlayVideo()
		{
			return true;
		}

		// Token: 0x06004BC1 RID: 19393 RVA: 0x0016AB39 File Offset: 0x00168D39
		public void Play(string rawVideoFileName, bool bLoop, bool bPlaySound = false, NKCUIComVideoPlayer.VideoPlayMessageCallback videoPlayMessageCallback = null, bool bForcePlay = false)
		{
			if (this.IsPlaying && rawVideoFileName == this.m_sFilename)
			{
				return;
			}
			this.m_sFilename = rawVideoFileName;
			this.Play(bLoop, bPlaySound, videoPlayMessageCallback, bForcePlay);
		}

		// Token: 0x06004BC2 RID: 19394 RVA: 0x0016AB68 File Offset: 0x00168D68
		public void Play(bool bLoop, bool bPlaySound = false, NKCUIComVideoPlayer.VideoPlayMessageCallback videoPlayMessageCallback = null, bool bForcePlay = false)
		{
			this.Register();
			this.PrepareMovieCaption();
			this.m_bForcePlay = bForcePlay;
			if (!bForcePlay)
			{
				NKCGameOptionData gameOptionData = NKCScenManager.GetScenManager().GetGameOptionData();
				if (gameOptionData != null && !gameOptionData.UseVideoTexture)
				{
					this.OnStateChange(NKCUIComVideoPlayer.VideoState.Stop);
					if (videoPlayMessageCallback != null)
					{
						videoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage.PlayComplete);
					}
					return;
				}
			}
			if (!base.gameObject.activeInHierarchy || !this.CanPlayVideo())
			{
				this.OnStateChange(NKCUIComVideoPlayer.VideoState.Stop);
				if (videoPlayMessageCallback != null)
				{
					videoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage.PlayComplete);
				}
				return;
			}
			if (this.VideoPlayer.isPlaying)
			{
				this.VideoPlayer.Stop();
			}
			this.m_bUseSound = bPlaySound;
			this.dVideoPlayMessageCallback = videoPlayMessageCallback;
			this.VideoPlayer.isLooping = bLoop;
			base.StopAllCoroutines();
			this.OnStateChange(NKCUIComVideoPlayer.VideoState.PreparingPlay);
			base.StartCoroutine(this.VideoPlayProcess());
		}

		// Token: 0x06004BC3 RID: 19395 RVA: 0x0016AC28 File Offset: 0x00168E28
		private void PrepareMovieCaption()
		{
			if (string.IsNullOrEmpty(this.m_sFilename))
			{
				return;
			}
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this.m_sFilename);
			if (string.IsNullOrEmpty(fileNameWithoutExtension))
			{
				return;
			}
			if (!AssetBundleManager.IsAssetExists("ab_script_movie_caption", "lua_" + fileNameWithoutExtension))
			{
				return;
			}
			NKMTempletContainer<NKCMovieCaptionTemplet>.Load("ab_script_movie_caption", "lua_" + fileNameWithoutExtension, "MOVIE_CAPTION", new Func<NKMLua, NKCMovieCaptionTemplet>(NKCMovieCaptionTemplet.LoadFromLUA));
			if (NKMTempletContainer<NKCMovieCaptionTemplet>.Values.Count<NKCMovieCaptionTemplet>() <= 0)
			{
				Debug.Log("NKCUIComVideoPlayer::PrepareMovieCaption() : lua_" + fileNameWithoutExtension + " - dont have caption");
				return;
			}
			List<NKCMovieCaptionTemplet> list = NKMTempletContainer<NKCMovieCaptionTemplet>.Values.ToList<NKCMovieCaptionTemplet>();
			list.Sort((NKCMovieCaptionTemplet x, NKCMovieCaptionTemplet y) => x.m_StartSecond.CompareTo(y.m_StartSecond));
			List<NKCUIComCaption.CaptionDataTime> list2 = new List<NKCUIComCaption.CaptionDataTime>();
			int num = 0;
			foreach (NKCMovieCaptionTemplet nkcmovieCaptionTemplet in list)
			{
				num++;
				string caption = nkcmovieCaptionTemplet.m_Caption;
				if (!string.IsNullOrEmpty(NKCStringTable.GetString(nkcmovieCaptionTemplet.m_StringKey, false)))
				{
					caption = NKCStringTable.GetString(nkcmovieCaptionTemplet.m_StringKey, false);
				}
				list2.Add(new NKCUIComCaption.CaptionDataTime(caption, num, (long)nkcmovieCaptionTemplet.m_StartSecond, (long)nkcmovieCaptionTemplet.m_ShowSecond, nkcmovieCaptionTemplet.m_bHideBackground));
			}
			NKCUIManager.NKCUIOverlayCaption.OpenCaption((from x in list2
			orderby x.startTime
			select x).ToList<NKCUIComCaption.CaptionDataTime>());
		}

		// Token: 0x06004BC4 RID: 19396 RVA: 0x0016ADB4 File Offset: 0x00168FB4
		public void InvalidateCallBack()
		{
			this.dVideoPlayMessageCallback = null;
		}

		// Token: 0x06004BC5 RID: 19397 RVA: 0x0016ADBD File Offset: 0x00168FBD
		public void Stop()
		{
			base.StopAllCoroutines();
			this.VideoPlayer.Stop();
			this.VideoPlayer.enabled = false;
			NKCUIManager.NKCUIOverlayCaption.CloseAllCaption();
			this.OnStateChange(NKCUIComVideoPlayer.VideoState.Stop);
		}

		// Token: 0x06004BC6 RID: 19398 RVA: 0x0016ADED File Offset: 0x00168FED
		private IEnumerator VideoPlayProcess()
		{
			Debug.Log("Videoplayer Play process begin");
			this.OnStateChange(NKCUIComVideoPlayer.VideoState.PreparingPlay);
			this.VideoPlayer.enabled = true;
			this.VideoPlayer.playbackSpeed = this.m_fMoviePlaySpeed;
			if (!this.VideoPlayer.isPrepared)
			{
				Debug.Log("Preparing Video");
				this.OnStateChange(NKCUIComVideoPlayer.VideoState.PreparingPlay);
				this.Prepare();
				float waitTime = 0f;
				float targetWaitTime = this.m_bForcePlay ? 15f : 1.5f;
				if (!this.VideoPlayer.isPrepared)
				{
					yield return null;
				}
				while (!this.VideoPlayer.isPrepared)
				{
					float unscaledDeltaTime = Time.unscaledDeltaTime;
					waitTime += unscaledDeltaTime;
					if (waitTime > targetWaitTime)
					{
						Debug.LogWarning("Video Loading Took too long. cancel video play");
						this.OnStateChange(NKCUIComVideoPlayer.VideoState.Stop);
						this.VideoPlayer.enabled = false;
						NKCUIComVideoPlayer.VideoPlayMessageCallback videoPlayMessageCallback = this.dVideoPlayMessageCallback;
						if (videoPlayMessageCallback != null)
						{
							videoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage.PlayFailed);
						}
						yield break;
					}
					yield return null;
				}
			}
			Debug.Log("Playing Video");
			this.OnStateChange(NKCUIComVideoPlayer.VideoState.Playing);
			this.VideoPlayer.Play();
			yield return null;
			Debug.Log("Video Begin. Waiting video to finish");
			NKCUIComVideoPlayer.VideoPlayMessageCallback videoPlayMessageCallback2 = this.dVideoPlayMessageCallback;
			if (videoPlayMessageCallback2 != null)
			{
				videoPlayMessageCallback2(NKCUIComVideoPlayer.eVideoMessage.PlayBegin);
			}
			while (this.VideoPlayer.isPlaying)
			{
				yield return null;
			}
			Debug.Log("Video Complete");
			NKCUIComVideoPlayer.VideoPlayMessageCallback videoPlayMessageCallback3 = this.dVideoPlayMessageCallback;
			if (videoPlayMessageCallback3 != null)
			{
				videoPlayMessageCallback3(NKCUIComVideoPlayer.eVideoMessage.PlayComplete);
			}
			yield break;
		}

		// Token: 0x06004BC7 RID: 19399 RVA: 0x0016ADFC File Offset: 0x00168FFC
		public void SetPlaybackSpeed(float speed)
		{
			this.m_fMoviePlaySpeed = speed;
			this.VideoPlayer.playbackSpeed = speed;
		}

		// Token: 0x06004BC8 RID: 19400 RVA: 0x0016AE11 File Offset: 0x00169011
		private void OnApplicationQuit()
		{
		}

		// Token: 0x06004BC9 RID: 19401 RVA: 0x0016AE14 File Offset: 0x00169014
		private void VolumeUpdated()
		{
			if (this.VideoPlayer == null)
			{
				return;
			}
			NKCScenManager scenManager = NKCScenManager.GetScenManager();
			NKCGameOptionData nkcgameOptionData = (scenManager != null) ? scenManager.GetGameOptionData() : null;
			if (nkcgameOptionData != null)
			{
				this.VideoPlayer.SetDirectAudioMute(0, !Application.isFocused || nkcgameOptionData.SoundMute);
			}
			else
			{
				this.VideoPlayer.SetDirectAudioMute(0, !Application.isFocused);
			}
			this.VideoPlayer.SetDirectAudioVolume(0, NKCSoundManager.GetFinalVol(0f, 0f, 1f));
		}

		// Token: 0x06004BCA RID: 19402 RVA: 0x0016AE97 File Offset: 0x00169097
		private void OnApplicationFocus(bool focus)
		{
			this.VolumeUpdated();
		}

		// Token: 0x04003A44 RID: 14916
		protected static HashSet<NKCUIComVideoPlayer> s_setVideoPlayers = new HashSet<NKCUIComVideoPlayer>();

		// Token: 0x04003A45 RID: 14917
		protected VideoPlayer m_VideoPlayer;

		// Token: 0x04003A46 RID: 14918
		[Header("영상 재생속도")]
		public float m_fMoviePlaySpeed = 1f;

		// Token: 0x04003A47 RID: 14919
		[Header("영상 사운드 사용하는가?")]
		public bool m_bUseSound;

		// Token: 0x04003A48 RID: 14920
		[Header("영상 파일명. ASSET_RAW/Movie/ 안에서의 경로를 입력할 것")]
		public string m_sFilename;

		// Token: 0x04003A49 RID: 14921
		private const float PREPARE_WAIT_LIMIT = 1.5f;

		// Token: 0x04003A4A RID: 14922
		private bool m_bForcePlay;

		// Token: 0x04003A4B RID: 14923
		protected NKCUIComVideoPlayer.VideoState m_VideoState;

		// Token: 0x04003A4C RID: 14924
		private NKCUIComVideoPlayer.VideoPlayMessageCallback dVideoPlayMessageCallback;

		// Token: 0x02001434 RID: 5172
		public enum eVideoMessage
		{
			// Token: 0x04009DB0 RID: 40368
			PlayFailed,
			// Token: 0x04009DB1 RID: 40369
			PlayBegin,
			// Token: 0x04009DB2 RID: 40370
			PlayComplete
		}

		// Token: 0x02001435 RID: 5173
		// (Invoke) Token: 0x0600A81E RID: 43038
		public delegate void VideoPlayMessageCallback(NKCUIComVideoPlayer.eVideoMessage message);

		// Token: 0x02001436 RID: 5174
		protected enum VideoState
		{
			// Token: 0x04009DB4 RID: 40372
			Stop,
			// Token: 0x04009DB5 RID: 40373
			PreparingPlay,
			// Token: 0x04009DB6 RID: 40374
			Playing
		}

		// Token: 0x02001437 RID: 5175
		public struct strVideoCaption
		{
			// Token: 0x0600A821 RID: 43041 RVA: 0x0034BB57 File Offset: 0x00349D57
			public strVideoCaption(long sTime, long eTime, string str)
			{
				this.startTime = sTime;
				this.endTime = eTime;
				this.caption = str;
			}

			// Token: 0x04009DB7 RID: 40375
			public long startTime;

			// Token: 0x04009DB8 RID: 40376
			public long endTime;

			// Token: 0x04009DB9 RID: 40377
			public string caption;
		}
	}
}
