using System;
using UnityEngine;

namespace NKC
{
	// Token: 0x020006CD RID: 1741
	public class SoundData
	{
		// Token: 0x06003CBC RID: 15548 RVA: 0x00138514 File Offset: 0x00136714
		public SoundData()
		{
			this.m_SoundGameObj = new GameObject("NKM_SOUND_OBJ");
			this.m_AudioSource = this.m_SoundGameObj.AddComponent<AudioSource>();
			this.m_AudioSource.mute = false;
			this.m_AudioSource.bypassEffects = false;
			this.m_AudioSource.bypassListenerEffects = false;
			this.m_AudioSource.bypassReverbZones = false;
			this.m_AudioSource.playOnAwake = false;
			this.m_AudioSource.loop = false;
			this.m_AudioSource.priority = 128;
			this.m_AudioSource.volume = 1f;
			this.m_AudioSource.pitch = 1f;
			this.m_AudioSource.panStereo = 0f;
			this.m_AudioSource.spatialBlend = 0f;
			this.m_AudioSource.reverbZoneMix = 1f;
			this.m_AudioSource.dopplerLevel = 0f;
			this.m_AudioSource.spread = 0f;
			this.m_AudioSource.rolloffMode = AudioRolloffMode.Custom;
			this.m_AudioSource.maxDistance = 2000f;
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x00138638 File Offset: 0x00136838
		public void Unload()
		{
			if (this.m_NKCAssetResourceData != null)
			{
				NKCAssetResourceManager.CloseResource(this.m_NKCAssetResourceData);
				this.m_NKCAssetResourceData = null;
			}
			if (this.m_SoundGameObj != null)
			{
				UnityEngine.Object.Destroy(this.m_SoundGameObj);
			}
			this.m_SoundGameObj = null;
			this.m_AudioSource = null;
			this.m_Track = SOUND_TRACK.NORMAL;
			this.m_GameUnitUID = 0;
			this.m_fDelay = 0f;
		}

		// Token: 0x040035EE RID: 13806
		public int m_SoundUID;

		// Token: 0x040035EF RID: 13807
		public GameObject m_SoundGameObj;

		// Token: 0x040035F0 RID: 13808
		public AudioSource m_AudioSource;

		// Token: 0x040035F1 RID: 13809
		public float m_fReserveTime;

		// Token: 0x040035F2 RID: 13810
		public bool m_bPlayed;

		// Token: 0x040035F3 RID: 13811
		public float m_fLocalVol = 1f;

		// Token: 0x040035F4 RID: 13812
		public float m_fSoundPosX;

		// Token: 0x040035F5 RID: 13813
		public float m_fRange;

		// Token: 0x040035F6 RID: 13814
		public SOUND_TRACK m_Track;

		// Token: 0x040035F7 RID: 13815
		public short m_GameUnitUID;

		// Token: 0x040035F8 RID: 13816
		public float m_fDelay;

		// Token: 0x040035F9 RID: 13817
		public NKCAssetResourceData m_NKCAssetResourceData;
	}
}
