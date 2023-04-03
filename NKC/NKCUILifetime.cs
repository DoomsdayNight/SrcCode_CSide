using System;
using System.Collections;
using NKM;
using UnityEngine;
using UnityEngine.UI;

namespace NKC.UI
{
	// Token: 0x020009B2 RID: 2482
	public class NKCUILifetime : NKCUIBase
	{
		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06006854 RID: 26708 RVA: 0x0021BD18 File Offset: 0x00219F18
		public static NKCUILifetime Instance
		{
			get
			{
				if (NKCUILifetime.m_Instance == null)
				{
					NKCUILifetime.m_Instance = NKCUIManager.OpenNewInstance<NKCUILifetime>("ab_ui_nkm_ui_personnel", "NKM_UI_PERSONNEL_LIFETIME_CONTRACT", NKCUIManager.eUIBaseRect.UIFrontCommon, new NKCUIManager.LoadedUIData.OnCleanupInstance(NKCUILifetime.CleanupInstance)).GetInstance<NKCUILifetime>();
					NKCUILifetime.m_Instance.Init();
				}
				return NKCUILifetime.m_Instance;
			}
		}

		// Token: 0x06006855 RID: 26709 RVA: 0x0021BD67 File Offset: 0x00219F67
		private static void CleanupInstance()
		{
			NKCUILifetime.m_Instance = null;
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06006856 RID: 26710 RVA: 0x0021BD6F File Offset: 0x00219F6F
		public static bool IsInstanceOpen
		{
			get
			{
				return NKCUILifetime.m_Instance != null && NKCUILifetime.m_Instance.IsOpen;
			}
		}

		// Token: 0x06006857 RID: 26711 RVA: 0x0021BD8A File Offset: 0x00219F8A
		public static void CheckInstanceAndClose()
		{
			if (NKCUILifetime.m_Instance != null && NKCUILifetime.m_Instance.IsOpen)
			{
				NKCUILifetime.m_Instance.Close();
			}
		}

		// Token: 0x06006858 RID: 26712 RVA: 0x0021BDAF File Offset: 0x00219FAF
		private void OnDestroy()
		{
			NKCUILifetime.m_Instance = null;
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06006859 RID: 26713 RVA: 0x0021BDB7 File Offset: 0x00219FB7
		public override NKCUIBase.eMenutype eUIType
		{
			get
			{
				return NKCUIBase.eMenutype.FullScreen;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x0600685A RID: 26714 RVA: 0x0021BDBA File Offset: 0x00219FBA
		public override string MenuName
		{
			get
			{
				return NKCUtilString.GET_STRING_LIFETIME;
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x0600685B RID: 26715 RVA: 0x0021BDC1 File Offset: 0x00219FC1
		public override NKCUIUpsideMenu.eMode eUpsideMenuMode
		{
			get
			{
				if (!this.m_bUpside)
				{
					return NKCUIUpsideMenu.eMode.Disable;
				}
				return base.eUpsideMenuMode;
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x0600685C RID: 26716 RVA: 0x0021BDD3 File Offset: 0x00219FD3
		public override bool IgnoreBackButtonWhenOpen
		{
			get
			{
				return !this.m_bUpside;
			}
		}

		// Token: 0x0600685D RID: 26717 RVA: 0x0021BDDE File Offset: 0x00219FDE
		public void Init()
		{
			this.m_charView.Init(null, null);
			this.m_contract.Init(new NKCUILifetimeContract.OnEndDrag(this.OnEndDrag), new NKCUILifetimeContract.OnEndAni(this.EndAni));
		}

		// Token: 0x0600685E RID: 26718 RVA: 0x0021BE10 File Offset: 0x0021A010
		public void Open(NKMUnitData targetUnit, bool replay)
		{
			if (targetUnit == null)
			{
				return;
			}
			NKCUIVoiceManager.StopVoice();
			this.EnableAllLifeTimeSoundFX(false);
			this.m_targetUnit = targetUnit;
			this.m_replay = replay;
			this.SetBackground(targetUnit);
			this.LoadCutScene(true);
			this.m_bUpside = true;
			this.m_contract.SetData(this.m_targetUnit);
			NKCUtil.SetGameobjectActive(base.gameObject, true);
			NKCUtil.SetGameobjectActive(this.m_contract, false);
			base.UIOpened(true);
			base.StartCoroutine(this.CheckScenLoading(true));
		}

		// Token: 0x0600685F RID: 26719 RVA: 0x0021BE8E File Offset: 0x0021A08E
		public override void CloseInternal()
		{
			this.m_charView.CleanUp();
			NKCUtil.SetGameobjectActive(base.gameObject, false);
			NKCSoundManager.PlayScenMusic(NKM_SCEN_ID.NSI_BASE, false);
		}

		// Token: 0x06006860 RID: 26720 RVA: 0x0021BEAE File Offset: 0x0021A0AE
		public override void UnHide()
		{
			base.UnHide();
			NKCUtil.SetGameobjectActive(this.m_contract, true);
		}

		// Token: 0x06006861 RID: 26721 RVA: 0x0021BEC2 File Offset: 0x0021A0C2
		private void OnEndDrag()
		{
			if (this.m_targetUnit == null)
			{
				return;
			}
			this.m_bUpside = false;
			NKCUIManager.UpdateUpsideMenu();
			if (this.m_replay)
			{
				this.PlayAni();
				return;
			}
			NKCPacketSender.Send_NKMPacket_CONTRACT_PERMANENTLY_REQ(this.m_targetUnit.m_UnitUID);
		}

		// Token: 0x06006862 RID: 26722 RVA: 0x0021BEF8 File Offset: 0x0021A0F8
		public void PlayAni()
		{
			NKCUILifetimeContract contract = this.m_contract;
			if (contract != null)
			{
				contract.PlayAni();
			}
			this.LoadCutScene(false);
		}

		// Token: 0x06006863 RID: 26723 RVA: 0x0021BF12 File Offset: 0x0021A112
		private void EndAni()
		{
			this.EnableAllLifeTimeSoundFX(false);
			base.StartCoroutine(this.CheckScenLoading(false));
		}

		// Token: 0x06006864 RID: 26724 RVA: 0x0021BF2C File Offset: 0x0021A12C
		private void LoadCutScene(bool bStart)
		{
			if (this.m_targetUnit == null)
			{
				return;
			}
			string cutsceneName = this.GetCutsceneName(bStart, this.m_targetUnit);
			if (!string.IsNullOrEmpty(cutsceneName))
			{
				this.LoadCutScene(cutsceneName, bStart);
			}
		}

		// Token: 0x06006865 RID: 26725 RVA: 0x0021BF60 File Offset: 0x0021A160
		private void LoadCutScene(string strID, bool bStart)
		{
			this.m_bLoading = true;
			NKCUICutScenPlayer.Instance.UnLoad();
			NKCUICutScenPlayer.Instance.Load(strID, true);
		}

		// Token: 0x06006866 RID: 26726 RVA: 0x0021BF80 File Offset: 0x0021A180
		private void PlayCutScene(bool bStart)
		{
			string cutsceneName = this.GetCutsceneName(bStart, this.m_targetUnit);
			if (!string.IsNullOrEmpty(cutsceneName))
			{
				if (!bStart)
				{
					NKCUIVoiceManager.PlayVoice(VOICE_TYPE.VT_LIFETIME_COMPLETE, this.m_targetUnit, false, false);
				}
				NKCUICutScenPlayer.Instance.Play(cutsceneName, 0, delegate()
				{
					this.EndCutScene(bStart);
				});
				return;
			}
			this.EndCutScene(bStart);
		}

		// Token: 0x06006867 RID: 26727 RVA: 0x0021BFFC File Offset: 0x0021A1FC
		private void SetBackground(NKMUnitData unitData)
		{
			string text = string.Empty;
			if (unitData == null)
			{
				return;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitData.m_SkinID);
			if (skinTemplet != null && !string.IsNullOrEmpty(skinTemplet.m_CutsceneLifetime_BG))
			{
				text = skinTemplet.m_CutsceneLifetime_BG;
			}
			if (string.IsNullOrEmpty(text))
			{
				NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(unitData.m_UnitID);
				if (unitTemplet != null && !string.IsNullOrEmpty(unitTemplet.m_CutsceneLifetime_BG))
				{
					text = unitTemplet.m_CutsceneLifetime_BG;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				NKCUtil.SetImageSprite(this.m_background, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_personnel_lifetime_sprite", "NKM_UI_PERSONNEL_LIFETIME_CONTRACT_BG", false), false);
				return;
			}
			string[] array = text.Split(new char[]
			{
				'@'
			});
			if (array.Length > 1)
			{
				NKCUtil.SetImageSprite(this.m_background, NKCResourceUtility.GetOrLoadAssetResource<Sprite>(array[0], array[1], false), false);
				return;
			}
			NKCUtil.SetImageSprite(this.m_background, NKCResourceUtility.GetOrLoadAssetResource<Sprite>("ab_ui_nkm_ui_personnel_lifetime_sprite", text, false), false);
		}

		// Token: 0x06006868 RID: 26728 RVA: 0x0021C0D0 File Offset: 0x0021A2D0
		private string GetCutsceneName(bool bStart, NKMUnitData unitData)
		{
			string text = string.Empty;
			if (unitData == null)
			{
				return text;
			}
			NKMSkinTemplet skinTemplet = NKMSkinManager.GetSkinTemplet(unitData.m_SkinID);
			if (skinTemplet != null)
			{
				if (bStart)
				{
					text = skinTemplet.m_CutsceneLifetime_start;
				}
				else
				{
					text = skinTemplet.m_CutsceneLifetime_end;
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				NKCCollectionUnitTemplet unitTemplet = NKCCollectionManager.GetUnitTemplet(unitData.m_UnitID);
				if (unitTemplet != null)
				{
					if (bStart)
					{
						text = unitTemplet.m_CutsceneLifetime_Start;
					}
					else
					{
						text = unitTemplet.m_CutsceneLifetime_End;
					}
				}
			}
			return text;
		}

		// Token: 0x06006869 RID: 26729 RVA: 0x0021C135 File Offset: 0x0021A335
		private void EndCutScene(bool bStart)
		{
			if (bStart)
			{
				this.m_charView.SetCharacterIllust(this.m_targetUnit, false, this.m_targetUnit.m_SkinID == 0, true, 0);
				return;
			}
			base.Close();
			NKCSoundManager.StopAllSound(SOUND_TRACK.VOICE);
		}

		// Token: 0x0600686A RID: 26730 RVA: 0x0021C169 File Offset: 0x0021A369
		private IEnumerator CheckScenLoading(bool bStart)
		{
			while (!this.m_bLoading)
			{
				yield return null;
			}
			while (!NKCAssetResourceManager.IsLoadEnd())
			{
				yield return null;
			}
			NKCResourceUtility.SwapResource();
			this.m_bLoading = false;
			this.PlayCutScene(bStart);
			yield return null;
			yield break;
		}

		// Token: 0x0600686B RID: 26731 RVA: 0x0021C17F File Offset: 0x0021A37F
		private void EnableAllLifeTimeSoundFX(bool bEnable)
		{
			NKCUtil.SetGameobjectActive(this.m_FX_UI_CONTRACT_PAPER, bEnable);
			NKCUtil.SetGameobjectActive(this.m_FX_UI_CONTRACT_STAMP, bEnable);
			NKCUtil.SetGameobjectActive(this.m_FX_UI_CONTRACT_NAME, bEnable);
			NKCUtil.SetGameobjectActive(this.m_FX_UI_CONTRACT_CONGRATUALTE, bEnable);
			NKCUtil.SetGameobjectActive(this.m_FX_UI_CONTRACT_COMPLETE, bEnable);
		}

		// Token: 0x04005456 RID: 21590
		public const string ASSET_BUNDLE_NAME = "ab_ui_nkm_ui_personnel";

		// Token: 0x04005457 RID: 21591
		public const string UI_ASSET_NAME = "NKM_UI_PERSONNEL_LIFETIME_CONTRACT";

		// Token: 0x04005458 RID: 21592
		private static NKCUILifetime m_Instance;

		// Token: 0x04005459 RID: 21593
		public Image m_background;

		// Token: 0x0400545A RID: 21594
		public NKCUICharacterView m_charView;

		// Token: 0x0400545B RID: 21595
		public NKCUILifetimeContract m_contract;

		// Token: 0x0400545C RID: 21596
		private NKMUnitData m_targetUnit;

		// Token: 0x0400545D RID: 21597
		private bool m_bUpside;

		// Token: 0x0400545E RID: 21598
		public GameObject m_FX_UI_CONTRACT_PAPER;

		// Token: 0x0400545F RID: 21599
		public GameObject m_FX_UI_CONTRACT_STAMP;

		// Token: 0x04005460 RID: 21600
		public GameObject m_FX_UI_CONTRACT_NAME;

		// Token: 0x04005461 RID: 21601
		public GameObject m_FX_UI_CONTRACT_CONGRATUALTE;

		// Token: 0x04005462 RID: 21602
		public GameObject m_FX_UI_CONTRACT_COMPLETE;

		// Token: 0x04005463 RID: 21603
		private bool m_replay;

		// Token: 0x04005464 RID: 21604
		private bool m_bLoading;
	}
}
