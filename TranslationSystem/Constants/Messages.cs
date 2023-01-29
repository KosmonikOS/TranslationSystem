namespace TranslationSystem.Constants;

public static class Messages
{
    public const string HelloMessage = "Данный бот разработан для улучшения навыков английского языка \n" +
                                        "Вам доступны следующие команды:\n" +
                                        "1. /add (Желаемое слово) - добавляет новое слово\n" +
                                        "2. /showall - показать все доступные слова\n" +
                                        "3. /show (Желаемое слово) - показать конкретное слово\n" +
                                        "3. /next - войти в режим викторины\n";
    public const string AddedMessage = "Слово успешно добавлено";
    public const string NotFoundMessage = "Слово не найдено";
    public const string NoWordsAddedMessage = "Невозможно войти в режим викторины. Сначала добавьте слова";
    public const string UnknownErrorMessage = "Что-то пошло не так";
}