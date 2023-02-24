using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetHeveUTIL
{
    public static class NumberUtil
    {

        public static string ToTerbilang(string number)
        {
            string _number = number;

            Dictionary<int, string> numberMap = new Dictionary<int, string>
            {
                {0, "Nol " },
                {1, "Se" },
                {2, "Dua " },
                {3, "Tiga " },
                {4, "Empat " },
                {5, "Lima " },
                {6, "Enam " },
                {7, "Tujuh " },
                {8, "Delapan " },
                {9, "Sembilan " },
            };

            Dictionary<int, string> standard3Map = new Dictionary<int, string>
            {
                {0, "" },
                {1, " ribu " },
                {2, " juta " },
                {3, " miliar " },
                {4, " triliun " }
            };

            Dictionary<int, string> standard1Map = new Dictionary<int, string>
            {
                {0, " " },
                {1, "puluh " },
                {2, "ratus " }
            };

            List<int[]> parsedNum = new List<int[]>();

            List<int> temp = new List<int>();
            for (int i = _number.Length; i > 0; i--)
            {
                
                temp.Add(int.Parse(_number[i - 1].ToString()));

                if( temp.Count == 3 || i == 1)
                {
                    if(temp.Count < 3)
                    {
                        for(int j = 0; j < 3; j++)
                        {
                            if(temp.ElementAtOrDefault(j) == 0)
                            {
                                temp.Add(0);
                            }
                        }
                    }

                    int[] _val = temp.ToArray();

                    Array.Reverse(_val);

                    parsedNum.Add(_val);
                    temp.Clear();
                }

            }

            List<string> _arrRes = new List<string>();

            for(int i = 0; i < parsedNum.Count; i++)
            {
                int[] _currNum = parsedNum[i];

                if (isAllZero(_currNum)) continue;

                _arrRes.Add(standard3Map.GetValueOrDefault(i));

                for (int j = _currNum.Length - 1, z = 0; j >= 0; j--, z++)
                {

                    if (_currNum[j] == 0) continue;

                    string _str1 = numberMap.GetValueOrDefault(_currNum[j]);
                    string _str2 = standard1Map.GetValueOrDefault(z);

                    if (_currNum[1] == 1 && j > 0)
                    {
                        _str2 = "Belas";

                        _arrRes.Add(_str1 + _str2);

                        j--;
                        z++;
                        continue;
                    }

                    if (j > 0)
                    {
                        if (_str1 == "Se" && _currNum[j - 1] == 0 && i > 1)
                        {
                            _str1 = "Satu";
                        }
                    }

                    if(_str1 == "Se" && j == 2)
                    {
                        _str1 = "Satu";
                    }

                    _arrRes.Add(_str1 + _str2);

                }        

            }

            Arr2Str(_arrRes.ToArray(), out string result);

            return result;
        }


        private static bool isAllZero(int[] arr)
        {
            bool result = true;

            for(int i = 0; i < arr.Length; i++) 
            {
                if (arr[i] != 0)
                {
                    result = false;
                    break;
                }
                
            }

            return result;
        }

        private static void Arr2Str(string[] arr, out string _target)
        {
            _target = "";

            for(int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] == " " || arr[i] == "") continue;

                _target += arr[i];
            }
        }
    }
}

