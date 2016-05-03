using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMCleaningSln
{
    public class TMSentenceClassifier
    {
        private int All_cap(string w)
        {
            int i;
            if (w.Length == 1)
                return 0;
            for (i = 0; i < w.Length; i++)
            {
                if (w[i] < 'A' || w[i] > 'Z')
                    return 0;
            }
            return 1;
        }

        private int All_num(string w)
        {
            int i;
            for (i = 0; i < w.Length; i++)
            {
                if (w[i] == '-' && i == 0 && w.Length > 1)
                    continue;
                if (w[i] < '0' || w[i] > '9')
                {

                    return 0;
                }
            }
            return 1;
        }

        private int max_len_word(string[] w)
        {
            int ma = 0;
            foreach (string r in w)
            {
                if (r.Length > ma)
                    ma = r.Length;
            }
            return ma;
        }

        private int count_hyphen(string w)
        {
            int i, c = 0;
            for (i = 0; i < w.Length; i++)
            {
                if (w[i] == '-')
                    c++;
            }
            return c;
        }

        public bool basis_length(string[] w1, string[] w2)
        {
            double l1 = w1.Length;
            double l2 = w2.Length;
            l1 = l1 / l2;
            if (l1 >= 0.7 && l1 <= 1.2)
                return true;
            return false;

        }

        public bool basis_letter(string[] w1, string[] w2)
        {
            if (w1[0][0] >= 'A' && w1[0][0] < 'Z' && (w2[0][0] < 'A' || w2[0][0] > 'Z'))
                return false;
            if (w1[0][0] >= 'a' && w1[0][0] < 'z' && (w2[0][0] < 'a' || w2[0][0] > 'z'))
                return false;
            return true;
        }

        public bool basis_capital(string[] w1, string[] w2)
        {
            foreach (string w in w1)
            {
                if (All_cap(w) == 1)
                {
                    int pos = Array.IndexOf(w2, w);
                    if (pos > -1)
                    {
                        pos++;
                    }
                    else
                        return false;
                }
            }

            return true;
        }

        public bool basis_number(string[] w1, string[] w2)
        {
            foreach (string w in w1)
            {
                if (All_num(w) == 1)
                {
                    int pos = Array.IndexOf(w2, w);
                    if (pos > -1)
                    {
                        pos++;
                    }
                    else
                        return false;
                }

            }

            return true;
        }

        public bool basis_max_word(string[] w1, string[] w2)
        {
            if (max_len_word(w2) >= 2 * max_len_word(w1))
                return false;
            return true;

        }

        public bool basis_eod_attached(string[] w1, string[] w2)
        {
            if (basis_dot_as_sep(w1, w2) == false)
                return false;
            if (w1.Length == 0 || w2.Length == 0)
                return false;
            string last_word1 = w1[w1.Length - 1];
            string last_word2 = w2[w2.Length - 1];
            if (last_word1.Length == 0 || last_word2.Length == 0)
                return false;
            if (last_word1[last_word1.Length - 1] == '.' && last_word2[last_word2.Length - 1] != '.')
                return false;
            else if (last_word1[last_word1.Length - 1] != '.' && last_word2[last_word2.Length - 1] == '.')
                return false;
            else if (last_word1[last_word1.Length - 1] == '?' && last_word2[last_word2.Length - 1] != '?')
                return false;
            else if (last_word1[last_word1.Length - 1] != '?' && last_word2[last_word2.Length - 1] == '?')
                return false;
            else if (last_word1[last_word1.Length - 1] == '!' && last_word2[last_word2.Length - 1] != '!')
                return false;
            else if (last_word1[last_word1.Length - 1] != '!' && last_word2[last_word2.Length - 1] == '!')
                return false;
            return true;
        }

        public bool basis_hyphen_as_sep(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                if (w == "-")
                    count1 = 1;
            }
            foreach (string w in w2)
            {
                if (w == "-")
                    count2 = 1;
            }
            if (count1 != count2)
                return false;
            return true;
        }

        public bool basis_comma_as_sep(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                if (w == ",")
                    count1 = 1;
            }
            foreach (string w in w2)
            {
                if (w == ",")
                    count2 = 1;
            }
            if (count1 != count2)
                return false;
            return true;
        }

        public bool basis_dot_as_sep(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                if (w == ".")
                    count1 = 1;
            }
            foreach (string w in w2)
            {
                if (w == ".")
                    count2 = 1;
            }
            if (count1 != count2)
                return false;
            return true;
        }

        public bool basis_percent_as_sep(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                if (w == "%")
                    count1++;
            }
            foreach (string w in w2)
            {
                if (w == "%")
                    count2++;
            }
            if (count1 != count2)
                return false;
            return true;
        }

        public bool basis_colon_as_sep(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                if (w == ":")
                    count1++;
            }
            foreach (string w in w2)
            {
                if (w == ":")
                    count2++;
            }
            if (count1 != count2)
                return false;
            return true;
        }

        public bool basis_semicolon_as_sep(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                if (w == ";")
                    count1 += 1;
            }
            foreach (string w in w2)
            {
                if (w == ";")
                    count2 += 1;
            }
            if (count1 != count2)
                return false;
            return true;
        }

        public bool basis_dash_bet_word(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                int flag = 0;
                if (w.Length > 1)
                {
                    count1 = w.Split('-').Length - 1;
                    foreach (string tarw in w2)
                    {
                        if (tarw.Length > 0)
                        {
                            count2 = tarw.Split('-').Length - 1;
                            if (count1 == count2)
                                flag = 1;
                        }
                    }
                    if (flag == 0)
                        return false;
                }
            }
            return true;
        }

        public bool basis_singlequote_as_sep(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                if (w == "'")
                    count1 = 1;
            }
            foreach (string w in w2)
            {
                if (w == "'")
                    count2 = 1;
            }
            if (count1 != count2)
                return false;
            return true;
        }

        public bool basis_dash_as_first_char(string[] w1, string[] w2)
        {
            int count1 = 0, count2 = 0;
            foreach (string w in w1)
            {
                if (w.Length > 1 && w[0] == '-')
                    count1 += 1;
            }
            foreach (string w in w2)
            {
                if (w.Length > 1 && w[0] == '-')
                    count2 += 1;
            }
            if (count1 != count2)
                return false;
            return true;
        }
    }
}
