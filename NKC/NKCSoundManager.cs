using System;
using System.Collections.Generic;
using Cs.Logging;
using NKC.UI;
using NKM;
using NKM.Templet.Base;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006CF RID: 1743
	public class NKCSoundManager
	{
		// Token: 0x06003CC1 RID: 15553 RVA: 0x001387C0 File Offset: 0x001369C0
		public static bool IsInit()
		{
			return NKCSoundManager.m_NKM_SOUND != null;
		}

		// Token: 0x06003CC2 RID: 15554 RVA: 0x001387D0 File Offset: 0x001369D0
		public static void Init()
		{
			Log.Debug("[SoundManager] Init]", "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCSoundManager.cs", 191);
			NKCSoundManager.m_NKM_SOUND = GameObject.Find("NKM_SOUND");
			NKCSoundManager.ReserveSoundData(NKCSoundManager.m_ReserveSoundDataCount);
			NKCSoundManager.m_MusicData1 = NKCSoundManager.OpenSoundData();
			NKCSoundManager.m_MusicData2 = NKCSoundManager.OpenSoundData();
			AudioSettings.OnAudioConfigurationChanged -= NKCSoundManager.OnAudioSettingsChanged;
			AudioSettings.OnAudioConfigurationChanged += NKCSoundManager.OnAudioSettingsChanged;
		}

		// Token: 0x06003CC3 RID: 15555 RVA: 0x00138840 File Offset: 0x00136A40
		public static void OnAudioSettingsChanged(bool deviceWasChanged)
		{
			Log.Debug(string.Format("[SoundManager] OnAudioSettingsChanged deviceChanged[{0}]", deviceWasChanged), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/NKCSoundManager.cs", 205);
		}

		// Token: 0x06003CC4 RID: 15556 RVA: 0x00138864 File Offset: 0x00136A64
		public static bool LoadFromLUA(string fileName)
		{
			NKMLua nkmlua = new NKMLua();
			if (nkmlua.LoadCommonPath("AB_SCRIPT", fileName, true) && nkmlua.OpenTable("m_dicScenMusicData"))
			{
				int num = 1;
				while (nkmlua.OpenTable(num))
				{
					NKCScenMusicData nkcscenMusicData = new NKCScenMusicData();
					nkcscenMusicData.LoadFromLUA(nkmlua);
					if (!NKCSoundManager.m_dicScenMusicData.ContainsKey(nkcscenMusicData.m_NKM_SCEN_ID))
					{
						NKCSoundManager.m_dicScenMusicData.Add(nkcscenMusicData.m_NKM_SCEN_ID, nkcscenMusicData);
					}
					else
					{
						NKCSoundManager.m_dicScenMusicData[nkcscenMusicData.m_NKM_SCEN_ID].DeepCopyFromSource(nkcscenMusicData);
					}
					num++;
					nkmlua.CloseTable();
				}
				nkmlua.CloseTable();
			}
			nkmlua.LuaClose();
			return true;
		}

		// Token: 0x06003CC5 RID: 15557 RVA: 0x00138902 File Offset: 0x00136B02
		public static void Unload()
		{
			while (NKCSoundManager.m_qSoundDataPool.Count > NKCSoundManager.m_ReserveSoundDataCount)
			{
				NKCSoundManager.m_qSoundDataPool.Dequeue().Unload();
			}
		}

		// Token: 0x06003CC6 RID: 15558 RVA: 0x00138928 File Offset: 0x00136B28
		public static void Update(float deltaTime)
		{
			NKCSoundManager.m_fCamPosXBefore = NKCSoundManager.m_fCamPosX;
			NKCSoundManager.m_fCamPosX = NKCCamera.GetPosNowX(false);
			for (LinkedListNode<SoundData> linkedListNode = NKCSoundManager.m_linklistSoundDataReserve.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				SoundData value = linkedListNode.Value;
				if (value != null)
				{
					value.m_fReserveTime -= deltaTime;
					if (value.m_fReserveTime <= 0f)
					{
						NKCSoundManager.PlaySound(value, false);
						LinkedListNode<SoundData> next = linkedListNode.Next;
						NKCSoundManager.m_linklistSoundDataReserve.Remove(linkedListNode);
						linkedListNode = next;
						continue;
					}
				}
			}
			int num = NKCSoundManager.m_linklistSoundData.Count - 30;
			for (LinkedListNode<SoundData> linkedListNode = NKCSoundManager.m_linklistSoundData.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				bool flag = false;
				SoundData value2 = linkedListNode.Value;
				if (value2 != null)
				{
					if (num > 0)
					{
						num--;
						value2.m_bPlayed = true;
						flag = true;
					}
					if (value2.m_AudioSource != null)
					{
						if (value2.m_AudioSource.isPlaying)
						{
							value2.m_bPlayed = true;
						}
						if (value2.m_bPlayed && !value2.m_AudioSource.isPlaying)
						{
							flag = true;
						}
						else if (NKCSoundManager.m_fCamPosXBefore != NKCSoundManager.m_fCamPosX && value2.m_fRange > 0f)
						{
							if (value2.m_Track == SOUND_TRACK.NORMAL)
							{
								value2.m_AudioSource.volume = NKCSoundManager.GetFinalVol(value2.m_fSoundPosX, value2.m_fRange, value2.m_fLocalVol);
							}
							else
							{
								value2.m_AudioSource.volume = NKCSoundManager.GetFinalVoiceVolume(value2.m_fSoundPosX, value2.m_fRange, value2.m_fLocalVol);
							}
						}
					}
					else
					{
						flag = true;
					}
					if (flag)
					{
						if (value2.m_AudioSource != null)
						{
							value2.m_AudioSource.Stop();
						}
						NKCSoundManager.CloseSoundData(value2);
						LinkedListNode<SoundData> next2 = linkedListNode.Next;
						NKCSoundManager.m_linklistSoundData.Remove(linkedListNode);
						linkedListNode = next2;
						continue;
					}
				}
			}
			NKCSoundManager.m_MusicData1Fade.Update(deltaTime);
			NKCSoundManager.m_MusicData2Fade.Update(deltaTime);
			if (NKCSoundManager.m_MusicData1Fade.IsTracking())
			{
				if (NKCSoundManager.m_MusicData1.m_AudioSource != null)
				{
					NKCSoundManager.m_MusicData1.m_AudioSource.volume = NKCSoundManager.GetFinalMusicVolume(NKCSoundManager.m_MusicData1.m_fLocalVol) * NKCSoundManager.m_MusicData1Fade.GetNowValue();
				}
			}
			else if (NKCSoundManager.m_MusicDataNow != 1 && NKCSoundManager.m_MusicData1.m_AudioSource != null && NKCSoundManager.m_MusicData1.m_AudioSource.isPlaying)
			{
				NKCSoundManager.m_MusicData1.m_AudioSource.Stop();
			}
			if (NKCSoundManager.m_MusicData2Fade.IsTracking())
			{
				if (NKCSoundManager.m_MusicData2.m_AudioSource != null)
				{
					NKCSoundManager.m_MusicData2.m_AudioSource.volume = NKCSoundManager.GetFinalMusicVolume(NKCSoundManager.m_MusicData2.m_fLocalVol) * NKCSoundManager.m_MusicData2Fade.GetNowValue();
				}
			}
			else if (NKCSoundManager.m_MusicDataNow == 1 && NKCSoundManager.m_MusicData2.m_AudioSource != null && NKCSoundManager.m_MusicData2.m_AudioSource.isPlaying)
			{
				NKCSoundManager.m_MusicData2.m_AudioSource.Stop();
			}
			if (!NKCSoundManager.m_MusicData1Fade.IsTracking() && !NKCSoundManager.m_MusicData2Fade.IsTracking())
			{
				if (NKCSoundManager.m_MusicDataNow == 1)
				{
					if (NKCSoundManager.m_MusicData1.m_AudioSource != null && NKCSoundManager.m_MusicData1.m_AudioSource.isPlaying)
					{
						NKCSoundManager.m_MusicData1.m_AudioSource.volume = NKCSoundManager.GetFinalMusicVolume(NKCSoundManager.m_MusicData1.m_fLocalVol);
						return;
					}
				}
				else if (NKCSoundManager.m_MusicData2.m_AudioSource != null && NKCSoundManager.m_MusicData2.m_AudioSource.isPlaying)
				{
					NKCSoundManager.m_MusicData2.m_AudioSource.volume = NKCSoundManager.GetFinalMusicVolume(NKCSoundManager.m_MusicData2.m_fLocalVol);
				}
			}
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x00138CA6 File Offset: 0x00136EA6
		private static int GetNewSoundUID()
		{
			return NKCSoundManager.m_UIDIndex++;
		}

		// Token: 0x06003CC8 RID: 15560 RVA: 0x00138CB8 File Offset: 0x00136EB8
		private static void ReserveSoundData(int count)
		{
			for (int i = 0; i < count; i++)
			{
				SoundData soundData = new SoundData();
				soundData.m_SoundGameObj.transform.SetParent(NKCSoundManager.m_NKM_SOUND.transform);
				if (soundData.m_SoundGameObj.activeSelf)
				{
					soundData.m_SoundGameObj.SetActive(false);
				}
				NKCSoundManager.m_qSoundDataPool.Enqueue(soundData);
			}
		}

		// Token: 0x06003CC9 RID: 15561 RVA: 0x00138D18 File Offset: 0x00136F18
		private static SoundData OpenSoundData()
		{
			SoundData soundData = null;
			if (NKCSoundManager.m_qSoundDataPool.Count > 0)
			{
				soundData = NKCSoundManager.m_qSoundDataPool.Dequeue();
			}
			if (soundData != null && soundData.m_SoundGameObj == null)
			{
				Debug.LogWarning("invalid SoundData detected. try recovering...");
				soundData.Unload();
				soundData = null;
			}
			if (soundData == null)
			{
				soundData = new SoundData();
				soundData.m_SoundGameObj.transform.SetParent(NKCSoundManager.m_NKM_SOUND.transform);
			}
			if (!soundData.m_SoundGameObj.activeSelf)
			{
				soundData.m_SoundGameObj.SetActive(true);
			}
			soundData.m_bPlayed = false;
			return soundData;
		}

		// Token: 0x06003CCA RID: 15562 RVA: 0x00138DA6 File Offset: 0x00136FA6
		private static void CloseSoundData(SoundData cSoundData)
		{
			if (cSoundData.m_SoundGameObj != null)
			{
				if (cSoundData.m_SoundGameObj.activeSelf)
				{
					cSoundData.m_SoundGameObj.SetActive(false);
				}
				NKCSoundManager.m_qSoundDataPool.Enqueue(cSoundData);
				return;
			}
			cSoundData.Unload();
		}

		// Token: 0x06003CCB RID: 15563 RVA: 0x00138DE4 File Offset: 0x00136FE4
		public static int PlaySound(string audioClipName, float fLocalVol, float fSoundPosX, float fRange, bool bLoop = false, float reserveTime = 0f, bool bShowCaption = false, float fStartTime = 0f)
		{
			return NKCSoundManager.PlaySound(SOUND_TRACK.NORMAL, audioClipName, 0, false, false, fLocalVol, fSoundPosX, fRange, bLoop, reserveTime, bShowCaption, fStartTime);
		}

		// Token: 0x06003CCC RID: 15564 RVA: 0x00138E08 File Offset: 0x00137008
		public static int PlaySound(NKMAssetName assetName, float fLocalVol, float fSoundPosX, float fRange, bool bLoop = false, float reserveTime = 0f, bool bShowCaption = false, float fStartTime = 0f)
		{
			return NKCSoundManager.PlaySound(SOUND_TRACK.NORMAL, assetName.m_BundleName, assetName.m_AssetName, 0, false, false, fLocalVol, fSoundPosX, fRange, bLoop, reserveTime, bShowCaption, fStartTime, 0f);
		}

		// Token: 0x06003CCD RID: 15565 RVA: 0x00138E3C File Offset: 0x0013703C
		public static int PlayVoice(string audioClipName, short gameUnitUID, bool bClearVoice, bool bIgnoreSameVoice, float fLocalVol, float fSoundPosX, float fRange, bool bLoop = false, float reserveTime = 0f, bool bShowCaption = false)
		{
			return NKCSoundManager.PlaySound(SOUND_TRACK.VOICE, audioClipName, gameUnitUID, bClearVoice, bIgnoreSameVoice, fLocalVol, fSoundPosX, fRange, bLoop, reserveTime, bShowCaption, 0f);
		}

		// Token: 0x06003CCE RID: 15566 RVA: 0x00138E64 File Offset: 0x00137064
		public static int PlayVoice(string bundleName, string audioClipName, short gameUnitUID, bool bClearVoice, bool bIgnoreSameVoice, float fLocalVol, float fSoundPosX, float fRange, bool bLoop = false, float reserveTime = 0f, bool bShowCaption = false, float startTime = 0f, float delay = 0f)
		{
			return NKCSoundManager.PlaySound(SOUND_TRACK.VOICE, bundleName, audioClipName, gameUnitUID, bClearVoice, bIgnoreSameVoice, fLocalVol, fSoundPosX, fRange, bLoop, reserveTime, bShowCaption, startTime, delay);
		}

		// Token: 0x06003CCF RID: 15567 RVA: 0x00138E90 File Offset: 0x00137090
		private static int PlaySound(SOUND_TRACK eTrack, string audioClipName, short gameUnitUID, bool bClearVoice, bool bIgnoreSameVoice, float fLocalVol, float fSoundPosX, float fRange, bool bLoop = false, float reserveTime = 0f, bool bShowCaption = false, float fStartTime = 0f)
		{
			string bundleName = NKCAssetResourceManager.GetBundleName(audioClipName);
			if (string.IsNullOrEmpty(bundleName))
			{
				return 0;
			}
			return NKCSoundManager.PlaySound(eTrack, bundleName, audioClipName, gameUnitUID, bClearVoice, bIgnoreSameVoice, fLocalVol, fSoundPosX, fRange, bLoop, reserveTime, bShowCaption, fStartTime, 0f);
		}

		// Token: 0x06003CD0 RID: 15568 RVA: 0x00138ED0 File Offset: 0x001370D0
		private static int PlaySound(SOUND_TRACK eTrack, string bundleName, string audioClipName, short gameUnitUID, bool bClearVoice, bool bIgnoreSameVoice, float fLocalVol, float fSoundPosX, float fRange, bool bLoop = false, float reserveTime = 0f, bool bShowCaption = false, float fStartTime = 0f, float delay = 0f)
		{
			if (NKCScenManager.GetScenManager() != null && NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable() && !NKCScenManager.GetScenManager().GetNKCPowerSaveMode().IsJukeBoxMode)
			{
				return 0;
			}
			if (NKCSoundManager.m_NKM_SOUND == null)
			{
				return 0;
			}
			if (!NKCAssetResourceManager.IsBundleExists(bundleName, audioClipName))
			{
				Debug.LogWarning(string.Concat(new string[]
				{
					"voice bundle ",
					bundleName,
					" for audioClip ",
					audioClipName,
					" not exists!"
				}));
				return 0;
			}
			if (bIgnoreSameVoice)
			{
				using (LinkedList<SoundData>.Enumerator enumerator = NKCSoundManager.m_linklistSoundDataReserve.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.m_NKCAssetResourceData.m_NKMAssetName.m_AssetName.Equals(audioClipName))
						{
							return 0;
						}
					}
				}
				using (LinkedList<SoundData>.Enumerator enumerator = NKCSoundManager.m_linklistSoundData.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.m_NKCAssetResourceData.m_NKMAssetName.m_AssetName.Equals(audioClipName))
						{
							return 0;
						}
					}
				}
			}
			SoundData soundData = NKCSoundManager.OpenSoundData();
			soundData.m_SoundUID = NKCSoundManager.GetNewSoundUID();
			soundData.m_fLocalVol = fLocalVol;
			soundData.m_fReserveTime = reserveTime;
			soundData.m_fSoundPosX = fSoundPosX;
			soundData.m_fRange = fRange;
			soundData.m_Track = eTrack;
			soundData.m_GameUnitUID = gameUnitUID;
			soundData.m_fDelay = delay;
			if (bClearVoice)
			{
				NKCSoundManager.ClearVoice(soundData.m_GameUnitUID);
			}
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<AudioClip>(bundleName, audioClipName, false, null);
			if (soundData.m_NKCAssetResourceData != null)
			{
				NKCAssetResourceManager.CloseResource(soundData.m_NKCAssetResourceData);
			}
			soundData.m_NKCAssetResourceData = nkcassetResourceData;
			if (soundData.m_NKCAssetResourceData != null && soundData.m_NKCAssetResourceData.GetAsset<AudioClip>() != null)
			{
				AudioClip asset = soundData.m_NKCAssetResourceData.GetAsset<AudioClip>();
				soundData.m_AudioSource.clip = asset;
			}
			soundData.m_AudioSource.loop = bLoop;
			soundData.m_AudioSource.time = fStartTime;
			if (soundData.m_fReserveTime > 0f)
			{
				NKCSoundManager.m_linklistSoundDataReserve.AddLast(soundData);
			}
			else
			{
				NKCSoundManager.PlaySound(soundData, bShowCaption);
			}
			return soundData.m_SoundUID;
		}

		// Token: 0x06003CD1 RID: 15569 RVA: 0x00139108 File Offset: 0x00137308
		private static void ClearVoice(short gameUnitUID)
		{
			LinkedListNode<SoundData> linkedListNode;
			for (linkedListNode = NKCSoundManager.m_linklistSoundDataReserve.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				SoundData value = linkedListNode.Value;
				if (value != null && value.m_Track == SOUND_TRACK.VOICE && value.m_GameUnitUID == gameUnitUID)
				{
					NKCSoundManager.CloseSoundData(value);
					LinkedListNode<SoundData> next = linkedListNode.Next;
					NKCSoundManager.m_linklistSoundDataReserve.Remove(linkedListNode);
				}
			}
			linkedListNode = NKCSoundManager.m_linklistSoundData.First;
			while (linkedListNode != null)
			{
				SoundData value2 = linkedListNode.Value;
				if (value2 != null && value2.m_Track == SOUND_TRACK.VOICE && value2.m_GameUnitUID == gameUnitUID)
				{
					value2.m_bPlayed = true;
					value2.m_AudioSource.Stop();
					NKCSoundManager.CloseSoundData(value2);
					LinkedListNode<SoundData> next2 = linkedListNode.Next;
					NKCSoundManager.m_linklistSoundData.Remove(linkedListNode);
					linkedListNode = next2;
				}
				else
				{
					linkedListNode = linkedListNode.Next;
				}
			}
		}

		// Token: 0x06003CD2 RID: 15570 RVA: 0x001391C0 File Offset: 0x001373C0
		private static bool PlaySound(SoundData cSoundData, bool bShowCaption = false)
		{
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable() && !NKCScenManager.GetScenManager().GetNKCPowerSaveMode().IsJukeBoxMode)
			{
				return false;
			}
			NKCSoundManager.m_linklistSoundData.AddLast(cSoundData);
			float volume = 0f;
			SOUND_TRACK track = cSoundData.m_Track;
			if (track != SOUND_TRACK.NORMAL)
			{
				if (track == SOUND_TRACK.VOICE)
				{
					volume = NKCSoundManager.GetFinalVoiceVolume(cSoundData.m_fSoundPosX, cSoundData.m_fRange, cSoundData.m_fLocalVol);
				}
			}
			else
			{
				volume = NKCSoundManager.GetFinalVol(cSoundData.m_fSoundPosX, cSoundData.m_fRange, cSoundData.m_fLocalVol);
			}
			cSoundData.m_AudioSource.volume = volume;
			if (cSoundData.m_fDelay <= 0f)
			{
				cSoundData.m_AudioSource.Play();
			}
			else
			{
				cSoundData.m_AudioSource.PlayDelayed(cSoundData.m_fDelay);
			}
			if (bShowCaption)
			{
				NKCUIManager.NKCUIOverlayCaption.OpenCaption(NKCUtilString.GetVoiceCaption(cSoundData.m_NKCAssetResourceData.m_NKMAssetName), cSoundData.m_SoundUID, 0f);
			}
			return true;
		}

		// Token: 0x06003CD3 RID: 15571 RVA: 0x001392A5 File Offset: 0x001374A5
		public static void SetSoundVolume(float fVol)
		{
			NKCSoundManager.m_fSoundVol = fVol;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x001392B0 File Offset: 0x001374B0
		public static void StopSound(int soundUID)
		{
			for (LinkedListNode<SoundData> linkedListNode = NKCSoundManager.m_linklistSoundData.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				SoundData value = linkedListNode.Value;
				if (value.m_SoundUID == soundUID)
				{
					value.m_AudioSource.Stop();
					NKCSoundManager.m_linklistSoundData.Remove(linkedListNode);
					NKCSoundManager.CloseSoundData(value);
					return;
				}
			}
			for (LinkedListNode<SoundData> linkedListNode = NKCSoundManager.m_linklistSoundDataReserve.First; linkedListNode != null; linkedListNode = linkedListNode.Next)
			{
				SoundData value2 = linkedListNode.Value;
				if (value2.m_SoundUID == soundUID)
				{
					value2.m_AudioSource.Stop();
					NKCSoundManager.m_linklistSoundDataReserve.Remove(linkedListNode);
					NKCSoundManager.CloseSoundData(value2);
					return;
				}
			}
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x00139348 File Offset: 0x00137548
		public static void StopAllSound()
		{
			foreach (SoundData soundData in NKCSoundManager.m_linklistSoundData)
			{
				soundData.m_AudioSource.Stop();
				NKCSoundManager.CloseSoundData(soundData);
			}
			NKCSoundManager.m_linklistSoundData.Clear();
			foreach (SoundData soundData2 in NKCSoundManager.m_linklistSoundDataReserve)
			{
				soundData2.m_AudioSource.Stop();
				NKCSoundManager.CloseSoundData(soundData2);
			}
			NKCSoundManager.m_linklistSoundDataReserve.Clear();
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x00139400 File Offset: 0x00137600
		public static void StopAllSound(SOUND_TRACK trackType)
		{
			foreach (SoundData soundData in NKCSoundManager.m_linklistSoundData)
			{
				if (soundData.m_Track == trackType)
				{
					soundData.m_AudioSource.Stop();
					NKCSoundManager.CloseSoundData(soundData);
				}
			}
			foreach (SoundData soundData2 in NKCSoundManager.m_linklistSoundDataReserve)
			{
				if (soundData2.m_Track == trackType)
				{
					soundData2.m_AudioSource.Stop();
					NKCSoundManager.CloseSoundData(soundData2);
				}
			}
		}

		// Token: 0x06003CD7 RID: 15575 RVA: 0x001394BC File Offset: 0x001376BC
		public static float GetFinalVol(float fSoundPosX, float fRange, float fLocalVol)
		{
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable() && !NKCScenManager.GetScenManager().GetNKCPowerSaveMode().IsJukeBoxMode)
			{
				return 0f;
			}
			if (NKCScenManager.GetScenManager().GetGameOptionData().SoundMute)
			{
				return 0f;
			}
			if (fRange <= 0f)
			{
				return NKCSoundManager.m_fSoundVol * fLocalVol * NKCSoundManager.m_fAllVol;
			}
			float num = Mathf.Abs(NKCSoundManager.m_fCamPosX - fSoundPosX);
			if (num <= fRange)
			{
				return NKCSoundManager.m_fSoundVol * fLocalVol * NKCSoundManager.m_fAllVol;
			}
			float num2 = 1f - num * 0.01f * 0.1f;
			if (num2 < 0.3f)
			{
				num2 = 0.3f;
			}
			return NKCSoundManager.m_fSoundVol * fLocalVol * num2 * NKCSoundManager.m_fAllVol;
		}

		// Token: 0x06003CD8 RID: 15576 RVA: 0x0013956E File Offset: 0x0013776E
		private static SoundData GetNowMusicData()
		{
			if (NKCSoundManager.m_MusicDataNow == 1)
			{
				return NKCSoundManager.m_MusicData1;
			}
			return NKCSoundManager.m_MusicData2;
		}

		// Token: 0x06003CD9 RID: 15577 RVA: 0x00139584 File Offset: 0x00137784
		private static SoundData GetNewMusicData()
		{
			if (NKCSoundManager.m_MusicDataNow == 1)
			{
				NKCSoundManager.m_MusicDataNow = 2;
				NKCSoundManager.m_MusicData2Fade.SetNowValue(0f);
				NKCSoundManager.m_MusicData2Fade.SetTracking(1f, 1f, TRACKING_DATA_TYPE.TDT_NORMAL);
				NKCSoundManager.m_MusicData1Fade.SetTracking(0f, 1f, TRACKING_DATA_TYPE.TDT_NORMAL);
				return NKCSoundManager.m_MusicData2;
			}
			NKCSoundManager.m_MusicDataNow = 1;
			NKCSoundManager.m_MusicData1Fade.SetNowValue(0f);
			NKCSoundManager.m_MusicData1Fade.SetTracking(1f, 1f, TRACKING_DATA_TYPE.TDT_NORMAL);
			NKCSoundManager.m_MusicData2Fade.SetTracking(0f, 1f, TRACKING_DATA_TYPE.TDT_NORMAL);
			return NKCSoundManager.m_MusicData1;
		}

		// Token: 0x06003CDA RID: 15578 RVA: 0x00139624 File Offset: 0x00137824
		public static void PlayMusic(string audioClipName, bool bLoop = false, float fLocalVol = 1f, bool bForce = false, float fStartTime = 0f, float fDelay = 0f)
		{
			if (!bForce && NKCSoundManager.m_MusicName.CompareTo(audioClipName) == 0)
			{
				return;
			}
			NKCAssetResourceData nkcassetResourceData = NKCAssetResourceManager.OpenResource<AudioClip>("ab_music/" + audioClipName, audioClipName, false, null);
			if (nkcassetResourceData == null || nkcassetResourceData.GetAsset<AudioClip>() == null)
			{
				return;
			}
			NKCSoundManager.SaveBackgroundBGMTime(audioClipName);
			NKCSoundManager.m_MusicName = audioClipName;
			SoundData newMusicData = NKCSoundManager.GetNewMusicData();
			if (newMusicData.m_NKCAssetResourceData != null)
			{
				NKCAssetResourceManager.CloseResource(newMusicData.m_NKCAssetResourceData);
			}
			newMusicData.m_NKCAssetResourceData = nkcassetResourceData;
			AudioClip asset = newMusicData.m_NKCAssetResourceData.GetAsset<AudioClip>();
			newMusicData.m_AudioSource.clip = asset;
			newMusicData.m_AudioSource.loop = bLoop;
			newMusicData.m_fLocalVol = fLocalVol;
			newMusicData.m_AudioSource.time = fStartTime;
			if (fDelay <= 0f)
			{
				newMusicData.m_AudioSource.Play();
				return;
			}
			newMusicData.m_AudioSource.PlayDelayed(fDelay);
		}

		// Token: 0x06003CDB RID: 15579 RVA: 0x001396EF File Offset: 0x001378EF
		public static void PlayScenMusic()
		{
			NKCSoundManager.PlayScenMusic(NKCScenManager.GetScenManager().GetNowScenID(), false);
		}

		// Token: 0x06003CDC RID: 15580 RVA: 0x00139704 File Offset: 0x00137904
		public static void PlayScenMusic(NKM_SCEN_ID eNKM_SCEN_ID, bool bForce = false)
		{
			if (!NKCSoundManager.m_dicScenMusicData.ContainsKey(eNKM_SCEN_ID))
			{
				return;
			}
			NKCScenMusicData nkcscenMusicData = NKCSoundManager.m_dicScenMusicData[eNKM_SCEN_ID];
			string text = nkcscenMusicData.m_MusicName;
			float fStartTime = 0f;
			float fLocalVol = 1f;
			if (nkcscenMusicData.m_MusicName == "FOLLOW_LOBBY")
			{
				NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
				if (nkmuserData != null)
				{
					NKCBackgroundTemplet nkcbackgroundTemplet = NKCBackgroundTemplet.Find(nkmuserData.BackgroundID);
					if (nkcbackgroundTemplet != null)
					{
						text = nkcbackgroundTemplet.m_Background_Music;
					}
					NKCBGMInfoTemplet nkcbgminfoTemplet = NKMTempletContainer<NKCBGMInfoTemplet>.Find(nkmuserData.BackgroundBGMID);
					if (nkcbgminfoTemplet != null)
					{
						text = nkcbgminfoTemplet.m_BgmAssetID;
						fLocalVol = nkcbgminfoTemplet.BGMVolume;
						if (nkmuserData.BackgroundBGMContinue)
						{
							NKCSoundManager.m_BackgroundBGMContinueData.strBackgroundBGMAssetID = text;
							if (!NKCSoundManager.m_BackgroundBGMContinueData.bPlayBackgroundBGM)
							{
								NKCSoundManager.m_BackgroundBGMContinueData.bPlayBackgroundBGM = true;
								fStartTime = NKCSoundManager.m_BackgroundBGMContinueData.fPlayBackgroundBGMTime;
							}
						}
					}
				}
			}
			NKCSoundManager.PlayMusic(text, true, fLocalVol, bForce, fStartTime, 0f);
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x001397D8 File Offset: 0x001379D8
		public static void SaveBackgroundBGMTime(string strBgmAssetID)
		{
			if (string.Equals(strBgmAssetID, NKCSoundManager.m_MusicName))
			{
				return;
			}
			if (string.IsNullOrEmpty(NKCSoundManager.m_BackgroundBGMContinueData.strBackgroundBGMAssetID))
			{
				return;
			}
			if (string.Equals(NKCSoundManager.m_BackgroundBGMContinueData.strBackgroundBGMAssetID, strBgmAssetID))
			{
				return;
			}
			NKMUserData nkmuserData = NKCScenManager.CurrentUserData();
			if (nkmuserData != null && nkmuserData.BackgroundBGMContinue && NKCSoundManager.m_BackgroundBGMContinueData.bPlayBackgroundBGM)
			{
				NKCSoundManager.m_BackgroundBGMContinueData.fPlayBackgroundBGMTime = NKCSoundManager.GetMusicTime();
				NKCSoundManager.m_BackgroundBGMContinueData.bPlayBackgroundBGM = false;
			}
		}

		// Token: 0x06003CDE RID: 15582 RVA: 0x0013984F File Offset: 0x00137A4F
		public static bool IsSameMusic(string audioClipName)
		{
			return NKCSoundManager.m_MusicName.CompareTo(audioClipName) == 0;
		}

		// Token: 0x06003CDF RID: 15583 RVA: 0x00139861 File Offset: 0x00137A61
		public static void ChangeAllVolume(float delta)
		{
			NKCSoundManager.SetAllVolume(NKCSoundManager.m_fAllVol + delta);
		}

		// Token: 0x06003CE0 RID: 15584 RVA: 0x0013986F File Offset: 0x00137A6F
		public static void SetAllVolume(float fVol)
		{
			NKCSoundManager.m_fAllVol = fVol;
			NKCUIComVideoPlayer.OnUpdateVolume();
			NKCSoundManager.SetMusicVolume(NKCSoundManager.m_fMusicVol);
			NKCSoundManager.SetVoiceVolume(NKCSoundManager.m_fVoiceVol);
			NKCSoundManager.SetSoundVolume(NKCSoundManager.m_fSoundVol);
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x0013989A File Offset: 0x00137A9A
		public static void SetMute(bool bMute, bool bIgnoreApplicationFocus = false)
		{
			AudioListener.volume = (float)((!bMute && (Application.isFocused || bIgnoreApplicationFocus)) ? 1 : 0);
			NKCUIComVideoPlayer.OnUpdateVolume();
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x001398B8 File Offset: 0x00137AB8
		public static void SetMusicVolume(float fVol)
		{
			NKCSoundManager.m_fMusicVol = fVol;
			if (NKCSoundManager.m_MusicData1.m_AudioSource != null)
			{
				NKCSoundManager.m_MusicData1.m_AudioSource.volume = NKCSoundManager.GetFinalMusicVolume(NKCSoundManager.m_MusicData1.m_fLocalVol);
			}
			if (NKCSoundManager.m_MusicData2.m_AudioSource != null)
			{
				NKCSoundManager.m_MusicData2.m_AudioSource.volume = NKCSoundManager.GetFinalMusicVolume(NKCSoundManager.m_MusicData2.m_fLocalVol);
			}
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x0013992C File Offset: 0x00137B2C
		public static void StopMusic()
		{
			if (NKCSoundManager.m_MusicData1.m_AudioSource != null)
			{
				NKCSoundManager.m_MusicData1.m_AudioSource.Stop();
			}
			if (NKCSoundManager.m_MusicData2.m_AudioSource != null)
			{
				NKCSoundManager.m_MusicData2.m_AudioSource.Stop();
			}
			NKCSoundManager.m_MusicName = "";
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x00139985 File Offset: 0x00137B85
		public static void FadeOutMusic()
		{
			NKCSoundManager.m_MusicData2Fade.SetTracking(0f, 1f, TRACKING_DATA_TYPE.TDT_NORMAL);
			NKCSoundManager.m_MusicData1Fade.SetTracking(0f, 1f, TRACKING_DATA_TYPE.TDT_NORMAL);
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x001399B4 File Offset: 0x00137BB4
		public static void SetMusicVolumeFactor(float fVolFactor)
		{
			NKCSoundManager.m_fMusicVolFactor = fVolFactor;
			if (NKCSoundManager.m_MusicData1.m_AudioSource != null)
			{
				NKCSoundManager.m_MusicData1.m_AudioSource.volume = NKCSoundManager.GetFinalMusicVolume(NKCSoundManager.m_MusicData1.m_fLocalVol);
			}
			if (NKCSoundManager.m_MusicData2.m_AudioSource != null)
			{
				NKCSoundManager.m_MusicData2.m_AudioSource.volume = NKCSoundManager.GetFinalMusicVolume(NKCSoundManager.m_MusicData2.m_fLocalVol);
			}
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x00139A27 File Offset: 0x00137C27
		private static float GetFinalMusicVolume(float fLocalVol)
		{
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable() && !NKCScenManager.GetScenManager().GetNKCPowerSaveMode().IsJukeBoxMode)
			{
				return 0f;
			}
			return NKCSoundManager.m_fMusicVol * fLocalVol * NKCSoundManager.m_fMusicVolFactor * NKCSoundManager.m_fAllVol;
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x00139A64 File Offset: 0x00137C64
		public static void SetVoiceVolume(float fVolume)
		{
			NKCSoundManager.m_fVoiceVol = fVolume;
		}

		// Token: 0x06003CE8 RID: 15592 RVA: 0x00139A6C File Offset: 0x00137C6C
		public static float GetFinalVoiceVolume(float fSoundPosX, float fRange, float fLocalVol)
		{
			if (NKCScenManager.GetScenManager().GetNKCPowerSaveMode().GetEnable())
			{
				return 0f;
			}
			if (fRange <= 0f)
			{
				return NKCSoundManager.m_fVoiceVol * fLocalVol * NKCSoundManager.m_fAllVol;
			}
			float num = Mathf.Abs(NKCSoundManager.m_fCamPosX - fSoundPosX);
			if (num <= fRange)
			{
				return NKCSoundManager.m_fVoiceVol * fLocalVol * NKCSoundManager.m_fAllVol;
			}
			float num2 = 1f - num * 0.01f * 0.1f;
			if (num2 < 0.3f)
			{
				num2 = 0.3f;
			}
			return NKCSoundManager.m_fVoiceVol * fLocalVol * num2 * NKCSoundManager.m_fAllVol;
		}

		// Token: 0x06003CE9 RID: 15593 RVA: 0x00139AF8 File Offset: 0x00137CF8
		public static bool IsPlayingVoice(int soundUID = -1)
		{
			LinkedListNode<SoundData> linkedListNode = NKCSoundManager.m_linklistSoundData.First;
			while (linkedListNode != null)
			{
				SoundData value = linkedListNode.Value;
				if (value != null && value.m_Track == SOUND_TRACK.VOICE && value.m_AudioSource.isPlaying)
				{
					if (soundUID == -1)
					{
						return true;
					}
					if (value.m_SoundUID == soundUID)
					{
						return true;
					}
				}
				if (linkedListNode != null)
				{
					linkedListNode = linkedListNode.Next;
				}
			}
			return false;
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x00139B54 File Offset: 0x00137D54
		public static SoundData GetPlayingVoiceData(int soundUID)
		{
			LinkedListNode<SoundData> linkedListNode = NKCSoundManager.m_linklistSoundData.First;
			while (linkedListNode != null)
			{
				SoundData value = linkedListNode.Value;
				if (value.m_SoundUID == soundUID)
				{
					return value;
				}
				if (linkedListNode != null)
				{
					linkedListNode = linkedListNode.Next;
				}
			}
			return null;
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x00139B90 File Offset: 0x00137D90
		public static void SetChangeMusicTime(float fValue)
		{
			SoundData nowMusicData = NKCSoundManager.GetNowMusicData();
			if (nowMusicData != null)
			{
				nowMusicData.m_AudioSource.time = nowMusicData.m_AudioSource.clip.length * Mathf.Min(0.99f, fValue);
				nowMusicData.m_AudioSource.Play();
			}
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x00139BD8 File Offset: 0x00137DD8
		public static float GetMusicTime()
		{
			SoundData nowMusicData = NKCSoundManager.GetNowMusicData();
			if (nowMusicData != null)
			{
				return nowMusicData.m_AudioSource.time;
			}
			return 0f;
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x00139C00 File Offset: 0x00137E00
		public static AudioSource GetAudioSource()
		{
			SoundData nowMusicData = NKCSoundManager.GetNowMusicData();
			if (nowMusicData != null)
			{
				return nowMusicData.m_AudioSource;
			}
			return null;
		}

		// Token: 0x040035FE RID: 13822
		private static int m_ReserveSoundDataCount = 20;

		// Token: 0x040035FF RID: 13823
		private static int m_UIDIndex = 1;

		// Token: 0x04003600 RID: 13824
		private static GameObject m_NKM_SOUND = null;

		// Token: 0x04003601 RID: 13825
		private static Dictionary<NKM_SCEN_ID, NKCScenMusicData> m_dicScenMusicData = new Dictionary<NKM_SCEN_ID, NKCScenMusicData>();

		// Token: 0x04003602 RID: 13826
		private static LinkedList<SoundData> m_linklistSoundData = new LinkedList<SoundData>();

		// Token: 0x04003603 RID: 13827
		private static LinkedList<SoundData> m_linklistSoundDataReserve = new LinkedList<SoundData>();

		// Token: 0x04003604 RID: 13828
		private static Queue<SoundData> m_qSoundDataPool = new Queue<SoundData>();

		// Token: 0x04003605 RID: 13829
		private static float m_fCamPosXBefore = 0f;

		// Token: 0x04003606 RID: 13830
		private static float m_fCamPosX = 0f;

		// Token: 0x04003607 RID: 13831
		private static float m_fAllVol = 1f;

		// Token: 0x04003608 RID: 13832
		private static float m_fSoundVol = 1f;

		// Token: 0x04003609 RID: 13833
		private static float m_fMusicVol = 1f;

		// Token: 0x0400360A RID: 13834
		private static float m_fMusicVolFactor = 1f;

		// Token: 0x0400360B RID: 13835
		private static float m_fVoiceVol = 1f;

		// Token: 0x0400360C RID: 13836
		private static string m_MusicName = "";

		// Token: 0x0400360D RID: 13837
		private static SoundData m_MusicData1 = null;

		// Token: 0x0400360E RID: 13838
		private static SoundData m_MusicData2 = null;

		// Token: 0x0400360F RID: 13839
		private static int m_MusicDataNow = 2;

		// Token: 0x04003610 RID: 13840
		private static NKMTrackingFloat m_MusicData1Fade = new NKMTrackingFloat();

		// Token: 0x04003611 RID: 13841
		private static NKMTrackingFloat m_MusicData2Fade = new NKMTrackingFloat();

		// Token: 0x04003612 RID: 13842
		private static NKCSoundManager.BackgroundContinueData m_BackgroundBGMContinueData = new NKCSoundManager.BackgroundContinueData(false, 0f);

		// Token: 0x020013A6 RID: 5030
		private struct BackgroundContinueData
		{
			// Token: 0x0600A66E RID: 42606 RVA: 0x00347130 File Offset: 0x00345330
			public BackgroundContinueData(bool bPlayingBGM, float fStartTime)
			{
				this.bPlayBackgroundBGM = bPlayingBGM;
				this.fPlayBackgroundBGMTime = fStartTime;
				this.strBackgroundBGMAssetID = "";
			}

			// Token: 0x04009AE6 RID: 39654
			public bool bPlayBackgroundBGM;

			// Token: 0x04009AE7 RID: 39655
			public float fPlayBackgroundBGMTime;

			// Token: 0x04009AE8 RID: 39656
			public string strBackgroundBGMAssetID;
		}
	}
}
