using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChevLoc
{
    static class ControleSaisie
    {
        public static bool Entier(string str, ref string msg)
        {
            try
            {
                Convert.ToInt16(str);
                return true;
            }
            catch (Exception ex)
            {
                msg = str + " n'est pas un entier valide";
                return false;
            }
        }
        public static bool Entier(string str, int borneMin, int borneMax, ref string msg)
        {
            string msg2="";
            if (Entier(str, ref msg2))
            {
                if (Convert.ToInt16(str) >= borneMin && Convert.ToInt16(str) <= borneMax)
                {
                    return true;
                }
                else
                {
                    msg = str + " n'est pas compris entre " + borneMin.ToString() + " et " + borneMax.ToString();
                    return false;
                }
            }
            else
            {
                msg = msg2;
                return false;
            }
        }
        public static bool Tel(string str, ref string msg)
        {
            string msg2="";
            if (Entier(str, ref msg2) && str.Length == 10)
            {
                return true;
            }
            else
            {
                msg = str + " n'est pas un téléphone valide";
                return false;
            }
        }
        public static bool Mail(string str, ref string msg)
        {
            if (str.Contains("@") && (str.Substring(str.Length - 4, 1) == "." || str.Substring(str.Length - 3, 1) == "."))
            {
                return true;
            }
            else
            {
                msg = str + " n'est pas un mail valide";
                return false;
            }
        }
    }
}
