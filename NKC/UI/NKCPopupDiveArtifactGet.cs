using System;
using System.Collections.Generic;
using NKC.FX;
using NKC.UI.Option;
using NKM;
using NKM.Templet;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x02000A3A RID: 2618
	public class NKCPopupDiveArtifactGet : NKCUIBase
	{
		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x060072AF RID: 29359 RVA: 0x00261746 File Offset: 0x0025F946
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.Popup;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x060072B0 RID: 29360 RVA: 0x00261749 File Offset: 0x0025F949
		public override string MenuName
		{
			get
			{
				return "PopupDiveArtifactGet";
			}
		}

		// Token: 0x060072B1 RID: 29361 RVA: 0x00261750 File Offset: 0x0025F950
		public void InvalidAuto()
		{
			this.m_bAuto = false;
		}

		// Token: 0x060072B2 RID: 29362 RVA: 0x00261759 File Offset: 0x0025F959
		private void OnCloseCallBack()
		{
			if (this.m_dOnCloseCallBack != null)
			{
				this.m_dOnCloseCallBack();
			}
			this.m_dOnCloseCallBack = null;
		}

		// Token: 0x060072B3 RID: 29363 RVA: 0x00261778 File Offset: 0x0025F978
		public void PlayOutroAni(int index)
		{
			this.m_amtorPopup.Play("OUTRO_" + index.ToString());
			NKCSoundManager.PlaySound("FX_UI_TITLE_IN_TEST", 1f, 0f, 0f, false, 0f, false, 0f);
		}

		// Token: 0x060072B4 RID: 29364 RVA: 0x002617C7 File Offset: 0x0025F9C7
		private void OnEffectExplode()
		{
			NKCSoundManager.PlaySound("FX_UI_ARTIFACT_GET_02", 1f, 0f, 0f, false, 0f, false, 0f);
			if (this.m_dOnEffectExplode != null)
			{
				this.m_dOnEffectExplode();
			}
		}

		// Token: 0x060072B5 RID: 29365 RVA: 0x00261802 File Offset: 0x0025FA02
		private void OnEffectDestSetting()
		{
			NKCSoundManager.PlaySound("FX_UI_ARTIFACT_GET_01", 1f, 0f, 0f, false, 0f, false, 0f);
			if (this.m_dOnEffectDestSetting != null)
			{
				this.m_dOnEffectDestSetting();
			}
		}

		// Token: 0x060072B6 RID: 29366 RVA: 0x0026183D File Offset: 0x0025FA3D
		public void SetEffectDestPos(Vector3 pos)
		{
			this.m_trEffectDest.position = pos;
		}

		// Token: 0x060072B7 RID: 29367 RVA: 0x0026184C File Offset: 0x0025FA4C
		public void InitUI(NKCPopupDiveArtifactGet.dOnEffectExplode _dOnEffectExplode, NKCPopupDiveArtifactGet.dOnEffectDestSetting _dOnEffectDestSetting)
		{
			this.m_dOnEffectExplode = _dOnEffectExplode;
			this.m_dOnEffectDestSetting = _dOnEffectDestSetting;
			for (int i = 0; i < this.m_lstNKCPopupDiveArtifactGetSlot.Count; i++)
			{
				this.m_lstNKCPopupDiveArtifactGetSlot[i].InitUI(i);
			}
			this.m_csbtnSkip.PointerClick.RemoveAllListeners();
			this.m_csbtnSkip.PointerClick.AddListener(new UnityAction(this.OnClickSkip));
			this.m_NKC_FXM_EVENT_Explode.Evt.AddListener(new UnityAction(this.OnEffectExplode));
			this.m_NKC_FXM_EVENT_DestSetting.Evt.AddListener(new UnityAction(this.OnEffectDestSetting));
			this.m_NKCUIOpenAnimator = new NKCUIOpenAnimator(base.gameObject);
			NKCUtil.SetGameobjectActive(base.gameObject, false);
		}

		// Token: 0x060072B8 RID: 29368 RVA: 0x00261910 File Offset: 0x0025FB10
		public void Open(List<int> lstArtifact, bool bAuto, NKCPopupDiveArtifactGet.dOnCloseCallBack _dOnCloseCallBack = null)
		{
			if (lstArtifact == null || lstArtifact.Count == 0)
			{
				NKCPopupMessageManager.AddPopupMessage(NKCUtilString.GET_STRING_DIVE_ARTIFACT_ALREADY_FULL, NKCPopupMessage.eMessagePosition.Top, false, true, 0f, false);
				this.OnClickSkip_();
				return;
			}
			NKCUIGameOption.CheckInstanceAndClose();
			this.m_dOnCloseCallBack = _dOnCloseCallBack;
			if (this.m_rtContents != null)
			{
				this.m_rtContents.localPosition = new Vector3(0f, this.m_rtContents.localPosition.y, this.m_rtContents.localPosition.z);
			}
			bool flag = false;
			for (int i = 0; i < this.m_lstNKCPopupDiveArtifactGetSlot.Count; i++)
			{
				if (!(this.m_lstNKCPopupDiveArtifactGetSlot[i] == null))
				{
					if (i < lstArtifact.Count)
					{
						NKMDiveArtifactTemplet nkmdiveArtifactTemplet = NKMDiveArtifactTemplet.Find(lstArtifact[i]);
						this.m_lstNKCPopupDiveArtifactGetSlot[i].SetData(nkmdiveArtifactTemplet);
						if (nkmdiveArtifactTemplet != null)
						{
							flag |= (nkmdiveArtifactTemplet.RewardId > 0);
						}
					}
					else
					{
						this.m_lstNKCPopupDiveArtifactGetSlot[i].SetData(null);
					}
				}
			}
			NKCUtil.SetGameobjectActive(this.m_lbReturnDesc, flag);
			this.m_bAuto = bAuto;
			this.m_fElapsedTime = 0f;
			this.m_bOpenSkipPopup = false;
			if (this.m_NKCUIOpenAnimator != null)
			{
				this.m_NKCUIOpenAnimator.PlayOpenAni();
			}
			base.UIOpened(true);
		}

		// Token: 0x060072B9 RID: 29369 RVA: 0x00261A48 File Offset: 0x0025FC48
		private void Update()
		{
			if (base.IsOpen)
			{
				this.m_NKCUIOpenAnimator.Update();
				if (this.m_bAuto)
				{
					this.m_fElapsedTime += Time.deltaTime;
					if (this.m_fElapsedTime >= 3f / NKCClientConst.DiveAutoSpeed && !NKMPopUpBox.IsOpenedWaitBox() && !this.m_bOpenSkipPopup)
					{
						NKMDiveGameData diveGameData = NKCScenManager.CurrentUserData().m_DiveGameData;
						if (diveGameData != null)
						{
							if (diveGameData.Player.PlayerBase.ReservedArtifacts.Count > 0 && this.m_lstNKCPopupDiveArtifactGetSlot.Count > 0)
							{
								bool flag = true;
								for (int i = 0; i < diveGameData.Player.PlayerBase.ReservedArtifacts.Count; i++)
								{
									if (NKMDiveArtifactTemplet.Find(diveGameData.Player.PlayerBase.ReservedArtifacts[i]) == null)
									{
										flag = false;
										break;
									}
								}
								int num;
								if (flag)
								{
									num = NKMRandom.Range(0, diveGameData.Player.PlayerBase.ReservedArtifacts.Count);
								}
								else
								{
									int num2 = 0;
									for (int j = 0; j < diveGameData.Player.PlayerBase.ReservedArtifacts.Count; j++)
									{
										NKMDiveArtifactTemplet.Find(diveGameData.Player.PlayerBase.ReservedArtifacts[j]);
									}
									num = num2;
								}
								if (num < this.m_lstNKCPopupDiveArtifactGetSlot.Count && num >= 0)
								{
									this.m_lstNKCPopupDiveArtifactGetSlot[num].OnClickSelect();
								}
							}
							else
							{
								this.OnClickSkip_();
							}
						}
						this.m_bAuto = false;
					}
				}
			}
		}

		// Token: 0x060072BA RID: 29370 RVA: 0x00261BD1 File Offset: 0x0025FDD1
		public void OnClickSkip()
		{
			this.m_bOpenSkipPopup = true;
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, NKCUtilString.GET_STRING_DIVE_ARTIFACT_GET_SKIP_CHECK_REQ, new NKCPopupOKCancel.OnButton(this.OnClickSkip_), delegate()
			{
				this.m_bOpenSkipPopup = false;
			}, false);
		}

		// Token: 0x060072BB RID: 29371 RVA: 0x00261C02 File Offset: 0x0025FE02
		public void OnClickSkip_()
		{
			NKCScenManager.GetScenManager().Get_NKC_SCEN_DIVE().GetDiveGame().SetLastSelectedArtifactSlotIndex(-1);
			NKCPacketSender.Send_NKMPacket_DIVE_SELECT_ARTIFACT_REQ(0);
			this.m_bAuto = false;
		}

		// Token: 0x060072BC RID: 29372 RVA: 0x00261C26 File Offset: 0x0025FE26
		public override void CloseInternal()
		{
			base.gameObject.SetActive(false);
			this.m_bAuto = false;
			this.OnCloseCallBack();
		}

		// Token: 0x060072BD RID: 29373 RVA: 0x00261C41 File Offset: 0x0025FE41
		public override void OnBackButton()
		{
		}

		// Token: 0x04005E94 RID: 24212
		public const string ASSET_BUNDLE_NAME = "AB_UI_NKM_UI_WORLD_MAP_DIVE";

		// Token: 0x04005E95 RID: 24213
		public const string UI_ASSET_NAME = "NKM_UI_DIVE_ARTIFACT_POPUP";

		// Token: 0x04005E96 RID: 24214
		public RectTransform m_rtContents;

		// Token: 0x04005E97 RID: 24215
		public List<NKCPopupDiveArtifactGetSlot> m_lstNKCPopupDiveArtifactGetSlot;

		// Token: 0x04005E98 RID: 24216
		public NKCUIComStateButton m_csbtnSkip;

		// Token: 0x04005E99 RID: 24217
		public Image m_imgReturnItemIcon;

		// Token: 0x04005E9A RID: 24218
		public Text m_lbReturnDesc;

		// Token: 0x04005E9B RID: 24219
		public Animator m_amtorPopup;

		// Token: 0x04005E9C RID: 24220
		public Transform m_trEffectDest;

		// Token: 0x04005E9D RID: 24221
		public NKC_FXM_EVENT m_NKC_FXM_EVENT_Explode;

		// Token: 0x04005E9E RID: 24222
		public NKC_FXM_EVENT m_NKC_FXM_EVENT_DestSetting;

		// Token: 0x04005E9F RID: 24223
		private bool m_bAuto;

		// Token: 0x04005EA0 RID: 24224
		private float m_fElapsedTime;

		// Token: 0x04005EA1 RID: 24225
		private bool m_bOpenSkipPopup;

		// Token: 0x04005EA2 RID: 24226
		private NKCUIOpenAnimator m_NKCUIOpenAnimator;

		// Token: 0x04005EA3 RID: 24227
		private NKCPopupDiveArtifactGet.dOnEffectExplode m_dOnEffectExplode;

		// Token: 0x04005EA4 RID: 24228
		private NKCPopupDiveArtifactGet.dOnEffectDestSetting m_dOnEffectDestSetting;

		// Token: 0x04005EA5 RID: 24229
		private NKCPopupDiveArtifactGet.dOnCloseCallBack m_dOnCloseCallBack;

		// Token: 0x0200177A RID: 6010
		// (Invoke) Token: 0x0600B36D RID: 45933
		public delegate void dOnCloseCallBack();

		// Token: 0x0200177B RID: 6011
		// (Invoke) Token: 0x0600B371 RID: 45937
		public delegate void dOnEffectExplode();

		// Token: 0x0200177C RID: 6012
		// (Invoke) Token: 0x0600B375 RID: 45941
		public delegate void dOnEffectDestSetting();
	}
}
