//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace ToiletApp.Utils
//{
//    public  class Validations
//    {
//        public static bool IsValidUserName(string userName)
//        {
//            if (userName == null || userName.Length == 0)
//            {
//                return false;
//            }
//            if(userName.IndexOf(' ') < 0 || 
//                !(userName[0] >= 'A' && userName[0]<='Z')||
//             !( userName[  userName.IndexOf(' ')+1] >= 'A' && userName[userName.IndexOf(' ') + 1] <= 'Z'))
//            {
//                return false;
//            }
           
//            for (int i = 0; i < userName.Length; i++)
//            {
//                bool isChar = userName[i] >= 'a' && userName[i] <= 'z' ||
//                     userName[i] >= 'A' && userName[i] <= 'Z';
//                if (!isChar)
//                {
//                    return false;
//                }
//            }
//            return true;
//        }
//    }
//}
