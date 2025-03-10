namespace Sample.Persistence.EF.Extensions;

public static class NationalCodeChecker
{
    public static bool NationalCode(this string nationalCode)
    {
        //بررسی تعداد و جنس کد ملی وارد شده
        if (string.IsNullOrWhiteSpace(nationalCode) ||
            nationalCode.Length != 10 ||
            !nationalCode.All(char.IsDigit))
            return false;

        //بررسی اینکه همه ارقام شبیه به هم نباشند مثلا 11111111
        if (new string(nationalCode[0], 10) == nationalCode)
            return false;

        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            sum += (nationalCode[i] - '0') * (10 - i);
        }

        int remainder = sum % 11;
        int checkDigit = nationalCode[9] - '0';//تبدیل رقم اخر کد ملی از کاراکتر عددی به عدد صحیح 

        return (remainder < 2 && checkDigit == remainder) || (remainder >= 2 && checkDigit == (11 - remainder));
    }
}
