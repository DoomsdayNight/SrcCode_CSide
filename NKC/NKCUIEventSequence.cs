using System;
using System.Collections.Generic;
using Cs.Logging;
using NKM;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace NKC.UI
{
	// Token: 0x0200098E RID: 2446
	public class NKCUIEventSequence : NKCUIBase
	{
		// Token: 0x0600650D RID: 25869 RVA: 0x00202223 File Offset: 0x00200423
		public static NKCUIEventSequence OpenInstance(NKMAssetName assetName)
		{
			return NKCUIManager.OpenNewInstance<NKCUIEventSequence>(assetName, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null).GetInstance<NKCUIEventSequence>();
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x00202237 File Offset: 0x00200437
		public static NKCUIEventSequence OpenInstance(string bundleName, string assetName)
		{
			return NKCUIManager.OpenNewInstance<NKCUIEventSequence>(bundleName, assetName, NKCUIManager.GetUIBaseRect(NKCUIManager.eUIBaseRect.UIFrontPopup), null).GetInstance<NKCUIEventSequence>();
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x0020224C File Offset: 0x0020044C
		public static void PlaySkinCutin(NKMSkinTemplet skinTemplet, NKCUIEventSequence.OnClose dOnClose)
		{
			if (skinTemplet == null || !skinTemplet.HasLoginCutin)
			{
				if (dOnClose != null)
				{
					dOnClose();
				}
				return;
			}
			NKMAssetName nkmassetName = NKMAssetName.ParseBundleName(skinTemplet.m_LoginCutin, skinTemplet.m_LoginCutin);
			NKCUIEventSequence nkcuieventSequence = NKCUIEventSequence.OpenInstance(nkmassetName);
			if (nkcuieventSequence == null)
			{
				Log.Error(string.Format("Skin cutin asset {0} not found!", nkmassetName), "/Users/buildman/buildAgent_ca18-1/work/e0bfb30763b53cef/CounterSide/CODE/CSClient/Assets/ASSET_STATIC/AS_SCRIPT/NKC/UI/NKCUIEventSequence.cs", 48);
			}
			nkcuieventSequence.Open(dOnClose);
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x06006510 RID: 25872 RVA: 0x002022AC File Offset: 0x002004AC
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06006511 RID: 25873 RVA: 0x002022AF File Offset: 0x002004AF
		public override string MenuName
		{
			get
			{
				return "AnimSequence";
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06006512 RID: 25874 RVA: 0x002022B6 File Offset: 0x002004B6
		public bool SoloUI
		{
			get
			{
				return this.m_bSoloUI;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06006513 RID: 25875 RVA: 0x002022BE File Offset: 0x002004BE
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				return NKCUIUpsideMenu.eMode.Disable;
			}
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x002022C1 File Offset: 0x002004C1
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x002022CF File Offset: 0x002004CF
		public override void Initialize()
		{
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x002022D1 File Offset: 0x002004D1
		public override void OpenByShortcut(Dictionary<string, string> dicParam)
		{
			this.Open(null);
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x002022DC File Offset: 0x002004DC
		public void Open(NKCUIEventSequence.OnClose onClose)
		{
			this.dOnClose = onClose;
			base.gameObject.SetActive(true);
			if (this.m_rtMainRect != null)
			{
				NKCCamera.RescaleRectToCameraFrustrum(this.m_rtMainRect, NKCCamera.GetSubUICamera(), Vector2.zero, NKCCamera.GetSubUICamera().transform.position.z, this.m_eFitMode, NKCCamera.ScaleMode.Scale);
			}
			base.UIOpened(true);
			this.StartEvent(true);
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x00202348 File Offset: 0x00200548
		public void StartEvent(bool bSoloUI = false)
		{
			this.m_fTimer = 0f;
			this.m_fFinishTime = 0f;
			this.m_currentEventIndex = 0;
			this.m_bPlaying = true;
			this.m_bSoloUI = bSoloUI;
			this.m_lstEvent.Sort((NKCUIEventSequence.SequenceEvent a, NKCUIEventSequence.SequenceEvent b) => a.fTime.CompareTo(b.fTime));
			this.m_fFinishTime = this.m_lstEvent[this.m_lstEvent.Count - 1].fTime;
			NKCUIVoiceManager.StopVoice();
			this.ProcessFrame();
		}

		// Token: 0x06006519 RID: 25881 RVA: 0x002023D8 File Offset: 0x002005D8
		private void ProcessFrame()
		{
			while (this.m_currentEventIndex < this.m_lstEvent.Count)
			{
				NKCUIEventSequence.SequenceEvent sequenceEvent = this.m_lstEvent[this.m_currentEventIndex];
				if (sequenceEvent.fTime > this.m_fTimer)
				{
					break;
				}
				this.ProcessEvent(sequenceEvent);
				this.m_currentEventIndex++;
			}
		}

		// Token: 0x0600651A RID: 25882 RVA: 0x00202430 File Offset: 0x00200630
		private void ProcessEvent(NKCUIEventSequence.SequenceEvent sequenceEvent)
		{
			NKCUIEventSequence.EventType eEvent = sequenceEvent.eEvent;
			if (eEvent <= NKCUIEventSequence.EventType.PlaySpineAnim)
			{
				if (eEvent == NKCUIEventSequence.EventType.ObjEnable)
				{
					NKCUtil.SetGameobjectActive(sequenceEvent.objTarget, sequenceEvent.boolArgument);
					return;
				}
				if (eEvent != NKCUIEventSequence.EventType.PlaySpineAnim)
				{
					return;
				}
				if (sequenceEvent.objTarget == null)
				{
					return;
				}
				SkeletonGraphic component = sequenceEvent.objTarget.GetComponent<SkeletonGraphic>();
				if (component == null)
				{
					return;
				}
				this.SetAnimation(sequenceEvent.fTime, component, sequenceEvent.strArgument, sequenceEvent.boolArgument, 0, true, 0f);
				return;
			}
			else
			{
				if (eEvent != NKCUIEventSequence.EventType.PlayAnimatorAnim)
				{
					switch (eEvent)
					{
					case NKCUIEventSequence.EventType.PlaySound:
					{
						NKMAssetName nkmassetName = NKMAssetName.ParseBundleName("", sequenceEvent.strArgument);
						if (string.IsNullOrEmpty(nkmassetName.m_BundleName))
						{
							NKCSoundManager.PlaySound(sequenceEvent.strArgument, 1f, 0f, 0f, false, 0f, false, 0f);
							return;
						}
						NKCSoundManager.PlaySound(nkmassetName, 1f, 0f, 0f, false, 0f, false, 0f);
						return;
					}
					case NKCUIEventSequence.EventType.PlayMusic:
						if (string.IsNullOrEmpty(sequenceEvent.strArgument))
						{
							NKCSoundManager.StopMusic();
							return;
						}
						NKCSoundManager.PlayMusic(sequenceEvent.strArgument, false, 1f, false, sequenceEvent.floatArgument, 0f);
						return;
					case NKCUIEventSequence.EventType.PlayVoice:
					{
						NKMAssetName nkmassetName2 = NKMAssetName.ParseBundleName("", sequenceEvent.strArgument);
						if (string.IsNullOrEmpty(nkmassetName2.m_BundleName))
						{
							NKCSoundManager.PlayVoice(nkmassetName2.m_AssetName, 0, false, false, 1f, 0f, 0f, false, 0f, sequenceEvent.boolArgument);
							return;
						}
						NKCUIVoiceManager.ForcePlayVoice(nkmassetName2, 0f, 1f, sequenceEvent.boolArgument, true);
						break;
					}
					default:
						if (eEvent == NKCUIEventSequence.EventType.Close)
						{
							this.Finish();
							return;
						}
						break;
					}
					return;
				}
				if (sequenceEvent.objTarget == null)
				{
					return;
				}
				Animator component2 = sequenceEvent.objTarget.GetComponent<Animator>();
				if (component2 == null)
				{
					return;
				}
				component2.Play(sequenceEvent.strArgument);
				return;
			}
		}

		// Token: 0x0600651B RID: 25883 RVA: 0x00202608 File Offset: 0x00200808
		public void SetAnimation(float startTime, SkeletonGraphic skeletonGraphic, string animationName, bool loop, int trackIndex = 0, bool bForceRestart = true, float fStartTime = 0f)
		{
			if (skeletonGraphic == null || skeletonGraphic.AnimationState == null)
			{
				Debug.LogError("AnimSequence : SkeletonGraphic not found!");
				return;
			}
			if (skeletonGraphic.SkeletonData == null)
			{
				return;
			}
			Spine.Animation animation = skeletonGraphic.SkeletonData.FindAnimation(animationName);
			if (animation == null)
			{
				Debug.LogError("AnimSequence : Animation " + animationName + " not found!");
				return;
			}
			this.m_fFinishTime = Mathf.Max(this.m_fFinishTime, startTime + animation.Duration - fStartTime);
			skeletonGraphic.SetUseHalfUpdate(false);
			if (bForceRestart)
			{
				Skeleton skeleton = skeletonGraphic.Skeleton;
				if (skeleton != null)
				{
					skeleton.SetToSetupPose();
				}
				TrackEntry trackEntry = skeletonGraphic.AnimationState.SetAnimation(trackIndex, animationName, loop);
				if (fStartTime > 0f)
				{
					trackEntry.TrackTime = fStartTime;
				}
			}
			skeletonGraphic.Update();
		}

		// Token: 0x0600651C RID: 25884 RVA: 0x002026C0 File Offset: 0x002008C0
		private void Update()
		{
			if (!this.m_bPlaying)
			{
				return;
			}
			this.m_fTimer += Time.deltaTime;
			this.ProcessFrame();
			if (this.m_bPlaying && this.m_fTimer > this.m_fFinishTime)
			{
				this.Finish();
			}
			if (Input.anyKeyDown)
			{
				this.OnTryCancel();
			}
		}

		// Token: 0x0600651D RID: 25885 RVA: 0x00202717 File Offset: 0x00200917
		private void OnTryCancel()
		{
			if (this.m_bSoloUI && NKCUIManager.IsTopmostUI(this))
			{
				NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_CUTSCENE_MOVIE_SKIP_TITLE, NKCUtilString.GET_STRING_CUTSCENE_MOVIE_SKIP_DESC, new NKCPopupOKCancel.OnButton(this.Finish), null, false);
			}
		}

		// Token: 0x0600651E RID: 25886 RVA: 0x00202748 File Offset: 0x00200948
		private void Finish()
		{
			this.m_currentEventIndex = this.m_lstEvent.Count;
			this.m_fFinishTime = 0f;
			this.m_bPlaying = false;
			NKCUIVoiceManager.StopVoice();
			NKCSoundManager.StopAllSound();
			if (this.m_bSoloUI)
			{
				base.Close();
			}
			NKCSoundManager.PlayScenMusic();
			NKCUIEventSequence.OnClose onClose = this.dOnClose;
			if (onClose != null)
			{
				onClose();
			}
			this.dOnClose = null;
		}

		// Token: 0x040050BC RID: 20668
		[Header("���� Rect�� �����ϸ� �ɼ�")]
		public RectTransform m_rtMainRect;

		// Token: 0x040050BD RID: 20669
		public NKCCamera.FitMode m_eFitMode = NKCCamera.FitMode.FitAuto;

		// Token: 0x040050BE RID: 20670
		[Header("�̺�Ʈ ������")]
		public List<NKCUIEventSequence.SequenceEvent> m_lstEvent;

		// Token: 0x040050BF RID: 20671
		private NKCUIEventSequence.OnClose dOnClose;

		// Token: 0x040050C0 RID: 20672
		private float m_fTimer;

		// Token: 0x040050C1 RID: 20673
		private float m_fFinishTime;

		// Token: 0x040050C2 RID: 20674
		private int m_currentEventIndex;

		// Token: 0x040050C3 RID: 20675
		private bool m_bPlaying;

		// Token: 0x040050C4 RID: 20676
		private bool m_bSoloUI;

		// Token: 0x02001654 RID: 5716
		[Serializable]
		public struct SequenceEvent
		{
			// Token: 0x0400A411 RID: 42001
			public float fTime;

			// Token: 0x0400A412 RID: 42002
			public GameObject objTarget;

			// Token: 0x0400A413 RID: 42003
			public NKCUIEventSequence.EventType eEvent;

			// Token: 0x0400A414 RID: 42004
			public string strArgument;

			// Token: 0x0400A415 RID: 42005
			public float floatArgument;

			// Token: 0x0400A416 RID: 42006
			public bool boolArgument;
		}

		// Token: 0x02001655 RID: 5717
		public enum EventType
		{
			// Token: 0x0400A418 RID: 42008
			ObjEnable,
			// Token: 0x0400A419 RID: 42009
			PlaySpineAnim = 10,
			// Token: 0x0400A41A RID: 42010
			PlayAnimatorAnim,
			// Token: 0x0400A41B RID: 42011
			PlaySound = 20,
			// Token: 0x0400A41C RID: 42012
			PlayMusic,
			// Token: 0x0400A41D RID: 42013
			PlayVoice,
			// Token: 0x0400A41E RID: 42014
			Close = 99999
		}

		// Token: 0x02001656 RID: 5718
		// (Invoke) Token: 0x0600AFF8 RID: 45048
		public delegate void OnClose();
	}
}
