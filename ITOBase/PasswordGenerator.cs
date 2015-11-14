using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Security.Cryptography;

namespace ITOBase
{
    class PasswordGenerator
    {
        private int lenght = 15; //размер пароля
        private string PASSWORD_CHARS_EN = "abcdefgijkmnopqrstwxyzABCDEFGHJKLMNPQRSTWXYZ";
        private string PASSWORD_CHARS_NUMERIC = "0123456789";

        public string GeneratePassword(int lenght_pass)
        {
            if (lenght_pass > 0)
                this.lenght = lenght_pass;

            ArrayList charGroups = new ArrayList();

            charGroups.Add(PASSWORD_CHARS_EN.ToCharArray());
            charGroups.Add(PASSWORD_CHARS_NUMERIC.ToCharArray());

            byte[] randomBytes = new byte[4];

            int[] charsLeftInGroup = new int[charGroups.Count];

            for (int i = 0; i < charsLeftInGroup.Length; i++)
                charsLeftInGroup[i] = ((char[])charGroups[i]).Length;

            int[] leftGroupsOrder = new int[charGroups.Count];

            for (int i = 0; i < leftGroupsOrder.Length; i++)
                leftGroupsOrder[i] = i;

            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            int seed = (randomBytes[0] & 0x7f) << 24 |
                    randomBytes[1] << 16 |
                    randomBytes[2] << 8 |
                    randomBytes[3];

            Random random = new Random(seed);
            char[] password = null;

            password = new char[lenght];

            int nextCharIdx;

            int nextGroupIdx;

            int nextLeftGroupsOrderIdx;

            int lastCharIdx;

            int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

            for (int i = 0; i < password.Length; i++)
            {
                if (lastLeftGroupsOrderIdx == 0)
                    nextLeftGroupsOrderIdx = 0;
                else
                    nextLeftGroupsOrderIdx = random.Next(0, lastLeftGroupsOrderIdx);

                nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

                lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

                if (lastCharIdx == 0)
                    nextCharIdx = 0;
                else
                    nextCharIdx = random.Next(0, lastCharIdx + 1);

                password[i] = ((char[])charGroups[nextGroupIdx])[nextCharIdx];

                if (lastCharIdx == 0)
                    charsLeftInGroup[nextGroupIdx] =
                                              ((char[])charGroups[nextGroupIdx]).Length;
                else
                {
                    if (lastCharIdx != nextCharIdx)
                    {
                        char temp = ((char[])charGroups[nextGroupIdx])[lastCharIdx];
                        ((char[])charGroups[nextGroupIdx])[lastCharIdx] =
                                    ((char[])charGroups[nextGroupIdx])[nextCharIdx];
                        ((char[])charGroups[nextGroupIdx])[nextCharIdx] = temp;
                    }
                    charsLeftInGroup[nextGroupIdx]--;
                }

                if (lastLeftGroupsOrderIdx == 0)
                    lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
                else
                {
                    if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                    {
                        int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                        leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                    leftGroupsOrder[nextLeftGroupsOrderIdx];
                        leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                    }
                    lastLeftGroupsOrderIdx--;
                }
            }

            return (new string(password));
        }

        
    }


}
