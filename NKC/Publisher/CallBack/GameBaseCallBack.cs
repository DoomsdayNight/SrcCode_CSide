using System;
using NKC.UI;

namespace NKC.Publisher.CallBack
{
	// Token: 0x02000869 RID: 2153
	public static class GameBaseCallBack
	{
		// Token: 0x06005595 RID: 21909 RVA: 0x0019EF80 File Offset: 0x0019D180
		public static void LoginResponseCallBack(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalInfo)
		{
			if (resultCode <= NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL_ALREADY_LOGIN)
			{
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_OK)
				{
					return;
				}
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL_ALREADY_LOGIN)
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, "Already logged in. \n Do you want to log out of the logged in ID?", delegate()
					{
						NKCPublisherModule.Auth.Logout(null);
					}, null, false);
					return;
				}
			}
			else
			{
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_QUIT_USER)
				{
					NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, "ID withdrawal in progress. \n immediate withdrawal : Ok , withdrawal cancel : Cancel", delegate()
					{
						NKCPublisherModule.Auth.Withdraw(null);
					}, delegate()
					{
						NKCPublisherModule.Auth.TryResolveUser(null);
					}, false);
					return;
				}
				if (resultCode == NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_LOGIN_FAIL)
				{
					NKCPopupOKCancel.OpenOKBox("GameBase", "Invalid idp string \n " + additionalInfo, null, "");
					return;
				}
			}
			NKCPopupOKCancel.OpenOKCancelBox(NKCUtilString.GET_STRING_NOTICE, "Login fail", null, null, false);
		}

		// Token: 0x06005596 RID: 21910 RVA: 0x0019F070 File Offset: 0x0019D270
		public static void AddMappingResponseCallBack(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalInfo)
		{
			switch (resultCode)
			{
			case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_SUCCESS_GUEST_SYNC:
				NKCPopupOKCancel.OpenOKBox("GameBase", "Mapping success \n Guest -> [" + additionalInfo + "]!", null, "");
				return;
			case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_FAIL_GUEST_ALREADY_MAPPED:
				NKCPopupOKCancel.OpenOKBox("GameBase", "Mapping fail \n Already mapped [" + additionalInfo + "]", null, "");
				return;
			case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_SUCCESS_QUIT:
				break;
			case NKC_PUBLISHER_RESULT_CODE.NPRC_AUTH_CHANGEACCOUNT_FAIL_NO_CHANGEABLE:
				NKCPopupOKCancel.OpenOKBox("GameBase", "Mapping fail \n [" + additionalInfo + "]", null, "");
				break;
			default:
				return;
			}
		}

		// Token: 0x06005597 RID: 21911 RVA: 0x0019F0FC File Offset: 0x0019D2FC
		public static void BillingProductListResponseCallBack(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalInfo)
		{
		}

		// Token: 0x06005598 RID: 21912 RVA: 0x0019F100 File Offset: 0x0019D300
		public static void TermResponseCallBack(NKC_PUBLISHER_RESULT_CODE resultCode, string additionalInfo)
		{
			if (resultCode != NKC_PUBLISHER_RESULT_CODE.NPRC_OK && resultCode == NKC_PUBLISHER_RESULT_CODE.NKRC_TERM_FAIL)
			{
				NKCPopupOKCancel.OpenOKBox("GameBase", "Term fail \n [" + additionalInfo + "]", null, "");
			}
		}
	}
}
