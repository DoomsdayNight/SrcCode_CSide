using System;

namespace NKM
{
	// Token: 0x020004F1 RID: 1265
	public static class NKM_USER_COMMON
	{
		// Token: 0x060023BA RID: 9146 RVA: 0x000BA884 File Offset: 0x000B8A84
		public static bool CheckNickName(string nickName)
		{
			if (string.IsNullOrEmpty(nickName))
			{
				return false;
			}
			int nickNameLength = NKM_USER_COMMON.GetNickNameLength(nickName);
			return nickNameLength >= NKM_USER_COMMON.USER_NICK_MIN_LENGTH && nickNameLength <= NKM_USER_COMMON.USER_NICK_MAX_LENGTH;
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x000BA8B5 File Offset: 0x000B8AB5
		public static bool CheckNickNameForServer(string nickName)
		{
			return !string.IsNullOrEmpty(nickName) && NKM_USER_COMMON.GetNickNameLength(nickName) >= NKM_USER_COMMON.USER_NICK_MIN_LENGTH;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x000BA8D4 File Offset: 0x000B8AD4
		public static int GetNickNameLength(string str)
		{
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] <= 'ÿ')
				{
					num++;
				}
				else
				{
					num += 2;
				}
			}
			return num;
		}

		// Token: 0x0400259A RID: 9626
		public static int USER_NICK_MIN_LENGTH = 4;

		// Token: 0x0400259B RID: 9627
		public static int USER_NICK_MAX_LENGTH = 16;
	}
}
