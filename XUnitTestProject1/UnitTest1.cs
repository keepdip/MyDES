using System;
using Xunit;
using MyDES;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            //���� ������������ ������ �� ����������� � �����
            string message = "My text";

            string enc = Des.DESEncryption(message, "mykeyzzz");
            string decr = Des.DESDecryption(enc, "mykeyzzz");

            Assert.Equal(message, decr);
        }

        [Fact]
        public void Test1_2()
        {
            //���� ������������ ������ �� ����������� � ����� (��������� ������ ������)
            string message = "My text. Hello i'm Kostya and i'm 20 years old. Spasibo za vnimanie";

            string enc = Des.DESEncryption(message, "mykeyzzz");
            string decr = Des.DESDecryption(enc, "mykeyzzz");

            Assert.Equal(message, decr);
        }

        [Fact]
        public void Test2()
        {
            //���� �� ������� ������ � �������� ��� � �������
            string message = "My text";

            string bit_mess = Des.ConvertToBinary(message);
            string str_mess = Des.BinaryToString(bit_mess);

            Assert.Equal(message, str_mess);
        }

        [Fact]
        public void Test3()
        {
            //���� xor-a
            string str1 = "100010001";
            string str2 = "101010101";

            Assert.Equal("001000100", Des.XOR(str1, str2));
        }

        [Fact]
        public void Test4()
        {
            //���� �������� ������
            string str = "100010001";

            Assert.Equal("001000110", Des.MoveLeft(str, 2));
        }
    }
}
