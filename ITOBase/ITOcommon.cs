using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITOCommon
{
    public class ListElement
    {
        public string Index;
        public string Type;
        public string Name;
        public string ShortName;

        public override string ToString()
        {
            return Name;
        }
        public ListElement(string _Index, string _Name)
        {
            Name = _Name;
            Index = _Index;
        }
        public ListElement(string _Index, string _Name,string _Type)
        {
            Name = _Name;
            Index = _Index;
            Type = _Type;
        }

        public ListElement(string _Index, string _Name, string _Type, string _ShortName)
        {
            Name = _Name;
            Index = _Index;
            Type = _Type;
            ShortName = _ShortName;
        }
    }
    
    
    public enum ePhoneTypes
    {
        Internal = 1,
        Work = 2,
        WorkMobile = 3,
        Mobile = 4,
        Home = 5
    }

    public enum ePasswordTypes
    {
        AD = 1,
        PayDox = 2,
        FTP = 3,
        VPN = 4,
        Email = 5,
        SVN = 6
    }

    public enum eUserSate
    {
        Deleted = -1,           //пользователь уволен
        New = 0,                //Пользователь только создан (возможно нет пароля почты, учетные данные не переданы пользователю, нет wss)
        Active = 1              //Пользователь активен
    }

    class ITO_StringConverter
    {
        private string m_FullName;
        private string m_FirstName;
        private string m_SecondName;
        private string m_LastName;

        public static string PhoneTypeToStr(ePhoneTypes _PhoneType)
        {
            switch (_PhoneType)
            {
                case ePhoneTypes.Home :
                    return "Домашний";
                case ePhoneTypes.Internal :
                    return "Внутренний";
                case ePhoneTypes.Mobile :
                    return "Мобильный";
                case ePhoneTypes.Work :
                    return "Рабочий";
                case ePhoneTypes.WorkMobile:
                    return "Моб. рабочий";
                default:
                    return "не определен";
            
            }
            
 
        }
        
        public string FormatPhone(string _Phone)
        {
            //TODO Проверить на ввод меньшго количества цифр
            if (_Phone == "")
                return "";
            string resultString = "";

            for (int i = 0; i < _Phone.Length; i++)
            {
                if (i < _Phone.Length - 1)
                    if ((_Phone[i] == '+') && (_Phone[i + 1] == '7'))
                    {
                        resultString += "8";
                        i++;
                        continue;
                    }
                if (char.IsDigit(_Phone[i]))
                {
                    resultString += _Phone[i];
                }
            }

            resultString = resultString.Substring(0, 1) + " " + resultString.Substring(1, 3) + " " + resultString.Substring(4, 3) + " " + resultString.Substring(7, 4);

            return resultString;
        }
        public ITO_StringConverter(string _FullName)
        {
            m_FullName = _FullName.Trim();

            int firstSpc = m_FullName.IndexOf(" ");
            int secondSpc = m_FullName.IndexOf(" ", firstSpc + 1);

            m_LastName = m_FullName.Substring(0, firstSpc).Trim();
            m_FirstName = m_FullName.Substring(firstSpc + 1, secondSpc - firstSpc - 1).Trim();
            m_SecondName = m_FullName.Substring(secondSpc + 1, m_FullName.Length - secondSpc - 1).Trim();


        }
        public string GetFirstName()
        {
            return m_FirstName;
        }
        public string GetSecondName()
        {
            return m_SecondName;
        }
        public string GetLastName()
        {
            return m_LastName;
        }

        static public string TranslitChar(string chRus)
        {
            string chEng = "";

            switch (chRus.ToLower())
            { 
                case "а":
                    chEng = "a";
                    break;
                case "б":
                    chEng = "b";
                    break;
                case "в":
                    chEng = "v";
                    break;
                case "г":
                    chEng = "g";
                    break;
                case "д":
                    chEng = "d";
                    break;
                case "е":
                case "ё":
                    chEng = "e";
                    break;
                case "ж":
                    chEng = "zh";
                    break;
                case "з":
                    chEng = "z";
                    break;
                case "и":
                    chEng = "i";
                    break;
                case "й":
                    chEng = "yi";
                    break;
                case "к":
                    chEng = "k";
                    break;
                case "л":
                    chEng = "l";
                    break;
                case "м":
                    chEng = "m";
                    break;
                case "н":
                    chEng = "n";
                    break;
                case "о":
                    chEng = "o";
                    break;
                case "п":
                    chEng = "p";
                    break;
                case "р":
                    chEng = "r";
                    break;
                case "с":
                    chEng = "s";
                    break;
                case "т":
                    chEng = "t";
                    break;
                case "у":
                    chEng = "y";
                    break;
                case "ф":
                    chEng = "f";
                    break;
                case "х":
                    chEng = "х";
                    break;
                case "ц":
                    chEng = "с";
                    break;
                case "ш":
                    chEng = "sh";
                    break;
                case "щ":
                    chEng = "sch";
                    break;
                case "ь":
                    chEng = "";
                    break;
                case "ъ":
                    chEng = "";
                    break;
                case "ы":
                    chEng = "i";
                    break;
                case "э":
                    chEng = "e";
                    break;
                case "ю":
                    chEng = "u";
                    break;
                case "я":
                    chEng = "ya";
                    break;
                
            }


            return chEng;
        }



    }


}
