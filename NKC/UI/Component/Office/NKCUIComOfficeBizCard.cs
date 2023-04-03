using System;
using ClientPacket.Common;
using ClientPacket.Office;
using NKM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace NKC.UI.Component.Office
{
	// Token: 0x02000C65 RID: 3173
	public class NKCUIComOfficeBizCard : MonoBehaviour
	{
		// Token: 0x060093AD RID: 37805 RVA: 0x00326F10 File Offset: 0x00325110
		public static NKCUIComOfficeBizCard GetInstance(int ID, Transform parent)
		{
			NKMAssetName nkmassetName = new NKMAssetName("AB_INVEN_ICON_BIZ_CARD", "BIZ_CARD_000");
			NKCAssetInstanceData nkcassetInstanceData = NKCAssetResourceManager.OpenInstance<GameObject>(nkmassetName, false, parent);
			if (((nkcassetInstanceData != null) ? nkcassetInstanceData.m_Instant : null) == null)
			{
				Debug.LogError(string.Format("NKCUIComOfficeBizCard : {0} not found!", nkmassetName));
				return null;
			}
			NKCUIComOfficeBizCard component = nkcassetInstanceData.m_Instant.GetComponent<NKCUIComOfficeBizCard>();
			if (component != null)
			{
				component.Init();
			}
			return component;
		}

		// Token: 0x060093AE RID: 37806 RVA: 0x00326F78 File Offset: 0x00325178
		private void Init()
		{
			NKCUtil.SetButtonClickDelegate(this.m_csbtnCard, new UnityAction(this.OnTouch));
		}

		// Token: 0x060093AF RID: 37807 RVA: 0x00326F91 File Offset: 0x00325191
		public void SetData(NKMOfficePost post, NKCUIComOfficeBizCard.OnClick onClick)
		{
			if (post == null)
			{
				return;
			}
			this.SetData(post.senderProfile, post.senderGuildData, post.expirationDate, onClick);
		}

		// Token: 0x060093B0 RID: 37808 RVA: 0x00326FB0 File Offset: 0x003251B0
		public void SetData(NKMCommonProfile profile, NKMGuildSimpleData guildData, NKCUIComOfficeBizCard.OnClick onClick)
		{
			if (this.m_userInfo != null)
			{
				this.m_userInfo.SetData(profile, guildData);
			}
			this.m_dtEndTime = DateTime.MinValue;
			NKCUtil.SetGameobjectActive(this.m_lbTimeleft, false);
			this.m_userUID = ((profile != null) ? profile.userUid : 0L);
			this.dOnClick = onClick;
		}

		// Token: 0x060093B1 RID: 37809 RVA: 0x0032700C File Offset: 0x0032520C
		public void SetData(NKMUserProfileData userProfileData, NKCUIComOfficeBizCard.OnClick onClick)
		{
			if (this.m_userInfo != null)
			{
				this.m_userInfo.SetData(userProfileData);
			}
			this.m_dtEndTime = DateTime.MinValue;
			NKCUtil.SetGameobjectActive(this.m_lbTimeleft, false);
			this.m_userUID = ((userProfileData != null) ? userProfileData.commonProfile.userUid : 0L);
			this.dOnClick = onClick;
		}

		// Token: 0x060093B2 RID: 37810 RVA: 0x0032706C File Offset: 0x0032526C
		public void SetData(NKMCommonProfile profile, NKMGuildSimpleData guildData, DateTime endTime, NKCUIComOfficeBizCard.OnClick onClick)
		{
			if (this.m_userInfo != null)
			{
				this.m_userInfo.SetData(profile, guildData);
			}
			NKCUtil.SetGameobjectActive(this.m_lbTimeleft, true);
			this.m_dtEndTime = endTime;
			this.SetTime();
			this.m_userUID = ((profile != null) ? profile.userUid : 0L);
			this.dOnClick = onClick;
		}

		// Token: 0x060093B3 RID: 37811 RVA: 0x003270C8 File Offset: 0x003252C8
		public void SetData(NKMUserProfileData userProfileData, DateTime endTime, NKCUIComOfficeBizCard.OnClick onClick)
		{
			if (this.m_userInfo != null)
			{
				this.m_userInfo.SetData(userProfileData);
			}
			NKCUtil.SetGameobjectActive(this.m_lbTimeleft, true);
			this.m_dtEndTime = endTime;
			this.SetTime();
			this.m_userUID = ((userProfileData != null) ? userProfileData.commonProfile.userUid : 0L);
			this.dOnClick = onClick;
		}

		// Token: 0x060093B4 RID: 37812 RVA: 0x00327127 File Offset: 0x00325327
		private void SetTime()
		{
			if (this.m_dtEndTime > DateTime.MinValue)
			{
				NKCUtil.SetLabelText(this.m_lbTimeleft, NKCSynchronizedTime.GetTimeLeftString(this.m_dtEndTime));
			}
		}

		// Token: 0x060093B5 RID: 37813 RVA: 0x00327151 File Offset: 0x00325351
		private void Update()
		{
			this.SetTime();
		}

		// Token: 0x060093B6 RID: 37814 RVA: 0x00327159 File Offset: 0x00325359
		private void OnTouch()
		{
			NKCUIComOfficeBizCard.OnClick onClick = this.dOnClick;
			if (onClick == null)
			{
				return;
			}
			onClick(this.m_userUID);
		}

		// Token: 0x040080A6 RID: 32934
		public const string DEFAULT_BUNDLE_NAME = "AB_INVEN_ICON_BIZ_CARD";

		// Token: 0x040080A7 RID: 32935
		public const string DEFAULT_ASSET_NAME = "BIZ_CARD_000";

		// Token: 0x040080A8 RID: 32936
		public NKCUIComUserSimpleInfo m_userInfo;

		// Token: 0x040080A9 RID: 32937
		public Text m_lbTimeleft;

		// Token: 0x040080AA RID: 32938
		public NKCUIComStateButton m_csbtnCard;

		// Token: 0x040080AB RID: 32939
		private DateTime m_dtEndTime;

		// Token: 0x040080AC RID: 32940
		private NKCUIComOfficeBizCard.OnClick dOnClick;

		// Token: 0x040080AD RID: 32941
		private long m_userUID;

		// Token: 0x02001A20 RID: 6688
		// (Invoke) Token: 0x0600BB28 RID: 47912
		public delegate void OnClick(long uid);
	}
}
