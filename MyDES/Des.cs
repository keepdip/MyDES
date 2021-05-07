﻿using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDES
{
    public class Des
    {
        public static int[] G = { 57, 49, 41, 33, 25, 17, 09, 01, 58, 50, 42, 34, 26, 18, 10, 02, 59, 51, 43, 35, 27, 19, 11, 03, 60, 52, 44, 36, 63, 55, 47, 39, 31, 23, 15, 07, 62, 54, 46, 38, 30, 22, 14, 06, 61, 53, 45, 37, 29, 21, 13, 05, 28, 20, 12, 04 };
        public static int[] IP = { 58, 50, 42, 34, 26, 18, 10, 2, 60, 52, 44, 36, 28, 20, 12, 4, 62, 54, 46, 38, 30, 22, 14, 6, 64, 56, 48, 40, 32, 24, 16, 8, 57, 49, 41, 33, 25, 17, 9, 1, 59, 51, 43, 35, 27, 19, 11, 3, 61, 53, 45, 37, 29, 21, 13, 5, 63, 55, 47, 39, 31, 23, 15, 7 };
        public static int[] IP_inver = { 40, 8, 48, 16, 56, 24, 64, 32, 39, 7, 47, 15, 55, 23, 63, 31, 38, 6, 46, 14, 54, 22, 62, 30, 37, 5, 45, 13, 53, 21, 61, 29, 36, 4, 44, 12, 52, 20, 60, 28, 35, 3, 43, 11, 51, 19, 59, 27, 34, 2, 42, 10, 50, 18, 58, 26, 33, 1, 41, 9, 49, 17, 57, 25 };
        public static int[] E = { 32, 1, 2, 3, 4, 5, 4, 5, 6, 7, 8, 9, 8, 9, 10, 11, 12, 13, 12, 13, 14, 15, 16, 17, 16, 17, 18, 19, 20, 21, 20, 21, 22, 23, 24, 25, 24, 25, 26, 27, 28, 29, 28, 29, 30, 31, 32, 1 };
        public static int[] P = { 16, 7, 20, 21, 29, 12, 28, 17, 1, 15, 23, 26, 5, 18, 31, 10, 2, 8, 24, 14, 32, 27, 3, 9, 19, 13, 30, 6, 22, 11, 4, 25 };
        public static int[] H = { 14, 17, 11, 24, 1, 5, 3, 28, 15, 6, 21, 10, 23, 19, 12, 4, 26, 8, 16, 7, 27, 20, 13, 2, 41, 52, 31, 37, 47, 55, 30, 40, 51, 45, 33, 48, 44, 49, 39, 56, 34, 53, 46, 42, 50, 36, 29, 32 };
        public static byte[,] S ={
                        {14, 4, 13, 1, 2, 15, 11, 8, 3, 10, 6, 12, 5, 9, 0, 7, 0, 15, 7, 4, 14, 2, 13, 1, 10, 6, 12, 11, 9, 5, 3, 8, 4, 1, 14, 8, 13, 6, 2, 11, 15, 12, 9, 7, 3, 10, 5, 0, 15, 12, 8, 2, 4, 9, 1, 7, 5, 11, 3, 14, 10, 0, 6, 13},
                        {15, 1, 8, 14, 6, 11, 3, 4, 9, 7, 2, 13, 12, 0, 5, 10, 3, 13, 4, 7, 15, 2, 8, 14, 12, 0, 1, 10, 6, 9, 11, 5, 0, 14, 7, 11, 10, 4, 13, 1, 5, 8, 12, 6, 9, 3, 2, 15, 13, 8, 10, 1, 3, 15, 4, 2, 11, 6, 7, 12, 0, 5, 14, 9},
                        {10, 0, 9, 14, 6, 3, 15, 5, 1, 13, 12, 7, 11, 4, 2, 8, 13, 7, 0, 9, 3, 4, 6, 10, 2, 8, 5, 14, 12, 11, 15, 1, 13, 6, 4, 9, 8, 15, 3, 0, 11, 1, 2, 12, 5, 10, 14, 7, 1, 10, 13, 0, 6, 9, 8, 7, 4, 15, 14, 3, 11, 5, 2, 12},
                        {7, 13, 14, 3, 0, 6, 9, 10, 1, 2, 8, 5, 11, 12, 4, 15, 13, 8, 11, 5, 6, 15, 0, 3, 4, 7, 2, 12, 1, 10, 14, 9, 10, 6, 9, 0, 12, 11, 7, 13, 15, 1, 3, 14, 5, 2, 8, 4, 3, 15, 0, 6, 10, 1, 13, 8, 9, 4, 5, 11, 12, 7, 2, 14},
                        {2, 12, 4, 1, 7, 10, 11, 6, 8, 5, 3, 15, 13, 0, 14, 9, 14, 11, 2, 12, 4, 7, 13, 1, 5, 0, 15, 10, 3, 9, 8, 6, 4, 2, 1, 11, 10, 13, 7, 8, 15, 9, 12, 5, 6, 3, 0, 14, 11, 8, 12, 7, 1, 14, 2, 13, 6, 15, 0, 9, 10, 4, 5, 3},
                        {12, 1, 10, 15, 9, 2, 6, 8, 0, 13, 3, 4, 14, 7, 5, 11, 10, 15, 4, 2, 7, 12, 9, 5, 6, 1, 13, 14, 0, 11, 3, 8, 9, 14, 15, 5, 2, 8, 12, 3, 7, 0, 4, 10, 1, 13, 11, 6, 4, 3, 2, 12, 9, 5, 15, 10, 11, 14, 1, 7, 6, 0, 8, 13},
                        {4, 11, 2, 14, 15, 0, 8, 13, 3, 12, 9, 7, 5, 10, 6, 1, 13, 0, 11, 7, 4, 9, 1, 10, 14, 3, 5, 12, 2, 15, 8, 6, 1, 4, 11, 13, 12, 3, 7, 14, 10, 15, 6, 8, 0, 5, 9, 2, 6, 11, 13, 8, 1, 4, 10, 7, 9, 5, 0, 15, 14, 2, 3, 12},
                        {13, 2, 8, 4, 6, 15, 11, 1, 10, 9, 3, 14, 5, 0, 12, 7, 1, 15, 13, 8, 10, 3, 7, 4, 12, 5, 6, 11, 0, 14, 9, 2, 7, 11, 4, 1, 9, 12, 14, 2, 0, 6, 10, 13, 15, 3, 5, 8, 2, 1, 14, 7, 4, 10, 8, 13, 15, 12, 9, 0, 3, 5, 6, 11}
                        };

        public static string DESEncryption(string message, string key)
        {
            while (message.Length % 8 != 0) message += (char)0;
            message = ConvertToBinary(message);

            StringBuilder sb = new StringBuilder();
            string[] keys = KeyGenerator(ConvertToBinary(key));

            for (int i = 0; i < message.Length; i += 64)
            {
                string right, left, middle, part = Transposition(message.Substring(i, 64), IP);
                right = part.Substring(32, part.Length / 2);
                left = part.Substring(0, part.Length / 2);
                for (int k = 0; k < 16; k++)
                {
                    middle = left;
                    left = right;
                    right = XOR(middle, f(right, keys[k]));
                }
                sb.Append(Transposition(right + left, IP_inver));
            }
            // Console.WriteLine(BinaryToString(sb.ToString()).Length);

            return BinaryToString(sb.ToString());
        }

        public static string DESDecryption(string message, string key)
        {
            message = ConvertToBinary(message);
            StringBuilder sb = new StringBuilder();
            string[] keys = KeyGenerator(ConvertToBinary(key));
            for (int i = 0; i < message.Length; i += 64)
            {
                string part = Transposition(message.Substring(i, 64), IP);
                string right = part.Substring(0, part.Length / 2);
                string left = part.Substring(32, part.Length / 2);
                string middle;
                for (int k = 15; k >= 0; k--)
                {
                    middle = right;
                    right = left;
                    left = XOR(middle, f(left, keys[k]));
                }
                sb.Append(Transposition(left + right, IP_inver));
            }
            string res = BinaryToString(sb.ToString());
            if (res.IndexOf((char)0) != -1)
                res = res.Remove(res.IndexOf((char)0));

            return res;

        }

        //перевод из двоичного представления в символьное
        public static string BinaryToString(string input)
        {
            string output = "";

            while (input.Length > 0)
            {
                string char_binary = input.Substring(0, 8);
                input = input.Remove(0, 8);

                int a = 0;
                int degree = char_binary.Length - 1;

                foreach (char c in char_binary)
                    a += Convert.ToInt32(c.ToString()) * (int)Math.Pow(2, degree--);

                output += ((char)a).ToString();
            }

            return output;

        }

        public static string f(string RightPart, string key) //+
        {
            RightPart = Transposition(RightPart, E);
            RightPart = XOR(key, RightPart);

            StringBuilder sb = new StringBuilder();
            for (int i = 0, j = 0; i < 8; i++, j += 6)
            {
                int row = Convert.ToInt32(Convert.ToString(RightPart.Substring(j, 6)[0]) + Convert.ToString(RightPart.Substring(j, 6)[5]), 2);
                int coloumn = Convert.ToInt32(RightPart.Substring(j, 6).Substring(1, 4), 2);

                sb.Append(Convert.ToString(S[i, row * 16 + coloumn], 2).PadLeft(4, '0'));
            }

            RightPart = Transposition(sb.ToString(), P);

            return RightPart;
        }

        public static string ConvertToBinary(string input)
        {
            string output = "";

            for (int i = 0; i < input.Length; i++)
            {
                string char_binary = Convert.ToString(input[i], 2);

                while (char_binary.Length < 8)
                    char_binary = "0" + char_binary;

                output += char_binary;
            }

            return output;
        }

        public static string XOR(string str1, string str2)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str2.Length; i++)
                sb.Append((byte)(str2[i] ^ str1[i]));
            return sb.ToString();
        }

        public static string Transposition(string str, int[] array)
        {
            StringBuilder sb = new StringBuilder("");
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append(str[array[i] - 1]);
            }
            return sb.ToString();
        }

        public static string[] KeyGenerator(string key)
        {

            string[] result = new string[16];
            int[] move = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1 };

            key = Transposition(key, G);
            string G0 = key.Substring(0, key.Length / 2);
            string D0 = key.Substring(key.Length / 2, key.Length / 2);

            for (int i = 0; i < 16; i++)
            {
                G0 = MoveLeft(G0, move[i]);
                D0 = MoveLeft(D0, move[i]);
                result[i] = Transposition(G0 + D0, H);

            }
            return result;
        }
        //сдвиг влево
        public static string MoveLeft(string str, int bit)
        {
            for (int i = 0; i < bit; i++)
            {
                char tmp = str[0];
                str = str.Remove(0, 1) + tmp;
            }
            return str;
        }
    }
}
