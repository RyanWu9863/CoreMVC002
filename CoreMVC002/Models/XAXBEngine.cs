namespace CoreMVC002.Models
{
    public class XAXBEngine
    {
        // 產生一個隨機四位數字（無重複）
        public string GenerateSecretNumber()
        {
            Random rand = new Random();
            string secretNumber = "";

            while (secretNumber.Length < 4)
            {
                string digit = rand.Next(0, 10).ToString();
                if (!secretNumber.Contains(digit))
                {
                    secretNumber += digit;
                }
            }

            return secretNumber;
        }

        // 比對猜測結果
        public string CheckGuess(string userGuess, string secretNumber)
        {
            int A = 0; // 位置正確且數字正確
            int B = 0; // 數字正確但位置不對

            for (int i = 0; i < userGuess.Length; i++)
            {
                if (userGuess[i] == secretNumber[i])
                {
                    A++;
                }
                else if (secretNumber.Contains(userGuess[i]))
                {
                    B++;
                }
            }

            return $"{A}A{B}B";       
        }


    }

}
