using Test.Work.BooksCatalogApi.BLL.Enums;

namespace Test.Work.BooksCatalogApi.BLL.Common;

public static class Helpers
{
    public static LanguageType GetLanguageType(string str)
    {
        str = String.Concat(str.Where(c => !Char.IsWhiteSpace(c)));
        int chCount = str.Length;
        int engCharLangCount = 0;
        int rusCharLangCount = 0;
        
        foreach (char ch in str.ToLower())
        {
            if (ch >= 'a' && ch <= 'z')
            {
                engCharLangCount++;
            }

            if (ch == 'а' || ch == 'б' || ch == 'в' || ch == 'г' || ch == 'д' || ch == 'е' || ch == 'ё' || ch == 'ж' || ch == 'з' || ch == 'и' || ch == 'й' || ch == 'к' || ch == 'л' || ch == 'м' || ch == 'н' || ch == 'о' || ch == 'п' || ch == 'р' || ch == 'с' || ch == 'т' || ch == 'у' || ch == 'ф' || ch == 'х' || ch == 'ц' || ch == 'ч' || ch == 'ш' || ch == 'щ' || ch == 'ъ' || ch == 'ы' || ch == 'ь' || ch == 'э' || ch == 'ю' || ch == 'я')
            {
                rusCharLangCount++;
            }
        }

        if (chCount == engCharLangCount)
        {
            return LanguageType.EnglishLanguage;
        }

        if (chCount == rusCharLangCount)
        {
            return LanguageType.RussianLanguage;
        }

        return LanguageType.UnspecifiedLanguage;
    }
}